using Brewery.Models;
using Brewery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace Brewery.Api.Controllers;

[ApiController]
[Route("bar")]
public class BarController : ControllerBase
{

    private readonly IBarService _barService;

    public BarController(IBarService barService)
    {
        _barService = barService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response = await _barService.GetAll();
            
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
            var response = await  _barService.GetById(id);
            
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
            var response =  await _barService.GetAllWithBeers();
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpGet, Route("{barId}/beer")]
    public async Task<IActionResult> GetBeersById([FromRoute] long barId)
    {
        try
        {
            var response = await  _barService.GetBeersById(barId);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpPut, Route("{id}")]
    public async Task<IActionResult> Put([FromRoute] long id, [FromBody] Bar bar)
    {
        try
        {
            
            if (string.IsNullOrEmpty(bar.Name))
            {
                return BadRequest("Name is required");
            }
            
            var response = await  _barService.Put(id, bar);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Bar bar)
    {
        try
        {
            
            if (string.IsNullOrEmpty(bar.Name))
            {
                return BadRequest("Name is required");
            }
            
            var response =  await _barService.Post(bar);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }
    
    [HttpPost, Route("beer")]
    public async Task<IActionResult> BeerLink([FromBody] Bar bar)
    {
        try
        {
            
            if (bar.Beers.Count is < 1 or > 1)
            {
                return BadRequest("Please provide one beer to link");
            }

            var response =  await _barService.BeerLink(bar);
            
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500,ex.Message);
        }
    }

    
}