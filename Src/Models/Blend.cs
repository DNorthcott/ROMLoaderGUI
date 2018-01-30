using System;
using System.Collections.Generic;

namespace ROMLoader.Models
{
    /// <summary>
    ///     A blend is the cycle in which coals are to be feed into the coal bin.
    ///     Contains methods to get the current coal in the cycle and to move the 
    ///     cycle forwards.
    /// </summary>
    public class Blend : IComparable
    {
        // Points to the index of the current coal in the blend cycle.
        private int index;

        /// <summary>
        /// Creates a new blend. Sets the index to -1 as the cycle has not started yet.
        /// </summary>
        public Blend()
        {
            index = -1;
        }

        /// <summary>
        /// Used to create the blend in a testing environment.
        /// </summary>
        public Blend(string dateOfBlend, int priority, string coal1, string coal2, string coal3, string coal4,
            string coal5, string coal6, string coal7, string coal8, string coal9, string coal10) : this()
        {
            DateOfBlend = dateOfBlend;
            Priority = priority;
            Coal1 = coal1;
            Coal2 = coal2;
            Coal3 = coal3;
            Coal4 = coal4;
            Coal5 = coal5;
            Coal6 = coal6;
            Coal7 = coal7;
            Coal8 = coal8;
            Coal9 = coal9;
            Coal10 = coal10;

        }


        /// <summary>
        ///     Returns the next coal in the cycle.  If the cycle has
        ///     not been started, will return the first item in the cycle.
        /// </summary>
        /// <returns></returns>
        public string GetNextCoal()
        {
            index++;

            return GetCurrentCoal();
        }

        /// <summary>
        ///     Gets the current coal in the cycle.  If the cycle has not
        ///     been started will return the first item in the cycle.
        /// </summary>
        /// <returns></returns>
        public string GetCurrentCoal()
        {
            if (index == 0 || index == -1)
            {
                return Cycle[0];
            }
            return Cycle[index % Cycle.Count];
        }

        /// <summary>
        /// Returns the int value for the current index.
        /// </summary>
        public int GetCoalIndex()
        {
            if (index == 0 || index == -1)
            {
                return 0;
            }
            return index % Cycle.Count;
        }

        /// <summary>
        /// Resets the cycle back to original state from constructor.
        /// </summary>
        public void ResetCycle()
        {
            index = -1;
        }

        /// <summary>
        /// Adds a coal to the blend cycle.
        /// </summary>
        /// <param name="coal">The string name of the coal to be added.</param>
        private void AddCoalToCycle(string coal)
        {
            if (coal == null)
            {
                return;
            }
            if (Cycle == null)
            {
                Cycle = new List<string>();
            }

            Cycle.Add(coal);
        }

        /// <summary>
        /// Compares a blend to another blend.
        /// Blends are only compared based on priority values.  The higher
        /// The lower priority ints preceed higher priority ints.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Blend otherBlend = (Blend) obj;

            if (Priority > otherBlend.Priority)
                return 1;
            if (Priority == otherBlend.Priority)
                return 0;
            return -1;
        }

        /// <summary>
        ///     A blend is considered equal when the date, priority and cycle is the same.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;

            Blend otherBlend = (Blend) obj;

            List<string> otherCycle = otherBlend.Cycle;

            if (DateOfBlend.Equals(otherBlend.DateOfBlend) && Priority == otherBlend.Priority && otherCycle.Count == Cycle.Count)
            {
                    for (int i = 0; i < Cycle.Count; i++)
                    {
                        if (otherCycle[i] != Cycle[i])
                        {
                            return false;
                        }
                    }
                    return true;
            }
            return false;
        }

        //--------------------------------------------------
        //Properties 
        //--------------------------------------------------
        public string DateOfBlend { get; set; }

        public int Priority { get; set; }

        public string Coal1
        {
            set => AddCoalToCycle(value);
        }

        public string Coal2
        {
            set => AddCoalToCycle(value);
        }

        public string Coal3
        {
            set => AddCoalToCycle(value);
        }

        public string Coal4
        {
            set => AddCoalToCycle(value);
        }

        public string Coal5
        {
            set => AddCoalToCycle(value);
        }

        public string Coal6
        {
            set => AddCoalToCycle(value);
        }

        public string Coal7
        {
            set => AddCoalToCycle(value);
        }

        public string Coal8
        {
            set => AddCoalToCycle(value);
        }

        public string Coal9
        {
            set => AddCoalToCycle(value);
        }

        public string Coal10
        {
            set => AddCoalToCycle(value);
        }

        public List<string> Cycle { get; private set; }
    }
}