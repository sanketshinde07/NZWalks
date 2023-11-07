using System;
using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
	public class UpdateWalkRequestDto
	{
        [Required]
        [MaxLength(10, ErrorMessage = "Length should be less than 10")]
        public string Name { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Length should be less than 10")]
        public string Description { get; set; }
        [Required]
        [Range(0.0, 15.0, ErrorMessage = "Length should be less than 15.0 Km")]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyID { get; set; }
        public Guid RegionID { get; set; }
    }
}

