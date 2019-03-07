﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using nUpdate.Updating;

// ReSharper disable once CheckNamespace
namespace nUpdate.WPFUserInterface.ViewModel
{
    public class UpdateUiBaseViewModel : ViewModelBase
    {
        internal UpdateManager UpdateManager { get; set; }

        public BitmapImage WindowIcon { get; set; }


        internal BitmapImage GetIcon(Icon icon)
        {
            using (var memory = new MemoryStream())
            {
                var bitmap = icon.ToBitmap();
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}