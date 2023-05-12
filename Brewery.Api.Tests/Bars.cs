using Brewery.Api.Controllers;
using Brewery.Models;
using Brewery.Repositories.Interfaces;
using Brewery.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Brewery.Api.Tests;

public class Bars
{
    private BarController _barController;
    private List<Beer> _beers;
    private List<Brewery.Models.Bar> _bars;
    private List<Brewery.Models.Bar> _barsWithoutBeers;
    private Brewery.Models.Bar _singleBar;
    private Brewery.Models.Bar _singleBarSingleBeer;

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
        
        _bars = new List<Brewery.Models.Bar>()
        {
            new()
            {
                Name = "Toms Bar",
                Beers = _beers
            },
            new()
            {
                Name = "Daves Bar",
                Beers = _beers
            },
            new()
            {
                Name = "Sams Bar",
                Beers = _beers
            }
        };
        
        _barsWithoutBeers = new List<Brewery.Models.Bar>()
        {
            new()
            {
                Name = "Toms Bar"
            },
            new()
            {
                Name = "Daves Bar"
            },
            new()
            {
                Name = "Sams Bar"
            }
        };
        
        _singleBar = new Brewery.Models.Bar()
        {
            Name = "Tims Bar",
            Beers = _beers
        };
        
        _singleBarSingleBeer = new Brewery.Models.Bar()
        {
            Name = "Tims Bar",
            Beers =  new List<Beer>(){_beers.First()}
        };
        
        var barRepository = new Mock<IBarRepository>();
        barRepository.Setup(m => m.GetAll()).ReturnsAsync(_bars);
        barRepository.Setup(m => m.GetById(1)).ReturnsAsync(_barsWithoutBeers.First);
        barRepository.Setup(m => m.GetBeersById(1)).ReturnsAsync(_bars.First);
        barRepository.Setup(m => m.GetAllWithBeers()).ReturnsAsync(_bars);
        barRepository.Setup(m => m.Put(1, _singleBar)).ReturnsAsync(_singleBar);
        barRepository.Setup(m => m.Post(_singleBar)).ReturnsAsync(_singleBar);
        barRepository.Setup(m => m.BeerLink(_singleBarSingleBeer)).ReturnsAsync(_singleBarSingleBeer);

        var barService = new BarService(barRepository.Object);
        _barController = new BarController(barService);
    }

    [Test]
    public async Task Insert_A_Single_Bar()
    {
        var result = await _barController.Post(_singleBar);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Tims Bar", ((Brewery.Models.Bar)okResult.Value).Name);
        Assert.AreEqual(3, ((Brewery.Models.Bar)okResult.Value).Beers.Count);

    }
    
    [Test]
    public async Task Update_A_Bar_By_Id()
    {
        var result = await _barController.Put(1, _singleBar);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Tims Bar", ((Brewery.Models.Bar)okResult.Value).Name);
        Assert.AreEqual(3, ((Brewery.Models.Bar)okResult.Value).Beers.Count);

    }
    
    [Test]
    public async Task Get_All_Bars()
    {
        var result = await _barController.GetAll();
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(3, ((IList<Brewery.Models.Bar>)okResult.Value).Count);

    }
    
    [Test]
    public async Task Get_Bar_By_Id()
    {
        var result = await _barController.GetById(1);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Toms Bar", ((Brewery.Models.Bar)okResult.Value).Name);
        Assert.AreEqual(null, ((Brewery.Models.Bar)okResult.Value).Beers);

    }
    
    [Test]
    public async Task Insert_A_Single_Bar_Beer_Link()
    {
        var result = await _barController.BeerLink(_singleBarSingleBeer);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Tims Bar", ((Brewery.Models.Bar)okResult.Value).Name);
        Assert.AreEqual(1, ((Brewery.Models.Bar)okResult.Value).Beers.Count);

    }
    
    [Test]
    public async Task Get_A_Single_Bar_With_Beers()
    {
        var result = await _barController.GetBeersById(1);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual("Toms Bar", ((Brewery.Models.Bar)okResult.Value).Name);
        Assert.AreEqual(3, ((Brewery.Models.Bar)okResult.Value).Beers.Count);

    }
    
    [Test]
    public async Task Get_All_Bars_With_Beers()
    {
        var result = await _barController.GetAllWithBeers();
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(3, ((IList<Brewery.Models.Bar>)okResult.Value).Count);
        Assert.AreEqual(3, ((IList<Brewery.Models.Bar>)okResult.Value).First().Beers.Count);

    }
}