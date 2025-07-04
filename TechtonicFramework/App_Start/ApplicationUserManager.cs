﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using TechtonicFramework.Data;
using TechtonicFramework.Models.Users;

public class ApplicationUserManager : UserManager<User>
{
    public ApplicationUserManager(IUserStore<User> store)
        : base(store)
    {
    }

    public static ApplicationUserManager Create(
        IdentityFactoryOptions<ApplicationUserManager> options,
        IOwinContext context)
    {
        var manager = new ApplicationUserManager(
            new UserStore<User>(context.Get<ApplicationDbContext>()));

        // Optional: Configure validation logic for usernames and passwords here
        manager.UserValidator = new UserValidator<User>(manager)
        {
            AllowOnlyAlphanumericUserNames = false,
            RequireUniqueEmail = true
        };

        manager.PasswordValidator = new PasswordValidator
        {
            RequiredLength = 6
        };

        return manager;
    }
}
