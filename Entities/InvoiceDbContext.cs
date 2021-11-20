using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Entities
{
    public class InvoiceDbContext : DbContext
    {
        /// <summary>
        /// Path to the sqlite database
        /// </summary>
        public string DbPath { get; private set; }
        /// <summary>
        /// Initializes the database path
        /// </summary>
        public InvoiceDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{Path.DirectorySeparatorChar}reservations.db";
        }
        /// <summary>
        /// Tell Entity Framework we are using Sqlite for this instance so it can taylor it's queries
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
        /// <summary>
        /// Allows for you to query against specific entities
        /// </summary>
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
    }
}
