using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using NDde.Client;
using System.Runtime.InteropServices;


namespace QrCodeGenerator
{
    class GetBrowserUrl
    {
        public static string GetUrl(string browser) 
        {
            try
            {
                DdeClient dde = new DdeClient(browser, "WWW_GetWindowInfo"); // Using DDE-lib to connect and get information from the firefox browser
                dde.Connect();
                string url = dde.Request("URL", int.MaxValue);
                string[] text = url.Split(new string[] { "\",\"" }, StringSplitOptions.RemoveEmptyEntries);
                dde.Disconnect();
                return text[0].Substring(1);
            }
            catch
            {
                MessageBox.Show("Is your firefox open?", "Error"); // If firefox isnt open
                return null;
            }
        }

        public static Bitmap GenerateQR(int width, int height, string text) // http://www.fluxbytes.com/csharp/how-to-generate-qr-barcodes-in-c/
        {
            var bw = new ZXing.BarcodeWriter();
            var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            var result = new Bitmap(bw.Write(text)); // Create Bitmap with ZXing
            return result;
        }
        public static BitmapImage BitmapToImageSource(Bitmap bitmap) // Converts Bitmap to ImageSource
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
    }
}
