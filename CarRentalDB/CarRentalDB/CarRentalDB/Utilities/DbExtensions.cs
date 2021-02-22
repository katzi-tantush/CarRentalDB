using CarRentalDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarRentalDB.Utilities
{
    // TODO: update documentation for class
    // handles post and actions for DbContext
    public static class DbExtensions
    {
        //public static string GetTableName()
        //{

        //}


        // saves to the db an object of type TEntity: handles the opening and closing of the connection, 
        // and setting the identity insert on and off
        public static async Task SaveToDbAsync<TEntity>
            (this CarRentalDbContext RentalsDb, string tableName, TEntity value)
            where TEntity : class
        {
            await RentalsDb.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{tableName} ON");
            await RentalsDb.Set<TEntity>().AddAsync(value);
            RentalsDb.SaveChanges();
            RentalsDb.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.{tableName} OFF");
        }
    }
}
