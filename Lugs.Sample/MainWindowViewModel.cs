using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Lugs.Sample
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _groupFeedback;
        private string _groupName;
        private string _userFeedback;
        private string _userName;
        private string _isInGroupFeedback;
        private string _addToGroupFeedback;
        private string _createUserFeedback;
        private string _getUsersInGroupFeedback;
        private string _password;
        private string _passwordFeedBack;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            _groupFeedback = string.Empty;
            _userFeedback = string.Empty;
            _groupName = string.Empty;
            _userName = string.Empty;
            _isInGroupFeedback = string.Empty;
            _addToGroupFeedback = string.Empty;
            _createUserFeedback = string.Empty;
            _getUsersInGroupFeedback = string.Empty;
            _password = string.Empty;
            _passwordFeedBack = string.Empty;

            TestIsInGroupCommand = new DelegateCommandHandler(TestIsInGroup);
            AddUserToGroupCommand = new DelegateCommandHandler(AddUserToGroup);
            CreateUserCommand = new DelegateCommandHandler(CreateUser);
            UserExistsCommand = new DelegateCommandHandler(CheckUserName);
            GroupExistsCommand = new DelegateCommandHandler(CheckGroupName);
            GetUsersInGroupCommand = new DelegateCommandHandler(GetUsersInGroup);
            SetPasswordCommand = new DelegateCommandHandler(SetPassword);
        }


        public ICommand SetPasswordCommand { get; set; }
        public ICommand TestIsInGroupCommand { get; set; }
        public ICommand GetUsersInGroupCommand { get; set; }
        public ICommand AddUserToGroupCommand { get; set; }
        public ICommand CreateUserCommand { get; set; }
        public ICommand GroupExistsCommand { get; set; }
        public ICommand UserExistsCommand { get; set; }

        private void SetPassword()
        {
            var user = new UserInfo(_userName);
            user.SetPassword(_password);
            PasswordFeedBack = "Done";
        }

        protected string PasswordFeedBack
        {
            get { return _passwordFeedBack; }
            set
            {
                if (_passwordFeedBack.Equals(value)) return;

                _passwordFeedBack = value;
                OnPropertyChanged();
            }
        }

        private void GetUsersInGroup()
        {
            var group = new GroupInfo(_groupName);
            var users = group.GetMembers();
            GetUsersInGroupFeedback = string.Join(", ", from u in users select u.Name);
        }

        private void CreateUser()
        {
            var user = new UserInfo(_userName);
            user.Create("password");
            CreateUserFeedback = "Done";
        }

        private void AddUserToGroup()
        {
            AddToGroupFeedback = "Adding";
            var user = new UserInfo(_userName);
            var group = new GroupInfo(_groupName);
            group.AddUser(user);
            AddToGroupFeedback = "Done";
        }

        private void TestIsInGroup()
        {
            var user = new UserInfo(_userName);
            var group = new GroupInfo(_groupName);
            IsInGroupFeedback = group.ContainsUser(user) ? "The user is in the group" : "The user is not in the group";
        }

        private void CheckUserName()
        {
            var userInfo = new UserInfo(_userName);
            UserFeedBack = userInfo.Exists ? "Exists" : "Does not exist";
        }

        private void CheckGroupName()
        {
            var gi = new GroupInfo(_groupName);
            GroupFeedBack = gi.Exists ? "Exists" : "Does not exist";
        }

        public string GetUsersInGroupFeedback
        {
            get { return _getUsersInGroupFeedback; }
            set
            {
                if (_getUsersInGroupFeedback.Equals(value)) return;

                _getUsersInGroupFeedback = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password.Equals(value)) return;

                _password = value;
                OnPropertyChanged();
            }
        }


        public string AddToGroupFeedback
        {
            get { return _addToGroupFeedback; }
            set
            {
                if (_addToGroupFeedback.Equals(value)) return;

                _addToGroupFeedback = value;
                OnPropertyChanged();
            }
        }

        public string IsInGroupFeedback
        {
            get { return _isInGroupFeedback; }
            set
            {
                if (_isInGroupFeedback.Equals(value)) return;

                _isInGroupFeedback = value;
                OnPropertyChanged();
            }
        }
        
        public string CreateUserFeedback
        {
            get { return _createUserFeedback; }
            set
            {
                if (_createUserFeedback.Equals(value)) return;

                _createUserFeedback = value;
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GroupFeedBack
        {
            get
            {
                return _groupFeedback;
            }
            set
            {
                if (_groupFeedback.Equals(value)) return;

                _groupFeedback = value;
                OnPropertyChanged();
            }
        }

        public string GroupName
        {
            get { return _groupName; }
            set
            {
                if (_groupName.Equals(value)) return;

                _groupName = value;
                OnPropertyChanged();
            }
        }

        public string UserFeedBack
        {
            get
            {
                return _userFeedback;
            }
            set
            {
                if (_userFeedback.Equals(value)) return;

                _userFeedback = value;
                OnPropertyChanged();
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName.Equals(value)) return;

                _userName = value;
                OnPropertyChanged();
            }
        }


    }
}
