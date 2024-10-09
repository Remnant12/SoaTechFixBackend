using System.ComponentModel.DataAnnotations;

namespace QuotationService.Models;

public class QuoteRequest
{
    [Key]
    [Required]
    public int RequestId { get; set; }  // Unique identifier for the request
    
    [Required]
    public int TechFixCustomerId { get; set; }  // Foreign key to the TechFix customer
    
    [Required]
    public int SupplierId { get; set; }  // Foreign key to the supplier (Supplier A, Supplier B)
    
    [Required]
    public DateTime RequestDate { get; set; }  // Date and time the request was sent
    
    [Required]
    public string RequestStatus { get; set; }  // Status of the request (e.g., Pending, Canceled)
    
    [Required]
    public DateTime? ValidUntil { get; set; }  // Optional: Validity date for the request
    
    [Required]
    public string RequestNotes { get; set; }  // Any special instructions or notes from TechFix
    
    [Required]
    public string CreatedBy { get; set; }  // Who created the request (for auditing)

    // One-to-many relationship: A quote request can have many products
    [Required]
    public List<QuoteProduct> Products { get; set; }

    public QuoteRequest()
    {
        Products = new List<QuoteProduct>();  // Initialize the product list
    }
}