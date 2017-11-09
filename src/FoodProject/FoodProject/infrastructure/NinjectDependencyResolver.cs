using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using FoodProject.Abstract;
using FoodProject.Concrete;
namespace FoodProject.infrastructure
{
        public class NinjectDependencyResolver : IDependencyResolver
        {
            //attributes
            private IKernel kernel;

            //constructor
            public NinjectDependencyResolver(IKernel kernelParam)
            {
                kernel = kernelParam;
                AddBindings();
            }

            //methods
            public object GetService(Type serviceType)
            {
                return kernel.TryGet(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return kernel.GetAll(serviceType);
            }

            private void AddBindings()
            {
                //binds the repositorys to these concrete classes
                kernel.Bind<IUserRepository>().To<UserRepository>();
                kernel.Bind<IFoodRepository>().To<FoodRepository>();
                kernel.Bind<IPantryRepository>().To<PantryRepository>();
                kernel.Bind<IRecipeRepository>().To<RecipeRepository>();
                kernel.Bind<IIngredientRepository>().To<IngredientRepository>();
            }

        }
}