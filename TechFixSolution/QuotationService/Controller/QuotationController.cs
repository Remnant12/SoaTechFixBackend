using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuotationService.DbConfig;
using QuotationService.DTO;
using QuotationService.Models;

namespace QuotationService.Controller;

[Route("api/[controller]")]
[ApiController]
public class QuotationController : ControllerBase
{
   private readonly QuotationdbContext _context;

   public QuotationController(QuotationdbContext context)
   {
      _context = context;
   }
   
   [HttpGet]
   public async Task<ActionResult<IEnumerable<QuoteRequestDto>>> GetQuoteRequests()
   {
      var quoteRequests = await _context.QuoteRequests
         .Include(q => q.Products)
         .Select(q => new QuoteRequestDto
         {
            RequestId = q.RequestId,
            TechFixCustomerId = q.TechFixCustomerId,
            SupplierId = q.SupplierId,
            RequestDate = q.RequestDate,
            RequestStatus = q.RequestStatus,
            ValidUntil = q.ValidUntil,
            RequestNotes = q.RequestNotes,
            CreatedBy = q.CreatedBy,
            Products = q.Products.Select(p => new QuoteProductDto
            {
               QuoteProductId = p.QuoteProductId,
               ProductId = p.ProductId,
               Quantity = p.Quantity,
               UnitPrice = p.UnitPrice,
               RequestId = p.RequestId
            }).ToList()
         }).ToListAsync();

      return Ok(quoteRequests);
   }
   
   [HttpGet("{id}")]
   public async Task<ActionResult<QuoteRequestDto>> GetQuoteRequest(int id)
   {
      var quoteRequest = await _context.QuoteRequests
         .Include(q => q.Products)
         .Select(q => new QuoteRequestDto
         {
            RequestId = q.RequestId,
            TechFixCustomerId = q.TechFixCustomerId,
            SupplierId = q.SupplierId,
            RequestDate = q.RequestDate,
            RequestStatus = q.RequestStatus,
            ValidUntil = q.ValidUntil,
            RequestNotes = q.RequestNotes,
            CreatedBy = q.CreatedBy,
            Products = q.Products.Select(p => new QuoteProductDto
            {
               QuoteProductId = p.QuoteProductId,
               ProductId = p.ProductId,
               Quantity = p.Quantity,
               UnitPrice = p.UnitPrice,
               RequestId = p.RequestId
            }).ToList()
         })
         .FirstOrDefaultAsync(qr => qr.RequestId == id);

      if (quoteRequest == null)
      {
         return NotFound();
      }

      return Ok(quoteRequest);
   }
   
   [HttpPost]
   public async Task<ActionResult<QuoteRequestDto>> PostQuoteRequest(QuoteRequestDto quoteRequestDto)
   {
      var quoteRequest = new QuoteRequest
      {
         RequestId = quoteRequestDto.RequestId,
         TechFixCustomerId = quoteRequestDto.TechFixCustomerId,
         SupplierId = quoteRequestDto.SupplierId,
         RequestDate = quoteRequestDto.RequestDate,
         RequestStatus = quoteRequestDto.RequestStatus,
         ValidUntil = quoteRequestDto.ValidUntil,
         RequestNotes = quoteRequestDto.RequestNotes,
         CreatedBy = quoteRequestDto.CreatedBy,
      };

      // Add QuoteProducts
      if (quoteRequestDto.Products != null && quoteRequestDto.Products.Any())
      {
         foreach (var productDto in quoteRequestDto.Products)
         {
            var quoteProduct = new QuoteProduct
            {
               QuoteProductId = productDto.QuoteProductId,
               ProductId = productDto.ProductId,
               Quantity = productDto.Quantity,
               UnitPrice = productDto.UnitPrice,
               RequestId = quoteRequest.RequestId
            };
            quoteRequest.Products.Add(quoteProduct);
         }
      }

      _context.QuoteRequests.Add(quoteRequest);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetQuoteRequest), new { id = quoteRequest.RequestId }, quoteRequestDto);
   }
   
   // PUT: api/quotations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuoteRequest(int id, QuoteRequestDto quoteRequestDto)
        {
            if (id != quoteRequestDto.RequestId)
            {
                return BadRequest();
            }

            var quoteRequest = await _context.QuoteRequests.Include(q => q.Products)
                .FirstOrDefaultAsync(qr => qr.RequestId == id);

            if (quoteRequest == null)
            {
                return NotFound();
            }

            // Update QuoteRequest fields
            quoteRequest.TechFixCustomerId = quoteRequestDto.TechFixCustomerId;
            quoteRequest.SupplierId = quoteRequestDto.SupplierId;
            quoteRequest.RequestDate = quoteRequestDto.RequestDate;
            quoteRequest.RequestStatus = quoteRequestDto.RequestStatus;
            quoteRequest.ValidUntil = quoteRequestDto.ValidUntil;
            quoteRequest.RequestNotes = quoteRequestDto.RequestNotes;
            quoteRequest.CreatedBy = quoteRequestDto.CreatedBy;

            // Update Products
            quoteRequest.Products.Clear();  // Remove existing products

            foreach (var productDto in quoteRequestDto.Products)
            {
                var quoteProduct = new QuoteProduct
                {
                    QuoteProductId = productDto.QuoteProductId,
                    ProductId = productDto.ProductId,
                    Quantity = productDto.Quantity,
                    UnitPrice = productDto.UnitPrice,
                    RequestId = quoteRequest.RequestId
                };
                quoteRequest.Products.Add(quoteProduct);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        // DELETE: api/quotations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuoteRequest(int id)
        {
           var quoteRequest = await _context.QuoteRequests.Include(q => q.Products)
              .FirstOrDefaultAsync(qr => qr.RequestId == id);

           if (quoteRequest == null)
           {
              return NotFound();
           }

           _context.QuoteRequests.Remove(quoteRequest);
           await _context.SaveChangesAsync();

           return NoContent();
        }
   
}