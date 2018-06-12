using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace DocproReport.Models
{
        [System.Web.DynamicData.TableName("UserName")]
        [PrimaryKey("ID", AutoIncrement = true)]
        public class UserName : DocproQLKDataContext<UserName>
        {
            [Column]
            public int ID { get; set; }

            [Column]
            public string Name { get; set; }

            [Column]
            public string Address { get; set; }

            [Column]
            public int Phone { get; set; }

            [Column]
            public int UserType { get; set; }


            [Column]
            public int CreateBy { get; set; }


            [Column]
            public DateTime? DateCreate { get; set; }
            
            [Column]
            public int IDChannel { get; set; }

         
        }
   }
