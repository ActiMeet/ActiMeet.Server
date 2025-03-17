using ActiMeet.Server.Application.Interfaces.Repositories;
using ActiMeet.Server.Application.Interfaces.UnitOfWorks;
using ActiMeet.Server.Infrastructure.Context;
using ActiMeet.Server.Infrastructure.Repositories;

namespace ActiMeet.Server.Infrastructure.UnitOfWorks;
public class UnitOfWork(
	ApplicationDbContext applicationDbContext) : IUnitOfWork
{
	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken)) => await applicationDbContext.SaveChangesAsync();
	public int SaveChanges() => applicationDbContext.SaveChanges();
	IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(applicationDbContext);
	IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(applicationDbContext);
	public async ValueTask DisposeAsync() => await applicationDbContext.DisposeAsync();
}
