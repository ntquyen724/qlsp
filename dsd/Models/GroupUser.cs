using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models
{
    [System.Web.DynamicData.TableName("GroupUser")]
    [PrimaryKey("ID", AutoIncrement = true)]
    public class GroupUser: DocproQLKDataContext<GroupUser>
    {
        [Column]
        public int ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public int CreateBy { get; set; }


        [Column]
        public DateTime? DateCreate { get; set; }

        [Column]
        public DateTime? DateModified { get; set; }

        [Column]
        public string ModifiedBy { get; set; }

        [Column]
        public string Redirect { get; set; }

        [Column]
        public int Sort { get; set; }
        [Column]
        public string OrderStatusAllow { get; set; }
    }
}