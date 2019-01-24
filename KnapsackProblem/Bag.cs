using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        void AddItem(Item i)
        {
            if ((TotalWeight + i.Weight) <= MaxWeightAllowed)
                items.Add(i);
        }

        public void Calculate(List<Item> items)
        {
            foreach (Item i in Sorte(items))
            {
                AddItem(i);
            }
        }

        List<Item> Sorte(List<Item> inputItems)
        {
            List<Item> choosenItems = new List<Item>();
            for (int i = 0; i < inputItems.Count; i++)
            {
                int j = -1;
                if (i == 0)
                {
                    choosenItems.Add(inputItems[i]);
                }
                if (i > 0)
                {
                    if (!RecursiveF(inputItems, choosenItems, i, choosenItems.Count - 1, false, ref j))
                    {
                        choosenItems.Add(inputItems[i]);
                    }
                }
            }
            return choosenItems;
        }

        bool RecursiveF(List<Item> knapsackItems, List<Item> choosenItems, int i, int lastBound, bool dec, ref int indxToAdd)
        {
            if (!(lastBound < 0))
            {
                if (knapsackItems[i].ResultWV < choosenItems[lastBound].ResultWV)
                {
                    indxToAdd = lastBound;
                }
                return RecursiveF(knapsackItems, choosenItems, i, lastBound - 1, true, ref indxToAdd);
            }
            if (indxToAdd > -1)
            {
                choosenItems.Insert(indxToAdd, knapsackItems[i]);
                return true;
            }
            return false;
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

        public class Item
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public int Value { get; set; }
            public int ResultWV { get { return Weight - Value; } }
            public override string ToString()
            {
                return "Name : " + Name + "        Wieght : " + Weight + "       Value : " + Value + "     ResultWV : " + ResultWV;
            }
        }
    }
}
