using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using LoginAuthBookAuthor.Models;

namespace LoginAuthBookAuthor.Mappings
{
    public class AuthorDetailsMap:ClassMap<AuthorDetails>
    {
        public AuthorDetailsMap()
        {
            Table("AuthorDetails");
            Id(a => a.Id).GeneratedBy.GuidComb();
            Map(a => a.Street);
            Map(a => a.City);
            Map(a => a.State);
            Map(a => a.Country);
            Map(a => a.IsActive);

            //one to one
            References(a => a.Author)
                .Column("AuthorId")
                .Unique()
                .Cascade.None();
        }
    }
}