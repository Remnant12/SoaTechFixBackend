using Microsoft.EntityFrameworkCore;
using QuotationService.Models;

namespace QuotationService.DbConfig;

public class QuotationdbContext : DbContext
{
    public DbSet<QuoteRequest> QuoteRequests { get; set; }
    public DbSet<QuoteProduct> QuoteProducts { get; set; }
    
    public QuotationdbContext(DbContextOptions<QuotationdbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuoteProduct>()
            .HasOne(qp => qp.QuoteRequest)  // A QuoteProduct belongs to one QuoteRequest
            .WithMany(qr => qr.Products)    // A QuoteRequest has many QuoteProducts
            .HasForeignKey(qp => qp.RequestId);
    }
}