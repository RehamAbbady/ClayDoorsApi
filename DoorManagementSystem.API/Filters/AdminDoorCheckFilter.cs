using DoorManagementSystem.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace DoorManagementSystem.API.Filters
{
    public class AdminForDoorAuthorizationAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var usersService = context.HttpContext.RequestServices.GetService<IUsersService>();

            var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var doorIdAsString = context.RouteData.Values["doorId"] as string;
            int doorId = 0;
            if (!string.IsNullOrEmpty(doorIdAsString) && int.TryParse(doorIdAsString, out int parsedDoorId))
            {
                doorId = parsedDoorId;
            }

            bool hasAccess = usersService.IsUserAdminForDoorAsync(Int32.Parse(userId), doorId).Result;

            if (!hasAccess)
            {
                context.Result = new ForbidResult();

            }

        }
    }

}
