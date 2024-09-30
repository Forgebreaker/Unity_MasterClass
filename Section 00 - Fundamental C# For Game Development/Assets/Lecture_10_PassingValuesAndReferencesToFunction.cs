using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_10_PassingValuesAndReferencesToFunction : MonoBehaviour
{
    void Start()
    {
        PlayerInstantiate MyPlayer = new PlayerInstantiate("Warrior", 250);
        MyPlayer.PlayerInfo(); // >>> Player Info! Class: 'Warrior', Health: '250'
        ChangePlayerClass(MyPlayer, "Wizard");
        MyPlayer.PlayerInfo(); // >>> Player Info! Class: 'Wizard', Health: '250'

        /*
            Why was the defaultclass of MyPlayer changed?
            - MyPlayer is a reference type, which means it holds a reference (or pointer) to the object stored in the heap.
            - When you pass MyPlayer to the ChangePlayerClass function, you're passing a copy of the reference, not the object itself.
            - Since both the original MyPlayer and the function parameter 'player' point to the same memory location in the heap,
              any changes made inside the function affect the original object.
         */

        int simplenumber = 10;
        print(simplenumber); // [Result] 10
        ChangeNumberValue(simplenumber);
        print(simplenumber); // [Result] 10

        /*
            Why is simplenumber still 10?
            - simplenumber is a value type (int), which means the variable holds the value directly (in this case, 10).
            - When you pass simplenumber to the ChangeNumberValue function, a **copy** of its value is made.
            - For example, simplenumber stays in its original location (e.g., 0x0001) with the value 10,
              while a new variable 'number' is created at a different memory address (e.g., 0x0002) with the copied value of 10.
            - Changing 'number' inside the function does not affect the original simplenumber.
         */

    }

    void ChangePlayerClass(PlayerInstantiate player, string changeclass) 
    { 
        player.defaultclass = changeclass;
    }

    void ChangeNumberValue(int number) 
    {
        number = 40;
    }
}

public class PlayerInstantiate
{
    public float defaulthealth = 100f;
    public string defaultclass = "Farmer";

    public PlayerInstantiate(string playerclass, float playerhealth) 
    {
        this.defaultclass = playerclass;
        this.defaulthealth= playerhealth;
    }
    public void PlayerInfo()
    {
        Debug.Log($">>> Player Info! Class: '{defaultclass}', Health: '{defaulthealth}'");
    }
}