using System.Data.Entity.Migrations;
using onsparkiy.api.DAL.Contexts;

namespace onsparkiy.api.DAL.Migrations
{
	/// <summary>
	/// SparkiyDbContext migrations configuration.
	/// </summary>
	public sealed class Configuration : DbMigrationsConfiguration<SparkiyDbContext>
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="Configuration"/> class.
		/// </summary>
		public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

		/// <summary>
		/// Seeds the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		protected override void Seed(SparkiyDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
