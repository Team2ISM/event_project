using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Business.Interfaces
{
    public interface IUserDataProvider
    {
        IList<User> GetAllUsers();

        string GetFullName(string email);

        User GetById(string id);

        User GetByMail(string mail);

        void CreateUser(User user);

        void UpdateUser(User user);
    }
}
