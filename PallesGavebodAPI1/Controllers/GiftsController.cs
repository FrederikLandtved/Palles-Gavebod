using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PallesGavebodAPI1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PallesGavebodAPI1.Controllers
{
	[Route("api/gifts/")]
	public class GiftsController : Controller
	{
		private readonly GiftDbContext _context;

		public GiftsController(GiftDbContext context)
		{
			_context = context;
		}

		// GET: api/Gifts
		[HttpGet]
		public IEnumerable<Gift> GetAllGifts()
		{
			return _context.Gifts;
		}

		[HttpGet("{GiftNumber}")]
		public async Task<IActionResult> GetGift([FromRoute] int giftNumber)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var gift = await _context.Gifts.FindAsync(giftNumber);

			return Ok(gift);
		}


		[HttpGet("girls")]
		public async Task<IActionResult> GetGirlGifts()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var gift = await _context.Gifts.Where(c => c.GirlGift == true).ToListAsync();

			return Ok(gift);
		}

		[HttpGet("boys")]
		public async Task<IActionResult> GetBoyGifts()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var gift = await _context.Gifts.Where(c => c.BoyGift == true).ToListAsync();

			return Ok(gift);
		}



		[HttpPost]
		public async Task<IActionResult> AddGift([FromBody]Gift gift)
		{
			var newGift = new Gift()
			{
				Title = gift.Title,
				Description = gift.Description,
				CreationDate = DateTime.Now,
				BoyGift = gift.BoyGift,
				GirlGift = gift.GirlGift
			};

			await _context.AddAsync(newGift);
			_context.SaveChanges();

			return Ok();
		}

	}
}
