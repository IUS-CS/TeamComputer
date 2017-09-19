using Prototype.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


/*
 * Create the Database Context
 * The main class that coordinates Entity Framework functionality for a given data model is the database context class. 
 * You create this class by deriving from the System.Data.Entity.DbContext class. 
 * In your code you specify which entities are included in the data model. 
 * You can also customize certain Entity Framework behavior. In this project, the class is named FoodContextContext.
 */
namespace Prototype.DAL
{
    public class FoodContext : DbContext
    {
        /*
         * Specifying the connection string
         * The name of the connection string (which you'll add to the Web.config file later) is passed in to the constructor.
         * If you don't specify a connection string or the name of one explicitly, Entity Framework assumes that the 
         * connection string name is the same as the class name. The default connection string name in this example 
         * would then be SchoolContext, the same as what you're specifying explicitly.
         */
        public FoodContext() : base("FoodContext")
        {
        }


        /*
         * Specifying entity sets
         * This code creates a DbSet property for each entity set. In Entity Framework terminology, 
         * an entity set typically corresponds to a database table, and an entity corresponds to a row in the table.
         */
        public DbSet<User>Users { get; set; }
        public DbSet<Food>Foods { get; set; }
        public DbSet<Recipe>Recipies { get; set; }
        public DbSet<ingredient> Ingredients { get; set; }
        public DbSet<Pantry> Party{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}