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
    }
}