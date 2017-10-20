# Design patterns we currently use
* Abstract factory

	We currently use an abstract factory for constructor dependency injection. Only the interface is revealed to the constructor not the concrete implmentation of the interface.
* Proxy 

	We currently use a proxy to defer the cost of creating and initializing the list of users from the database until we need to read from the database. It is a feature part of entity framework called 'lazy loading'

# Desing patterns that may fit well in our design
* Command 

	Its intent is to encapsulate a request as an object, thereby letting you parameterize clients withdifferent requests, queue or log requests, and supportundoable operations. It may be useful for user interface objects like buttons that don't know anything about what should be done.
* Interpreter
	
	Its intent is to define a represention for its grammar along with an interpreter that uses the representation to interpret sentences in the language. This might be useful if we want to build an interpreter to interpret expressions the user enters to sort their list of foods a certian way.
# Designing modules going forward
	We intend to design our modules with Object Oriented Design patterns in mind going forward.