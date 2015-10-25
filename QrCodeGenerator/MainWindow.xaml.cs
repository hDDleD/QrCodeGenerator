using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace QrCodeGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = SystemParameters.PrimaryScreenHeight - this.Height - 40; // Set window to bottom left corner of screen
            Bitmap bm = GetBrowserUrl.GenerateQR(200, 200, "www.test.com");  // Generate QRcode with ZXing
            QR.Source = GetBrowserUrl.BitmapToImageSource(bm); // Convert Bitmap to ImageSource and set it to the programs image
            QRText.Text = "www.test.com"; 
        }

        private void Firefox_Button_Click(object sender, RoutedEventArgs e) // Clicking the firefox buttons retrieves url from firefox browser and generates QRcode from that with ZXing
        {
            string url = GetBrowserUrl.GetUrl("firefox");
            if(url != null)
            {
                Bitmap bm = GetBrowserUrl.GenerateQR(200, 200, url);
                QR.Source = GetBrowserUrl.BitmapToImageSource(bm);
                QRText.Text = url;
            }
        }
        private void Text_Button_Click(object sender, RoutedEventArgs e) // Clicking the other buttons creates a QRCode from textbox string, if textbox is empty there is an error box
        {
            if(BoxText.Text.Length > 0)
            {
                Bitmap bm = GetBrowserUrl.GenerateQR(200, 200, BoxText.Text);
                QR.Source = GetBrowserUrl.BitmapToImageSource(bm);
                QRText.Text = BoxText.Text;
                BoxText.Text = null;
            }
            else
            {
                MessageBox.Show("No text in textbox", "Error");
            }
        }

        private void BoxText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (BoxText.Text.Length > 0)
                {
                    Bitmap bm = GetBrowserUrl.GenerateQR(200, 200, BoxText.Text);
                    QR.Source = GetBrowserUrl.BitmapToImageSource(bm);
                    QRText.Text = BoxText.Text;
                    BoxText.Text = null;
                }
                else
                {
                    MessageBox.Show("No text in textbox", "Error");
                }
            }
        }
            
    }
}
