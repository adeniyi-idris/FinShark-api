using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class AppUser: IdentityUser
    {
        public List<Portfolio> portfolio {get; set;} = new List<Portfolio> ();
    }
}