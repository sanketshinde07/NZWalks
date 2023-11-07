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

        public async Task<List<Region>> GetAllAsync(string? filterBy=null,string? filterOn=null,string? sortBy=null,bool isAsscending=false,
                                                     int pageNumber=1,int pageSize=2)
        {
            //return await dbContext.Regions.ToListAsync();
            var lis =  dbContext.Regions.AsQueryable();

            
            //filtering
            if (string.IsNullOrEmpty(filterBy) == false && string.IsNullOrEmpty(filterOn) == false)
            {
                if (filterBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                        lis = lis.Where(x => x.Name.Contains(filterOn));
                }
            }

            if( string.IsNullOrEmpty(sortBy)==false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                    lis = isAsscending ? lis.OrderBy(x => x.Name) : lis.OrderByDescending(x => x.Name);
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await lis.Skip(skipResults).Take(pageSize).ToListAsync();

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

