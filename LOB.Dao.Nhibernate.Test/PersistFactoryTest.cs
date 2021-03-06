﻿#region Usings

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using LOB.Dao.Contract;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace LOB.Dao.Nhibernate.Test {
    [TestClass]
    public class PersistFactoryTest {
        [Import("Sql")] public IRepository Repository { get; set; }

        [TestMethod]
        public void GetInstanceTest() {
            //new PersistFactory(this);
            Assert.IsNotNull(Repository);
        }

        [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
        private class PersistFactory {
            private readonly AggregateCatalog _catalog;
            private readonly CompositionContainer _container;

            private readonly IUnityContainer _ccontainer = new UnityContainer();
            [Import] private Inner _inner;

            public PersistFactory(object obj) {
                Debug.WriteLine("Tryng to load dll from: " + Assembly.GetExecutingAssembly().Location);

                _catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                                                new AssemblyCatalog(Assembly.LoadFrom("LOB.Dao.Nhibernate.dll")));
                _container = new CompositionContainer(_catalog);
                //_container.SatisfyImportsOnce(this);
                //_container.SatisfyImportsOnce(obj);
                _container.ComposeParts(this, obj);

                Assert.AreEqual(_ccontainer, _inner.Container);
            }

            /// <summary>
            ///     Compose a part, making the imports work
            /// </summary>
            //public void Compose(object obj) { _container.ComposeParts(obj); }
            //public IRepository GetInstance(PersistType type = PersistType.MySql) {
            //    if(type == PersistType.MySql) return _container.GetExportedValue<IRepository>("Sql");
            //    if(type == PersistType.Memory) return _container.GetExportedValue<IRepository>("GetAll");
            //    if(type == PersistType.File) return _container.GetExportedValue<IRepository>("File");
            //    throw new ArgumentNullException();
            //}
            private class Inner {
                public readonly IUnityContainer Container;

                [ImportingConstructor]
                public Inner(IUnityContainer container) { Container = container; }
            }
        }
    }
}