using LibGit2Sharp;
using System;

namespace VBAGitAddin.Git.Extensions
{
    public static class RepositoryExtensions
    {
        public static string GetBranchDescription(this IRepository repository, string branch)
        {
            var key = string.Format("branch.{0}.description", branch);
            return  repository.Config.Get<string>(key)?.Value;
        }

        public static void SetBranchDescription(this IRepository repository, string branch, string description)
        {
            var key = string.Format("branch.{0}.description", branch);
            if (string.IsNullOrWhiteSpace(description))
            {
                repository.Config.Unset(key);
            }
            else
            {
                description.Trim();
                repository.Config.Set(key, description.Replace(Environment.NewLine, string.Empty));
            }
        }
    }
}
