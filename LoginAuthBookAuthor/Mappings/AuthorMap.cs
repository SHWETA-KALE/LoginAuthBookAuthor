using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using LoginAuthBookAuthor.Models;

namespace LoginAuthBookAuthor.Mappings
{
    public class AuthorMap:ClassMap<Author>
    {
        public AuthorMap()
        {
            Table("Authors");
            Id(a => a.Id).GeneratedBy.GuidComb();
            Map(a => a.Email);
            Map(a => a.Age);
            Map(a=>a.UserName);
            Map(a => a.Password);
            HasOne(d => d.AuthorDetails).Cascade.All().PropertyRef(d => d.Author).Constrained();
            HasMany(a => a.Books).Inverse().Cascade.All();
        }
    }
}