# Project Reflections

## David
### My experience with the project
After working through this project, I realized that doing research thoroughly is important. We used Asp.net for our project. It is a framework we had no experience with. There were many times we ran into problems we did not understand. If we would have researched more it would have been an easier process to overcome those problems.

### Tasks I have done
#### Unit testing
 I setup some of the test for the controllers. I had to create mock objects for those tests which is something I have never had to do before. The controller classes I was testing needed the repository interfaces we created, so I had to create a mock repository for the tests. The mock repository allowed me to test without the test affecting our database.
#### Creating the food controller
 I was tasked with creating the controller to handle displaying and editing the user's food items. I had to learn about how Asp.net uses the MVC design pattern. The controller works by getting the users food items from the database and storing it into a list. The list is then passed to the view. When you pass an object to a view, that view's model is based off the passed object. Then the view iterates through the list and shows the food.
#### Planning the project 
When planning on how to design and create the database I thought we agreed on the same thing. We continued to work through the project, getting our models to hold the data from the database. Then we found out how we were making our models work conflicted. I learned that the planning process should have been more in depth, so we would know that we were agreeing on the same thing.

# Ryan
### General
The idea of a dot net, c#, database project to learn some new technologies that we would probably run into in the industry.   I had done some tutorials and assumed most of what we're doing would be easy, I was wrong.  As soon as you deviate from the script It was easy to become completely lost with little clue of where to go or what to do next.   The foreignness of .net was the cause for most of our issues.
### Planning 
At the start we didn't really know what was needed to complete most of what we wanted to do.  With a general idea we tried to split tasks amongst ourselves but also Ideally wanted all of us to touch everything at least a bit, mostly so we could all come out of this with some new skills.  Planning for something you know little about it difficult.  This lead to us basically jumping in headfirst with entity framework, .net and database design. 
### Coding/Testing
 Dot net and Entity Framework are powerful tools to ease web development, but they do so much that it's hard for me to grasp all that they do behind the scene.  That abstraction added with the scope of this project (while still relatively small) is many times larger than anything else I have worked with turned every small road bump into a road block.  This was a pain and lead to many hours of googling to fix mostly simple fixes to our work.  Keeping track of all the ways data was passes between controllers and views was and still can be confusing. 
Even know our final project is a fraction of what I thought it could be or wanted it to be (due to actual time needed, learning several new technologies and having every team member essentially building the parachute on the way down I feel I have a much better understanding of the dot net framework and working in a group environment than before.


# Garrett
### Planning
The idea behind the project was to use ASP.net and C# to build the website so we get experience with .net, C# with putting a front end to a back end.  After having got to where we are now it definitely turned out to be more complicated than anticipated.  We should have had better planning and research before we started coding.  If we had spent a sprint or two on planning and research before even touching code I think it would have been smoother.  This is something to keep in mind for future projects regardless of the platform being used.

### Unit Testing
Prior to this project I had no experience in testing at all, I always just ran the program and debugged inside the command line or in the code till I got the desired output.  I have learned about automating testing of methods for unit testing through writing test cases to test different scenarios of the program to make it more effective and easier to test.  I wrote test cases for the user log-in and sign-up where I added user input regex to verify the input is an email address.  This basically made sure the input wasn't trying to add code to the database or other areas of the website.

### MVC
Going along with learning about MVC in C490, I'm learning how it works in web development with .Net/C#.  It seems to be a pretty common standard for development.  It makes keeping the separation between the data and the view easy and is very useful.  This is actually something I was asked about during a job interview and having had these classes it helped me be able to answer the technical question.  So I'm very happy it was something I ran into with this project since it will help me in the future.