using CarRentalDB.Helpers;
using CarRentalDB.Models;
using CarRentalDB.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.FullModels
{
    public class FullBranch
    {
        public FullBranch(CarRentalDbContext rentalsDb, int id)
        {
            Branch branch = rentalsDb.GetById<Branch>(id) as Branch;
            ID = id;
            Address = branch.Address;
            Name = branch.Name;
            BranchLocation = rentalsDb.GetById<Location>(branch.LocationID) as Location;
        }

        public int ID { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public Location BranchLocation { get; set; }
    }
}
