﻿using Microsoft.AspNetCore.Authorization;

namespace LoginHrSystems.Helpers
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission)
        {
            Policy = $"Permission:{permission}";
        }
    }
}
