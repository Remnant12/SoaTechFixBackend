namespace QuotationService.DTO;


public class QuoteRequestDto
{
    public int RequestId { get; set; }  // Unique identifier for the request
    
    public int TechFixCustomerId { get; set; }  // Foreign key to the TechFix customer
    
    public int SupplierId { get; set; }  // Foreign key to the supplier (Supplier A, Supplier B)
    
    public DateTime RequestDate { get; set; }  // Date and time the request was sent
    
    public string RequestStatus { get; set; }  // Status of the request (e.g., Pending, Canceled)
    
    public DateTime? ValidUntil { get; set; }  // Optional: Validity date for the request
    
    public string RequestNotes { get; set; }  // Any special instructions or notes from TechFix
    
    public string CreatedBy { get; set; }  // Who created the request (for auditing)

    // One-to-many relationship: A quote request can have many products
    public List<QuoteProductDto> Products { get; set; }

    public QuoteRequestDto()
    {
        Products = new List<QuoteProductDto>();  // Initialize the product list
    }
}
