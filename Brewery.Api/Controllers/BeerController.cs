using Brewery.Models;
using Brewery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

[ApiController]
[Route("beer")]
public class BeerController : ControllerBase
{
    private readonly IBeerService _beerService;

    public BeerController(IBeerService beerService)
    {
        _beerService = beerService;
    }

    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        try
        {

            var response = await  _beerService.GetById(id);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetByFilter([FromQuery] decimal? gtAlcoholByVolume,[FromQuery] decimal? ltAlcoholByVolume)
    {
        try
        {

            if (gtAlcoholByVolume == null && ltAlcoholByVolume == null)
            {
                return BadRequest("You must provide either gtAlcoholByVolume or ltAlcoholByVolume");
            }

            var response =  await _beerService.GetByFilter(gtAlcoholByVolume, ltAlcoholByVolume);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpPut, Route("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] Beer beer)
    {
        try
        {

            if (string.IsNullOrEmpty(beer.Name))
            {
                return BadRequest("Name is required");
            }
   
            var response =  await _beerService.Put(id, beer);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Beer beer)
    {
        try
        {
            
            if (string.IsNullOrEmpty(beer.Name))
            {
                return BadRequest("Name is required");
            }
            
            var response =  await _beerService.Post(beer);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }

    
}