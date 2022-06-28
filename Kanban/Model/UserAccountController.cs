using Kanban.DataAccessLayer.Entities;
using Kanban.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kanban.Model
{
    internal class UserAccountController
    {
        private IEncryptor passwordEncryptor;

        public UserAccountController(IEncryptor passwordEncryptor)
        {
            this.passwordEncryptor = passwordEncryptor;
        }

        public User? CurrentlyLoggedUser
        {
            get => _currentlyLoggedUser;
            private set
            {
                _currentlyLoggedUser = value;
                OnUserChanged?.Invoke(CurrentlyLoggedUser);
            }
        }

        public event Action<User?>? OnUserChanged;

        public void TryLogin(string login, string password)
        {
            var encryptedPassword = passwordEncryptor.Encrypt(password);
            var user = UsersRepository.GetUserFromLoginAndPassword(login, encryptedPassword);

            if (user is null)
            {
                MessageBox.Show("Nie istnieje użytkownik o takim loginie i haśle.");
            }

            CurrentlyLoggedUser = user;
        }

        private User? _currentlyLoggedUser;

        internal void Register(string name, string login, string password)
        {
            if (UsersRepository.CheckIfLoginIsTaken(login))
            {
                MessageBox.Show("Ten login jest zajęty.");
                return;
            }

            var encryptedPassword = passwordEncryptor.Encrypt(password);
            User newUser = new(name, login, encryptedPassword);
            UsersRepository.AddUser(newUser);

            MessageBox.Show($"Pomyślnie utworzono konto dla {name}");
        }
    }
}
