namespace QuotationService.Models;

public class QuoteRequest
{
    public Guid RequestId { get; set; }  // Unique identifier for the request
    public Guid TechFixCustomerId { get; set; }  // Foreign key to the TechFix customer
    public Guid SupplierId { get; set; }  // Foreign key to the supplier (Supplier A, Supplier B)
    public List<QuoteProduct> Products { get; set; }  // List of products included in the request
    public DateTime RequestDate { get; set; }  // Date and time the request was sent
    public string RequestStatus { get; set; }  // Status of the request (e.g., Pending, Canceled)
    public DateTime? ValidUntil { get; set; }  // Optional: Validity date for the request
    public string RequestNotes { get; set; }  // Any special instructions or notes from TechFix
    public string CreatedBy { get; set; }  // Who created the request (for auditing)

    public QuoteRequest()
    {
        Products = new List<QuoteProduct>();  // Initialize the products list
    }
}