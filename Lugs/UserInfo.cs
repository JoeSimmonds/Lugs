using System;
using System.DirectoryServices.AccountManagement;
using System.Security;

namespace Lugs
{
    public class UserInfo
    {
        public UserInfo(string name)
        {
            Name = name;
        }

        public String Name { get; private set; }

        public bool Exists
        {
            get
            {
                var context = new PrincipalContext(ContextType.Machine);
                var user = UserPrincipal.FindByIdentity(context, Name);
                try
                {
                    return user != null;
                }
                finally
                {
                    context.Dispose();
                    if (user != null) user.Dispose();
                }
            }
        }

        public bool IsInGroup(GroupInfo group)
        {
            return group.ContainsUser(this);

        }

        public void SetPassword(string password)
        {
            var context = new PrincipalContext(ContextType.Machine);
            var user = UserPrincipal.FindByIdentity(context, Name);
            try
            {
                if (user == null)
                {
                    throw new UserDoesNotExistException(Name);
                }
                user.SetPassword(password);
                user.Save();
            }
            finally
            {
                context.Dispose();
                if (user != null) user.Dispose();
            }
        }

        public void Create(string password)
        {
            if (Exists)
            {
                throw new UserAlreadyExistsException(Name);
            }
            
            var context = new PrincipalContext(ContextType.Machine);
            var user = new UserPrincipal(context);
            try
            {
                user.Name = Name;
                user.SetPassword(password);
                user.PasswordNeverExpires = true;
                user.Save();
            }
            finally
            {
                context.Dispose();
                user.Dispose();
            }

        }
    }
}
