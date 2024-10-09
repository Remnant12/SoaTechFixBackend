using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace QuotationService.Models;

public class QuoteProduct
{
    [Key]
    [Required]
    public int QuoteProductId { get; set; }  // Unique identifier for the product entry in the quote
    
    [Required]
    public int ProductId { get; set; }  // Unique identifier for the actual product
    
    [Required]
    public int Quantity { get; set; }  // Quantity of the product being quoted
    
    [Required]
    public decimal? UnitPrice { get; set; }  // Optional price per unit at the time of request

    // Foreign key to relate each product to a specific QuoteRequest
    [Required]
    public int RequestId { get; set; }  // Foreign key to QuoteRequest
    public QuoteRequest QuoteRequest { get; set; }  // Navigation property (EF)
}