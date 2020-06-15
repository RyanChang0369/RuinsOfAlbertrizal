using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal
{
    public class RoundBasedItemKeeper
    {
        public List<List<IRoundBasedObject>> RoundBasedObjects_List { get; set; }

        public List<IRoundBasedObject[]> RoundBasedObjects_Array { get; set; }

        public List<IRoundBasedObject> RoundBasedObjects_Single { get; set; }

        public RoundBasedItemKeeper()
        {
            RoundBasedObjects_List = new List<List<IRoundBasedObject>>();
            RoundBasedObjects_Array = new List<IRoundBasedObject[]>();
            RoundBasedObjects_Single = new List<IRoundBasedObject>();
        }

        public void Add<T>(List<T> roundBasedList) where T : IRoundBasedObject
        {
            RoundBasedObjects_List.Add(roundBasedList.Cast<IRoundBasedObject>().ToList());
        }

        public void Add<T>(T[] roundBasedArray) where T : IRoundBasedObject
        {
            RoundBasedObjects_Array.Add(roundBasedArray.Cast<IRoundBasedObject>().ToArray());
        }

        public void Add<T>(T roundBasedObject) where T : IRoundBasedObject
        {
            RoundBasedObjects_Single.Add(roundBasedObject);
        }

        public void RoundStart()
        {
            foreach (IRoundBasedObject single in RoundBasedObjects_Single)
            {
                single.StartRound();
            }

            foreach (List<IRoundBasedObject> list in RoundBasedObjects_List)
            {
                foreach (IRoundBasedObject single in list)
                {
                    single.StartRound();
                }
            }

            foreach (IRoundBasedObject[] array in RoundBasedObjects_Array)
            {
                foreach (IRoundBasedObject single in array)
                {
                    if (single != null)
                        single.StartRound();
                }
            }
        }

        public void RoundEnd()
        {
            foreach (IRoundBasedObject single in RoundBasedObjects_Single)
            {
                single.EndRound();
            }

            foreach (List<IRoundBasedObject> list in RoundBasedObjects_List)
            {
                foreach (IRoundBasedObject single in list)
                {
                    single.EndRound();
                }
            }

            foreach (IRoundBasedObject[] array in RoundBasedObjects_Array)
            {
                foreach (IRoundBasedObject single in array)
                {
                    if (single != null)
                        single.EndRound();
                }
            }
        }

        public void TurnStart()
        {
            foreach (IRoundBasedObject single in RoundBasedObjects_Single)
            {
                single.StartTurn();
            }

            foreach (List<IRoundBasedObject> list in RoundBasedObjects_List)
            {
                foreach (IRoundBasedObject single in list)
                {
                    single.StartTurn();
                }
            }

            foreach (IRoundBasedObject[] array in RoundBasedObjects_Array)
            {
                foreach (IRoundBasedObject single in array)
                {
                    if (single != null)
                        single.StartTurn();
                }
            }
        }

        public void TurnEnd()
        {
            foreach (IRoundBasedObject single in RoundBasedObjects_Single)
            {
                single.EndTurn();
            }

            foreach (List<IRoundBasedObject> list in RoundBasedObjects_List)
            {
                foreach (IRoundBasedObject single in list)
                {
                    single.EndTurn();
                }
            }

            foreach (IRoundBasedObject[] array in RoundBasedObjects_Array)
            {
                foreach (IRoundBasedObject single in array)
                {
                    if (single != null)
                        single.EndTurn();
                }
            }
        }
    }
}
