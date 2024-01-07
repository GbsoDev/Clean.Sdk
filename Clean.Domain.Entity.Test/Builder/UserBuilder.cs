using Clean.Domain.Entity.Test.TestModel;

namespace Clean.Domain.Tests.Builders
{
	public class UserBuilder
	{
		private Guid _id = Guid.NewGuid();
		private string _name = "Default Name";
		private string _email = "default@example.com";

		public UserBuilder WithId(Guid id)
		{
			_id = id;
			return this;
		}

		public UserBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public UserBuilder WithEmail(string email)
		{
			_email = email;
			return this;
		}

		public User Build(bool isToUpdate)
		{
			if (isToUpdate)
			{
				return new User(_id, _name, _email);
			}
			else
			{
				return new User(_name, _email);
			}
		}
	}
}
