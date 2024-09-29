using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Lecture_05_Loops : MonoBehaviour
{
    void For_Loop() 
    {
        for (int counter = 0; counter < 10; counter++)
        // for integer counter, as long as counter is smaller than 10, increment counter then do the following
        // counter++ <=> use the current value of counter and then increment by 1
        // ++counter <=> increment the value of counter by 1 and then use it
        {
            print("[For Loop]The value of counter is: " + counter);
        }
        /*
         The value of counter is: 0
         ...
         The value of counter is: 9 => stop because when counter == 10, it is not satisfied the condition smaller than 10 anymore
        */
    }

    void While_Loop()
    {
        int counter = 0;

        while (counter < 10)
        // Important: when you are using a while loop, you need to make sure that this condition here eventually evaluates to be false
        {
            print("[While Loop] The value of counter is: " + counter);
            counter++; // in the end of each loop remember to increment the counter unless you want an infinited loop
        }

        int number = 0;
        do // execute the code first and then check the condition later
        {
            print("[Do-While Loop] The value of counter is: " + number);
            number++;
        }
        while (number < 10);
        print("[Do-While Loop - Hidden] The value of counter is: " + number);
        // [Do-While Loop - Hidden] The value of counter is: 10
    }
    void Start()
    {
        For_Loop();
        While_Loop();
    }

    void Update()
    {
        
    }
}
