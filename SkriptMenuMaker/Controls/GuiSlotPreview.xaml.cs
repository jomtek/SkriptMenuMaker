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
    /// Logique d'interaction pour GuiSlot.xaml
    /// </summary>
    public partial class GuiSlotPreview : UserControl
    {
        private Generation.GuiSlot AssociatedSlot;
        public GuiSlotPreview(Generation.GuiSlot slot)
        {
            InitializeComponent();

            AssociatedSlot = slot;

            this.DataContext = AssociatedSlot;
            this.SetValue(ToolTipService.IsEnabledProperty, false);
            this.SetValue(ToolTipService.InitialShowDelayProperty, 0);

            PreviewSlot();
        }

        private void PreviewSlot()
        {
            if (AssociatedSlot.ItemName.Length > 0)
            {
                ItemPreviewIMG.Source =
                    new BitmapImage(new Uri(LoadedData.MinecraftItems[AssociatedSlot.ItemName], UriKind.Absolute));
                CM_RemoveItem_Item.IsEnabled = true;
                CM_SetADesc_Item.IsEnabled = true;
                CM_SetAnAction_Item.IsEnabled = true;
            }
            else
            {
                ItemPreviewIMG.Source = null;
                CM_RemoveItem_Item.IsEnabled = false;
                CM_SetADesc_Item.IsEnabled = false;
                CM_SetAnAction_Item.IsEnabled = false;
            }
        }

        #region UI
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            GridComponent.Background = (SolidColorBrush)FindResource("ActiveBrush");
            ItemPreviewIMG.Opacity = .7;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            GridComponent.Background = (SolidColorBrush)FindResource("IdleBrush");
            ItemPreviewIMG.Opacity = 1;
        }
        #endregion

        #region Tooltip
        private DependencyObject GetParentWindow()
        {
            DependencyObject ucParent = this.Parent;

            while (!(ucParent is Window))
            {
                ucParent = LogicalTreeHelper.GetParent(ucParent);
            }

            return ucParent;
        }

        private void CM_SetAnItem_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = (MainWindow)GetParentWindow();

            var window = new ItemSearchWindow() { Owner = parentWindow };
            window.ItemClicked += (object _, string item) =>
            {
                AssociatedSlot.ItemName = item;
                PreviewSlot();
            };

            parentWindow.WindowBlurEffect.Radius = 3;
            window.Show();
        }

        private void CM_RemoveTheItem_Click(object sender, RoutedEventArgs e)
        {
            AssociatedSlot.Clear();
            PreviewSlot();
            this.SetValue(ToolTipService.IsEnabledProperty, false);
        }

        private void CM_SetADesc_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = (MainWindow)GetParentWindow();

            parentWindow.WindowBlurEffect.Radius = 3;

            var window = new ItemDescConfigWindow(AssociatedSlot) { Owner = parentWindow };
            window.Show();

            this.DataContext = AssociatedSlot;

            if (AssociatedSlot.Title.Length > 0 || AssociatedSlot.Lore.Length > 0) // Description entered
            {
                CM_RemoveDesc_Item.IsEnabled = true;
                this.SetValue(ToolTipService.IsEnabledProperty, true);
            }
            else
            {
                this.SetValue(ToolTipService.IsEnabledProperty, false);
            }
        }

        private void CM_RemoveDesc_Click(object sender, RoutedEventArgs e)
        {
            AssociatedSlot.Title = "";
            AssociatedSlot.Lore = "";
            CM_RemoveDesc_Item.IsEnabled = false;
            this.SetValue(ToolTipService.IsEnabledProperty, false);
        }

        private void CM_SetAnAction_Click(object sender, RoutedEventArgs e)
        {
            var parentWindow = (MainWindow)GetParentWindow();

            var window =
                new ItemActionConfigWindow(AssociatedSlot) { Owner = parentWindow };

            parentWindow.WindowBlurEffect.Radius = 3;
            window.Show();
        }

        private void CM_RemoveAction_Click(object sender, RoutedEventArgs e)
        {
            AssociatedSlot.ActionIndex = 0;
            AssociatedSlot.ActionArgs = null;
            CM_RemoveAction_Item.IsEnabled = false;
        }
        #endregion
    }
}