using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkriptMenuMaker.Generation
{
    public static class Utils
    {
        public static List<T> PadListWithItem<T>(List<T> list, int desiredSize, T item)
        {
            if (list.Count < desiredSize)
            {
                for (int i = 0; i < desiredSize - list.Count; i++)
                {
                    list.Add(item);
                }
            }

            return list;
        }
    }
}
