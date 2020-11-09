using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace aphrie.DBL.Entities.Config
{
    class PostReactionConfig : IEntityTypeConfiguration<PostReaction>
    {
        public void Configure(EntityTypeBuilder<PostReaction> builder)
        {
            builder.HasKey(pr => new { pr.UserId, pr.PostId });

            builder.HasOne(pr => pr.Post)
                .WithMany(p => p.PostReactions)
                .HasForeignKey(pr => pr.PostId);

            builder.HasOne(pr => pr.User)
                .WithMany(u => u.PostReactions)
                .HasForeignKey(pr => pr.UserId);
                
        }
    }
}
