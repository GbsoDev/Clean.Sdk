using AutoMapper;
using Clean.Sdk.Domain.Tests.TestModel.ClientsTest;

namespace Clean.Sdk.Data.EfCore.Tests;

public class ClientTestEntityMappingProfile : Profile
{
	public ClientTestEntityMappingProfile()
	{
		CreateMap<ClientTest, ClientTestEntity>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			.ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
			.ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
			.ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age));
	}
}