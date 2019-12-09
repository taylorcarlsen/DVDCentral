using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC.DVDCentral.Data;
using TC.DVDCentral.Models;

namespace TC.DVDCentral.BusinessLogic
{
    public class UserManager : IDisposable
    {
        private readonly DVDCentralContext db;

        public UserManager()
        {
            db = new DVDCentralContext();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Models.User Login(Models.User user)
        {
            //see if the user exists with this username and password
            var userRow = db.Users.SingleOrDefault(x => x.Username.ToLower() == user.Username.ToLower());
            if (userRow == null)
                return null;
            else
            {
                if (userRow.Password == GetHash(user.Password))
                {
                    user.Id = userRow.Id;
                    user.FirstName = userRow.FirstName;
                    user.LastName = userRow.LastName;
                    user.Password = string.Empty; //once returned the string goes empty to not allow the user to see the hashed password
                    return user;
                }
                return null;
            }
        }

        public Models.User Update(Models.User user)
        {
            var userRow = db.Users.SingleOrDefault(x => x.Id == user.Id);
            if (userRow == null)
                return null;

            userRow.FirstName = user.FirstName;
            userRow.LastName = user.LastName;

            if (db.Users.Any(x => x.Username.ToLower() == user.Username.ToLower()))
                throw new ArgumentOutOfRangeException("user.Username", "The username is already taken");
            userRow.Username = user.Username;
            userRow.Password = GetHash(user.Password);

            db.SaveChanges();

            return user;
        }

        public int AddUser(Models.User user)
        {
            var userRow = db.Users.FirstOrDefault(x => x.Username.ToLower() == user.Username.ToLower());
            if (userRow != null)
            {
                throw new ArgumentOutOfRangeException("user.UserName", "user name already exists");
            }
            else
            {
                Data.User newRow = new Data.User();
                newRow.FirstName = user.FirstName;
                newRow.LastName = user.LastName;
                newRow.Username = user.Username;
                //newRow.Password = user.Password;

                //Hash the password
                newRow.Password = GetHash(user.Password);

                db.Users.Add(newRow);
                db.SaveChanges();
                return newRow.Id;

            }

        }

        private string GetHash(string password)
        {
            using (var hasher = new System.Security.Cryptography.SHA1Managed())
            {
                var hashBytes = System.Text.Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(hasher.ComputeHash(hashBytes));
            }
        }
    }
}
