namespace Freelance.Provider.Migrations
{
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Freelance.Provider.Context.FreelanceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Freelance.Provider.Context.FreelanceDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            //create roles
            var clientRole = new IdentityRole { Name = "client" };
            var freelancerRole = new IdentityRole { Name = "freelancer" };
            var adminRole = new IdentityRole { Name = "admin" };
            //add to database
            roleManager.Create(clientRole);
            roleManager.Create(freelancerRole);
            roleManager.Create(adminRole);
        }
    }
}
