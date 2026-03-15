using DTO.Data.Cnotext;
using DTO.Data.Models;
using DTO.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.UserRoleRepository
{
    public class UserRoleRepository:GenericRepository<UserRole> , IUserRoleRepository
    {
        private readonly EddbAppContext UserRollCon;
        public UserRoleRepository(EddbAppContext eddbAppCont):base(eddbAppCont)
        {
            UserRollCon = eddbAppCont;
        }

        
    }

}
