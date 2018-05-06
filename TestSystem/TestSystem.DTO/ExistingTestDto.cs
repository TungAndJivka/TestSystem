using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.DTO
{
    public class ExistingTestDto
    {
        public string Id { get; set; }
        public string TestName { get; set; }
        public string Category { get; set; }
        public bool IsPusblished { get; set; }
        public bool IsDisabled { get; set; }
    }
}
