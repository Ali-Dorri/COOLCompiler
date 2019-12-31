using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOL_Compiling
{
    public class CustomStack<T>
    {
        T[] array;
        public int Count { get; private set; } = 0;

        public CustomStack()
        {
            array = new T[4];
        }

        public CustomStack(int cpacity)
        {
            array = new T[cpacity];
        }

        public T this[int index]
        {
            get
            {
                return array[index];
            }
            set
            {
                array[index] = value;
            }
        }

        public void Push(T data)
        {
            if(Count < array.Length)
            {
                array[Count] = data;
            }
            else
            {
                T[] prevArray = array;
                array = new T[Count * 2];
                for (int i = 0; i < prevArray.Length; i++)
                {
                    array[i] = prevArray[i];
                }
                array[Count] = data;
            }

            Count++;
        }

        public T Peek()
        {
            if(Count > 0)
            {
                return array[Count - 1];
            }

            throw new InvalidOperationException("Stack is empty");
        }

        public T Pop()
        {
            if (Count > 0)
            {
                Count--;
                return array[Count];
            }

            throw new InvalidOperationException("Stack is empty");
        }

        public void Clear()
        {
            Count = 0;
        }
    }
}
