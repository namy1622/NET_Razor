using Microsoft.AspNetCore.Identity;

namespace App.Services{
    public class AppIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateRoleName(string role)
        {
            var er = base.DuplicateRoleName(role);
            return new IdentityError{
                Code = er.Code,
                Description = $"Role có tên {role} bị trùng"
            };
        }
    }
}