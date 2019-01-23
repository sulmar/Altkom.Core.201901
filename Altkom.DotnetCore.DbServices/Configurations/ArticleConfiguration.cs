using Altkom.DotnetCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.DbServices.Configurations
{
    class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder
                 .Property(p => p.Weight)
                 .HasColumnName("Waga");
        }
    }
}
