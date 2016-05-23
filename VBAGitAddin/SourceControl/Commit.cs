﻿namespace VBAGitAddin.SourceControl
{
    public interface ICommit
    {
        string Message { get; }
        string Author { get; }
        string Id { get; }
    }

    public class Commit : ICommit
    {
        public string Id { get; private set; }
        public string Author { get; private set; }
        public string Message { get; private set; }

        public Commit(string id, string author, string message)
        {
            this.Id = id;
            this.Author = author;
            this.Message = message;
        }

        public Commit(LibGit2Sharp.Commit commit)
            :this(commit.Sha, commit.Author.Name, commit.Message)
        { }
    }
}
