using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace Lugs
{
    public class GroupInfo
    {
        public GroupInfo(string groupName)
        {
            Name = groupName;
        }

        public String Name { get; private set; }

        public bool Exists
        {
            get
            {
                var context = new PrincipalContext(ContextType.Machine);
                var group = GroupPrincipal.FindByIdentity(context, Name);
                try
                {
                    return group != null;
                }
                finally
                {
                    context.Dispose();
                    if (group != null) group.Dispose();
                }
            }
        }

        public IList<UserInfo> GetMembers()
        {
            var context = new PrincipalContext(ContextType.Machine);
            var group = GroupPrincipal.FindByIdentity(context, Name);
            try
            {
                var results = from p in @group.Members 
                                where p is UserPrincipal
                                select new UserInfo(p.Name);
                return results.ToList();
            }
            finally
            {
                context.Dispose();
                if (group != null) group.Dispose();
            }
        }

        public bool ContainsUser(UserInfo userInfo)
        {
            if (!Exists)
            {
                throw new GroupDoesNotExistException(Name);
            }

            if (!userInfo.Exists)
            {
                throw new UserDoesNotExistException(userInfo.Name);
            }

            var context = new PrincipalContext(ContextType.Machine);
            var group = GroupPrincipal.FindByIdentity(context, Name);
            try
            {
                var members = group.Members;
                return members.Any(m => m.Name == userInfo.Name);
            }
            finally
            {
                context.Dispose();
                if (group != null) group.Dispose();
            }
        }

        public void AddUser(UserInfo userInfo)
        {
            if (!Exists)
            {
                throw new GroupDoesNotExistException(Name);
            }

            if (!userInfo.Exists)
            {
                throw new UserDoesNotExistException(userInfo.Name);
            }
            var context = new PrincipalContext(ContextType.Machine);
            var group = GroupPrincipal.FindByIdentity(context, Name);
            var user = UserPrincipal.FindByIdentity(context, userInfo.Name);
            try
            {
                group.Members.Add(user);
                group.Save();
            }
            finally
            {
                context.Dispose();
                if (group != null) group.Dispose();
                if (user != null) user.Dispose();
            }
        }
    }
}