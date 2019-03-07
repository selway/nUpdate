﻿// Author: Dominic Beger (Trade/ProgTrade) 2017

namespace nUpdate.Administration.ViewModels
{
    /// <summary>
    ///     Represents a view model for a wizard page.
    /// </summary>
    public abstract class WizardPageViewModel : ViewModel
    {
        private bool _canBeShown = true;
        private bool _canGoBack;
        private bool _canGoForward;
        private bool _needsUserInteraction = true;
        
        public bool CanBeShown
        {
            get => _canBeShown;
            set => SetProperty(value, ref _canBeShown);
        }

        public bool CanGoBack
        {
            get => _canGoBack;
            set => SetProperty(value, ref _canGoBack);
        }

        public bool CanGoForward
        {
            get => _canGoForward;
            set => SetProperty(value, ref _canGoForward);
        }

        public bool NeedsUserInteraction
        {
            get => _needsUserInteraction;
            set => SetProperty(value, ref _needsUserInteraction);
        }

        public virtual void OnNavigateBack(WizardViewModel window)
        { }

        public virtual void OnNavigateForward(WizardViewModel window)
        { }

        public virtual void OnNavigated(WizardPageViewModel fromPage, WizardViewModel window)
        { }
    }
}