using System;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repositories
{
	public interface IRegionRepository
	{
		Task<List<Region>> GetAllAsync(string? filterBy,string? filterOn,string? sortBy,bool isAsscending,int pageNumber, int pageSize);
		Task<Region> Create(Region region);
		Task<Region?> GetById(Guid id);
		Task<Region?> Update(Guid id, UpdateRegionRequestDto region);
		Task<Region?> Delete(Guid id);
	}
}

