using CarRentalDB.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDB.Models
{
    public class Image: IDataModel
    {
        // TODO: implement the image db
        [Key]
        public int ID { get; set; }
        public int CarCategoryID { get; set; }
        public byte[] File { get; set; }
    }
}
