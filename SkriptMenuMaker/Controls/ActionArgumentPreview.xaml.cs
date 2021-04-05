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
    /// Logique d'interaction pour ActionArgumentPreview.xaml
    /// </summary>
    public partial class ActionArgumentPreview : UserControl
    {
        public string ArgumentName { get; private set; }
        public ActionArgumentPreview(string name)
        {
            InitializeComponent();
            ArgumentName = name;
            ArgumentNameTB.Text = name;
        }

        public string GetArgument()
        {
            if (!_placeholderActive)
            {
                return ArgumentTB.Text.Trim();
            }
            else
            {
                return "";
            }
        }

        public void SetArgument(string arg)
        {
            ArgumentTB.Text = arg;
            ArgumentTB.Foreground = Brushes.Black;
            _placeholderActive = false;
        }

        #region Placeholder
        private bool _placeholderActive = true;

        private void ArgumentTB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (_placeholderActive)
            {
                ArgumentTB.Foreground = Brushes.Black;
                ArgumentTB.Clear();
                _placeholderActive = false;
            }   
        }

        private void ArgumentTB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (ArgumentTB.Text.Trim().Length == 0)
            {
                ArgumentTB.Foreground = Brushes.Gray;
                ArgumentTB.Text = "enter argument here";
                _placeholderActive = true;
            }
        }
        #endregion
    }
}
