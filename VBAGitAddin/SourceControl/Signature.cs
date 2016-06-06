using System;

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

        public static implicit operator LibGit2Sharp.Signature(Signature signature)
        {
            return new LibGit2Sharp.Signature(signature.Name, signature.Email, signature.When);
        }
       
        public string Email { get; private set; }

        public string Name { get; private set; }

        public DateTimeOffset When { get; private set; }
    }
}
