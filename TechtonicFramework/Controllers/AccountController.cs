using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using TechtonicFramework.Dtos;
using TechtonicFramework.Models.Users;
using System.Web;
using System.Data.Entity;
using TechtonicFramework.Dtos.Account;
using System.Net.Http;
using System;
using System.Collections.Generic;
using TechtonicFramework.Dtos.Management.AccountRelated;

namespace TechtonicFramework.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;

        public AccountController() { }


        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        [HttpPost]
[Route("login")]
public async Task<IHttpActionResult> Login(LoginDto dto)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var result = await SignInManager.PasswordSignInAsync(
        dto.Email, dto.Password, isPersistent: true, shouldLockout: false);

    if (result == SignInStatus.Success)
        return Ok();

    return Unauthorized();
}

        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> RegisterUser(RegisterDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                    return BadRequest("Email and password are required.");
                var user = new User
                {
                    UserName = dto.Email,
                    Email = dto.Email
                };

                var result = await UserManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                    return BadRequest(string.Join(" | ", result.Errors));

                await UserManager.AddToRoleAsync(user.Id, "Customer");

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                var message = ex.InnerException?.InnerException?.Message ?? ex.Message;
                return InternalServerError(new Exception($"Registration failed: {message}"));
            }
        }

        [HttpGet]
        [Route("user-info")]
        public async Task<IHttpActionResult> GetUserInfo()
        {
            if (!User.Identity.IsAuthenticated)
                return StatusCode(System.Net.HttpStatusCode.NoContent);

            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
                return Unauthorized();

            var roles = await UserManager.GetRolesAsync(user.Id);

            return Ok(new
            {
                user.Email,
                user.UserName,
                Roles = roles
            });
        }

        [HttpPost]
        [Route("logout")]
        public IHttpActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("address")]
        public async Task<IHttpActionResult> CreateOrUpdateAddress(Address address)
        {
            var user = await UserManager.Users.Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);

            if (user == null)
                return Unauthorized();

            user.Address = address;
            var result = await UserManager.UpdateAsync(user);
            if (!result.Succeeded)
                return BadRequest("Problem updating user address");

            return Ok(user.Address);
        }

        [Authorize]
        [HttpGet]
        [Route("address")]
        public async Task<IHttpActionResult> GetSavedAddress()
        {
            var address = await UserManager.Users
                .Where(x => x.UserName == User.Identity.Name)
                .Select(x => x.Address)
                .FirstOrDefaultAsync();

            if (address == null)
                return StatusCode(System.Net.HttpStatusCode.NoContent);

            return Ok(address);
        }

        [HttpGet]
        [Route("admin/users")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> GetUsersByRole(string role, string search = "")
        {
            var validRoles = new[] { "Customer", "Moderator", "Admin" };
            if (!validRoles.Contains(role))
                return BadRequest("Invalid role.");

            var allUsers = await UserManager.Users
                .Where(u => string.IsNullOrEmpty(search) || u.Email.Contains(search))
                .ToListAsync();

            var result = new List<object>();

            foreach (var user in allUsers)
            {
                var roles = await UserManager.GetRolesAsync(user.Id);
                bool include = false;

                if (role == "Customer")
                {
                    include = roles.Count == 1 && roles.Contains("Customer");
                }
                else if (role == "Moderator")
                {
                    include = roles.Count == 2 && roles.Contains("Customer") && roles.Contains("Moderator");
                }
                else if (role == "Admin")
                {
                    include = roles.Contains("Admin");
                }

                if (include)
                {
                    result.Add(new
                    {
                        user.Id,
                        user.Email,
                        user.UserName,
                        Roles = roles
                    });
                }
            }

            return Ok(result);
        }




        [HttpPost]
        [Route("admin/update-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> UpdateUserRole(UpdateUserRoleDto dto)
        {
            var user = await UserManager.FindByIdAsync(dto.UserId);
            if (user == null) return NotFound();

            var validRoles = new[] { "Customer", "Moderator", "Admin" };
            if (!validRoles.Contains(dto.NewRole))
                return BadRequest("Invalid role.");

            var currentRoles = await UserManager.GetRolesAsync(user.Id);

            if (currentRoles.Contains("Admin") && dto.NewRole != "Admin")
                return BadRequest("Admin users cannot be demoted.");

            var removeResult = await UserManager.RemoveFromRolesAsync(user.Id, currentRoles.ToArray());
            if (!removeResult.Succeeded)
                return BadRequest("Failed to remove existing roles.");

            string[] newRoles = dto.NewRole == "Admin"
                ? new[] { "Customer", "Moderator", "Admin" }
                : dto.NewRole == "Moderator"
                    ? new[] { "Customer", "Moderator" }
                    : new[] { "Customer" };

            var addResult = await UserManager.AddToRolesAsync(user.Id, newRoles);
            if (!addResult.Succeeded)
                return BadRequest("Failed to assign new roles.");

            return Ok("User roles updated successfully.");
        }


        [HttpPost]
        [Route("admin-create")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> CreateUserWithRole(AdminCreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email and password are required.");

            var validRoles = new[] { "Customer", "Moderator", "Admin" };
            if (!validRoles.Contains(dto.Role))
                return BadRequest("Invalid role.");

            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email
            };

            var result = await UserManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(string.Join(" | ", result.Errors));

            string[] rolesToAdd = dto.Role == "Admin"
                ? new[] { "Customer", "Moderator", "Admin" }
                : dto.Role == "Moderator"
                    ? new[] { "Customer", "Moderator" }
                    : new[] { "Customer" };

            var roleResult = await UserManager.AddToRolesAsync(user.Id, rolesToAdd);
            if (!roleResult.Succeeded)
                return BadRequest("User created but failed to assign roles.");

            return Ok("User created and roles assigned successfully.");
        }

        [HttpDelete]
        [Route("admin/delete-user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> DeleteCustomerOnlyUser(string userId)
        {
            var user = await UserManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var roles = await UserManager.GetRolesAsync(user.Id);

            if (roles.Count != 1 || !roles.Contains("Customer"))
                return BadRequest("User cannot be deleted. Only pure 'Customer' users can be deleted.");

            var result = await UserManager.DeleteAsync(user);
            if (!result.Succeeded)
                return BadRequest(string.Join(" | ", result.Errors));

            return Ok("User deleted successfully.");
        }




    }
}
