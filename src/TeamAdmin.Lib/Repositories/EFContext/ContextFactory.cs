using Microsoft.EntityFrameworkCore;
using System;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class ContextFactory
    {
        public static T Create<T>() where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            var connection = @"Server=.\SQL2012;Database=TeamAdmin;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
        }
    }
}
