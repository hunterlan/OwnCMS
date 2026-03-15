## Overview

A personal CMS project built with .NET stack.

## Proejct idea

In the beginning, it was just a simple static website, that was hosted on server that support communatication
via HTTP protocol. However, I felt that I need to build CMS to satisfy my needs.
This is a CMS that still supports Internet Explorer 8 browsers, for people, who still need it.

## Tech Stack
- .NET 10, ASP.NET Core.
- Entity Framework Core 10 with PostgreSQL.

## Project structure

- `src/OwnCMS.Presentation` - related ASP.NET Core
- `src/OwnCMS.Application` - business logic
- `src/OwnCMS.Entities` - Entities, value objects, enums, domain events
- `src/OwnCMS.Persistence` - Context class, entity configurations

## Commands

- Build: `dotnet build`
- Test: `dotnet test`
- Format: `dotnet format`

## Architecture Rules

- Domain layer has ZERO external dependencies
- Application layer defines interfaces, Infrastructure implements them
- All database access goes through EF Core DbContext (no repository pattern)
- Use MediatR for all command/query handling
- API layer is thin - endpoint definitions

## Code Conventions

### Naming
- DTOs: `[Entity]Dto`, `Create[Entity]Request`

### Patterns We Use
- Primary constructors for DI
- Records for DTOs
- Result<T> pattern for error handling (no exceptions for flow control)
- File-scoped namespaces
- Always pass CancellationToken to async methods

### Patterns We DON'T Use (Never Suggest)
- Repository pattern (use EF Core directly)
- AutoMapper (write explicit mappings)
- Exceptions for business logic errors
- Stored procedures
- Create any migrations.

# Coding Style

## General Guidelines
- Follow the official Microsoft .NET C# coding conventions: https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions
- Prefer clarity and readability over brevity.
- Use consistent formatting and naming throughout the codebase.

## Naming Conventions
- Use `PascalCase` for class, method, and property names.
- Use `camelCase` for local variables and method parameters.
- Use `ALL_CAPS` for constants.
- Prefix interfaces with `I` (e.g., `IOrderService`).
- Use meaningful, descriptive names; avoid abbreviations.

## Formatting
- Use 4 spaces for indentation (no tabs).
- Use file-scoped namespaces to simplify structure and improve readability.
- Add a blank line between method definitions.
- Place opening braces on a new line for methods, properties, and types (unless using file-scoped namespaces, then follow the file-scoped style).

### Example: File-Scoped Namespaces
```csharp
// Before
namespace MyNamespace
{
    public class ExampleClass
    {
        // ...existing code...
    }
}
// After
namespace MyNamespace;

public class ExampleClass
{
    // ...existing code...
}
```
- All new files must use file-scoped namespaces. Refactor existing files during updates or maintenance.

## Variable Declaration
- Use `var` for local variable declarations when the type is obvious.
- Prefer explicit types if it improves clarity.

### Example
```csharp
// Before
int x = 1;
double y = 2.0;
string z = "Hello";
ProductBacklogItem item = new ProductBacklogItem("Test", "Test", 1, 1, 1);
// After
var x = 1;
var y = 2.0;
var z = "Hello";
var item = new ProductBacklogItem("Test", "Test", 1, 1, 1);
```

## Sealed Classes
- Make classes `sealed` by default. If a class needs to be inherited, mark it as `virtual` explicitly.

## Use Nameof with Exceptions
- When throwing exceptions, use `nameof` to refer to the parameter name instead of hardcoding it.

### Example
```csharp
// Before
throw new ArgumentNullException("parameterName");
// After
throw new ArgumentNullException(nameof(parameterName));
```

## Code Structure
- One type per file (class, interface, enum, etc.).
- Organize files by feature/domain when possible.
- Group using directives at the top of the file, outside the namespace.
- Place related types in the same namespace.
- Use partial classes only when necessary (e.g., for code generation).

## Comments & Documentation
- Use XML documentation comments (`///`) for public APIs.
- Write comments to explain why, not what, when necessary.
- Remove commented-out code before committing.

## Null Checks & Exceptions
- Use guard clauses for argument validation.
- Use `nameof` for parameter names in exceptions.

## Modern C# Features
- Use pattern matching and expression-bodied members where appropriate.
- Prefer object and collection initializers.

# References
- Adhere to Microsoft's [coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions).
