using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_Elmir_Bekric
{
    internal class Weapon
    {
        public ItemType Type { get; set; }
        public int Damage { get; set; }
        public bool IsLegendary { get; set; }
    }
}
