using Brewery.Models;
using Brewery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

[ApiController]
[Route("brewery")]

public class BreweryController : ControllerBase
{

    private readonly IBreweryService _breweryService;

    public BreweryController(IBreweryService breweryService)
    {
        _breweryService = breweryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response = await  _breweryService.GetAll();
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] long id)
    {
        try
        {
            var response =  await _breweryService.GetById(id);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }

    [HttpGet, Route("beer")]
    public async Task<IActionResult> GetAllWithBeers()
    {
        try
        {
            var response = await  _breweryService.GetAllWithBeers();
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpGet, Route("{breweryId}/beer")]
    public async Task<IActionResult> GetBeersById([FromRoute] long breweryId)
    {
        try
        {
            var response = await  _breweryService.GetBeersById(breweryId);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpPut, Route("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] Models.Brewery brewery)
    {
        try
        {
            if (string.IsNullOrEmpty(brewery.Name))
            {
                return BadRequest("Name is required");
            }
            
            var response =  await _breweryService.Put(id, brewery);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Brewery brewery)
    {
        try
        {
            
            
            if (string.IsNullOrEmpty(brewery.Name))
            {
                return BadRequest("Name is required");
            }
            
            var response = await  _breweryService.Post(brewery);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpPost, Route("beer")]
    public async Task<IActionResult> BeerLink([FromBody] Models.Brewery brewery)
    {
        try
        {
            
            if (brewery.Beers.Count is < 1 or > 1)
            {
                return BadRequest("Please provide one beer to link");
            }
            
            var response = await  _breweryService.BeerLink(brewery);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }


}