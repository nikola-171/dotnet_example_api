using Microsoft.EntityFrameworkCore;
using PaymentApp.Models;


namespace PaymentApp.Models
{
    public class PaymentDetailsContext : DbContext
    {
        public PaymentDetailsContext(DbContextOptions<PaymentDetailsContext> options) : base(options)
        {

        }

        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        
    }
}