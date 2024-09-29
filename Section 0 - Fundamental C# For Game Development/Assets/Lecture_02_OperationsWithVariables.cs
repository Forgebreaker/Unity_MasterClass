using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_02_OperationsWithVariables : MonoBehaviour
{

    void Start() // this is called when we run the game 
    {
        int a = 5;
        int b = 6;
        int sum = a + b;
        int sub = 5 - b;
        int mul = a * 6;
        int div = a / b; // [Result] 0 (because it is identified as an integer so it can't show a decimal point number => the closet hold number)
        float division = 5f / 6f;  // [Result] 0.83333, as you can see the "division" and 2 numbers that created have the same type is "float"

        float complex = (2 + 3) * (4 - (4 / 3)); // piority is the same in math

        float c = 2.5f;
        double d = 5.1f;
        // int errorcase = c / d; => can't use float number to calculate and put it in an integer variable, but the oposite is okay 
        
        // [*] casting is converting
        int fixedcase = (int)c / (int)d;

        // both methods below have the same function is to print out the content inside
        // and we need to add this script to a game object to able to see the result in the Console Window
        print(sum); // [Result] 11
        Debug.Log(sub); // [Result] -1

        // string concatenation
        print("The sum of a and b is: " + sum); // [Result] The sum of a and b is: 11

        print(fixedcase);
    }

    void Update()
    {
        
    }
}
