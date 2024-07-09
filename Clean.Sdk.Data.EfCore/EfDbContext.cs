using Clean.Sdk.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Clean.Sdk.Data.EfCore
{
	public abstract class EfDbContext<TContext> : DbContext, IEfDbContext
		where TContext : DbContext
	{
		public EfDbContext(DbContextOptions<TContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			ApplyConfigurations(modelBuilder);
			foreach (var entityType in modelBuilder.Model.GetEntityTypes()
				.Where(entityType => EntityHelper.IsIAuditableEntity(entityType.ClrType)))
			{
				modelBuilder.Entity(entityType.Name).Property<DateTime>(IEfDbContext.REGISTER_DATE_PROPERTY_NAME)
					.IsRequired()
					.HasDefaultValue(DateTime.UtcNow);

				modelBuilder.Entity(entityType.Name).Property<DateTime>(IEfDbContext.LAST_UPDATE_PROPERTY_NAME);
			}
		}

		protected abstract void ApplyConfigurations(ModelBuilder modelBuilder);
	}
}
