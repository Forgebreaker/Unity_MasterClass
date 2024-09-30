using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_03_Functions : MonoBehaviour
{
    // function is a block of code that we can reuse it over and over again => clean code

    /**********************************************************************************************************/

    // category 1: simple function (doesn't return value)

    void CalculateTwoNumber( /* inside this don't have any parameters */) // void means it return nothing
    {
        // the code that we put here
        int sum = 3 + 5;
        print("the sum of a and b is: " + sum);
    }

    /**********************************************************************************************************/
    
    // category 2: adaptable void function

    void CalculateThreeNumber(int a, int b, int c) // no limited in integer, use any type you need
    { 
        int sum = a + b + c;
        print("the sum of 3 number a, b and c is: " + sum);
    }

    /**********************************************************************************************************/

    // category 3: function that return value

    // In this case I want to create a function that return an integer

    int CalculateRectangleArea() 
    {
        int Area = 4 * 5;
        return Area;
    }

    /**********************************************************************************************************/

    // category 4: adaptable return function

    float CalculateCircleArea(float r)
    {
        float pi = 3.14f;
        float Area = r*r*pi;
        return Area;
    }

    /**********************************************************************************************************/

    // Special case

    // Event they have the same name, both are void type but one don't take any parameter and one is adaptable
    // => C# compiler indentifies them are completely different function, so there is no error for this situation, however if you make to exactly the same function there will be one
    void CalculateSumOf2Numbers() 
    {
        int a = 4;
        int b = 4;
        int sum = a + b;
        print(">> The Sum of a and b is: " + sum);
    }

    void CalculateSumOf2Numbers(int a, int b)
    {
        int sum = a + b;
        print(">> The Sum of a and b is: " + sum);
    }

    /**********************************************************************************************************/
    void Start()
    {
        // just call the name of the function to run it;
       
        CalculateTwoNumber();
        CalculateThreeNumber(1, 2, 4);

        int RectangleArea = CalculateRectangleArea(); // <=> the function return a number so that we can use the function as a number
        float CircleArea = CalculateCircleArea(5f);

        print(">> Rectangle Area: " + RectangleArea);
        print(">> Circle Area: " + CircleArea);
    }

    void Update()
    {
        
    }
}
