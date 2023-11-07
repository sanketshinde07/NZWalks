using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data
{
	public class NZWalksAuthDbContext:IdentityDbContext
	{
		public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options):base(options)
		{
		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readRoleId = "d8806c9f-98cd-4a7e-8b3a-bd1b4b1ddeea";
            var writeRoleId = "af33c44d-9c9e-4882-a7e7-90b62dbbaec4";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readRoleId,
                    ConcurrencyStamp=writeRoleId,
                    Name="Writer",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                 {
                    Id=writeRoleId,
                    ConcurrencyStamp=readRoleId,
                    Name="Reader",
                    NormalizedName="Writer".ToUpper()
                }
            };
        }
    }
}

