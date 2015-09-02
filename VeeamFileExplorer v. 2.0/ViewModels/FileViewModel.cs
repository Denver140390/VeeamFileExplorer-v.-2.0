using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace VeeamFileExplorer_v._2._0.ViewModels
{
    class FileViewModel : ViewModelBase, IFileSystemEntityViewModel
    {
        private readonly FileInfo _fileInfo;
        
        public string Name => _fileInfo.Name;

        public DateTime LastModifiedTime => _fileInfo.LastWriteTime;

        public string Extension => _fileInfo.Extension;

        public long Size => _fileInfo.Length;

        public string FullPath => _fileInfo.FullName;

        public BitmapSource Icon { get; }

        public FileViewModel(string fullPath)
        {
            _fileInfo = new FileInfo(fullPath);

            var extractedIcon = System.Drawing.Icon.ExtractAssociatedIcon(fullPath);
            if (extractedIcon == null) return;

            var iconBitmap = extractedIcon.ToBitmap();
            Icon = Bitmap2BitmapImage(iconBitmap);
        }

        public void Open()
        {
            Process.Start(FullPath);
        }

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private BitmapSource Bitmap2BitmapImage(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapSource source = null;

            try
            {
                App.Current.Dispatcher.Invoke(() =>
                    source = Imaging.CreateBitmapSourceFromHBitmap(
                        hBitmap,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions())
                    );
            }
            finally
            {
                DeleteObject(hBitmap);
            }

            return source;
        }
    }
}
