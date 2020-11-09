using aphrie.DBL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace aphrie.DBL.Entities
{
    public class PostReaction
    {
        public string PostId{ get; set; }
        public string  UserId{ get; set; }
        public Reactions Reactions{ get; set; }
        public virtual Post Post{ get; set; }
        public virtual ApplicationUser User{ get; set; }
    }
}
