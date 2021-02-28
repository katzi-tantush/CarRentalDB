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
        // Creats a new CarRentalsDbContext and assigns it to the RentalsDb
        public static void ReInitilize(this CarRentalDbContext RentalsDb)
        {
            RentalsDb = new CarRentalDbContext();
        }

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

        // sets the IDataModel's ID: if the entity set is empty -> 1, else sets ID to the highest ID + 1
        public static void IDGen<TEntity>(this CarRentalDbContext rentalsDb, TEntity model)
            where TEntity : class, IDataModel
        {
            DbSet<TEntity> dbSet = rentalsDb.Set<TEntity>();
            model.ID = dbSet.Any() ?
                dbSet.Max(m => m.ID) + 1
                :
                1;
        }

        // saves to the db an object of type TEntity: handles the opening and closing of the connection, 
        // and setting the identity insert on and off.
        // used for User, Car, and RentedCar
        public static IActionResult Post<TEntity>
            (this CarRentalDbContext rentalsDb, string tableName, TEntity value)
            where TEntity : class
        {
            DbSet<TEntity> dbSet = rentalsDb.Set<TEntity>();
            IActionResult response;
            try
            {
                using (var connection = rentalsDb.Database.GetDbConnection())
                {
                    connection.Open();

                     rentalsDb.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT dbo.{tableName} ON");
                     rentalsDb.Set<TEntity>().Add(value);
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
        // used for models that dont use Post db extension method
        public static IActionResult PostIdGen<TEntity>
            (this CarRentalDbContext rentalsDb, string tableName, TEntity value)
            where TEntity : class, IDataModel
        {
            rentalsDb.IDGen<TEntity>(value);

            IActionResult response = rentalsDb.Post<TEntity>(tableName, value);
            return response;
        }

        // Saves new values for a given IDataModel. Returns NotFound, Ok, Bad Request apropriatly
        public static IActionResult Put<TEntity>
            (this CarRentalDbContext rentalsDb, IDataModel updatedModel)
            where TEntity : class, IDataModel
        {
            DbSet<TEntity> entitySet = rentalsDb.Set<TEntity>();
            IActionResult response = new NotFoundResult();

            TEntity oldModel = entitySet.FirstOrDefault(m => m.ID == updatedModel.ID);

            if (oldModel != null)
            {
                try
                {
                    rentalsDb.Entry(oldModel).CurrentValues.SetValues(updatedModel);
                    rentalsDb.SaveChanges();
                    response = new OkObjectResult(updatedModel);
                }
                catch (Exception e)
                {
                    response = new BadRequestObjectResult(e);
                }
            }

            return response;
        }

        // If matching id is found, deletes the model. else returns not found
        public static IActionResult Delete<TEntity>
            (this CarRentalDbContext rentalsDb, int id)
            where TEntity : class, IDataModel
        {
            DbSet<TEntity> entitySet = rentalsDb.Set<TEntity>();
            IActionResult response = new NotFoundResult();

            TEntity foundModel = entitySet.FirstOrDefault(m => m.ID == id);

            if (foundModel != null)
            {
                entitySet.Remove(foundModel);
                rentalsDb.SaveChanges();
                response = new OkObjectResult(foundModel);
            }

            return response;
        }
    }
}
