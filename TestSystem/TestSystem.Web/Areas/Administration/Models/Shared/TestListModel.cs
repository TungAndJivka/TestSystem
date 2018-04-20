using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSystem.Web.Areas.Administration.Models.Shared
{
    public class TestListModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsPusblished { get; set; }

    }
}
