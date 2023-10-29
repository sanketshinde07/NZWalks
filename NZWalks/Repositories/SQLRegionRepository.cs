using System;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repositories
{
	public class SQLRegionRepository:IRegionRepository
	{
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
		{
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await dbContext.Regions.FindAsync(id);
        }

        public async Task<Region> Create(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> Update(Guid id, UpdateRegionRequestDto region)
        {
            var existingRegion = await dbContext.Regions.FindAsync(id);
            if(existingRegion != null)
            {
                existingRegion.Code = region.Code;
                existingRegion.Name = region.Name;
                existingRegion.RegionImageUrl = region.RegionImageUrl;

                await dbContext.SaveChangesAsync();
                return existingRegion;
            }
            else
            {
                return null;
            }

        }

        public async Task<Region?> Delete(Guid id)
        {
            var existingRegion = await dbContext.Regions.FindAsync(id);
            if(existingRegion!=null)
            {
                 dbContext.Regions.Remove(existingRegion);
                await dbContext.SaveChangesAsync();
                return existingRegion;
            }
            else
            {
                return null;
            }
        }
    }
}

