using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aphrie.app.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is requied. Please, fill in the field")]
        public string Email{ get; set; }
        public string Phone{ get; set; }
        [Required(ErrorMessage = "Password is requied. Please, fill in the field")]
        public string Password{ get; set; }
    }
}
