using Microsoft.AspNetCore.Mvc;
using Quotes.Model;

namespace Quotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : ControllerBase
    {
        private static readonly List<Quote> _quotes = new List<Quote>
        {
            new Quote { Id = 1, Text = "La mejor victoria es vencer sin combatir", Author = "Sun Tzu" },
            new Quote { Id = 2, Text = "Lo que no te mata, te hace más fuerte", Author = "Friedrich Nietzsche" },
            new Quote { Id = 3, Text = "El que madruga, Dios lo ayuda", Author = null },
        };

        private readonly ILogger<QuotesController> _logger;

        [HttpGet]
        public IReadOnlyList<Quote> GetAll() => _quotes;

        [HttpGet("{id:int}")]
        public Quote? GetById(int id) => _quotes.FirstOrDefault(q => q.Id == id);

        [HttpGet("by-author/{author}")]
        public ActionResult<IEnumerable<Quote>> GetByAuthor(string author)
        {
            if (author.Equals("anonymous", StringComparison.OrdinalIgnoreCase))
            {
                return Ok(_quotes.Where(q => q.Author == null));
            }

            var result = _quotes
                .Where(q => q.Author != null &&
                            q.Author.Equals(author, StringComparison.OrdinalIgnoreCase));

            return Ok(result);
        }
    }
}
