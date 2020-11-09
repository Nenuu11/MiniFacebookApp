using aphrie.DBL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aphrie.app.Models
{
    public class PostModel
    {
        [Required(ErrorMessage = "Post description is required. Please, fill in the field")]
        public string Description{ get; set; }
        public List<string> ImgSrc{ get; set; }
    }
}
