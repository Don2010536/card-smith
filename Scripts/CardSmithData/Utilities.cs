using System;
using System.IO;

namespace CardSmithData {
    public static class Utilities
    {
        public static void SaveArray(ref BinaryWriter writer, int[] values)
        {
            writer.Write(values.Length);
            foreach (int i in values)
            {
                writer.Write(i);
            }
        }

        public static void SaveArray(ref BinaryWriter writer, long[] values)
        {
            writer.Write(values.Length);
            foreach (long i in values)
            {
                writer.Write(i);
            }
        }

        public static void SaveArray(ref BinaryWriter writer, float[] values)
        {
            writer.Write(values.Length);
            foreach (float i in values)
            {
                writer.Write(i);
            }
        }

        public static void SaveArray(ref BinaryWriter writer, double[] values)
        {
            writer.Write(values.Length);
            foreach (double i in values)
            {
                writer.Write(i);
            }
        }

        public static void SaveArray(ref BinaryWriter writer, bool[] values)
        {
            writer.Write(values.Length);
            foreach (bool i in values)
            {
                writer.Write(i);
            }
        }

        public static void SaveArray(ref BinaryWriter writer, string[] values)
        {
            writer.Write(values.Length);
            foreach (string i in values)
            {
                writer.Write(i);
            }
        }

        public static void SaveArray(ref BinaryWriter writer, ISavable[] values)
        {
            writer.Write(values.Length);
            foreach (ISavable i in values)
            {
                i.Save(ref writer);
            }
        }


        public static void LoadArray(ref BinaryReader reader, int[] values)
        {
            values = new int[reader.ReadInt32()];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = reader.ReadInt32();
            }
        }

        public static void LoadArray(ref BinaryReader reader, long[] values)
        {
            values = new long[reader.ReadInt32()];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = reader.ReadInt64();
            }
        }

        public static void LoadArray(ref BinaryReader reader, float[] values)
        {
            values = new float[reader.ReadInt32()];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = reader.ReadSingle();
            }
        }

        public static void LoadArray(ref BinaryReader reader, double[] values)
        {
            values = new double[reader.ReadInt32()];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = reader.ReadDouble();
            }
        }

        public static void LoadArray(ref BinaryReader reader, bool[] values)
        {
            values = new bool[reader.ReadInt32()];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = reader.ReadBoolean();
            }
        }

        public static void LoadArray(ref BinaryReader reader, string[] values)
        {
            values = new string[reader.ReadInt32()];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = reader.ReadString();
            }
        }

        public static void LoadArray<T>(ref BinaryReader reader, T[] values) where T : ILoadable, new()
        {
            T temp;
            
            values = new T[reader.ReadInt32()];
            for (int i = 0; i < values.Length; i++)
            {
                temp = new();
                temp.Load(ref reader);
                values[i] = temp;
            }
        }
    }
}