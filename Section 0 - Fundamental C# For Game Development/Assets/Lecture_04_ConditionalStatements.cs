using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lecture_04_ConditionalStatements : MonoBehaviour
{

    void If_Else_Statement() 
    {
        if (true)
        {
            print("this line is printed out because the statement is true");
        }
        if (false)
        {
            print("this line will not be printed out because the statement is false");
        }

        int a = 6;
        int b = 3;
        int c = 4;
    /**********************************************************************************************************/
        if (a > b)
        {
            print("a is greater than b");
        }
    /**********************************************************************************************************/
        if (a < b)
        {
            print("a is smaller than b");
        }
        else if (a == b)
        {
            print("a is equal to b");
        }
        else // this refers to every other cases, in this situation, the only one left is a > b
        {
            print("a is greater than b");
        }
    /**********************************************************************************************************/
        if (a <= b)
        {
            print("a is smaller than or equal to b");

            if (a == b)
            {
                print("a is equal to b");
            }
            else 
            {
                print("a is smaller than b");
            }
        }
        else if (a >= b)
        {
            print("a is greater than or equal to b");
            
            if (a == b)
            {
                print("a is equal to b");
            }
            else
            {
                print("a is greater than b");
            }
        }
    /**********************************************************************************************************/
        if (a > b && a > c) // true and true <=> true
                            // true and false <=> false
        {
            print("a is greater than b and a is greater than c");
        }

        if (a > b || a > c) // true or false <=> true
        {
            print("a is greater than b or a is greater than c");
        }

        if (!(a > b) && a > c)
        {
            print("a is not greater than b and a is greater than c");
        }
    }

    void Switch_Statement() 
    { 
        int a = 6;
        
        switch (a) // choose 'a' as the target then list all situations of a that you want using "case"
        {
            case 1: // in case a == 1
                print("a is equal to 1");    
                break; // break out of the case, if you don't have this the case with true statement will execute forever => error

            case 2:
                print("a is equal to 2");
                break;

            case 3:
                print("a is equal to 3");
                break;

            default: // in the default case if all the cases above is executed then run this, works kinda same with else
                print("No case is executed");
                break;
        }
    }
    void Start()
    {
        If_Else_Statement();
        Switch_Statement();
    }

    void Update()
    {
        
    }
}
