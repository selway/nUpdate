﻿using System;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;
using nUpdate.Internal.Core;
using nUpdate.Internal.Core.Localization;
using nUpdate.Updating;
using nUpdate.WPFUserInterface.ServiceInterfaces;
using nUpdate.WPFUserInterface.ViewModel.Interfaces;

// ReSharper disable once CheckNamespace
namespace nUpdate.WPFUserInterface.ViewModel
{
    public class UpdateSearchViewModel : UpdateUiBaseViewModel, IDialogViewModel
    {
        public UpdateSearchViewModel(UpdateManager manager)
        {
            UpdateManager = manager;
            LocProperties = LocalizationHelper.GetLocalizationProperties(UpdateManager.LanguageCulture,
                UpdateManager.CultureFilePaths);

            WindowIcon = GetIcon(IconHelper.ExtractAssociatedIcon(Application.ExecutablePath));
            WindowTitle = LocProperties.UpdateSearchDialogHeader;

            Abort = new RelayCommand(o => Abort_Execute(), o => true);
        }

        internal bool UpdatesFound { get; set; }


        internal bool HasError { get; set; }
        public LocalizationProperties LocProperties { get; }


        public ICommand Abort { get; }
        public bool DialogResult { get; set; }
        public string WindowTitle { get; set; }
        public Dispatcher CurrentDispatcher { get; set; }


        public async void DialogLoaded()
        {
            try
            {
                UpdatesFound = await UpdateManager.SearchForUpdatesAsync();
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception ex)
            {
                var msgService = ServiceContainer.Instance.GetService<IMessageboxService>();
                msgService.Show(ex.Message, LocProperties.UpdateSearchErrorCaption, EnuMessageBoxButton.Ok,
                    EnuMessageBoxImage.Error);

                UpdatesFound = false;
                HasError = true;
            }

            var dialogService = ServiceContainer.Instance.GetService<IDialogWindowService>();
            dialogService.CloseDialog();
        }

        public void DialogClosing()
        {
            DialogResult = UpdatesFound;
            if (!DialogResult) UpdateManager.CancelSearchAsync();
        }

        private void Abort_Execute()
        {
            UpdatesFound = false;
            var dialogService = ServiceContainer.Instance.GetService<IDialogWindowService>();
            dialogService.CloseDialog();
        }
    }
}