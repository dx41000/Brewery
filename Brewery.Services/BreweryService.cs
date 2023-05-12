using Brewery.Models;
using Brewery.Repositories.Interfaces;
using Brewery.Services.Interfaces;

namespace Brewery.Services;

public class BreweryService : IBreweryService
{

    private readonly IBreweryRepository _breweryRepository;


    public BreweryService(IBreweryRepository breweryRepository)
    {
        _breweryRepository = breweryRepository;
    }

    public async Task<IList<Models.Brewery>> GetAll()
    {
        return await _breweryRepository.GetAll();
    }

    public async  Task<Models.Brewery> GetById(long id)
    {
        return await _breweryRepository.GetById(id);
    }

    public async  Task<Models.Brewery> GetBeersById(long breweryId)
    {
        return await _breweryRepository.GetBeersById(breweryId);
    }

    public async  Task<IList<Models.Brewery>> GetAllWithBeers()
    {
        return await _breweryRepository.GetAllWithBeers();
    }

    public async  Task<Models.Brewery> Put(long id, Models.Brewery brewery)
    {
        return await _breweryRepository.Put(id, brewery);
    }

    public async  Task<Models.Brewery> Post(Models.Brewery brewery)
    {
        return await _breweryRepository.Post(brewery);
    }

    public async  Task<Models.Brewery> BeerLink(Models.Brewery brewery)
    {
        return await _breweryRepository.BeerLink(brewery);
    }
}