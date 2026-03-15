using DTO.Data.Cnotext;
using DTO.Data.Models;
using DTO.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.UserRepository
{
    public class UserRepository:GenericRepository<User> ,IUserRepository
    {
        private readonly EddbAppContext eddbuserCont;
        public UserRepository (EddbAppContext usercontext) :base (usercontext)
        {
            eddbuserCont = usercontext;
        }

        #region Get User By UserName
        public User GetUserByUserName(string username)
        {
            var DataUser = eddbuserCont.Users.Include(u => u.Roles).FirstOrDefault(x => x.Username == username);

            return DataUser;


        }

        #endregion

    }
}
