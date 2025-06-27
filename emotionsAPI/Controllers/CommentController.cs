using emotionsAPI.Data;
using emotionsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emotionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentContext _context;

        public CommentController(CommentContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("comments")]
        public async Task<IActionResult> NewComment(Comment comment)
        {
            string mensaje = comment.message.ToLower(); // Para comparar sin importar mayúsculas
            var palabras = mensaje.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Emotions.Emotion emocionDetectada = Emotions.Emotion.neutral;

            if (palabras.Any(p => Emotions.Positivo.Contains(p)))
            {
                emocionDetectada = Emotions.Emotion.positivo;
            }
            else if (palabras.Any(p => Emotions.Negativo.Contains(p)))
            {
                emocionDetectada = Emotions.Emotion.negativo;
            }

            comment.emotion = emocionDetectada.ToString();
            await _context.AddAsync(comment);
            await _context.SaveChangesAsync();

            return Ok(comment);
        }

        [HttpGet]
        [Route("comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> CommentList([FromQuery] string? filter)
        {
            var query = _context.Comment.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                if (long.TryParse(filter, out long id))
                {
                    query = query.Where(c => c.id == id);
                }
                else
                {
                    filter = filter.ToLower();
                    query = query.Where(c => c.emotion.ToLower() == filter);
                }
            }

            var commentList = await query
                .OrderByDescending(c => c.createdAt)
                .ToListAsync();

            return Ok(commentList);
        }

        [HttpGet]
        [Route("sentiment-sumary")]
        public async Task<ActionResult<IEnumerable<Comment>>> Summary()
        {
            var total = await _context.Comment.CountAsync();

            var counts = await _context.Comment
                .GroupBy(c => c.emotion.ToLower())
                .Select(g => new { Emotion = g.Key, Count = g.Count() })
                .ToListAsync();

            var summary = new
            {
                total_comments = total,
                sentiment_counts = new
                {
                    positivo = counts.FirstOrDefault(x => x.Emotion == "positivo")?.Count ?? 0,
                    negativo = counts.FirstOrDefault(x => x.Emotion == "negativo")?.Count ?? 0,
                    neutral = counts.FirstOrDefault(x => x.Emotion == "neutral")?.Count ?? 0
                }
            };

            return Ok(summary);
        }

        
    }
}
