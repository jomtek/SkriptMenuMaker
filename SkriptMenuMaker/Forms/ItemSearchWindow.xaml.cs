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
using System.IO;
using System.ComponentModel;

namespace SkriptMenuMaker
{
    /// <summary>
    /// Logique d'interaction pour ItemSearchWindow.xaml
    /// </summary>
    public partial class ItemSearchWindow : Window
    {
        private bool _exitMode = false;

        #region Events
        [Category("SkriptMenuMaker")]
        public event EventHandler<string> ItemClicked;
        #endregion

        public ItemSearchWindow()
        {
            InitializeComponent();
            LoadItems();
        }

        private void LoadItems()
        {
            foreach (var preview in LoadedData.MinecraftItemsPreviews.Values)
            {
                preview.PreviewMouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
                {
                    if (!_exitMode)
                    {
                        ItemClicked?.Invoke(this, preview.ItemName.Text);
                        _exitMode = true;
                        ((MainWindow)Owner).WindowBlurEffect.Radius = 0;
                        this.Close();
                    }
                };

                ItemsSP.Children.Add(preview);
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            ItemsSP.Children.Clear();
            ((MainWindow)Owner).WindowBlurEffect.Radius = 0;
            if (!_exitMode) this.Close();
        }
    }
}
