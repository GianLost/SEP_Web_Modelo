using System.Collections.Generic;
using SEP_Web.Models;

namespace SEP_Web.Services
{
    public interface IUsersServices
    {
        Users SearchForId(int id);
        Users RegisterUsers(Users users);
        ICollection<Users> ListUsers();
        Users EditUser(Users users);
        bool DeleteUser(int id);
        Users UserSignIn(string login);
    }
}