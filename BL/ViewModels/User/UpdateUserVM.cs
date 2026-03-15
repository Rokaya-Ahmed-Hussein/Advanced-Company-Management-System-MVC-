using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.ViewModels.User
{
    public class UpdateUserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? FullName { get; set; }
    }
}
