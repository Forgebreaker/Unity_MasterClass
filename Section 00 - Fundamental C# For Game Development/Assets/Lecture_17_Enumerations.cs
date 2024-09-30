using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_17_Enumerations : MonoBehaviour
{
    public enum GameState // you can think this is special class combine with array with all variable inside is static
    { 
        Started,
        Paused,
        Ended,
        MainMenu
    }

    GameState CurrentState = GameState.MainMenu;
    void Start()
    {
        if (CurrentState == GameState.MainMenu) 
        { 
            CurrentState = GameState.Started;
        }

        if (CurrentState == GameState.Started) 
        {
            print("The Game has been started");
        }
    }

    void Update()
    {
        
    }
}
