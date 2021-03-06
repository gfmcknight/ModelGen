# ModelGen
This project is meant to a binding generator which converts C#
classes to TypeScript classes in order to make maintaining
back/front models consistent.

## Roadmap
- [X] Conversions for accessors when they have the public keyword
- [X] Constructors that take a JSON object that would come from a .NET Core app
- [X] Sensitivity to JsonProperty and JsonIgnore
- [X] Take a project and transpile all classes with a given annotation
- [X] Directories that mirror namespaces
- [X] A toJson() function on models to serialize to the server
- [X] Support for C# types: double, int, string, bool, Guid, DateTime
- [X] Models that hold other models
- [X] Inheritance in models
- [X] Transpilation of methods when exact TS code is given
- [X] RPC procedure for methods
- [X] Some sort of library for RPC binding on the server side
- [X] RPC support for async functions
- [X] RPC middleware supports dependency injection of services
- [X] Support for lists
- [X] Support for dictionaries
- [X] Partial support for JsonSubTypes(https://github.com/manuc66/JsonSubTypes)
