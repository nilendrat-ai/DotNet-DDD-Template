// REMOVED: IRepository<T> generic interface has been replaced with concrete,
// query-specific repositories per aggregate (e.g. OrderRepository in Infrastructure).
//
// The domain tier no longer defines repository contracts.
// This removes the leaky generic abstraction while keeping EF Core
// fully decoupled from application handlers.
//
// See: src/Infrastructure/Persistence/Repositories/OrderRepository.cs
