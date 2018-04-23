using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.Shared
{
    public class ResultListModel
    {
        public string UserName { get; set; }
        public string TestName { get; set; }
        public string Category { get; set; }
        public TimeSpan RequestedTime { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public bool Result { get; set; }        
    }
}
