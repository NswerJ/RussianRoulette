using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETC
{
    public class Function
    {
        public static T[] ResetArray<T>(int size)
        {
            T[] resizeArray = new T[size];

            for (int i = 0; i < size; i++)
            {
                resizeArray[i] = default(T);
            }

            return resizeArray;
        }
    }
}
