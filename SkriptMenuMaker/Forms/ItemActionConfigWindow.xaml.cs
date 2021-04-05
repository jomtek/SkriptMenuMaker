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
    /// Logique d'interaction pour ItemActionConfigWindow.xaml
    /// </summary>
    public partial class ItemActionConfigWindow : Window
    {
        private Generation.GuiSlot _associatedSlot; 

        private Dictionary<int, List<string>> _argumentsDic;
        private bool _itemsLoaded = false;

        public ItemActionConfigWindow(Generation.GuiSlot slot)
        {
            InitializeComponent();

            _associatedSlot = slot;
            _argumentsDic = new Dictionary<int, List<string>>();

            InitActions();
        }

        private void InitActions()
        {
            for (int i = 0; i < LoadedData.Actions.Count; i++)
            {
                var action = LoadedData.Actions[i];

                var item = new ComboBoxItem() { Content = action.Content };
                ActionCMB.Items.Add(item);

                _argumentsDic.Add(i, action.Arguments);
            }

            _itemsLoaded = true;
            ActionCMB.SelectedIndex = _associatedSlot.ActionIndex;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (_argumentsDic[ActionCMB.SelectedIndex].Count != 0)
            {
                var args = new Dictionary<string, string>();
                foreach (Controls.ActionArgumentPreview item in ArgumentsSP.Children)
                {
                    if (item.GetArgument().Trim().Length > 0)
                    {
                        args.Add(item.ArgumentName, item.GetArgument());
                    }
                    else
                    {
                        return;
                    }
                }
                _associatedSlot.ActionArgs = args;
            }
            else
            {
                _associatedSlot.ActionArgs = null;
            }

            _associatedSlot.ActionIndex = ActionCMB.SelectedIndex;

            ((MainWindow)Owner).WindowBlurEffect.Radius = 0;
            this.Close();
        }

        private void ActionCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_itemsLoaded)
            {
                return;
            }

            NoArgTB.Visibility = Visibility.Collapsed;
            ArgumentsSP.Children.Clear();

            var arguments = _argumentsDic[ActionCMB.SelectedIndex];

            if (arguments.Count == 0)
            {
                NoArgTB.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (string arg in arguments)
                {
                    var preview = new Controls.ActionArgumentPreview(arg)
                    {
                        Width = 600, Height = 55,
                        Margin = new Thickness(0, 10, 0, 0)
                    };

                    if (ActionCMB.SelectedIndex == _associatedSlot.ActionIndex)
                    {
                        preview.SetArgument(_associatedSlot.ActionArgs[preview.ArgumentName]);
                    }

                    ArgumentsSP.Children.Add(preview);
                }
            }
        }
    }
}
