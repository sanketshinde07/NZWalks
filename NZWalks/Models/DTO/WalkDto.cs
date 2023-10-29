using System;
using NZWalks.Models.Domain;

namespace NZWalks.Models.DTO
{
	public class WalkDto
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        public Difficulty Difficulty { get; set; }
        public Region Region { get; set; }
    }
}

