using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_16_EventsAndDelegates : MonoBehaviour
{
    // Use for game initialization, the very first one that would be called when you start the game
    private void Awake()
    {
       
    }

    // Activate when the object that this script is added is enabled or when you start the game
    private void OnEnable()
    {
        print("Start");
        PlayerBluePrint.OnDied += ExecutedAfterEventCall; // ExecutedAfterEventCall is subscribed to the OnDie event
        /*
         Sorry if not mentioned earlier
            a = a + 5
            a += 5
         Both way give us the same result
         */
       
    }

    // executed after OnEnable()
    void Start()
    {
    }

    // Activate when the object that this script is added is disabled or when you stop the game
    private void OnDisable()
    {
        print("End");
        PlayerBluePrint.OnDied -= ExecutedAfterEventCall; // unsubscribed
    }
    void ExecutedAfterEventCall(int firstnumb, int secondnumb)
    {
        print("Printed after event was called");
        print($">>> First Number: {firstnumb}" + "\n" +
            $">>> Second Number: {secondnumb}");
    }
    void Update()
    {
        
    }
}
