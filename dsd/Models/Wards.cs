using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocproReport.Models;

namespace DocproReport.Models
{
    [TableName("Ward")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class Ward : DocproQLKDataContext<Ward>  
    {
        [Column]
        public int ID { get; set; }

        [Column]
        public int IDChannel { get; set; }

        [Column]
        public int IDDistrict { get; set; }

        [Column]
        public string Code { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public DateTime Created { get; set; }

        [Column]
        public int CreatedBy { get; set; }

        [Column]
        public DateTime? Updated { get; set; }

        [Column]
        public int? UpdatedBy { get; set; }
    }
}