using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkriptMenuMaker.Generation
{
    public class GuiMenu
    {
        public string Name { get; set; }
        public List<List<GuiSlot>> Lines { get; set; }

        public GuiMenu()
        {
            Lines = new List<List<GuiSlot>>();
        }

        public void AddNewLine()
        {
            var line = new List<GuiSlot>();
            for (int i = 0; i < 9; i++)
                line.Add(new GuiSlot());
            Lines.Add(line);
        }

        public void RemoveLastLine()
        {
            Lines.RemoveAt(Lines.Count - 1);
        }
    }
}
