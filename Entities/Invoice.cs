using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace GroupProject.Entities
{
    public class Invoice : IEntityTypeConfiguration<Invoice>
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public Decimal TotalCost { get; set; }

        public IList<InvoiceLineItem> InvoiceLineItems { get; set; }

        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x=> x.InvoiceId);
            builder.Property(x => x.TotalCost)
                .HasPrecision(19, 4);
        }
    }
}