using AutoMapper;
using ProjectOverdrive.API.Data.ValueObjects.Request;
using ProjectOverdrive.API.Data.ValueObjects.Response;
using ProjectOverdrive.API.Models;

namespace ProjectOverdrive.API.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config => 
            {
                config.CreateMap<CompanyRequest, Company>().ReverseMap();
                config.CreateMap<CompanyResponse, Company>().ReverseMap();
                config.CreateMap<CompanyUpdateRequest, Company>().ReverseMap();
                config.CreateMap<SearchCompanyResponse, Company>().ReverseMap();
                config.CreateMap<SearchCompanyInPeopleResponse, Company>().ReverseMap();
                config.CreateMap<CompanyOffAddressResponse, Company>().ReverseMap();
                config.CreateMap<CompanyOffAddressAndPeopleResponse, Company>().ReverseMap();


                config.CreateMap<PeopleRequest, People>().ReverseMap();
                config.CreateMap<PeopleResponse, People>().ReverseMap();
                config.CreateMap<PeopleUpdateRequest, People>().ReverseMap();

                config.CreateMap<AddressResponse, Address>().ReverseMap();
            });
            return mappingconfig;
        }
    }
}
