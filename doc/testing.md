# How testing is performed
We have a unit test project that is setup with references to our main project. We are unit testing the methods in our Controller Classes. Since the controller classes use dependency injection for their repositorys, we have mock objects of those repositorys in our unit tests.

# How the test are setup
The test are setup in a "setup, execute, assert pattern." The names of the unit test methods describe what the test is doing. There are comments explaing the assert statments in each test.