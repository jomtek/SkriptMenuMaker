using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkriptMenuMaker
{
    public class Action
    {
        public string Content { get; private set; }
        public List<string> Arguments { get; private set; }

        public Action(string content, List<string> arguments)
        {
            Content = content;
            Arguments = arguments;
        }
    }
}
