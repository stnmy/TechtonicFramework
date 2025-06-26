using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechtonicFramework.Dtos.Management.AccountRelated
{
    public class UpdateUserRoleDto
    {
        public string UserId { get; set; }
        public string NewRole { get; set; }
    }
}