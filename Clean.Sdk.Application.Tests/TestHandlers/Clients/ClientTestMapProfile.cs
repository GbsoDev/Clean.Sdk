using AutoMapper;
using Clean.Sdk.Application.Mapper;
using Clean.Sdk.Application.Tests.TestHandlers.Clients.Commands;
using Clean.Sdk.Domain.Tests.TestEntites.ClientsTest;

namespace Clean.Sdk.Application.Tests.TestHandlers.Clients
{
	[MapperProfile]
	public class ClientTestMapProfile : Profile
	{
		public ClientTestMapProfile()
		{
			CreateMap<RegisterClientTestCommand, ClientTest>()
				.ConstructUsing(clientDto => new ClientTest(clientDto.Name, clientDto.MiddleName, clientDto.Surname, clientDto.Age))
				.ForAllMembers(opts => opts.Ignore());

			CreateMap<UpdateClientTestCommand, ClientTest>()
				.ConstructUsing(clientDto => new ClientTest(clientDto.Id, clientDto.Name, clientDto.MiddleName, clientDto.Surname, clientDto.Age))
				.ForAllMembers(opts => opts.Ignore());

			CreateMap<ClientTest, ClientTestDto>()
				.ConstructUsing(client => new ClientTestDto(client.Id, client.Name, client.MiddleName, client.Surname, client.Age))
				.ForAllMembers(opts => opts.Ignore());
		}
	}
}
