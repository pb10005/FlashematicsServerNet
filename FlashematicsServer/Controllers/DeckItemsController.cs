using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashematicsServer.Models;

namespace FlashematicsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeckItemsController : ControllerBase
    {
        private readonly DeckContext _context;

        public DeckItemsController(DeckContext context)
        {
            _context = context;
        }

        // GET: api/DeckItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeckItem>>> GetDeckItems()
        {
            return await _context.DeckItems.ToListAsync();
        }

        // GET: api/DeckItems/5
        [HttpGet("{name}")]
        public async Task<ActionResult<DeckItem>> GetDeckItem(string name)
        {
            var deckItem = await _context.DeckItems.FirstOrDefaultAsync(d => d.Name == name);

            if (deckItem == null)
            {
                return NotFound();
            }

            return deckItem;
        }

        // PUT: api/DeckItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeckItem(long id, DeckItem deckItem)
        {
            if (id != deckItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(deckItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeckItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DeckItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DeckItem>> PostDeckItem(DeckItem deckItem)
        {
            // Insert if deckItem not exists
            // Update if deckItem exists

            var deck = await _context.DeckItems.FirstOrDefaultAsync(x => x.Name == deckItem.Name);

            if (deck == null)
            {
                deckItem.UpdatedAt = DateTime.Now.ToUniversalTime();
                // Insert
                _context.DeckItems.Add(deckItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetDeckItem", new { name = deckItem.Name }, deckItem);
            }
            else
            {
                // Update
                deck.Base64 = deckItem.Base64;
                var nowDate = DateTime.Now.ToUniversalTime();
                if (deckItem.UpdatedAt >= deck.UpdatedAt)
                {
                    deck.UpdatedAt = nowDate;
                    _context.Entry(deck).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                    return AcceptedAtAction("GetDeckItem", new { name = deck.Name }, deck);
                }
                else
                {
                    var message = string.Format("ERROR: Someone changed the deck on this server after you fetched it." +
                        "{0}, {1}", deckItem.UpdatedAt,deck.UpdatedAt);
                    return BadRequest(message);
                }
            }

        }

        // DELETE: api/DeckItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeckItem(long id)
        {
            var deckItem = await _context.DeckItems.FindAsync(id);
            if (deckItem == null)
            {
                return NotFound();
            }

            _context.DeckItems.Remove(deckItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeckItemExists(long id)
        {
            return _context.DeckItems.Any(e => e.Id == id);
        }
    }
}
