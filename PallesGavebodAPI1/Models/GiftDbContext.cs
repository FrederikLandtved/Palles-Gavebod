using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PallesGavebodAPI1.Models
{
	public class GiftDbContext : DbContext
	{
		public GiftDbContext(DbContextOptions<GiftDbContext> options) : base(options)
		{
			Database.EnsureCreated();
			if (Gifts.CountAsync().Result == 0)
			{
				var item = new Gift()
				{
					Title = "LG fjernsyn",
					Description = "55 tommer"
				};
				Gifts.Add(item);
				SaveChanges();
				item = new Gift()
				{
					Title = "Samsung Galaxy",
					Description = "Smartphone"
				};
				Gifts.Add(item);
				SaveChanges();
			}
		}
		public DbSet<Gift> Gifts { get; set; }
	}
}
