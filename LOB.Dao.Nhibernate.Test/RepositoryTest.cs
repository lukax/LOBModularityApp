﻿#region Usings

using System;
using System.Diagnostics;
using System.Linq;
using LOB.Domain;
using LOB.Domain.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace LOB.Dao.Nhibernate.Test
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void AddDeleteTest()
        {
            var repo = new DomainRepository(new UnityOfWork(new SessionCreator()));

            var p1 = new Product
                {
                    Description = "Teste description",
                    UnitsInStock = 1234
                };

            using (repo.Uow)
            {
                repo.Uow.BeginTransaction();
                p1 = repo.Save(p1);
                Assert.AreNotEqual(0, p1.Id);
                repo.Uow.CommitTransaction();
                Assert.IsTrue(repo.Contains(p1));
                Assert.IsTrue(repo.Contains<Product>(x => x.Description == p1.Description));
            }
            using (repo.Uow)
            {
                repo.Uow.BeginTransaction();
                repo.Delete(p1);
                repo.Uow.CommitTransaction();
                Assert.IsFalse(repo.Contains(p1));
            }
        }


        [TestMethod]
        public void GetTest()
        {
            var repo = new DomainRepository(new UnityOfWork(new SessionCreator()));

            var p1 = new Product
                {
                    Description = "Madeira",
                    UnitsInStock = 10
                };

            repo.Uow.BeginTransaction();
            p1 = repo.Save(p1);
            var p2 = repo.Get<Product>(p1.Id);
            Assert.AreEqual(p1, p2);
        }

        [TestMethod]
        public void SaveOrUpdateTest()
        {
            var repo = new DomainRepository(new UnityOfWork(new SessionCreator()));
            var entity = new Product() {Description = "Test description service", Name = "Test Name"};
            using (repo.Uow)
            {
                repo.Uow.BeginTransaction();
                repo.SaveOrUpdate(entity);
                repo.Uow.CommitTransaction();

                Assert.IsTrue(repo.Contains(entity));

                repo.Uow.Dispose();
                repo.Uow.BeginTransaction();
                entity.Description = "Changed description test";
                repo.SaveOrUpdate(entity);
                repo.Uow.CommitTransaction();

                Assert.AreEqual(entity, repo.Get<Product>(entity.Id));
            }
        }

        [TestMethod]
        public void SaveGetPolymorphismTest()
        {
            var repo = new DomainRepository(new UnityOfWork(new SessionCreator()));
            var person = new LegalPerson() { Cnpj = 123456, Ie = 1234, FirstName = "Dude", LastName = "Martin", NickName = "Doesn't have" , BirthDate = DateTime.Now};
            var client = new Client
                {
                    Person = person,
                    Status = ClientStatus.New
                };

            using (repo.Uow)
            {
                repo.Uow.BeginTransaction();
                repo.Save(client);
                repo.Uow.CommitTransaction();

                Debug.WriteLine("ID: " + person.Id);

                var person2 = repo.Get<Person>(person.Id);

                Assert.IsTrue(person2 is LegalPerson);
                Assert.IsTrue(person.Id.Equals(person2.Id));
                Assert.IsTrue(((LegalPerson)person2).Cnpj == person.Cnpj);
            }
        }

        [TestMethod]
        public void GetListCriteriaTest()
        {
            var repo = new DomainRepository(new UnityOfWork(new SessionCreator()));
            var person1 = new LegalPerson() { Cnpj = 123456, Ie = 1234, FirstName = "Dude1", LastName = "Martin", NickName = "Doesn't have1", BirthDate = DateTime.Now };
            var person2 = new LegalPerson() { Cnpj = 12345678, Ie = 12345, FirstName = "Dude2", LastName = "Martin", NickName = "Doesn't have2", BirthDate = DateTime.Now };
            var client1 = new Client
            {
                Person = person1,
                Status = ClientStatus.New
            };
            var client2 = new Client()
                {
                    Person = person2,
                    Status = ClientStatus.New
                };
            using (repo.Uow)
            {
                repo.Uow.BeginTransaction();
                repo.Save(person1);
                repo.Save(person2);
                repo.Uow.CommitTransaction();

                var list1 = repo.GetList<LegalPerson>(x => x.LastName == "Martin");
                Assert.IsTrue(list1.Any());
                var list2 = repo.GetList<Client>(x=> x.Status == ClientStatus.New);
                Assert.IsTrue(list1.Count() > 1);
            }
        }
    }
}