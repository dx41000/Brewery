namespace Brewery.Repositories.Interfaces;

public interface IBreweryRepository
{
    Task<IList<Models.Brewery>> GetAll();
    Task<Models.Brewery> GetById(long id);

    Task<Models.Brewery> GetBeersById(long breweryId);
    Task<IList<Models.Brewery>> GetAllWithBeers();
    Task<Models.Brewery> Put(long id, Models.Brewery brewery);
    Task<Models.Brewery> Post(Models.Brewery brewery);
    Task<Models.Brewery> BeerLink(Models.Brewery brewery);
}