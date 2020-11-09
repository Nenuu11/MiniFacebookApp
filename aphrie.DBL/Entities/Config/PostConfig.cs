using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace aphrie.DBL.Entities.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

            builder.Property(p => p.Description)
                .HasMaxLength(1300)
                .IsRequired();

            builder.HasMany(p => p.Medias)
                .WithOne(m => m.Post)
                .HasForeignKey(m => m.PostId);

        }
    }
}
