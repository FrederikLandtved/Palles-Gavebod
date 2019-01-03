using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PallesGavebodAPI1.Models
{
	public class Gift
	{
		[Key]
		[DisplayName("Gavenummer")]
		public int GiftNumber { get; set; }

		[DisplayName("Titel")]
		public string Title { get; set; }

		[DisplayName("Beskrivelse")]
		public string Description { get; set; }

		[DisplayName("Tidspunkt")]
		public DateTime CreationDate { get; set; }

		[DisplayName("Til drenge")]
		public bool BoyGift { get; set; }

		[DisplayName("Til piger")]
		public bool GirlGift { get; set; }
	}
}
