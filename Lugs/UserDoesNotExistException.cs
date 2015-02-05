using System;

namespace Lugs
{
    public class UserDoesNotExistException : Exception
    {
        private string name;

        public UserDoesNotExistException(string name)
        {
            this.name = name;
        }

        public override string Message
        {
            get { return string.Format("User {0} does not exist", name); }
        }
    }
}