namespace Schemy
{
    using System;
    // using System.Text;
    using System.Collections.Generic;

    public class CPtr
    {
        IBlock block;
        int offset;

        public CPtr()
        {
        }

        public CPtr(IBlock block, int offset)
        {
            this.block = block;
            this.offset = offset;
        }

        public Type valueType
        {
            get => block.ValueType(offset); 
        }

        public void Free()
        {
            block.Free(offset);
        }

#region 算数运算
        public CPtr Add(CPtr x)
        {
            return null;
        }

        public void AddBy(CPtr x)
        {
        }
#endregion
    }

    public interface IBlock
    {
        Type ValueType(int offset);

        void Free(int offset);
    }

    public class Allocator<T> : IBlock
    {
        List<T> buffer;
        List<byte> flags;

        List<int> free; // free memory index
        int freeCount = 0;

        public static readonly T defaultValue = default(T);

        public Allocator(int capacity)
        {
            buffer = new List<T>(capacity);
            flags = new List<byte>(capacity);
            free = new List<int>(capacity);
        }

        public bool TryGet(int index, out T result)
        {
            int size = buffer.Count;
            if (index >= 0 && index < size && flags[index] > 0)
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
                if (flags[index] > 0)
                    buffer[index] = value;
            }
        }

        public virtual Type ValueType(int offset)
        {
            return typeof(T);
        }

        public CPtr Alloc()
        {
            return Alloc(defaultValue);
        }
        
        public CPtr Alloc(T value)
        {
            return null;
        }

        public virtual void Free(int index)
        {
            if (index >= 0 && index < buffer.Count && 1 == flags[index])
            {
                buffer[index] = defaultValue;
            }
        }

        public void Vacuate()
        {
        }

        // class MemArray
        // {
        //     List<T> arr;
        //     int size;
        //     int capacity;

        //     public MemArray(int capacity)
        //     {
        //         arr = new List<T>(capacity);
        //         size = 0;
        //         capacity = arr.Capacity;
        //     }

        //     public void RemoveAt(int index)
        //     {
        //         if (index >= 0 && index < size)
        //         {
        //         }
        //     }

        //     public void Remove(T item)
        //     {
        //     }
        // }
    }

    public class ObjectAllocator : Allocator<object>
    {
        public ObjectAllocator(int capacity): base(capacity)
        {
        }

        public override Type ValueType(int offset)
        {
            return this[offset].GetType();
        }
    }

    public class Buffer
    {
        public Allocator<byte> mByte = new Allocator<byte>(64);
        public Allocator<sbyte> mSByte = new Allocator<sbyte>(64);

        public Allocator<short> mInt16 = new Allocator<short>(64);
        public Allocator<ushort> mUInt16 = new Allocator<ushort>(64);

        public Allocator<int> mInt32 = new Allocator<int>(64);
        public Allocator<uint> mUInt32 = new Allocator<uint>(64);

        public Allocator<long> mInt64 = new Allocator<long>(64);
        public Allocator<ulong> mUInt64 = new Allocator<ulong>(64);

        public Allocator<string> mStr = new Allocator<string>(64);
        public Allocator<char> mChar = new Allocator<char>(64);

        public Allocator<bool> mBoolean = new Allocator<bool>(64);

        public Allocator<float> mFloat = new Allocator<float>(64);
        public Allocator<double> mDouble = new Allocator<double>(64);

        public Allocator<object> mObject = new Allocator<object>(64);
        public Allocator<DateTime> mDateTime = new Allocator<DateTime>(64);

        public CPtr AllocBool(bool value)
        {
            return mBoolean.Alloc(value);
        }

        public CPtr AllocInt32(int value)
        {
            return mInt32.Alloc(value);
        }

        public CPtr AllocInt64(int value)
        {
            return mInt64.Alloc(value);
        }

        public CPtr AllocFloat(float value)
        {
            return mFloat.Alloc(value);
        }

        public CPtr AllocDouble(double value)
        {
            return mDouble.Alloc(value);
        }

        public CPtr AllocString(string value)
        {
            return mStr.Alloc(value);
        }

        public CPtr AllocDateTime(DateTime dt)
        {
            return mDateTime.Alloc(dt);
        }

        public CPtr AllocObject(object value)
        {
            return mObject.Alloc(value);
        }

    }

}
