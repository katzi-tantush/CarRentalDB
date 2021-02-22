using CarRentalDB.Models;
using Microsoft.EntityFrameworkCore;
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

        private static string GetTableName<TEntity>(this CarRentalDbContext RentalsDb) 
            where TEntity : class
        {
            ObjectContext objectContext = ((IObjectContextAdapter)RentalsDb).ObjectContext;

            return objectContext.GetTableName<TEntity>();
        }

        public static string GetTableName<TEntity>(this ObjectContext context)
            where TEntity : class
        {
            string sql = context.CreateObjectSet<TEntity>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);

            string table = match.Groups["table"].Value;
            return table;
        }


        // saves to the db an object of type TEntity: handles the opening and closing of the connection, 
        // and setting the identity insert on and off
        public static void SaveToDb<TEntity>(this CarRentalDbContext RentalsDb, TEntity obj)
            where TEntity : class
        {
            string tableName = RentalsDb.GetTableName<TEntity>();

            RentalsDb.Database.OpenConnection();

            RentalsDb.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.{tableName} ON");
            RentalsDb.Set<TEntity>().Add(obj);
            RentalsDb.SaveChanges();
            RentalsDb.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.{tableName} OFF");

            RentalsDb.Database.CloseConnection();
        }
    }
}
