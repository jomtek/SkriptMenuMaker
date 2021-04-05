using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkriptMenuMaker.Generation
{
    public class ScriptGenerator
    {
        private GuiMenu _menu;
        public string Code { get; private set; } = "";

        public ScriptGenerator(GuiMenu menu)
        {
            _menu = menu;
        }

        public void AddLine(string line)
        {
            Code += "\t\t" + line + '\n';
        }

        public string BuildActionString(int index, Dictionary<string, string> args)
        {
            string actionText = LoadedData.Actions[index].Content;
            if (args != null)
            {
                foreach (KeyValuePair<string, string> arg in args)
                {
                    actionText = actionText.Replace($"{{{arg.Key}}}", arg.Value);
                }
            }
            return actionText;
        }

        public void Generate()
        {
            var lines = _menu.Lines;
            AddLine($"open virtual chest inventory with size {lines.Count} named \"{_menu.Name}\" to player");

            int slotIndex = 0;
            foreach (List<GuiSlot> slots in lines)
            {
                foreach (var slot in slots)
                {
                    if (slot.ItemName != "")
                    {
                        var line = new StringBuilder();
                        line.Append($"format gui slot {slotIndex} of player with a {slot.ItemName.Replace('_', ' ')} ");
                        if (slot.Title != "") line.Append($"named \"{slot.Title}\" ");
                        if (slot.Lore != "") line.Append($"with lore \"{slot.Lore}\" ");
                        line.Append(BuildActionString(slot.ActionIndex, slot.ActionArgs));

                        AddLine(line.ToString());
                    }

                    slotIndex += 1;
                }
            }
        }
    }
}
