using Clean.Sdk.Data.EfCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Sdk.Data.EfCore
{
	/// <summary>
	/// Base class for the Entity Framework database context.
	/// </summary>
	/// <typeparam name="TContext">The type of the database context.</typeparam>
	public abstract class EfDbContext<TContext> : DbContext, IEfDbContext
		where TContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EfDbContext{TContext}"/> class.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
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
				modelBuilder.Entity(entityType.Name).Property<DateTime>(IEfDbContext.SAVE_DATE_PROPERTY_NAME)
					.IsRequired();

				modelBuilder.Entity(entityType.Name).Property<DateTime>(IEfDbContext.LAST_UPDATE_PROPERTY_NAME);
			}
		}

		/// <summary>
		/// Applies the configurations for the model.
		/// </summary>
		/// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
		protected abstract void ApplyConfigurations(ModelBuilder modelBuilder);
	}
}
