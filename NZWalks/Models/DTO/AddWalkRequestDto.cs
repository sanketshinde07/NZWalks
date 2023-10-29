using System;
namespace NZWalks.Models.DTO
{
	public class AddWalkRequestDto
	{
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyID { get; set; }
        public Guid RegionID { get; set; }
    }
}

