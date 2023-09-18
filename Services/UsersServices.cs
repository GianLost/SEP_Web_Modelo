using System;
using System.Collections.Generic;
using System.Linq;
using SEP_Web.Database;
using SEP_Web.Models;

namespace SEP_Web.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly SEP_Context _database;

        public UsersServices(SEP_Context database)
        {
            _database = database;
        }

        public Users RegisterUsers(Users users)
        {
            _database.Users.Add(users);
            _database.SaveChanges();
            return users;
        }

        public ICollection<Users> ListUsers()
        {
            ICollection<Users> users = _database.Users.ToList();
            return users;   
        }

        public Users EditUser(Users users)
        {
            Users searchUser = SearchForId(users.Id) ?? throw new Exception("Houve um erro na atualização do usuário");

            searchUser.Masp = users.Masp;
            searchUser.FullName = users.FullName;
            searchUser.Login = users.Login;
            searchUser.Password = users.Password;
            searchUser.Email = users.Email;
            searchUser.Phone = users.Phone;
            searchUser.PublicOffice = users.PublicOffice;
            searchUser.UserType = users.UserType;

            _database.Users.Update(searchUser);
            _database.SaveChanges();

            return searchUser;
        }

        public bool DeleteUser(int id)
        {
            Users searchUser = SearchForId(id) ?? throw new Exception("Houve um erro na exclusão do usuário");

            _database.Users.Remove(searchUser);
            _database.SaveChanges();

            return true;
        }
        
        public Users UserSignIn(string login)
        {
            return _database.Users.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public Users SearchForId(int id)
        {
            return _database.Users.FirstOrDefault(x => x.Id == id);
        }

    }   
}