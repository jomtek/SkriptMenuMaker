using System;
using System.Collections.Generic;
using System.IO;
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

namespace SkriptMenuMaker
{
    /// <summary>
    /// Logique d'interaction pour LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
        }

        private void Progress()
        {
            ProgressBarComponent.Value += 15;
        }

        private async void LoadData()
        {
            string itemsPath = $"{AppDomain.CurrentDomain.BaseDirectory}items";
            string actionsPath = $"{AppDomain.CurrentDomain.BaseDirectory}actions.txt";

            // Integrity check
            if (!Directory.Exists(itemsPath))
            {
                MessageBox.Show("PROBLEM: `items` directory is missing !", "SkriptMenuMaker");
                Environment.Exit(1);
            }
            if (!File.Exists(actionsPath))
            {
                MessageBox.Show("PROBLEM: `actions.txt` file is missing !", "SkriptMenuMaker");
                Environment.Exit(1);
            }

            // Load textures
            string[] items = Directory.GetFiles(itemsPath);
            int tick = 0;

            foreach (string item in items)
            {
                string name = Path.GetFileNameWithoutExtension(item);
                var preview = new Controls.GuiItemPreview(item, name);
                
                LoadedData.MinecraftItemsPreviews.Add(name, preview);
                LoadedData.MinecraftItems.Add(name, item);

                ProgressBarComponent.Value += 100d / items.Length;
                ItemNamePreviewTB.Text = name;

                if (tick == 8)
                {
                    await Task.Delay(1);
                    tick = 0;
                }
                tick += 1;
            }

            // Load actions
            foreach (string action in File.ReadAllLines(actionsPath))
            {
                string[] split = action.Split(new string[] { "-WITH-" }, StringSplitOptions.None);
                
                string content = split[0];
                var arguments = new List<string>();

                if (split.Length == 2)
                {
                    foreach (string arg in split[1].Split(','))
                    {
                        arguments.Add(arg.Trim());
                    }
                }

                LoadedData.Actions.Add(new Action(content, arguments));
            }

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
