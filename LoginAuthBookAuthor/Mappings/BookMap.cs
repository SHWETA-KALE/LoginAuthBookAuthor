using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using LoginAuthBookAuthor.Models;

namespace LoginAuthBookAuthor.Mappings
{
    public class BookMap:ClassMap<Book>
    {
        public BookMap()
        {
            Table("Books");
            Id(b => b.Id).GeneratedBy.GuidComb();
            Map(b => b.Name);
            Map(b => b.Genre);
            Map(b => b.Description);
            References(b => b.Author).Column("AuthorId").Cascade.None().Nullable();
        }
    }
}