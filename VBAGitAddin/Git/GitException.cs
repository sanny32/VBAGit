using System;
using System.Runtime.Serialization;

namespace VBAGitAddin.Git
{
    [Serializable]
    public class GitException : Exception
    {
        public GitException() { }
        public GitException(string message) : base(message) { }
        public GitException(string message, Exception inner) : base(message, inner) { }

        protected GitException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }

}
