using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace DocproReport.Models
{
    [System.Web.DynamicData.TableName("Product")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class Product: DocproQLSPDataContext<Product>
    {
        [Column]
        public int ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int IDVersionCurrent { get; set; }

        [Column]
        public int ProductType { get; set; }
        
        [Column]
        public string Logo { get; set; }

        [Column]
        public string Description { get; set; }
        [Column]
        public string Code { get; set; }
    }
}