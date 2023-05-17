using Brewery.Models;
using Brewery.Repositories.Interfaces;
using Brewery.Services.Interfaces;

namespace Brewery.Services;

public class BeerService : IBeerService
{
    private readonly IBeerRepository _beerRepository;


    public BeerService(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task<Beer> GetById(long id)
    {
        return await _beerRepository.GetById(id);
    }

    public async Task<IList<Beer>> GetByFilter(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
    {
        return await _beerRepository.GetByFilter(gtAlcoholByVolume, ltAlcoholByVolume);
    }

    public async Task<Beer> Post(Beer beer)
    {
        return await _beerRepository.Post(beer);
    }

    public async Task<Beer> Put(long id, Beer beer)
    {
        return await _beerRepository.Put(id, beer);
    }
}