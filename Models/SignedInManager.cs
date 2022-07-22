using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using userDemo1.Context;

namespace userDemo1.Models
{
    public class SignedInManager
    {
        public User User { get; set; }
        public IEnumerable<Permission> Permissions { get; set; }
    }

    public class Permission
    {
        public string ModuleName { get; set; }
        public bool ViewPermission { get; set; }
        public bool AddPermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
    }
}