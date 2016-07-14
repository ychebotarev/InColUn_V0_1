﻿
namespace ConcurrencyUtilities
{
    /*
     * This interfaces are only used to maintain a consistent signature of the different implementations
     *  
     */


    internal interface ValueReader<out T>
    {
        T GetValue();
        T NonVolatileGetValue();
    }

    internal interface ValueWriter<in T>
    {
        void SetValue(T newValue);
        void LazySetValue(T newValue);
        void NonVolatileSetValue(T newValue);
    }

    internal interface VolatileValue<T> : ValueReader<T>, ValueWriter<T> { }

    internal interface ValueAdder<T> : ValueReader<T>
    {
        T GetAndReset();
        void Add(T value);
        void Increment();
        void Increment(T value);
        void Decrement();
        void Decrement(T value);
        void Reset();
    }

    internal interface AtomicValue<T> : VolatileValue<T>
    {
        T Add(T value);

        T GetAndAdd(T value);
        T GetAndIncrement();
        T GetAndIncrement(T value);
        T GetAndDecrement();
        T GetAndDecrement(T value);

        T Increment();
        T Increment(T value);
        T Decrement();
        T Decrement(T value);

        T GetAndReset();
        T GetAndSet(T newValue);
        bool CompareAndSwap(T expected, T updated);
    }

    internal interface AtomicArray<T>
    {
        int Length { get; }
        T GetValue(int index);
        T NonVolatileGetValue(int index);
        void SetValue(int index, T value);
        void LazySetValue(int index, T value);
        void NonVolatileSetValue(int index, T value);
        T Add(int index, T value);
        T GetAndAdd(int index, T value);
        T GetAndIncrement(int index);
        T GetAndIncrement(int index, T value);
        T GetAndDecrement(int index);
        T GetAndDecrement(int index, T value);
        T Increment(int index);
        T Increment(int index, T value);
        T Decrement(int index);
        T Decrement(int index, T value);
        T GetAndReset(int index);
        T GetAndSet(int index, T newValue);
        bool CompareAndSwap(int index, T expected, T updated);
    }
}
