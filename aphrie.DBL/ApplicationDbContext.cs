using aphrie.DBL.Entities;
using aphrie.DBL.Entities.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace aphrie.DBL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PostConfig());
            builder.ApplyConfiguration(new MediaConfig());
            builder.ApplyConfiguration(new FriendConfig());
            builder.ApplyConfiguration(new PostReactionConfig());
        }

        public virtual DbSet<Friend> Friends{ get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        

    }
}
