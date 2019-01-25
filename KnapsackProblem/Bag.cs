using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Refrence : https://rosettacode.org/wiki/Knapsack_problem/0-1#C.23

namespace KnapsackProblem
{
    class Bag : IEnumerable<Bag.Item>
    {
        List<Item> items;
        const int MaxWeightAllowed = 400;

        public Bag()
        {
            items = new List<Item>();
        }

        public void AddItem(Item i)
        {
            items.Add(i);
        }

        #region IEnumerable<Item> Members
        IEnumerator<Item> IEnumerable<Item>.GetEnumerator()
        {
            foreach (Item i in items)
                yield return i;
        }
        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
        #endregion

        public int TotalWeight
        {
            get
            {
                var sum = 0;
                foreach (Item i in this)
                {
                    sum += i.Weight;
                }
                return sum;
            }
        }

        public int TotalValue
        {
            get
            {
                var sum = 0;
                foreach (Item i in this)
                {
                    sum += i.Value;
                }
                return sum;
            }
        }

        public class Item
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public int Value { get; set; }
            public int ResultWV { get { return Weight - Value; } }
            public override string ToString()
            {
                return "Name : " + Name + "        Wieght : " + Weight + "       Value : " + Value;
            }
        }
    }
}
