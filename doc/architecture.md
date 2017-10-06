#  Food Project!!
## Archietcture v.01
arthor(s) Team Computer (TC)
(work in progress)



1. The Model View Controller (MVC) Pattern
The MVC pattern is standard for DOT Net web applications.

	1. MODELS	
		Models represent the underlying data/objects that will be used and views by the end-user.  
		To acheive our goal we needed to create 5 models, food, user, pantry, recipe and ingredient.
		The models created are the tables in the Food Project Database. A code first approach was used in creating the tables 			based on the model classes designed. 
		Each model(table) will have a controller, controllers are classes tied to each model and act as the middleman between 			the end-user and database.  

	1. VIEWS - HTML output for user to view/interact with
		*Index - a list of desiered model
		*Detail - more detailed information of the model such as list of food that the current User "owns"
		*Delete/Create - for adding new food or deleting a user from database.
	1. CONTROLLERS
		*Each controller will generate a view, handle modifying database tables for each model. 
		*Controllers will handle FUTURE methods to check to see if user has desired foods for recipes or check what food user needs to cook desired recipe.

1. Database Schema
		use md link thing here.

	1.	
		1.There are five tables in the FoodProject Database.
			* Food
			* Name
			* Units - units will hold volume/weight info for each food.
	* User
		* Name
		* (Log-in info here soon)
		* virtual collection<pantry>
	* Pantry - bridge table for user and Food
		* User ID
		* Food ID
	* Recipe
		* Name
		* other info we may want
		* virtual collection<food>
	* Ingredients - Bridge Table for food and recipe	
		*Food ID
		*Recipe ID
	* Navagational attributes are added as Virtual ICollections.  These virtual properties inform Entity Framework 
#### Probably need more....
* Entity Framework - interfaces/context class
* Repository Pattern
	* probably read more on this, then update soon
	* Controller --> Repository --> IRepository --> Database
* More on EF?
	* EF --> context.cs --> database
	* db context?? linq??


![Image of basic mvc from web](images/basicMVC.png)
![mvc with food project content](images/mvcFood.png)
![db schema](images/schema.png)
