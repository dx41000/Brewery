using Brewery.Api.Controllers;
using Brewery.Models;
using Brewery.Repositories.Interfaces;
using Brewery.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Brewery.Api.Tests;

public class Beers
{
    
    private List<Beer> _beers;
    private Beer _singleBeer;
    private BeerController _beerController;
    
    [SetUp]
    public void Setup()
    {
        _singleBeer = new Beer()
        {
            Name = "Bitter",
            PercentageAlcoholByVolume = 3.8m
        };
        
        _beers = new List<Beer>()
        {
            new()
            {
                Name = "IPA",
                PercentageAlcoholByVolume = 5.0m
            },
            new()
            {
                Name = "Golden Ale",
                PercentageAlcoholByVolume = 4.0m
            },
            new()
            {
                Name = "Stout",
                PercentageAlcoholByVolume = 4.5m
            }
        };
        
        var beerRepository = new Mock<IBeerRepository>();
        beerRepository.Setup(m => m.GetById(1)).ReturnsAsync(_beers.First);
        beerRepository.Setup(m => m.Put(1, _singleBeer)).ReturnsAsync(_singleBeer);
        beerRepository.Setup(m => m.Post(_singleBeer)).ReturnsAsync(_singleBeer);
        beerRepository.Setup(m => m.GetByFilter(1,2)).ReturnsAsync(_singleBeer);

        
        var beerService = new BeerService(beerRepository.Object);
        _beerController = new BeerController(beerService);
        
    }

    [Test]
    public async Task Get_Beer_By_Id()
    {
        var result = await _beerController.GetById(1);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("IPA", ((Brewery.Models.Beer)okResult.Value).Name);

    }
    
    [Test]
    public async Task Insert_A_Single_Beer()
    {
        var result = await _beerController.Post(_singleBeer);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Bitter", ((Brewery.Models.Beer)okResult.Value).Name);

    }
    
    [Test]
    public async Task Update_A_Beer_By_Id()
    {
        var result = await _beerController.Put(1, _singleBeer);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Bitter", ((Brewery.Models.Beer)okResult.Value).Name);
    }
    
    [Test]
    public async Task Get_All_Beers_With_Optional_Filtering()
    {
        var result = await _beerController.GetByFilter(1,2);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Bitter", ((Brewery.Models.Beer)okResult.Value).Name);
    }
}