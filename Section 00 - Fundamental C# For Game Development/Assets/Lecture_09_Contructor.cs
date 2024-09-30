using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_09_Contructor : MonoBehaviour
{
    private void Start()
    {
        // The PlayerClass() syntax looks like a function call, but it is actually calling the constructor.
        // A constructor is a special method that runs when an instance of the class is created.
        // It allows you to set up initial values or run setup code as soon as the object is instantiated.
        // If no constructor is defined, a default one exists automatically, but you can also define your own.
        
        PlayerClass Player1 = new PlayerClass(); // >>> Player Info! Class: 'Gunner', Health: '250'

        PlayerClass Player2 = new PlayerClass(500, "Wizard");
        Player2.PlayerInfo(); // >>> Player Info! Class: 'Wizard', Health: '500'
    }
}

public class PlayerClass
{

    public float defaulthealth = 100f;
    public string defaultclass = "Warrior";

    public PlayerClass() 
    // the function already exist, we just call it out and fix stuff no need to define the function is void or return...
    {
        defaulthealth = 250f;
        defaultclass = "Gunner";
        PlayerInfo();
    }

    public PlayerClass(float playerhealth, string playerclass) 
    // multiple contructor is allowed, so that we can create many kinds of object with different workflow with a single class
    {
        this.defaulthealth = playerhealth;
        // "this" refers to the current class, that means we are ref to a variable named "defaulthealth" in the class level, which is the one we created above
        this.defaultclass = playerclass;
    }

    public void PlayerInfo()
    {
        Debug.Log($">>> Player Info! Class: '{defaultclass}', Health: '{defaulthealth}'");
    }
}