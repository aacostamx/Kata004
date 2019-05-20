# Kata 004

## Summary
Discusses solution structure, CQRS, mediator pattern, and razor pages

*Creating Solutions with Separate Projects for Entities, Data Access, and Website Functionality
https://www.pluralsight.com/guides/asp-net-mvc-creating-solutions-with-separate-projects-for-entities-data-access-and-website-functionality
Well-factored, SOLID applications using .NET Core
https://marketplace.visualstudio.com/items?itemName=GregTrevellick.CleanArchitecture

*Razor Pages
Razor Pages is a new aspect of ASP.NET Core MVC that makes coding page-focused scenarios easier and more productive

## Pre Reading
- [CQRS](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [CQRS/MediatR](https://www.stevejgordon.co.uk/cqrs-using-mediatr-asp-net-core)

## Dicussion
- Ensure .net core sdk 2.1.3 is installed on your machine
- In a temporary folder, clone [Contoso University](https://github.com/jbogard/ContosoUniversityDotNetCore-Pages)
- Examine the project structure and describe some of the design patterns used.
    - CQS/CQRS 
    *Command–query separation (CQS) A simple but powerful pattern
    https://www.dotnetcurry.com/patterns-practices/1461/command-query-separation-cqs
    *CQRS (Command Query Responsibility Segregation)
    https://altkomsoftware.pl/en/blog/microservices-net-core-cqrs-mediatr/
    - Mediator
    Simple, unambitious mediator implementation in .NET
    https://github.com/jbogard/MediatR
- Try following the instructions to scaffold the project. What are some similarities to Kata002? Differences? 
*Both projects are using SQL scripts (database first approach), but the difference is that Kata002 is using dbUp https://dbup.readthedocs.io/en/latest/ and we don't need to run the scripts manually 
- Run the project under IIS Express and Kestrel. What are use cases for each? 
*The IIS Express is supported by Windows Only and Kestrel is supported on all platforms and versions that .NET Core supports.
- Describe your experience with Mini Profiler. How is this tool useful?
*A simple but effective mini-profiler for .NET
MiniProfiler is a library and UI for profiling your application. By letting you see where you time is spent, which queries are run, and any other custom timings you want to add, MiniProfiler helps you debug issues and optimize performance.
Alternatives
https://github.com/glimpse/glimpse
https://miniprofiler.com/

## Code Exercise
Implement an Exams module, following the existing patterns:
- The Exam will have an ID, a Title, and a Date. It will not be related to any other models (done)
- Scaffold the table with a SQL file (done)
- Implement CRUD for Exams using the existing patterns.
    * Was scaffolding fairly simple?
    * How much code duplication is there between modules? How would you reduce this?
