using System;

namespace WingsOn.WebApi.Exceptions
{
    public class DomainObjectNotFoundException:Exception
    {
        public DomainObjectNotFoundException(string message) : base(message) { }
        public DomainObjectNotFoundException(string message, Exception exception) : base(message,exception) { }

    }
}