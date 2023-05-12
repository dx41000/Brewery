using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Brewery.Api.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Models.Bar, Datalayer.Entities.Bar>();
            CreateMap<Models.Brewery, Datalayer.Entities.Brewery>();
            CreateMap<Models.Beer, Datalayer.Entities.Beer>();

            CreateMap<Datalayer.Entities.Bar, Models.Bar>();
            CreateMap<Datalayer.Entities.Brewery, Models.Brewery>();
            CreateMap<Datalayer.Entities.Beer, Models.Beer>();
        }
    }
}
