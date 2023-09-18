using SEP_Web.Models;

namespace SEP_Web.Helper
{
    public interface IUsersSession
    {
        void UserCheckIn(Users users);
        void UserCheckOut();
        Users SearchUserSession();
    }
}