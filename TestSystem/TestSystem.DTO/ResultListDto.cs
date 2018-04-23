using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.DTO
{
    public class ResultListDto
    {
        public string UserName { get; set; }
        public string TestName { get; set; }
        public string Category { get; set; }
        public TimeSpan RequestedTime { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public bool Result { get; set; }
    }
}
