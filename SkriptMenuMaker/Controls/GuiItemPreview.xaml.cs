using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkriptMenuMaker.Controls
{
    /// <summary>
    /// Logique d'interaction pour GuiItemPreview.xaml
    /// </summary>
    public partial class GuiItemPreview : UserControl
    {
        public GuiItemPreview(string imageSource, string name)
        {
            InitializeComponent();
            ItemImage.Source = new BitmapImage(new Uri(imageSource, UriKind.Absolute));
            ItemName.Text = name;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            UCBorder.BorderBrush = Brushes.DeepSkyBlue;
            ItemDropShadow.Opacity = .5;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            UCBorder.BorderBrush = Brushes.LightGray;
            ItemDropShadow.Opacity = 0;
        }
    }
}
