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


        public static void LoadArray(ref BinaryReader reader, out int[] values)
        {
            values = 
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
    }
}