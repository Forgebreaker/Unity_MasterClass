using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_15_Coroutines : MonoBehaviour
{
    // Sometime we want to delay a behavior, lie we want to call a function but execute it after 3 or 4 seconds
    [SerializeField] private float Time2Live;
    [SerializeField] private bool TimeController;
    private Coroutine printCoroutine;
    void Start()
    {
        PrintAfterDelay(); // No output 
        // we can't just call Coroutine like this, since it's not a regular function
        
        StartCoroutine(PrintAfterDelay());
        // we use StartCoroutine( <The Coroutine function> ) to use this kind of function 
        // you can thing this is a time function, and you need a special tool to run it
        
        StartCoroutine(PrintAfterDelay(5)); // we still able to pass parameter here

    /*****************************************************************************************/

        StopCoroutine(PrintAfterDelay()); // this will not work since it need to know exactly the coroutine that need to stop not the function

        printCoroutine = StartCoroutine(PrintAfterDelay(3)); // a coroutine variable to store exactly coroutine that start
        StopCoroutine(printCoroutine); // Correctly stopping coroutine by reference

    /*****************************************************************************************/

        StartCoroutine("PrintAfterDelay");
        // only pass the name of the function to the StartCoroutine(), there is no difference between this way an traditional one
        // also you can't add parameter in this kind of function call
        // however there is a pros when you use this way is that you can stop coroutine

        StopCoroutine("PrintAfterDelay"); // if you stop before the coroutine ends, the code after yield will never be printed out

        StartCoroutine("DestroyAfterDelay"); // the time now can be adjust via editor
    }

    void Update()
    {
        if (TimeController == false)
        {
            Time.timeScale = 1; // this controller the time of all activities that happen in the scene
        }
        else { 
            Time.timeScale = 0; // freeze the time
        }
    }

    IEnumerator PrintAfterDelay() 
    {
        print("Print After 0 Second");
        yield return new WaitForSeconds(2f); // WaitForSecond() is affected by Time.timeScale
        print("Print After 2 Seconds");
    }

    IEnumerator DestroyAfterDelay()
    {
        print("Create the bullet");
        yield return new WaitForSecondsRealtime(Time2Live); // WaitForSecondsRealtime() does not care about Time.timeScale
        print("Destroyed the bullet");
    }

    IEnumerator PrintAfterDelay(int second)
    {
        print("Print After 0 Second");
        yield return new WaitForSeconds(second);
        print($"Print After {second} Seconds");
    }
}
