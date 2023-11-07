using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
	public class AddRegionRequestDto
	{
        [Required]
        [MaxLength(10,ErrorMessage = "Length should be less than 10")]
        public string Code { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Length should be less than 10")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}

