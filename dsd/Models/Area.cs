using DocProModel.Models;
using DocproReport.Models;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models.UBDT
{
    [TableName("Area")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class Area : DocproQLKDataContext<Area>
    {
        [Column]
        public int ID { get; set; }

        [Column]
        public int IDChannel { get; set; }

        [Column]
        public string Code { get; set; }

        [Column]
        public int AreaType { get; set; }

        [Column]
        public string Name { get; set; }


        [Column]
        public int Parent { get; set; }


        [Column]
        public DateTime Created { get; set; }

        [Column]
        public int CreatedBy { get; set; }

        [Column]
        public DateTime? Updated { get; set; }

        [Column]
        public int? UpdatedBy { get; set; }

        [Column]
        public string SearchMeta { get; set; }

      

        public override List<string> GetFieldSearchs()
        {
            return new List<string>()
            {
                "Name", "Code"
            };
        }

    }
}