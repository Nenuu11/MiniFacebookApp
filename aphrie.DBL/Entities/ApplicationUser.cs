using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace aphrie.DBL.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        {
            Posts = new HashSet<Post>();
            Users = new HashSet<Friend>();
            Friends = new HashSet<Friend>();
            PostReactions = new HashSet<PostReaction>();
        }

        public virtual ICollection<Post> Posts{ get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Friend> Users{ get; set; }
        [InverseProperty("UserFriend")]
        public virtual ICollection<Friend> Friends{ get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }
    }
}
