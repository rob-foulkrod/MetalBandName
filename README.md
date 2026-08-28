# MetalBandName

A Kanban-inspired ASP.NET Core MVC TODO application.

## Run

```sh
dotnet run
```

Tasks are stored by the registered `ITodoStore` provider. The default
`InMemoryTodoStore` is intentionally ephemeral; replace its DI registration in
`Program.cs` to use another persistence provider.