using Microsoft.EntityFrameworkCore;
using System;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class ContextFactory
    {
        public static T Create<T>() where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            var connection = Util.Settings.DefaultConnectionString;
            optionsBuilder.UseSqlServer(connection);
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
        }
    }
}
