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
    public static class DbExtensions
    {
        // TODO: implement in all controllers
        // returns either Ok() with a model by ID or NotFound() from a db set 
        public static IActionResult GetByID<TEntity>(this CarRentalDbContext rentalsDb, int id)
            where TEntity : class, IDataModel
        {
            DbSet<TEntity> dbSet = rentalsDb.Set<TEntity>();
            IActionResult response = new NotFoundResult();

            TEntity foundModel = dbSet.FirstOrDefault(m => m.ID == id);

            if (foundModel != null)
            {
                response = new OkObjectResult(foundModel);
            }

            return response;
        }

        // TODO: update all id gens in the controllers
        // sets the IDataModel's ID: if the entity set is empty -> 1, else sets ID to the highest ID + 1
        public async static Task IDGen<TEntity>(this CarRentalDbContext rentalsDb, TEntity model)
            where TEntity : class, IDataModel
        {
            DbSet<TEntity> dbSet = rentalsDb.Set<TEntity>();
            model.ID = await dbSet.AnyAsync() ?
                await dbSet.MaxAsync(m => m.ID) + 1
                :
                1;
        }

        // saves to the db an object of type TEntity: handles the opening and closing of the connection, 
        // and setting the identity insert on and off
        // TODO: implemnt post in all controllers
        public static async Task<IActionResult> Post<TEntity>
            (this CarRentalDbContext rentalsDb, string tableName, TEntity value)
            where TEntity : class
        {
            DbSet<TEntity> dbSet = rentalsDb.Set<TEntity>();
            IActionResult response;
            try
            {
                using (var connection = rentalsDb.Database.GetDbConnection())
                {
                    await connection.OpenAsync();

                    await rentalsDb.Database.ExecuteSqlRawAsync($"SET IDENTITY_INSERT dbo.{tableName} ON");
                    await rentalsDb.Set<TEntity>().AddAsync(value);
                    rentalsDb.SaveChanges();
                    rentalsDb.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.{tableName} OFF");

                    response = new CreatedResult(tableName, value);
                }
            }
            catch (Exception e)
            {
                response = new BadRequestObjectResult(e);
            }

            return response;
        }

        // increments or generates an ID for the IDataModel, handles the identity insert
        public static async Task<IActionResult> PostIdGen<TEntity>
            (this CarRentalDbContext rentalsDb, string tableName, TEntity value)
            where TEntity : class, IDataModel
        {
            await rentalsDb.IDGen<TEntity>(value);

            IActionResult response = await rentalsDb.Post<TEntity>(tableName, value);
            return response;
        }

        // Saves new values for a given IDataModel. Returns NotFound, Ok, Bad Request apropriatly
        // TODO: implement put in all controllers
        public static async Task<IActionResult> Put<TEntity>
            (this CarRentalDbContext rentalsDb, IDataModel updatedModel)
            where TEntity : class, IDataModel
        {
            DbSet<TEntity> entitySet = rentalsDb.Set<TEntity>();
            IActionResult response = new NotFoundResult();

            TEntity oldModel = await entitySet.FirstOrDefaultAsync(m => m.ID == updatedModel.ID);

            if (oldModel != null)
            {
                try
                {
                    rentalsDb.Entry(oldModel).CurrentValues.SetValues(updatedModel);
                    await rentalsDb.SaveChangesAsync();
                    response = new OkObjectResult(updatedModel);
                }
                catch (Exception e)
                {
                    response = new BadRequestObjectResult(e);
                }
            }

            return response;
        }

        // id matching id is found, deletes the model. else returns not found
        // TODO: implemnt delete in all controllers
        public static async Task<IActionResult> Delete<TEntity>
            (this CarRentalDbContext rentalsDb, int id)
            where TEntity : class, IDataModel
        {
            DbSet<TEntity> entitySet = rentalsDb.Set<TEntity>();
            IActionResult response = new NotFoundResult();

            TEntity foundModel = await entitySet.FirstOrDefaultAsync(m => m.ID == id);

            if (foundModel != null)
            {
                entitySet.Remove(foundModel);
                await rentalsDb.SaveChangesAsync();
                response = new OkObjectResult(foundModel);
            }

            return response;
        }
    }
}
