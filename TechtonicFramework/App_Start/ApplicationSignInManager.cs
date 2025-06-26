using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using TechtonicFramework.Models.Users;

public class ApplicationSignInManager : SignInManager<User, string>
{
    public ApplicationSignInManager(
        ApplicationUserManager userManager,
        IAuthenticationManager authenticationManager)
        : base(userManager, authenticationManager)
    {
    }

    public static ApplicationSignInManager Create(
        IdentityFactoryOptions<ApplicationSignInManager> options,
        IOwinContext context)
    {
        return new ApplicationSignInManager(
            context.GetUserManager<ApplicationUserManager>(),
            context.Authentication);
    }

    public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
    {
        return ((ApplicationUserManager)UserManager).CreateIdentityAsync(
            user, DefaultAuthenticationTypes.ApplicationCookie);
    }
}
