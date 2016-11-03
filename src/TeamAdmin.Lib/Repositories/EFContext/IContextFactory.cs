using Microsoft.EntityFrameworkCore;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal interface IContextFactory
    {
        T Create<T>() where T : DbContext;
    }
}
