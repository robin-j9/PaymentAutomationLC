using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PaymentAutomationLC.Models;

namespace PaymentAutomationLC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentProfile> PaymentProfiles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
