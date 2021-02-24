using CarRentalDB.Helpers;
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
using Microsoft.AspNetCore.Mvc;

namespace CarRentalDB.Utilities
{
    // TODO: update documentation for class
    // handles post and actions for DbContext
    public static class DbExtensions
    {
        // saves to the db an object of type TEntity: handles the opening and closing of the connection, 
        // and setting the identity insert on and off
        public static async Task IdentityInsertAndUpdateDbAsync<TEntity>
            (this CarRentalDbContext rentalsDb, string tableName, TEntity value)
            where TEntity : class
        {
            await rentalsDb.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{tableName} ON");
            await rentalsDb.Set<TEntity>().AddAsync(value);
            rentalsDb.SaveChanges();
            rentalsDb.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.{tableName} OFF");
        }

        // saves new values for a given IDataModel
        public static async Task IDataModelUpdateDb<TEntity>(this CarRentalDbContext rentalsDb, IDataModel updatedModel)
            where TEntity : class, IDataModel
        {
            TEntity oldModel = await rentalsDb.Set<TEntity>()
                .FirstOrDefaultAsync(m => m.ID == updatedModel.ID);

            rentalsDb.Entry(oldModel).CurrentValues.SetValues(updatedModel);
            rentalsDb.SaveChanges();
        }

        // id matching id is found, deletes the model. else returns not found
        //public static async Task<IActionResult> Delete<TEntity>(this CarRentalDbContext rentalsDb, int id)
        //    where TEntity : class, IDataModel
        //{
        //    IActionResult response = new NotFoundResult();

        //    TEntity foundModel = await rentalsDb.Set<TEntity>().FirstOrDefaultAsync(m => m.ID == id);

        //    if (foundModel != null)
        //    {
        //        rentalsDb.Set<TEntity>().Remove(foundModel);
        //        await rentalsDb.SaveChangesAsync();
        //        response = new OkObjectResult(foundModel);
        //    }

        //    return response;
        //}

        // FIXME: trial delete
        public static async Task<IActionResult> Delete<TEntity>(this CarRentalDbContext rentalsDb, int id)
            where TEntity : class, IDataModel
        {
            DbSet<TEntity> set = rentalsDb.Set<TEntity>();
            IActionResult response = new NotFoundResult();

            TEntity foundModel = await set.FirstOrDefaultAsync(m => m.ID == id);

            if (foundModel != null)
            {
                set.Remove(foundModel);
                await rentalsDb.SaveChangesAsync();
                response = new OkObjectResult(foundModel);
            }

            return response;
        }
    }
}
