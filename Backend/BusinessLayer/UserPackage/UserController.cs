using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
    class UserController
    {
        private Dictionary<string, User> users;
        private DataAccessLayer.DalController UserData;
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private int Online = 0;
        private const int MAX_PASS = 25;
        private const int MIN_PASS = 5;

        public UserController()
        {

            users = new Dictionary<string, User>();
            UserData = new DataAccessLayer.DalController("data.db");
        }

        public void LoadAllUsers()
        {
            if (users.Count != 0)
            {
                logger.Warn("UserController:data has been already loaded");
                throw new Exception("Data Already Loaded!");
            }
            List<DataAccessLayer.User> list = UserData.LoadAllUsers();
            foreach (DataAccessLayer.User u in list)
                users.Add(u.email, new User(u));
            logger.Info("UserController:data has started loading!!!");
        }
        public void Login(string email, string password)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("email is null");
            }
            string tmp = email.ToLower();
            if (!users.ContainsKey(tmp))
            {
                logger.Warn("UserController: this email: " + email + " not found!");
                throw new Exception("email not found");
            }
            if (users[tmp].isloggedin())
            {
                logger.Warn("user is already logged in");
                throw new Exception("this user is already logged in");
            }
            if (Online > 0)
            {
                logger.Warn("cant allow more than 1 user to login at the same time");
                throw new Exception("cant allow more than 1 user to login in at the same time");
            }

            users[tmp].login(tmp, password);
            Online++;
            logger.Info("UserController: user with this email: " + email + "has successfully logged in!");
        }
        public void logout(string email)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("email is null");
            }
            string tmp = email.ToLower();

            if (!users.ContainsKey(tmp))
            {
                logger.Warn("UserController: this email: " + email + " not found!");
                throw new Exception("email does not exist");
            }

            if (!users[tmp].isloggedin())
            {
                logger.Warn("UserController: cant logout if the user is not logged in!");
                throw new Exception("cant logout if the user is not logged in");
            }

            users[tmp].logout();
            Online--;
            logger.Info("UserController: user with this email: " + email + "has successfully logged out!");
        }
        public void validatePassword(string password)
        {
            bool hasNumber = false;
            bool hasUpperCaseLetter = false;
            bool hasLowerCaseLetter = false;
            if (password.Length < MIN_PASS || password.Length > MAX_PASS)
            {
                logger.Warn("password size is less than 4 or more than 20");
                throw new Exception("invalid length");
            }
            for (int i = 0; i < password.Length; i++)
            {
                if (!hasNumber)
                {
                    if (password[i] >= '0' && password[i] <= '9')
                    {
                        hasNumber = true;
                    }
                }
                if (!hasLowerCaseLetter)
                {
                    if (password[i] >= 'a' && password[i] <= 'z')
                    {
                        hasLowerCaseLetter = true;
                    }
                }

                if (!hasUpperCaseLetter)
                {
                    if (password[i] >= 'A' && password[i] <= 'Z')
                    {
                        hasUpperCaseLetter = true;
                    }
                }
            }
            if (!hasLowerCaseLetter)
            {
                logger.Warn("password doesn't have any Lower Case Letter");
                throw new Exception("password doesnt contain any lower case letter");
            }
            if (!hasUpperCaseLetter)
            {
                logger.Warn("password doesn't have any Upper Case Letter");
                throw new Exception("password doesnt contain any upper case letter");
            }
            if (!hasNumber)
            {
                logger.Warn("password doesn't have any number");
                throw new Exception("password doesnt contain any number");
            }
            logger.Info("password fits all requirements and has successfully been used");
        }

        public void ValidateEmail(String email)
        {
            bool valid = false;
            if (email.IndexOf('@') == 0 | email.IndexOf('@') == -1 | email.IndexOf('@') == email.Length - 1)
            {
                logger.Warn("Invalid Email");

                throw new Exception("Invalid Email");
            }
            if (email.IndexOf('.') == 0 | email.IndexOf('.') == -1 | email.IndexOf('.') == email.Length - 1)
            {
                logger.Warn("Invalid Email");

                throw new Exception("Invalid Email");
            }
            if (email[email.IndexOf('@') - 1] == '.')
            {
                logger.Warn("Invalid Email");

                throw new Exception("Invalid Email");
            }
            if (email[email.IndexOf('@') + 1] == '.')
            {
                logger.Warn("Invalid Email");

                throw new Exception("Invalid Email");
            }
            if (email[email.IndexOf('@') + 1] < 'a'|| email[email.IndexOf('@') + 1] > 'z')
            {
                logger.Warn("Invalid Email");

                throw new Exception("Invalid Email");
            }

            for (int i = email.IndexOf('@') + 1; i < email.Length - 1; i = i + 1)
            {
                if (email[i] == '@')
                {
                    logger.Warn("Invalid Email");

                    throw new Exception("Invalid Email");
                }
                else if (email[i] == '.' & email[i + 1] == '.')
                {
                    logger.Warn("Invalid Email");

                    throw new Exception("Invalid Email");
                }
             
                else if (email[i] == '.')
                    valid = true;
            }
            if (!valid)
            {
                logger.Warn("invalid email");
                throw new Exception("Invalid Email");
            }
            logger.Info("email is valid");
        }

        public void Register(string email, string password, string nickname,string emailHost)
        {
            if (email == null || password == null || nickname == null)
            {
                logger.Warn("invalid parameters");
                throw new Exception("Invalid Parmeters!");
            }
            string tmp = email.ToLower();
            ValidateEmail(tmp);
            if (users.ContainsKey(tmp))
            {
                logger.Warn("UserController: attempt to register a new user with this email:" + email + "has failed because the email is already used by someone else.");
                throw new Exception("email already used");
            }
            if (tmp == null)
            {
                logger.Warn("email is null");
                throw new Exception("email is null");
            }

            validatePassword(password);
            if (nickname == null)
            {
                logger.Warn("nickname is null");
                throw new Exception("invalid nickname");
            }
            if (nickname.Length == 0)
            {
                logger.Warn("empty nickname");
                throw new Exception("invalid nickname");
            }
            User user = new User(tmp, password, nickname,emailHost);
            users.Add(tmp, user);
            user.AddToDatabase();
            logger.Info("UserController: a new user has successfully registered with this email:" + email + "!");
        }

        public User getUser(string email)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("email is null");
            }
            string tmp = email.ToLower();

            if (!users.ContainsKey(tmp))
            {
                logger.Warn("UserController: failed to return a user because the given email is not used by any user.");
                throw new Exception("email does not exist");
            }
            logger.Info("UserController; the user with the email:" + email + "has successfully been returned!");
            return users[tmp];
        }

        public void ValidateLoggedIn(string email)
        {
            if (email == null)
            {
                logger.Warn("email is null");
                throw new Exception("email is null");
            }
            string tmp = email.ToLower();

            if (!users.ContainsKey(tmp))
            {
                logger.Warn("UserController: the user with the email:" + email + "is not logged in because he hasn't been registered yet.");
                throw new Exception("no such user!");
            }
            if (!users[tmp].isloggedin())
            {
                logger.Warn("UserController: the user with the email:" + email + "is not logged in at this moment.");
                throw new Exception("user is not logged in");
            }
            logger.Info("UserController: the user with the email:" + email + "is logged in right  now!");
        }
    }
}

