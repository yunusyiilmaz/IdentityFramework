using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Context.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {        
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey("Id");
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.ToTable("Categories");
        }
    }
}
