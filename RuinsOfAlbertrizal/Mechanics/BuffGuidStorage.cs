using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinsOfAlbertrizal.Mechanics
{
    public class BuffGuidStorage
    {
        private List<Buff> Buffs { get; set; }

        public List<Guid> BuffGuids { get; set; }

        public List<int> BuffLevels { get; set; }

        public BuffGuidStorage()
        {
            Buffs = new List<Buff>();
            BuffGuids = new List<Guid>();
            BuffLevels = new List<int>();
        }

        //public BuffGuidStorage(List<int> buffLevels)
        //{
        //    BuffLevels = buffLevels;
        //}

        public List<Buff> Load(List<Buff> storedBuffs)
        {
            Buffs = storedBuffs.FilterByGlobalID(BuffGuids).MemoryClone();

            if (BuffLevels != null && BuffLevels.Count > 0)
            {
                for (int i = 0; i < Buffs.Count; i++)
                {
                    try
                    {
                        Buffs[i].Level = BuffLevels[i];
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Buffs[i].Level = 1;
                    }
                }
            }

            return Buffs;
        }

        public void Unload(List<Buff> buffs)
        {
            Buffs = buffs;

            BuffGuids = Buffs.ToGlobalIDList();

            BuffLevels = new List<int>();

            foreach (Buff buff in Buffs)
            {
                BuffLevels.Add(buff.Level);
            }
        }
    }
}
