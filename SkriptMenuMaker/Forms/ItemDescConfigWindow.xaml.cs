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
using System.Windows.Shapes;

namespace SkriptMenuMaker
{
    /// <summary>
    /// Logique d'interaction pour ItemDescConfigWindow.xaml
    /// </summary>
    public partial class ItemDescConfigWindow : Window
    {
        private Generation.GuiSlot _associatedSlot;

        private bool _namePlaceholderActive = true;
        private bool _lorePlaceholderActive = true;

        public ItemDescConfigWindow(Generation.GuiSlot slot)
        {
            InitializeComponent();

            _associatedSlot = slot;

            ItemPreviewIMG.Source =
                new BitmapImage(new Uri(LoadedData.MinecraftItems[slot.ItemName], UriKind.Absolute));

            if (slot.Title != "")
            {
                NameTB.Text = slot.Title;
                _namePlaceholderActive = false;
                NameTB.Foreground = Brushes.Black;
            }

            if (slot.Lore != "")
            {
                LoreTB.Text = slot.Lore;
                _lorePlaceholderActive = false;
                LoreTB.Foreground = Brushes.Black;
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!_namePlaceholderActive) _associatedSlot.Title = NameTB.Text.Trim();
            if (!_lorePlaceholderActive) _associatedSlot.Lore = LoreTB.Text.Trim();
            ((MainWindow)Owner).WindowBlurEffect.Radius = 0;
            this.Close();
        }

        #region Placeholders
        private void NameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_namePlaceholderActive)
            {
                NameTB.Foreground = Brushes.Black;
                NameTB.Clear();
                _namePlaceholderActive = false;
            }
        }

        private void NameTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NameTB.Text.Trim().Length == 0)
            {
                NameTB.Foreground = Brushes.Gray;
                NameTB.Text = "Name";
                _namePlaceholderActive = true;
            }
        }

        private void LoreTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_lorePlaceholderActive)
            {
                LoreTB.Foreground = Brushes.Black;
                LoreTB.Clear();
                _lorePlaceholderActive = false;
            }
        }

        private void LoreTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (LoreTB.Text.Trim().Length == 0)
            {
                LoreTB.Foreground = Brushes.Gray;
                LoreTB.Text = "Lore";
                _lorePlaceholderActive = true;
            }
        }
        #endregion
    }
}
