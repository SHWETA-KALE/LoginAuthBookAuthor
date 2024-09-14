using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAuthBookAuthor.Models
{
    public class Author
    {
        public virtual Guid Id { get; set; }
        //public virtual string Name { get; set; }
        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }
        public virtual int Age { get; set; }

        public virtual string Email { get; set; }

        public virtual IList<Book> Books { get; set; } = new List<Book>(); //many

        public virtual AuthorDetails AuthorDetails { get; set; } = new AuthorDetails();

       
    }
}