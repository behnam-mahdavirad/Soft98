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

        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        } // end method AddUser

        public bool IsMobileNumberExists(string mobileNumber)
        {
            return _context.Users.Any(u => u.Mobile == mobileNumber);
        } // end method IsMobileNumberExists

        public User LoginUser(string mobileNumber, string password)
        {
            string hashPassword = HashGenerator.EncodingPassWithMd5(password);

            return _context.Users.FirstOrDefault(u => u.Mobile == mobileNumber && u.Password == hashPassword);
        } // end method LoginUser

    } // end public class UserService

} // end namespace Soft98.Core.Services
