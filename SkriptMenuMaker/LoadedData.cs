using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkriptMenuMaker
{
    public static class LoadedData
    {
        public static Dictionary<string, string> MinecraftItems = new Dictionary<string, string>();
        public static Dictionary<string, Controls.GuiItemPreview> MinecraftItemsPreviews = new Dictionary<string, Controls.GuiItemPreview>();
        public static List<Action> Actions = new List<Action>();
    }
}
