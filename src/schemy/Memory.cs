namespace Schemy
{
    using System;
    // using System.Text;
    using System.Collections.Generic;

    public struct Pointer
    {
        public int index;
        public int section;

        public Pointer(int index, int section)
        {
            this.index = index;
            this.section = section;
        }
    }

    public class Allocator<T>
    {
        List<T> buffer;
        List<byte> flag;

        List<int> free; // free memory index
        int freeCount = 0;

        public int section = 0;

        public static readonly T defaultValue = default(T);

        public Allocator(int capacity)
        {
            buffer = new List<T>(capacity);
            flag = new List<byte>(capacity);
            free = new List<int>(capacity);
        }

        public bool TryGet(int index, out T result)
        {
            int size = buffer.Count;
            if (index >= 0 && index < size && flag[index] == 1)
            {
                result = buffer[index];
                return true;
            }
            result = defaultValue;
            return false;
        }

        public T this[int index]
        {
            get
            {
                return buffer[index];
            }

            set
            {
                if (flag[index] == 1)
                    buffer[index] = value;
            }
        }

        public int Alloc()
        {
            return -1;
        }
        
        public int Alloc(T value)
        {
            return -1;
        }

        public void Free(int index)
        {
            if (index >= 0 && index < buffer.Count && 1 == flag[index])
            {
                buffer[index] = defaultValue;
            }
        }

        public void Vacuate()
        {
        }

        // public string GetReadable()
        // {
        //     int maxShow = 16;
        // }
    }

    public class Buffer
    {
        public Allocator<byte> mByte;
        public Allocator<sbyte> mSByte;

        public Allocator<short> mInt16;
        public Allocator<ushort> mUInt16;

        public Allocator<int> mInt32;
        public Allocator<uint> mUInt32;

        public Allocator<long> mInt64;
        public Allocator<ulong> mUInt64;

        public Allocator<string> mStr;
        public Allocator<char> mChar;

        public Allocator<bool> mBoolean;

        public Allocator<float> mFloat;
        public Allocator<double> mDouble;

        public Allocator<object> mObject;
        public Allocator<DateTime> mDatetime;

        public Pointer NewBool(bool value)
        {
            return new Pointer();
        }

        public Pointer NewInt(int value)
        {
            return new Pointer();
        }

        public Pointer NewFloat(float value)
        {
            return new Pointer();
        }

        public Pointer NewString(string value)
        {
            return new Pointer();
        }

        public Pointer NewObject(object value)
        {
            return new Pointer();
        }

    }

}
