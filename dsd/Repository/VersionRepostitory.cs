using System;
using DocproReport.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocproReport.Repository
{
    public class VersionRepostitory: Models.Version
    {
        public static Models.Version ProductVersion(int Id)
        {
            return Instance.GetById(Id);

        }
    }
}