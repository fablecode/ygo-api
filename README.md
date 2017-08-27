![alt text](https://fablecode.visualstudio.com/_apis/public/build/definitions/22ebd0cf-e8a2-4659-997b-95d960acfe61/3/badge?maxAge=0 "Visual studio team services build status") 

# Ygo-api
A C# .NET Core 2 api for [Yu-Gi-Oh](http://www.yugioh-card.com/uk/) data such as Card Images, descriptions, Tips and Trivia.

## Why?
To provide access to the latest [Yu-Gi-Oh](http://www.yugioh-card.com/uk/)  banlist & card errata in a simple JSON format.

## Live Demo
[http://www.ygo-api.com](http://www.ygo-api.com)

## Prerequisite
1. Setup the [Ygo database](https://github.com/fablecode/ygo-database)
2. For data, install the windows services (NOT IMPLEMENTED EXCEPTION).

## Installing
```
 $ git clone https://github.com/fablecode/ygo-api.git
```
1. Build the solution
2. Set 'ygo.api' as startup project
3. Run

## Built With
* [Onion Architecture](http://jeffreypalermo.com/blog/the-onion-architecture-part-1/) and [CQRS](https://martinfowler.com/bliki/CQRS.html).
* [Visual Studio 2017](https://www.visualstudio.com/downloads/)
* [.NET Core 2)](https://www.microsoft.com/net/download/core)
* [Swagger](https://swagger.io/)
* [Mediatr](https://www.nuget.org/packages/MediatR/) for CQRS and the Mediator Design Pattern. Mediator design pattern defines how a set of objects interact with each other. You can think of a Mediator object as a kind of traffic-coordinator, it directs traffic to appropriate parties.
* [Entity Framework Core 2](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/)
* [Fluent Validations](https://www.nuget.org/packages/FluentValidation)
* [Fluent Assertions](https://www.nuget.org/packages/FluentAssertions)
* [MSTest](https://www.nuget.org/packages/MSTest.TestFramework)
* [Visual Studio Team Services](https://www.visualstudio.com/team-services/release-management/) for CI and deployment.

## License
This project is licensed under the MIT License - see the [LICENSE.md](LICENSE) file for details.
