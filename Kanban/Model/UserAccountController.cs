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
        public User? CurrentlyLoggedUser
        {
            get => _currentlyLoggedUser;
            private set
            {
                _currentlyLoggedUser = value;

                if (CurrentlyLoggedUser is { })
                {
                    OnUserChanged?.Invoke(CurrentlyLoggedUser!);
                }
            }
        }

        public event Action<User>? OnUserChanged;

        public UserAccountController()
        {
            CurrentlyLoggedUser = new("kuba mruz", "srogimrus", "123");
        }

        public void TryLogin(string login, string password)
        {
            var user = UsersRepository.GetUserFromLoginAndPassword(login, password);

            if (user is null)
            {
                MessageBox.Show("Nie istnieje użytkownik o takim loginie i haśle.");
            }

            CurrentlyLoggedUser = user;
        }

        private User? _currentlyLoggedUser;
    }
}
