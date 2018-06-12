using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models
{
    [System.Web.DynamicData.TableName("Version")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class Version : DocproQLSPDataContext<Version>
    {
        [Column]
        public int ID { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public int Weight { get; set; }
        [Column]
        public int IDProduct { get; set; }
        [Column]
        public DateTime StartDate { get; set; }
        [Column]
        public DateTime EndDate { get; set; }
        [Column]
        public DateTime Released { get; set; }
        [Column]
        public string FunctionNew { get; set; }
        [Column]
        public string FunctionUpdate { get; set; }
        [Column]
        public string LinkDemo { get; set; }
        [Column]
        public string AccountDemo { get; set; }
        [Column]
        public int Status { get; set; }
        [Column]
        public DateTime Approved { get; set; }
        [Column]
        public int ApprovedBy { get; set; }
        [Column]
        public string Reson { get; set; }
        [Column]
        public int IsPublish { get; set; }
        [Column]
        public DateTime Published { get; set; }
        [Column]
        public int PublishedBy { get; set; }
        


    }
}