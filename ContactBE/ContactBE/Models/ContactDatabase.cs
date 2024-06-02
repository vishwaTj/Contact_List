using Microsoft.EntityFrameworkCore;
using System;

namespace ContactBE.Models{
    public class ContactDatabase : DbContext
    {
        public DbSet<ContactData> Contacts { get; set; }

        public ContactDatabase(DbContextOptions<ContactDatabase> options) : base(options)
        {
        }
    }
}


