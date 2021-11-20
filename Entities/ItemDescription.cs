using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Entities
{
    public class ItemDescription : IEntityTypeConfiguration<ItemDescription>
    {
        public int ItemDescriptionId { get; set; }
        public string Description { get; set; }
        public Decimal Cost { get; set; }

        public IList<InvoiceLineItem> InvoiceLineItems { get; set; }
        public void Configure(EntityTypeBuilder<ItemDescription> builder)
        {
            builder.HasKey(x => x.ItemDescriptionId);
            builder.Property(x => x.Cost)
                .HasPrecision(19, 4);
        }
    }
}
