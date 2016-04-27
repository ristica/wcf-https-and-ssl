using System;
using System.IdentityModel.Selectors;

namespace Demo.SecurityProvider
{
    public class CustomCredentialsValidator : UserNamePasswordValidator
    {
        public override void Validate(string username, string password)
        {
            if (username == "pingo" && password == "07061971")
            {
                // ...
            }
            else
            {
                throw new UnauthorizedAccessException("not authenticated...");
            }
        }
    }
}
