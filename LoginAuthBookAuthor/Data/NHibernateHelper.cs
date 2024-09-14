using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using LoginAuthBookAuthor.Mappings;
using NHibernate.Tool.hbm2ddl;

namespace LoginAuthBookAuthor.Data
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory = null;

        //building session factory
        public static ISession CreateSession()
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012.ConnectionString("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LoginAuthBookAuthorDB;Integrated Security=True;Connect Timeout=30;"))
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<AuthorMap>())
                    .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))  //used for making tables 2nd true is for update karna hai ya nhi and 1st true is for firing query on console
                    .BuildSessionFactory();
            }
            return _sessionFactory.OpenSession();
        }
    }
}