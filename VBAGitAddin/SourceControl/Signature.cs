using System;
using System.Net.Mail;

namespace VBAGitAddin.SourceControl
{
    public sealed class Signature
    {              
        public Signature(string name, string email, DateTimeOffset when)
        {
            Name = name;
            Email = email;
            When = when;
        }

        public Signature(string mailAddress, DateTimeOffset when)
        {
            try
            {
                MailAddress email = new MailAddress(mailAddress);
                Name = email.DisplayName;
                Email = email.Address;
            }
            catch(FormatException)
            {
                string message = string.Format("author '{0}' is not 'Name <email>' and matches no existing author", mailAddress);
                throw new FormatException(message);
            }

            When = when;
        }

        public static implicit operator LibGit2Sharp.Signature(Signature signature)
        {
            return new LibGit2Sharp.Signature(signature.Name, signature.Email, signature.When);
        }
       
        public string Email { get; private set; }

        public string Name { get; private set; }

        public DateTimeOffset When { get; private set; }
    }
}
