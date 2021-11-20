using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace GroupProject.Entities
{
    public class InvoiceLineItem : IEntityTypeConfiguration<InvoiceLineItem>
    {
        public int InvoiceLineItemId { get; set; }
        public int InvoiceId { get; set;}
        public int ItemId { get; set; }
        public ItemDescription ItemDescription { get; set; }
        public Invoice Invoice { get; set; }

        public void Configure(EntityTypeBuilder<InvoiceLineItem> builder)
        {
            builder.HasKey(x => x.InvoiceLineItemId);
            builder.HasOne(x => x.ItemDescription)
                .WithMany( x=>x.InvoiceLineItems);
            builder.HasOne(x => x.Invoice)
                .WithMany(x => x.InvoiceLineItems);
        }
    }
}