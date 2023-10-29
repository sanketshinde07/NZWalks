using System;
using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;


namespace NZWalks.Mappings
{
	public class AutoMapperProfile:Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Region, RegionDto>().ReverseMap();
			CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
			CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
			CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
        }
    }
}

