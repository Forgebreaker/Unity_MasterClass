using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_07_ArraysAndFunctions : MonoBehaviour
{

    int[] WoodsInEachDay;

    // Funfact: by declaring the variable WoodsInEachDay at the class level (outside of Start()), so it is a field.
                // Fields in C# are automatically initialized to their default values.
                // In the case of an array, the default value is null, and that's why there's no CS0165 error
    void Start()
    {
        int[] SpinaInEachDayList = new int[10];
        ProcessArray1(SpinaInEachDayList);

        // error: int[] WoodsInEachDay;
            // WoodsInEachDay is declared as a local variable inside the Start() method.
            // Local variables must be initialized before use.
            // Since WoodsInEachDay is not initialized, the compiler throws the CS0165: Use of unassigned local variable
        ProcessArray2(WoodsInEachDay);
        
        for (int i = 0; i < WoodsInEachDay.Length; i++)
        {
            WoodsInEachDay[i] = Random.RandomRange(0, 100);
            print(">>> " + WoodsInEachDay[i]);
        }
    }

    void ProcessArray1(int[] data)
    {
        for (int i = 0; i < data.Length; i++) 
        { 
            data[i] = Random.RandomRange(0, 100);
            print(data[i]);
        }
    }

    int[] ProcessArray2(int[] data) 
    { 
        data = new int[10];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Random.RandomRange(0, 100);
        }
        return data;
    }
}
