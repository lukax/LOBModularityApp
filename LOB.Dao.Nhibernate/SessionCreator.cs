﻿#region Usings

using System;
using System.ComponentModel.Composition;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using LOB.Dao.Interface;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Remotion.Linq.Utilities;

#endregion

namespace LOB.Dao.Nhibernate
{
    public class SessionCreator : ISessionCreator
    {
        private const String MySqlDefaultConnectionString = @"Server=192.168.0.150;
                        Database=LOB;Uid=LOB;Pwd=LOBPASSWD;";
        private const String MsSqlDefaultConnectionString = @"Data Source=192.168.0.151;
                        Initial Catalog=LOB;User ID=LOB;Password=LOBSYSTEMDB";
        private String _connectionString;

        [ImportingConstructor]
        public SessionCreator()
            : this(PersistType.Sql, null)
        {
        }

        public SessionCreator(PersistType persistIn, String connectionString)
        {
            if (connectionString != null)
                ConnectionString = connectionString;

            Orm = SessionCreatorFactory(persistIn).OpenSession();
        }

        public String ConnectionString
        {
            get { return _connectionString ?? MySqlDefaultConnectionString; }
            set { _connectionString = value; }
        }

        public Object Orm { get; private set; }

        private ISessionFactory SessionCreatorFactory(PersistType persistIn)
        {
            Configuration cfg;
            switch (persistIn) {
                case PersistType.Sql:
                    cfg = StoreInMySqlConfiguration();
                    break;
                case PersistType.File:
                    cfg = StoreInFileConfiguration();
                    break;
                case PersistType.Memory:
                    cfg = StoreInMemoryConfiguration();
                    break;
                default:
                    throw new ArgumentEmptyException("persistIn");
            }
            return cfg.BuildSessionFactory();
        }

        private Configuration StoreInMySqlConfiguration()
        {
            return Mapping().Database(MySQLConfiguration.Standard
                                                        .ConnectionString(ConnectionString)
                                                        .ShowSql)
                            .BuildConfiguration();
        }

        private Configuration StoreInMsSqlConfiguration()
        {
            return Mapping().Database(MsSqlConfiguration.MsSql2008
                                                        .ConnectionString(ConnectionString)
                                                        .ShowSql())
                            .BuildConfiguration();
        }

        private Configuration StoreInMemoryConfiguration()
        {
            return Mapping().Database(SQLiteConfiguration.Standard
                                                         .InMemory()
                                                         .ShowSql())
                            .BuildConfiguration();
        }

        private Configuration StoreInFileConfiguration()
        {
            return Mapping().Database(SQLiteConfiguration.Standard
                                                         .UsingFile("local.db")
                                                         .ShowSql())
                            .BuildConfiguration();
        }

        private FluentConfiguration Mapping()
        {
            return Fluently.Configure().Mappings(x => x.FluentMappings.AddFromAssemblyOf<SessionCreator>())
                //Generate Tables
                           .ExposeConfiguration(SchemaCreator);
        }

        private SchemaExport _sqlSchema;
        private void SchemaCreator(Configuration cfg)
        {
            _sqlSchema = new SchemaExport(cfg);
            _sqlSchema.Drop(false, true);
            _sqlSchema.Create(false, true);
            //_sqlSchema.Execute(false, true, false);
        }
    }
}