using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController:ControllerBase
	{
        private readonly IMapper mapper;
        private readonly IWalkRepository walkResposiotry;

        public WalksController(IMapper mapper, IWalkRepository walkResposiotry)
		{
            this.mapper = mapper;
            this.walkResposiotry = walkResposiotry;
        }

        //Create Walk
		[HttpPost]
        //https://localhost:7294/api/Walks/
        public async Task<IActionResult> Create([FromBody]AddWalkRequestDto addWalkResquestDTO) 
		{
			var walkDomain = mapper.Map<Walk>(addWalkResquestDTO);
            await walkResposiotry.Create(walkDomain);

            return Ok(mapper.Map<WalkDto>(walkDomain));

        }

        //Get all walks
        [HttpGet]
        //https://localhost:7294/api/Walks/
        public async Task<IActionResult> GetAllWalks()
        {
           var walkDomain = await walkResposiotry.GetAllAsync();
            return Ok(mapper.Map<List<WalkDto>>(walkDomain));
        }

        //Get walk by ID
        [HttpGet]
        [Route("{id:Guid}")]
        //https://localhost:7294/api/Walks/{id}
        public async Task<IActionResult> GetWalkByID([FromRoute]Guid id)
        {
            var walkDomain = await walkResposiotry.GetById(id);
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        //Delete walk by id
        [HttpDelete]
        [Route("{id:Guid}")]
        //https://localhost:7294/api/Walks/{id}
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomain = await walkResposiotry.Delete(id);
            if (walkDomain != null)
            {
                return Ok(mapper.Map<WalkDto>(walkDomain));
            }
            else
            {
                return NotFound();
            }
        }

        //Update walk
        [HttpPut]
        [Route("{id:Guid}")]
        //https://localhost:7294/api/Walks/{id}
        public async Task<IActionResult> UpdateWalk([FromBody] UpdateWalkRequestDto updateWalkRequestDto,[FromRoute]Guid id)
        {
            var walkDomain = await walkResposiotry.Update(id,updateWalkRequestDto);
            if (walkDomain != null)
            {
                return Ok(mapper.Map<WalkDto>(walkDomain));
            }
            else
            {
                return NotFound();
            }
        }
	}
}

