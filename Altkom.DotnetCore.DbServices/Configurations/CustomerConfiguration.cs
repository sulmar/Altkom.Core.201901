using Altkom.DotnetCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.DotnetCore.DbServices.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                // add package Microsoft.EntityFrameworkCore.Relational
                .ToTable("Klienci");

            builder
                .Ignore(p => p.IsSelected);

            builder
                .Property(p => p.FirstName)
                .HasMaxLength(50);

            builder
               .Property(p => p.LastName)
               .HasMaxLength(50)
               .IsRequired();
        }
    }
}
