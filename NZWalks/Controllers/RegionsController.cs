using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : Controller
    {
        private readonly NZWalksDbContext DbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext,IRegionRepository regionRepository, IMapper mapper)
        {
            DbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET all Regions
        //https://localhost:7294/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]string? filterBy,[FromQuery]string? filterOn,
                                                    [FromQuery]string? sortBy, [FromQuery]bool isAsscending,
                                                       [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {

            var regionsDomain =await regionRepository.GetAllAsync(filterBy,filterOn,sortBy, isAsscending,pageNumber,pageSize);
    
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionsDto);
        }

        //GET all Regions
        //https://localhost:7294/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByID([FromRoute] Guid id)
        {
            var regionsDomain =await regionRepository.GetById(id);

            if (regionsDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionsDomain));

        }

        //Create Region
        //https://localhost:7294/api/Regions/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            await regionRepository.Create(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetByID), new { id = regionDto.Id }, regionDto);
        }


        //Update Region
        //https://localhost:7294/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            var regionDomainModel =await regionRepository.Update(id,updateRegionRequestDto);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        //Delete Region
        //https://localhost:7294/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await DbContext.Regions.FirstOrDefaultAsync(x=>x.Id==id);

            if(regionDomainModel==null)
            {
                return NotFound();
            }

            DbContext.Regions.Remove(regionDomainModel);

           await DbContext.SaveChangesAsync();

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(mapper.Map<RegionDto>(regionDomainModel));

        }

    }

}


