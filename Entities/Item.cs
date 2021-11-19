using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Entities
{
    public class Item : IEntityTypeConfiguration<Item>
    {
        public int ItemId { get; set; }
        public string Description { get; set; }
        public Decimal Cost { get; set; }

        public IList<InvoiceLineItem> InvoiceLineItems { get; set; }
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.ItemId);
            builder.Property(x => x.Cost)
                .HasPrecision(19, 4);
        }
    }
}
