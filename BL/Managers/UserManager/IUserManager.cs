using BL.ViewModels.CompanyDet;
using BL.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.UserManager
{
    public interface IUserManager
    {
        List<AccountReadVM> GetAllUsers();
        AccountReadVM GetUserById(int id);
        AccountReadVM AddUser(RegisterVM NewUser);
        bool UpdateUser(UpdateUserVM UpdateData);
        AccountReadVM SignIn(LoginVM LogInData);

       
    }
}
