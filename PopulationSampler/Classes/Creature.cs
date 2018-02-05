using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationSampler.Classes
{
    public class Creature
    {
        private int _ID;
        private Boolean _marked;
        private Boolean _selected;

        public int ID { get => _ID; set => _ID = value; }
        public bool Marked { get => _marked; set => _marked = value; }
        public bool Selected { get => _selected; set => _selected = value; }

        public Creature(int ID)
        {
            _ID = ID;
            _marked = false;
            _selected = false;
        }

        override
        public string ToString()
        {
            String creature = "";

            creature = String.Format("ID: [{0}], Marked: [{1}], Selected: [{2}]", ID.ToString(), Marked.ToString(), Selected.ToString());
            return creature;
        }
    }
}
