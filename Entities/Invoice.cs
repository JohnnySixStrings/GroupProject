using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace GroupProject.Entities
{
    public class Invoice : IEntityTypeConfiguration<Invoice>
    {
        /// <summary>
        /// Invoice Key
        /// </summary>
        public int InvoiceId { get; set; }
        /// <summary>
        /// User Set Invoice date
        /// </summary>
        public DateTime InvoiceDate { get; set; }
        /// <summary>
        /// TotalCost
        /// </summary>
        public Decimal TotalCost { get; set; }
        /// <summary>
        /// List of Related InvoiceLineItems
        /// </summary>
        public IList<InvoiceLineItem> InvoiceLineItems { get; set; }

        /// <summary>
        /// builder for setting up properties and relationships
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x=> x.InvoiceId);
            builder.Property(x => x.TotalCost)
                .HasPrecision(19, 4);
        }
    }
}