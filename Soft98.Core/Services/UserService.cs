using Soft98.Core.Classes;
using Soft98.Core.Interfaces;
using Soft98.DataAccessLayer.Context;
using Soft98.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Soft98.Core.Services
{
    public class UserService : IUser
    {
        Soft98Context _context;

        public UserService(Soft98Context context)
        {
            _context = context;
        } // end constructor UserService

        public User ActiveUser(string activeCode)
        {
            var user = _context.Users.FirstOrDefault(u => u.IsActive == false && u.Code == activeCode);
            if (user != null)
            {
                user.Code = CodeGenerator.ActiveCode();
                user.IsActive = true;
                _context.SaveChanges();
            }
            return user;
        }

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        } // end method AddUser

        public User ForgetPassword(string mobileNumber)
        {
            return _context.Users.FirstOrDefault(u => u.Mobile == mobileNumber);
        }

        public bool IsMobileNumberExists(string mobileNumber)
        {
            return _context.Users.Any(u => u.Mobile == mobileNumber);
        } // end method IsMobileNumberExists

        public User LoginUser(string mobileNumber, string password)
        {
            string hashPassword = HashGenerator.EncodingPassWithMd5(password);

            return _context.Users.FirstOrDefault(u => u.Mobile == mobileNumber && u.Password == hashPassword);
        } // end method LoginUser

        public bool ResetPassword(string activeCode, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Code == activeCode && u.IsActive == true);

            if (user != null)
            {
                string hashpassword = HashGenerator.EncodingPassWithMd5(password);
                user.Password = hashpassword;
                user.Code = CodeGenerator.ActiveCode();
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    } // end public class UserService

} // end namespace Soft98.Core.Services
