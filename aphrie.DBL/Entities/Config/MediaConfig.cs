using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace aphrie.DBL.Entities.Config
{
    public class MediaConfig : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Path)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(m => m.Post)
                .WithMany(p => p.Medias)
                .HasForeignKey(m => m.PostId);
        }
    }
}
