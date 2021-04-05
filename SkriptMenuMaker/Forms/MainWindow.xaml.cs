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
using SkriptMenuMaker.Generation;

namespace SkriptMenuMaker
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GuiMenu _menu;

        public MainWindow()
        {
            InitializeComponent();
            
            _menu = new GuiMenu();
            _menu.AddNewLine();
            _menu.AddNewLine();
            _menu.AddNewLine();

            RenderMenuPreview();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PreviewMenuTitleTB != null)
            {
                PreviewMenuTitleTB.Text = MenuTitleTB.Text;
            }
        }

        private void RenderMenuPreview()
        {
            SlotLinesSP.Children.Clear();

            foreach (List<GuiSlot> slots in _menu.Lines)
            {
                // Building the StackPanel containing the slots
                var slotsSp = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Width = 648, Height = 72
                };

                foreach (GuiSlot slot in slots)
                {
                    var previewSlot = new Controls.GuiSlotPreview(slot)
                    {
                        Width = 72, Height = 72
                    };
                    slotsSp.Children.Add(previewSlot);
                }


                // Building the Grid containing the StackPanel and the Image
                var grid = new Grid()
                {
                    Margin = new Thickness(0, -1, 0, 0),
                    Width = 704, Height = 72
                };
                
                var image = new Image()
                {
                    Source = new BitmapImage(new Uri("/SkriptMenuMaker;component/Assets/menu_mid.png", UriKind.Relative))
                };

                grid.Children.Add(image);
                grid.Children.Add(slotsSp);


                // Add the generated grid as a new line among the others
                SlotLinesSP.Children.Add(grid);
            }
        }

        bool valueReset = false;
        private void MenuSizeUPDOWN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (valueReset)
            {
                valueReset = false;
                return;
            }

            if (_menu != null)
            {
                if (MenuSizeUPDOWN.Value > _menu.Lines.Count)
                {
                    // Add a line
                    _menu.AddNewLine();
                }
                else
                {
                    // Remove the last line
                    _menu.Lines.RemoveAt(_menu.Lines.Count - 1);
                }

                RenderMenuPreview();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _menu.Name = MenuTitleTB.Text.Trim();

            var generator = new ScriptGenerator(_menu);
            generator.Generate();

            Clipboard.SetText(generator.Code);

            MessageBox.Show("The generated code has been copied to clipboard !", "SkriptMenuMaker", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void Window_Activated(object sender, EventArgs e)
        {
            if (LoadedData.MinecraftItems.Count == 0)
            {
                await Task.Delay(50);

                WindowBlurEffect.Radius = 4;

                new LoadingWindow() { Owner = this }.ShowDialog();

                WindowBlurEffect.Radius = 0;
            }
        }
    }
}
