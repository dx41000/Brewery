using AutoMapper;
using Brewery.Datalayer.Context;
using Brewery.Models;
using Brewery.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Beer = Brewery.Datalayer.Entities.Beer;

namespace Brewery.Repositories;

public class BarRepository : IBarRepository
{
    private readonly BreweryContext _breweryContext;
    private readonly IMapper _mapper;

    public BarRepository(BreweryContext breweryContext, IMapper mapper)
    {
        _breweryContext = breweryContext;
        _mapper = mapper;
    }

    public async Task<IList<Bar>> GetAll()
    {
        return _mapper.Map<IList<Bar>>(await _breweryContext.Bars.ToListAsync());
    }

    public async Task<Bar> GetById(long id)
    {
        return _mapper.Map<Bar>(await _breweryContext.Bars.Where(x => x.Id == id).SingleOrDefaultAsync());
    }

    public async Task<IList<Bar>> GetAllWithBeers()
    {
        return _mapper.Map<IList<Bar>>(await _breweryContext.Bars.Include(x=>x.Beers).ToListAsync());
    }

    public async Task<Bar> GetBeersById(long barId)
    {
        return _mapper.Map<Bar>(await _breweryContext.Bars.Where(x => x.Id == barId).Include(x=>x.Beers).SingleOrDefaultAsync());
    }

    public async Task<Bar> BeerLink(Bar bar)
    {
        var entity = await _breweryContext.Bars.SingleAsync(x => x.Name == bar.Name);

        if (entity.Beers == null)
        {
            entity.Beers = new List<Beer>();
        }

        entity.Beers.Add(_mapper.Map<Datalayer.Entities.Beer>(bar.Beers.First()));
        _breweryContext.Update(entity);
        await _breweryContext.SaveChangesAsync();
        return _mapper.Map<Bar>(entity);
    }

    public async Task<Bar> Post(Bar bar)
    {
        var entity = _mapper.Map<Datalayer.Entities.Bar>(bar);
        await _breweryContext.AddAsync(entity);
        await _breweryContext.SaveChangesAsync();
        return _mapper.Map<Bar>(entity);
    }

    public async Task<Bar> Put(long id, Bar bar)
    {
        var entity = await _breweryContext.Bars.Where(x => x.Id == id).SingleAsync();
        entity.Name = bar.Name;
        entity.Beers = _mapper.Map<IList<Beer>>(bar.Beers);
        _breweryContext.Update(entity);
        await _breweryContext.SaveChangesAsync();
        return _mapper.Map<Bar>(entity);
    }
}