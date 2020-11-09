using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace aphrie.DBL.Entities
{
    public class Friend
    {
        public string UserId{ get; set; }
        public string FriendId{ get; set; }
        public virtual ApplicationUser User{ get; set; }
        public virtual ApplicationUser UserFriend { get; set; }

    }
}
