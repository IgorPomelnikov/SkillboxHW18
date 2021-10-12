using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillboxHW18
{
    public class Hero
    {
        public Hero()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public Wearpon Wearpon { get; set; }
        public Armor Armor { get; set; }
    }
}
