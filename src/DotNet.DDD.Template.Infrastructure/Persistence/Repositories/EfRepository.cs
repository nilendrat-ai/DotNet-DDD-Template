// REMOVED: Generic EfRepository<T> has been replaced with concrete,
// query-specific repositories per aggregate (e.g. OrderRepository).
//
// Rationale: IRepository<T> forced all queries through GetByIdAsync/GetAllAsync,
// leading to in-memory filtering or bypassed abstractions for real query needs.
// A concrete repository per aggregate exposes only the queries that aggregate
// actually needs, keeps EF Core includes/filters co-located with the data access,
// and is still fully injectable and mockable.
//
// To add a new aggregate repository:
//   1. Create src/Infrastructure/Persistence/Repositories/FooRepository.cs
//   2. Inject AppDbContext, write query-specific methods (Include, Where, etc.)
//   3. Register: services.AddScoped<FooRepository>() in DependencyInjection.cs
//   4. Inject FooRepository directly into your MediatR handler constructor
