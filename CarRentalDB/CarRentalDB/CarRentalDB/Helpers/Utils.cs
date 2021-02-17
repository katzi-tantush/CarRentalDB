using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Helpers
{
    public static class Utils
    {
        // if existingModels is empty returns 1, else returns the max ID + 1
        public static int IDGen(IEnumerable<IDataModel> existingModels)
        {
            // TODO: implement overrides for UserID and CarID
            int nextID = existingModels.Any() ? 
                existingModels.Max(model => model.ID) + 1 
                : 
                1;

            return nextID;
        }
    }
}
