﻿using AutoMapper;
using BL.ViewModels.User;
using DTO.Data.Models;
using DTO.Repositories.CompanyAuditLogRepository;
using DTO.Repositories.RoleRepository;
using DTO.Repositories.UserRepository;
using DTO.Repositories.UserRoleRepository;
using System.Security.Cryptography;
using System.Text;

namespace BL.Managers.UserManager
{
    public class UserManager: IUserManager
    
    {
        private readonly IUserRepository userRepo;
        private readonly ICompanyAuditLogRepository AuditRep;
        private readonly IRoleRepository RoleRepo;
        private readonly IUserRoleRepository URRepo;
        private readonly IMapper Usermapper;
        public UserManager(IUserRepository manag ,ICompanyAuditLogRepository Audit , IRoleRepository Roles ,IUserRoleRepository UserRoleRepo , IMapper mapp) 
        {
            userRepo = manag;
            AuditRep = Audit;
            RoleRepo = Roles;
            URRepo = UserRoleRepo;
            Usermapper = mapp;
        
        }

        #region Get All Users
        public List<AccountReadVM> GetAllUsers()
        {
            var UsersData = userRepo.GetAllEntities();
            return Usermapper.Map<List<AccountReadVM>>(UsersData);
        }
        #endregion

        #region Get User By Id
        public AccountReadVM GetUserById(int userid)
        {
            var User = userRepo.GetEntityById(userid);
            if (User == null)
                return null;
            return Usermapper.Map<AccountReadVM>(User);
        }
        #endregion

        #region Get User By UserName (Sign In)
        public AccountReadVM SignIn(LoginVM loginData)
        {
            if (string.IsNullOrWhiteSpace(loginData.Username))
                return null;

            var user = userRepo.GetUserByUserName(loginData.Username);
            
            if (user == null)
                return null;

            // Check if user is active
            if (user.IsActive != true)
                return null;

            // Verify password
            var passwordHash = HashPassword(loginData.Password);
            if (passwordHash != user.Password)
                return null;

            // Map to AccountReadVM with roles
            return Usermapper.Map<AccountReadVM>(user);
        }
        #endregion

        #region Add User
        public AccountReadVM AddUser(RegisterVM NewUser)
        {
            // Check if username already exists
            var existingUser = userRepo.GetUserByUserName(NewUser.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists. Please choose a different username.");
            }

            var NewUserData = Usermapper.Map<User>(NewUser);

            // Hash password
            NewUserData.Password = HashPassword(NewUser.Password);
            
            // Set user as active
            NewUserData.IsActive = true;
            NewUserData.CreatedDate = DateTime.Now;

            // Assign default "User" role (or "Admin" if it's the first user)
            var userRole = RoleRepo.GetRoleByName("User");
            if (userRole != null)
            {
                NewUserData.Roles.Add(userRole);
            }

            userRepo.Add(NewUserData);
            userRepo.SaveChanges();
            
            return Usermapper.Map<AccountReadVM>(NewUserData);

        }
        #endregion

        public static string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        #region User Update
        public bool UpdateUser(UpdateUserVM UpdateData)
        {
            var UserOldData = userRepo.GetEntityById(UpdateData.Id);
            if(UserOldData == null)
                return false;
            Usermapper.Map(UpdateData , UserOldData);
            userRepo.Update(UserOldData);
            userRepo.SaveChanges();
            return true;
        }
        #endregion
        

    }
}
