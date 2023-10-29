using System;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repositories
{
	public class SQLWalkRepostiory : IWalkRepository
	{
        private readonly NZWalksDbContext nZWalksDbContext;

        public SQLWalkRepostiory(NZWalksDbContext nZWalksDbContext)
		{
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> Create(Walk walk)
        {
           await nZWalksDbContext.Walks.AddAsync(walk);
           await nZWalksDbContext.SaveChangesAsync();
           return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await nZWalksDbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetById(Guid id)
        {
            var lis =await nZWalksDbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            return lis.FirstOrDefault(opt => opt.Id == id);
        }

            public async Task<Walk?> Update(Guid id, UpdateWalkRequestDto walkDto)
        {
            var walkDomain= await nZWalksDbContext.Walks.FindAsync(id);
            if(walkDomain!=null)
            {
                walkDomain.Name = walkDto.Name;
                walkDomain.LengthInKm = walkDto.LengthInKm;
                walkDomain.Description = walkDto.Description;
                walkDto.WalkImageUrl = walkDto.WalkImageUrl;
                walkDomain.DifficultyID = walkDto.DifficultyID;
                walkDomain.RegionID = walkDto.RegionID;
                await nZWalksDbContext.SaveChangesAsync();
            }

            return walkDomain;
        }

        public async Task<Walk?> Delete(Guid id)
        {
            var walkDomain = await nZWalksDbContext.Walks.FindAsync(id);
            if (walkDomain != null)
            {
                nZWalksDbContext.Walks.Remove(walkDomain);
                await nZWalksDbContext.SaveChangesAsync();
            }

            return walkDomain;
        }
    }
}

