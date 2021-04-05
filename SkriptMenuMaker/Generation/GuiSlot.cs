using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkriptMenuMaker.Generation
{
    public class GuiSlot
    {
        public string Title { get; set; }
        public string Lore { get; set; }
        public string ItemName { get; set; }
        public Dictionary<string, string> ActionArgs { get; set; }
        public int ActionIndex { get; set; }
        public int Slot { get; set; }

        public GuiSlot(
            string item = "",
            string title = "",
            string lore = "",
            Dictionary<string, string> actionArgs = null,
            int actionIndex = 0,
            int slot = -1)
        {
            Title = title;
            Lore = lore;
            ItemName = item;
            ActionArgs = actionArgs;
            ActionIndex = 0;
            Slot = slot;
        }

        public void Clear()
        {
            Title = "";
            Lore = "";
            ItemName = "";
            ActionArgs = null;
            ActionIndex = 0;
            Slot = -1;
        }
    }
}
