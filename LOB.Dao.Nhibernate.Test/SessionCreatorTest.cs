﻿#region Usings

using LOB.Domain;
using LOB.Domain.SubEntity;
using LOB.Log;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

#endregion

namespace LOB.Dao.Nhibernate.Test {
    [TestClass]
    public class SessionCreatorTest {
        [TestMethod]
        public void CreateSessionTest() {
            var creator = new SessionFactoryCreator(new Logger());
            Assert.IsNotNull(creator.ORMFactory);
        }

        [TestMethod]
        public void TransactionTest() {
            var sCreator = new SessionFactoryCreator(new Logger());

            var address = new Address {
                Country = "Brazil",
                State = "RJ",
                Status = AddressStatus.Active,
                Street = "Street b",
                StreetNumber = "1001",
                ZipCode = "123456789"
            };
            Email e1 = "thisdude@you.com";
            Email e2 = "another@you.com";
            var contact = new ContactInfo {
                PhoneNumbers =
                    new[] {
                        new PhoneNumber {Number = "123456", Detail = "Casa", Type = PhoneNumberType.Telephone},
                        new PhoneNumber {Number = "1234567", Detail = "Trabalho", Type = PhoneNumberType.Cellphone}
                    },
                WebSite = "www.thisdude.com",
                PS = "Hes at home by the night",
                Status = ContactStatus.Active,
                //Contact = "Hes at home by the night",
                Emails = new[] {e1, e2}
            };

            var stores = new[] {new Store {Name = "Fresh Bar"}, new Store {Name = "Total Drinks"}};
            var entity = new LegalPerson {
                TradingName = "Coke",
                CorporateName = "The Coca-Cola Company",
                ContactInfo = contact,
                // Stores = stores,
            };

            var session = ((ISession)sCreator.ORMFactory);
            using(session) {
                session.BeginTransaction();
                session.Save(entity);
                Assert.IsTrue((session).Contains(entity));
                session.Transaction.Commit();
            }
        }
    }
}