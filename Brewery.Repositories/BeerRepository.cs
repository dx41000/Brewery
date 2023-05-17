using AutoMapper;
using Brewery.Datalayer.Context;
using Brewery.Models;
using Brewery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Repositories;

public class BeerRepository : IBeerRepository
{
    
    private readonly BreweryContext _breweryContext;
    private readonly IMapper _mapper;

    public BeerRepository(BreweryContext breweryContext, IMapper mapper)
    {
        _breweryContext = breweryContext;
        _mapper = mapper;
    }

    public async Task<Beer> GetById(long id)
    {
        return _mapper.Map<Beer>(await _breweryContext.Beers.Where(x => x.Id == id).SingleOrDefaultAsync());
    }

    public async Task<IList<Beer>> GetByFilter(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
    {
        return _mapper.Map<IList<Beer>>(await _breweryContext.Beers
            .Where(x => (gtAlcoholByVolume == null || x.PercentageAlcoholByVolume >= gtAlcoholByVolume) 
                        && (ltAlcoholByVolume == null || x.PercentageAlcoholByVolume <= ltAlcoholByVolume) ).ToListAsync());
    }

    public async Task<Beer> Put(long id, Beer beer)
    {
        var entity = await _breweryContext.Beers.Where(x => x.Id == id).SingleAsync();
        entity.Name = beer.Name;
        entity.PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume;
        _breweryContext.Update(entity);
        var response = await _breweryContext.SaveChangesAsync();
        return _mapper.Map<Beer>(response);
    }

    public async Task<Beer> Post(Beer beer)
    {
        var entity = _mapper.Map<Datalayer.Entities.Beer>(beer);
        await _breweryContext.AddAsync(entity);
        var response =await _breweryContext.SaveChangesAsync();
        return _mapper.Map<Beer>(response);
    }
}