using Brewery.Models;

namespace Brewery.Repositories.Interfaces;

public interface IBarRepository
{
    Task<IList<Bar>> GetAll();
    Task<Bar> GetById(long id);
    Task<IList<Bar>> GetAllWithBeers();
    Task<Bar> GetBeersById(long barId);
    Task<Bar> Put(long id, Bar bar);
    Task<Bar> Post(Bar bar);
    Task<Bar> BeerLink(Bar bar);
}