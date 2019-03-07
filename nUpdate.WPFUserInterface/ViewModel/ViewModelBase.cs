﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using nUpdate.WPFUserInterface.Annotations;

// ReSharper disable once CheckNamespace
namespace nUpdate.WPFUserInterface.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private static readonly List<string> HostProcesses = new List<string> {"XDesProc", "devenv", "WDExpress"};

        private bool _disposedValue;

        public bool IsInDesignMode
        {
            get
            {
                var ret = HostProcesses.Contains(Process.GetCurrentProcess().ProcessName);
                return ret;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
                if (disposing)
                {
                }

            _disposedValue = true;
        }
    }
}