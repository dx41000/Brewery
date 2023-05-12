using Brewery.Api.Controllers;
using Brewery.Models;
using Brewery.Repositories.Interfaces;
using Brewery.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Brewery.Api.Tests;

public class Breweries
{
    private BreweryController _breweryController;
    private List<Beer> _beers;
    private List<Brewery.Models.Brewery> _breweries;
    private List<Brewery.Models.Brewery> _breweriesWithoutBeers;
    private Brewery.Models.Brewery _singleBrewery;
    private Brewery.Models.Brewery _singleBrewerySingleBeer;

    

    [SetUp]
    public void Setup()
    {
        
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
        
        _breweries = new List<Brewery.Models.Brewery>()
        {
            new()
            {
                Name = "Youngs",
                Beers = _beers
            },
            new()
            {
                Name = "Lakeland",
                Beers = _beers
            },
            new()
            {
                Name = "Coach House",
                Beers = _beers
            }
        };
        
        _breweriesWithoutBeers = new List<Brewery.Models.Brewery>()
        {
            new()
            {
                Name = "Youngs"
            },
            new()
            {
                Name = "Lakeland"
            },
            new()
            {
                Name = "Coach House"
            }
        };
        
        _singleBrewery = new Brewery.Models.Brewery
        {
            Name = "Tophat",
            Beers = _beers
        };
        
        _singleBrewerySingleBeer = new Brewery.Models.Brewery
        {
            Name = "Tophat",
            Beers = new List<Beer>(){_beers.First()}
        };
        
        var breweryRepository = new Mock<IBreweryRepository>();
        breweryRepository.Setup(m => m.GetAll()).ReturnsAsync(_breweries);
        breweryRepository.Setup(m => m.GetById(1)).ReturnsAsync(_breweriesWithoutBeers.First);
        breweryRepository.Setup(m => m.GetBeersById(1)).ReturnsAsync(_breweries.First);
        breweryRepository.Setup(m => m.GetAllWithBeers()).ReturnsAsync(_breweries);
        breweryRepository.Setup(m => m.Put(1, _singleBrewery)).ReturnsAsync(_singleBrewery);
        breweryRepository.Setup(m => m.Post(_singleBrewery)).ReturnsAsync(_singleBrewery);
        breweryRepository.Setup(m => m.BeerLink(_singleBrewerySingleBeer)).ReturnsAsync(_singleBrewerySingleBeer);

        var breweryService = new BreweryService(breweryRepository.Object);
        _breweryController = new BreweryController(breweryService);
    }

    [Test]
    public async Task Insert_A_Single_Brewery()
    {
        var result = await _breweryController.Post(_singleBrewery);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Tophat", ((Brewery.Models.Brewery)okResult.Value).Name);
        Assert.AreEqual(3, ((Brewery.Models.Brewery)okResult.Value).Beers.Count);
    }
    
    [Test]
    public async Task Update_A_Brewery_By_Id()
    {
        var result = await _breweryController.Put(1, _singleBrewery);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Tophat", ((Brewery.Models.Brewery)okResult.Value).Name);
        Assert.AreEqual(3, ((Brewery.Models.Brewery)okResult.Value).Beers.Count);
    }
    
    [Test]
    public async Task Get_All_Breweries()
    {
        var result = await _breweryController.GetAll();
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(3, ((IList<Brewery.Models.Brewery>)okResult.Value).Count);
    }
    
    [Test]
    public async Task Get_Brewery_By_Id()
    {
        var result = await _breweryController.GetById(1);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Youngs", ((Brewery.Models.Brewery)okResult.Value).Name);
        Assert.AreEqual(null, ((Brewery.Models.Brewery)okResult.Value).Beers);
    }
    
    [Test]
    public async Task Insert_A_Single_Brewery_Beer_Link()
    {
        var result = await _breweryController.BeerLink(_singleBrewerySingleBeer);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Tophat", ((Brewery.Models.Brewery)okResult.Value).Name);
        Assert.AreEqual(1, ((Brewery.Models.Brewery)okResult.Value).Beers.Count);

    }
    
    [Test]
    public async Task Get_A_Single_Brewery_By_Id_With_Beers()
    {
        var result = await _breweryController.GetBeersById(1);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Youngs", ((Brewery.Models.Brewery)okResult.Value).Name);
        Assert.AreEqual(3, ((Brewery.Models.Brewery)okResult.Value).Beers.Count);
    }
    
    [Test]
    public async Task Get_All_Breweries_With_Beers()
    {
        var result = await _breweryController.GetAllWithBeers();
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(3, ((IList<Brewery.Models.Brewery>)okResult.Value).Count);
        Assert.AreEqual(3, ((IList<Brewery.Models.Brewery>)okResult.Value).First().Beers.Count);
    }
}