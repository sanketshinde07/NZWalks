using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : Controller
    {
        private readonly NZWalksDbContext DbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            DbContext = dbContext;
        }

        //GET all Regions
        //https://localhost:7294/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            var regionsDomain = DbContext.Regions.ToList();
            var regionsDto = new List<RegionDto>();

            foreach (var regionData in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionData.Id,
                    Code = regionData.Code,
                    Name = regionData.Name,
                    RegionImageUrl = regionData.RegionImageUrl
                });
            }
            return Ok(regionsDto);
        }

        //GET all Regions
        //https://localhost:7294/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetByID([FromRoute] Guid id)
        {
            var regionsDomain = DbContext.Regions.Find(id);

            var regionsDto = new List<RegionDto>();

            if (regionsDomain == null)
            {
                return NotFound();
            }

            regionsDto.Add(new RegionDto()
            {
                Id = regionsDomain.Id,
                Code = regionsDomain.Code,
                Name = regionsDomain.Name,
                RegionImageUrl = regionsDomain.RegionImageUrl
            });


            return Ok(regionsDto);

        }

        //Create Region
        //https://localhost:7294/api/Regions/
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            DbContext.Regions.Add(regionDomainModel);
            DbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionByID), new { id = regionDto.Id }, regionDto);
        }


        //Update Region
        //https://localhost:7294/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            var regionDomainModel = DbContext.Regions.Find(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;


            DbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        //Delete Region
        //https://localhost:7294/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel = DbContext.Regions.FirstOrDefault(x=>x.Id==id);

            if(regionDomainModel==null)
            {
                return NotFound();
            }

            DbContext.Regions.Remove(regionDomainModel);

            DbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);

        }

    }

}


