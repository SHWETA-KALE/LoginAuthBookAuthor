using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAuthBookAuthor.Models
{
    public class AuthorDetails
    {
        public virtual Guid Id { get; set; }

        public virtual string Street { get; set; }

        public virtual string City { get; set; }

        public virtual string State { get; set; }

        public virtual string Country { get; set; }

        public virtual Author Author { get; set; }

        public virtual bool IsActive { get; set; } = true;
        
        //add isActive flag => soft delete
    }
}