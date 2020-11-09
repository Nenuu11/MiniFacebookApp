using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace aphrie.DBL.Entities
{
    public class Media
    {
        public string Id { get; set; }
        public string Path { get; set; }
        public string PostId { get; set; }

        public virtual Post Post{ get; set; }
    }
}
