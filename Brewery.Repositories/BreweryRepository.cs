using AutoMapper;
using Brewery.Datalayer.Context;
using Brewery.Datalayer.Entities;
using Brewery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Repositories;

public class BreweryRepository : IBreweryRepository
{
    
    private readonly BreweryContext _breweryContext;
    private readonly IMapper _mapper;

    public BreweryRepository(BreweryContext breweryContext, IMapper mapper)
    {
        _breweryContext = breweryContext;
        _mapper = mapper;
    }

    public async Task<IList<Models.Brewery>> GetAll()
    {
        return _mapper.Map<IList<Models.Brewery>>(await _breweryContext.Breweries.ToListAsync());
    }

    public async Task<Models.Brewery> GetById(long id)
    {
        return _mapper.Map<Models.Brewery>(await _breweryContext.Breweries.Where(x => x.Id == id).SingleOrDefaultAsync());
    }

    public async Task<Models.Brewery> GetBeersById(long breweryId)
    {
        return _mapper.Map<Models.Brewery>(await _breweryContext.Breweries.Where(x => x.Id == breweryId).Include(x=>x.Beers).SingleOrDefaultAsync());
    }

    public async Task<IList<Models.Brewery>> GetAllWithBeers()
    {
        return _mapper.Map<IList<Models.Brewery>>(await _breweryContext.Breweries.Include(x=>x.Beers).ToListAsync());
    }

    public async Task<Models.Brewery> Put(long id, Models.Brewery brewery)
    {
        var entity = await _breweryContext.Breweries.Where(x => x.Id == id).SingleAsync();
        entity.Name = brewery.Name;
        entity.Beers = _mapper.Map<IList<Beer>>(brewery.Beers);
        _breweryContext.Update(entity);
        await _breweryContext.SaveChangesAsync();
        return _mapper.Map<Models.Brewery>(entity);
    }

    public async Task<Models.Brewery> Post(Models.Brewery brewery)
    {
        var entity = _mapper.Map<Datalayer.Entities.Brewery>(brewery);
        await _breweryContext.AddAsync(entity);
        await _breweryContext.SaveChangesAsync();
        return _mapper.Map<Models.Brewery>(entity);
    }

    public async Task<Models.Brewery> BeerLink(Models.Brewery brewery)
    {
        var entity = await _breweryContext.Breweries.Where(x => x.Name == brewery.Name).SingleAsync();

        if (entity.Beers == null)
        {
            entity.Beers = new List<Beer>();
        }
        
  
        entity.Beers.Add(_mapper.Map<Datalayer.Entities.Beer>(brewery.Beers.First()));
        _breweryContext.Update(entity);
        await _breweryContext.SaveChangesAsync();
        return _mapper.Map<Models.Brewery>(entity);
    }
}