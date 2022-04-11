using CryptoTradingPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTradingPlatform.Test.Mocks
{
    public class UserManagerMock
    {
        public static UserManager<ApplicationUser> Instance
        {
            get
            {
                var store = new Mock<IUserStore<ApplicationUser>>();
                var mgr = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
                mgr.Object.UserValidators.Add(new UserValidator<ApplicationUser>());
                mgr.Object.PasswordValidators.Add(new PasswordValidator<ApplicationUser>());

                mgr.Setup(x => x.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);
                mgr.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

                return mgr.Object;
            }
        }
    }
}
