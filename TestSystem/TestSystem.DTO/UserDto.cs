using System;
using System.Collections.Generic;

namespace TestSystem.DTO
{
    public class UserDto
    {
        public ICollection<TestDto> Tests { get; set; }

        public ICollection<UserTestDto> UserTests { get; set; }
    }
}
