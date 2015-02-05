using System;

namespace Lugs
{
    public class UserAlreadyExistsException : Exception
    {
        private string name;

        public UserAlreadyExistsException(string name)
        {
            this.name = name;
        }

        public override string Message
        {
            get { return String.Format("User {0} already exists"); }
        }
    }
}