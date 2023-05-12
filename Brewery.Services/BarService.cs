using Brewery.Models;
using Brewery.Repositories.Interfaces;
using Brewery.Services.Interfaces;

namespace Brewery.Services;

public class BarService : IBarService
{
    private readonly IBarRepository _barRepository;


    public BarService(IBarRepository barRepository)
    {
        _barRepository = barRepository;
    }

    public async Task<IList<Bar>> GetAll()
    {
        return await _barRepository.GetAll();
    }

    public async Task<Bar> GetById(long id)
    {
        return await _barRepository.GetById(id);
    }

    public async Task<IList<Bar>> GetAllWithBeers()
    {
        return await _barRepository.GetAllWithBeers();
    }

    public async Task<Bar> GetBeersById(long barId)
    {
        return await _barRepository.GetBeersById(barId);
    }

    public async Task<Bar> Put(long id, Bar bar)
    {
        return await _barRepository.Put(id, bar);
    }

    public async Task<Bar> Post(Bar bar)
    {
        return await _barRepository.Post(bar);
    }

    public async Task<Bar> BeerLink(Bar bar)
    {
        return await _barRepository.BeerLink(bar);
    }
}