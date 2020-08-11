using Soft98.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soft98.Core.Interfaces
{
    public interface IUser
    {
        User LoginUser(string mobileNumber, string password);

        bool IsMobileNumberExists(string mobileNumber);

        int AddUser(User user);

        bool ActiveUser(string activeCode);

        User ForgetPassword(string mobileNumber);

        bool ResetPassword(string activeCode, string password);
    } // end public interface IUser

} // end namespace Soft98.Core.Interfaces
