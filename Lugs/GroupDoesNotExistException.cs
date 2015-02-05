using System;

namespace Lugs
{
    public class GroupDoesNotExistException : Exception
    {
        private string groupName;

        public GroupDoesNotExistException(string groupName)
        {
            this.groupName = groupName;
        }

        public override string Message
        {
            get { return string.Format("Group {0} does not exist", groupName); }
        }
    }
}