using DTO.Data.Cnotext;
using DTO.Data.Models;
using DTO.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.RoleRepository
{
    public class RoleRepository: GenericRepository<Role> ,IRoleRepository 
    {
        private readonly EddbAppContext RoleCont;
        public RoleRepository(EddbAppContext RoleContext) : base(RoleContext)
        {
            RoleCont = RoleContext;

        }


        public Role GetRoleByName(string Name)
        {
            return  RoleCont.Roles.FirstOrDefault(r => r.Name == Name); ;
        }
    }
}
