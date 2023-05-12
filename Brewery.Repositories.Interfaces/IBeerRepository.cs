using Brewery.Models;

namespace Brewery.Repositories.Interfaces;

public interface IBeerRepository
{
    Task<Beer> GetById(long id);
    Task<Beer> GetByFilter(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume);
    Task<Beer> Put(long id, Beer beer);
    Task<Beer> Post(Beer beer);
}