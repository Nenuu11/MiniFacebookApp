using aphrie.DBL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aphrie.app.Models
{
    public class AuthenticationModal : ApplicationUser
    {
        public string Token { get; set; }

    }
}
