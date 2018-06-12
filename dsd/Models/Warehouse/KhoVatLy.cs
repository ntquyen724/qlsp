using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models
{
    [PrimaryKey("ID", AutoIncrement = true)]
    [TableName("KhoVatLy")]
    public class KhoVatLy : DocproQLKDataContext<KhoVatLy>
    {
        [Column]
        public int ID { get; set; }
        [Column]
        public int IDChannel { get; set; }
        [Column]
        public string Ma { get; set; }
        [Column]
        public string TenKe { get; set; }
        [Column]
        public string TenTu { get; set; }
        [Column]
        public string Day { get; set; }
        [Column]
        public int KichThuoc { get; set; }
        [Column]
        public int DaSuDung { get; set; }
        [Column]
        public int ConTrong { get; set; }
        [Column]
        public int TinhTrang { get; set; }
        [Column]
        public DateTime Created { get; set; }
        [Column]
        public int CreatedBy { get; set; }
    }
}