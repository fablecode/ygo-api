![alt text](https://fablecode.visualstudio.com/_apis/public/build/definitions/22ebd0cf-e8a2-4659-997b-95d960acfe61/3/badge?maxAge=0 "Visual studio team services build status") 

# Ygo-api
A C# .NET Core 2.2 api for [Yu-Gi-Oh](http://www.yugioh-card.com/uk/) data such as Card Images, descriptions, Tips and Trivia.

## Why?
To provide access to the latest [Yu-Gi-Oh](http://www.yugioh-card.com/uk/)  banlist & card errata in a simple JSON format.

## Prerequisite
1. Setup the [Ygo database](https://github.com/fablecode/ygo-database)
2. For data, install the [ygo-scheduled-tasks](https://github.com/fablecode/ygo-database).

## Installing
```
 $ git clone https://github.com/fablecode/ygo-api.git
```
1. Build the solution
2. Set 'ygo.api' as startup project
3. Run

## Built With
* [Visual Studio 2017](https://www.visualstudio.com/downloads/)
* [.NET Core 2.2](https://www.microsoft.com/net/download/core)
* [Onion Architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/) and [CQRS](https://martinfowler.com/bliki/CQRS.html).
* [Strategy Pattern](https://en.wikipedia.org/wiki/Strategy_pattern)
* [Swagger](https://swagger.io/)
* [Mediatr](https://www.nuget.org/packages/MediatR/) for CQRS and the Mediator Design Pattern. Mediator design pattern defines how a set of objects interact with each other. You can think of a Mediator object as a kind of traffic-coordinator, it directs traffic to appropriate parties.
* [Entity Framework Core 2](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
* [Fluent Validations](https://www.nuget.org/packages/FluentValidation)
* [Fluent Assertions](https://www.nuget.org/packages/FluentAssertions)
* [MSTest](https://www.nuget.org/packages/MSTest.TestFramework)
* [Visual Studio Team Services](https://www.visualstudio.com/team-services/release-management/) for CI and deployment.

## Dependency graph
 The basic principle of Onion Architecture is to follow the boundaries of these layers – the inner layer can’t depend on its outer layer but can depend on layers beneath.
 
![ygo-api Dependencies Graph](/assets/images/ygo-api%20Dependencies%20Graph.png?raw=true "ygo-api Dependencies Graph")
 
 As you see from the diagram, all the dependency directions are downwards, towards domain.

### Key tenets of Onion Architecture:
1. The application is built around an independent object model
2. Inner layers define interfaces.  Outer layers implement interfaces
3. Direction of coupling is toward the center
4. All application core code can be compiled and run separate from infrastructure

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.
