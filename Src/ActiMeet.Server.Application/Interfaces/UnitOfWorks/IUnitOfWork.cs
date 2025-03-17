using ActiMeet.Server.Application.Interfaces.Repositories;
using ActiMeet.Server.Domain.Abstractions;

namespace ActiMeet.Server.Application.Interfaces.UnitOfWorks;
public interface IUnitOfWork : IAsyncDisposable
{
	IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new();
	IWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity, new();
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
	int SaveChanges();
}
