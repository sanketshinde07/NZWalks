using System;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repositories
{
	public interface IWalkRepository
	{
        Task<List<Walk>> GetAllAsync();
        Task<Walk> Create(Walk walk);
        Task<Walk?> GetById(Guid id);
        Task<Walk?> Update(Guid id, UpdateWalkRequestDto region);
        Task<Walk?> Delete(Guid id);
    }
}

