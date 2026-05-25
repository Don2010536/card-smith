using System.Collections.Generic;
using System.IO;

namespace CardSmithData.Managers {
    public class ConditionManager<T> : ISavable, ILoadable where T : ICondition, new()
    {
        public Dictionary<int, T> Conditions { get; private set; } = [];

        public void AddCondition(T condition)
        {
            Conditions[condition.ID] = condition;
        }

        public T GetCondition(int id)
        {
            return Conditions[id];
        }

        public void Save(ref BinaryWriter writer)
        {
            writer.Write(Conditions.Count);

            foreach (int key in Conditions.Keys)
            {
                writer.Write(key);
                Conditions[key].Save(ref writer);
            }
        }

        public void Load(ref BinaryReader reader)
        {
            int count = reader.ReadInt32();
            int key;
            T temp;

            for (int i = 0; i < count; i++)
            {
                key = reader.ReadInt32();
                temp = new T();
                temp.Load(ref reader);

                Conditions.Add(key, temp);
            }
        }
    }
}