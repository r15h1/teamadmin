using Microsoft.EntityFrameworkCore;
using System;

namespace TeamAdmin.Lib.Repositories.EFContext
{
    internal class ClubContextFactory
    {
        public static T Create<T>() where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>();
            var connection = @"Server=.\SQL2012;Database=TeamAdminTests;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
            return (T)Activator.CreateInstance(typeof(T), optionsBuilder.Options);
        }
    }
}
