using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationSampler.Classes
{
    public class Sample
    {
        private int samplesTaken;
        private int lowerLimit = -1;
        private int upperLimit = -1;
        private List<Creature> creatures = new List<Creature>();

        public int SamplesTaken { get => samplesTaken; set => samplesTaken = value; }
        public int LowerLimit
        {
            get
            {
                return lowerLimit;
            }
            set
            {
                if (!(upperLimit < 0))
                {
                    if (value > upperLimit)
                    {
                        throw new Exception("Sample's lower limit can not be greater than the upper limit.");
                    }
                    else
                    {
                        lowerLimit = value;
                    }
                }
                else
                {
                    lowerLimit = value;
                }
            }
        }
        public int UpperLimit
        {
            get
            {
                return upperLimit;
            }
            set
            {
                if (!(lowerLimit < 0))
                {
                    if (value < lowerLimit)
                    {
                        throw new Exception("Sample's upper limit can not be less than the lower limit.");
                    }
                    else
                    {
                        upperLimit = value;
                    }
                }
                else
                {
                    upperLimit = value;
                }
            }
        }
        public List<Creature> Creatures { get => creatures; }

        public Sample(int samplesTaken, int lowerLimit, int upperLimit)
        {
            this.SamplesTaken = samplesTaken;
            this.LowerLimit = lowerLimit;
            this.UpperLimit = upperLimit;
        }

        public int countSelected()
        {
            int count = 0;

            foreach(Creature c in Creatures)
            {
                if (c.Selected)
                {
                    count++;
                }
            }

            return count;
        }

        public int countMarks()
        {
            int count = 0;

            foreach (Creature c in Creatures)
            {
                if (c.Marked)
                {
                    count++;
                }
            }

            return count;
        }

    }
}
