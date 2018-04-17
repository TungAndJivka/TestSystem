using System;
using System.Collections.Generic;
using System.Text;

namespace TestSystem.DTO
{
    public class UserTestDto
    {
        public string UserId { get; set; }
        public UserDto User { get; set; } //Np

        public Guid TestId { get; set; }
        public TestDto Test { get; set; } //NP

        public bool Passed { get; set; }

        public double Score { get; set; }
    }
}
