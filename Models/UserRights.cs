using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace userDemo1.Models
{
    public class UserRights
    {
        public bool ViewAuthorized { get; set; }
        public bool AddAuthorized { get; set; }
        public bool EditAuthorized { get; set; }
        public bool DeleteAuthorized { get; set; }
    }
}