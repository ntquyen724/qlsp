using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocproReport.Models;
using DocproReport.Repository;

namespace DocproReport.Models
{
    [TableName("District")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class District: DocproQLKDataContext<District>
    {
        [Column]
        public int ID { get; set; }

        [Column]
        public int IDChannel { get; set; }
        [Column]
        public int IDRegion { get; set; }
        
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