using AdsAPI.Data;
using AdsAPI.DTO;
using AdsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ControllerBase
    {
        private readonly AdsDbContext _context;

        public AdsController(AdsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ad>>> GetAds()
        {
            return Ok(await _context.Ads.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetAd(int id)
        {
            var ad = await _context.Ads.FindAsync(id);
            if (ad == null) return NotFound();
            return Ok(ad);
        }

        [HttpPost]
        public async Task<ActionResult<Ad>> CreateAd(AdCreateDto adDto)
        {
            var ad = new Ad
            {
                Title = adDto.Title,
                Description = adDto.Description,
                Price = adDto.Price
            };

            _context.Ads.Add(ad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAd), new { id = ad.Id }, ad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAd(int id, AdUpdateDto adDto)
        {
            var ad = await _context.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }

            ad.Title = adDto.Title;
            ad.Description = adDto.Description;
            ad.Price = adDto.Price;

            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAd(int id)
        {
            var ad = await _context.Ads.FindAsync(id);
            if (ad == null) return NotFound();

            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
