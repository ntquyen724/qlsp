using DocproReport.Customs.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Models.Views
{
    public class UserModel
    {
        public string Url { get; set; }
        public List<UserName> Users { get; set; }
        public UserName User { get; set; }
       public List<UserName> ListUser { get; set; }
        public TestParam SearchParam { get; set; }
        public List<Mail> Mail { get; set; }
    }
    public class Mail
    {
        public string MailString { get; set; }
        public int MailValue { get; set; }
    }
   
}