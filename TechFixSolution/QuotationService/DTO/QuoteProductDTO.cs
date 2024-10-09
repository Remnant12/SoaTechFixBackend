namespace QuotationService.DTO;

public class QuoteProductDto
{
    public int QuoteProductId { get; set; }  // Unique identifier for the product entry in the quote
    
    public int ProductId { get; set; }  // Unique identifier for the actual product
    
    public int Quantity { get; set; }  // Quantity of the product being quoted
    
    public decimal? UnitPrice { get; set; }  // Optional price per unit at the time of request
    
    public int RequestId { get; set; }  // Foreign key to the QuoteRequest
}
