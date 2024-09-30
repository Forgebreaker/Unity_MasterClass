using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_01_IntroToVariables : MonoBehaviour
{
    // This is a comment, which is used to document code

    /*
        what you type in here is a comment, remember "Good code document itself"
     */

    // <type> <name> = <value>
    float power = 2.5f; // "f" in the end of the float to identify this number as a float not a double
    float life = 5f; // <=> 5.0
    double speed = 10.25; // no need "f" in the end, and more precises than float (64 bit > 32 bit)
    double stamina = 51.26d; // "d" in the end of double is optional
    int score = 55; // int <=> integer, ";" to the end the command
    string name = "The Wizard"; // use double quotes for string type
    
    // imagine a running character 
    bool isrunning = true;
    bool isstop = false; // use for conditional statement
    void Start()

    {
        
    }

    void Update()
    {
        
    }
}
