using DTO.Data.Models;
using DTO.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Repositories.RoleRepository
{
    public interface IRoleRepository:IGenericRepository<Role>
    {
        Role GetRoleByName(string Name);
    }
}
