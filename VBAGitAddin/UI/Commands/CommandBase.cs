﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using LibGit2Sharp;
using VBAGitAddin.UI.Extensions;
using Microsoft.Vbe.Interop;
using VBAGitAddin.Git;

namespace VBAGitAddin.UI.Commands
{
    public abstract class CommandBase : IGitCommand
    {
        private BackgroundWorker _bgw;
        private Stopwatch _watch;
        private CommandStatus _status;

        private readonly VBProject _project;
        private GitProvider _provider;

        public CommandBase(VBProject project)
        {
            _project = project;

            _bgw = new BackgroundWorker();
            _bgw.DoWork += _bgw_DoWork;
            _bgw.ProgressChanged += _bgw_ProgressChanged;
            _bgw.RunWorkerCompleted += _bgw_RunWorkerCompleted;
            _bgw.WorkerReportsProgress = true;
            _bgw.WorkerSupportsCancellation = true;

            _watch = new Stopwatch();

            _status = CommandStatus.NotExecuted;
        }

        public VBProject VBProject
        {
            get
            {
                return _project;
            }
        }

        public GitProvider Provider
        {
            get
            {
                return _provider;
            }
            protected set
            {
                if (_provider != null)
                    _provider.Dispose();

                _provider = value;
            }
        }

        public TimeSpan LastExecutionDuration
        {
            get
            {
                return _watch.Elapsed;
            }
        }

        public CommandStatus Status
        {
            get
            {
                return _status;
            }
        }

        public abstract string Name
        {
            get;      
        }

        public virtual Bitmap ProgressImage
        {
            get
            {
                return null;
            }      
        }

        public virtual IRepository Repository
        {
            get
            {
                return _provider?.Repository;
            }
        }

        public event EventHandler CommandAborted;
        public event EventHandler<ErrorEventArgs> CommandFailed;
        public event EventHandler<ProgressEventArgs> CommandProgress;
        public event EventHandler CommandSuccess;

        public virtual void Abort()
        {
            _bgw.CancelAsync();
        }

        public abstract void Execute();      
        protected abstract void OnExectute(DoWorkEventArgs e);

        protected virtual void RunCommandAsync(object argument = null)
        {
            _bgw.RunWorkerAsync(argument);
        }

        protected bool CancellationPending
        {
            get
            {
                return _bgw.CancellationPending;
            }
        }

        protected void ReportProgress(int progress, string info)
        {
            _bgw.ReportProgress(progress, info);
        }
                    
        private void _bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            _status = CommandStatus.InProgress;
            _watch = Stopwatch.StartNew();
            OnExectute(e);
        }

        private void _bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CommandProgress?.Raise(this, new ProgressEventArgs(e.ProgressPercentage, e.UserState.ToString()));
        }

        private void _bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _watch.Stop();

            if (e.Cancelled)
            {
                _status = CommandStatus.Aborted;
                CommandAborted?.Raise(this, new EventArgs());
            }
            else
            {
                if (e.Error != null)
                {
                    _status = CommandStatus.Error;
                    CommandFailed?.Raise(this, new ErrorEventArgs(e.Error));
                }
                else
                {
                    _status = CommandStatus.Success;
                    CommandSuccess?.Raise(this, new EventArgs());
                }
            }
        }

        public void Dispose()
        {
            _bgw.Dispose();

            if (_provider != null)
            {
                _provider.Dispose();
            }
        }
    }
}
