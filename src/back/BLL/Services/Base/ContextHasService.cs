using System;
using System.Threading.Tasks;
using DAL.EF;

namespace BLL.Services.Base;

public abstract class ContextHasService : IDisposable
{
    protected AppDbContext Context { get; }

    protected ContextHasService(AppDbContext context)
    {
        Context = context;
    }

    protected async Task SaveChanges()
    {
        await Context.SaveChangesAsync();
    }

    protected async Task ExecTran<TArg0, TArg1>(Func<TArg0, TArg1, Task> exec, TArg0 arg0, TArg1 arg1)
    {
        await using var db = await Context.Database.BeginTransactionAsync();
        try
        {
            await exec(arg0, arg1);
            await db.CommitAsync();
        }
        catch
        {
            await db.RollbackAsync();
            throw;
        }
    }

    protected async Task ExecTran<TArg0, TArg1, TArg2>(Func<TArg0, TArg1, TArg2, Task> exec, TArg0 arg0, TArg1 arg1, TArg2 arg2)
    {
        await using var db = await Context.Database.BeginTransactionAsync();
        try
        {
            await exec(arg0, arg1, arg2);
            await db.CommitAsync();
        }
        catch
        {
            await db.RollbackAsync();
            throw;
        }
    }

    public void Dispose()
    {
        Context?.Dispose();
    }
}
