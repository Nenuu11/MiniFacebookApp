using aphrie.DBL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace aphrie.DBL.Entities
{
    public class Post
    {
        public Post()
        {
            Medias = new HashSet<Media>();
            PostReactions = new HashSet<PostReaction>();
        }
        public string Id { get; set; }
        public string UserId{ get; set; }
        public string Description { get; set; }
        public virtual ApplicationUser Author{ get; set; }
        public virtual ICollection<Media> Medias { get; set; }
        public virtual ICollection<PostReaction> PostReactions { get; set; }


    }
}
