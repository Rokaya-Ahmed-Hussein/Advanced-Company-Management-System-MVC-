using DTO.Data.Models;
using DTO.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.UserRepository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        User GetUserByUserName(string username);
    }
}
