using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_08_ClassesAndObjects: MonoBehaviour
{
    private void Start()
    {
        Player Player1 = new Player(); // "new" is used to create a new object (imagine it like a spot) in the memory
        // we have created a variable of type Player named Player1, which is a pointer that point to the object that created in the memory
        // and now Player1 is an instance of class Player that we model
        
        Player1.PlayerInfo(); // >>> Player created! Class: 'Warrior', Health: '100'

        Player1.defaultclass = "Wizard"; // Yep! we can override the object info
        Player1.defaulthealth = 200f;

        Player1.PlayerInfo(); // >>> Player created! Class: 'Wizard', Health: '200'

    /*****************************************************************************************/

        Player Player2 = new Player();
         
        Player2.PlayerInfo(); // >>> Player created! Class: 'Warrior', Health: '100'

        Player2.defaultclass = "Knight";
        Player2.defaulthealth = 500f;

        Player2.PlayerInfo(); // >>> Player created! Class: 'Knight', Health: '500'

    /*****************************************************************************************/

        // as I said before, Player1 or Player2 is just a pointer to the object that created in memory
        // so what if I do this

        Player2 = Player1;
        /*
         what happens is that Player2's reference is set to point directly to the same memory location as Player1. 
         it's not that Player2 points to Player1 which then points to the memory location. 
         Instead, both Player1 and Player2 point directly to the same object in memory.
         */

        // Now! the Player2 is pointing to the Player1's object memory

        Player1.PlayerInfo(); // >>> Player created! Class: 'Wizard', Health: '200'
        Player2.PlayerInfo(); // >>> Player created! Class: 'Wizard', Health: '200'
    }
}

public class Player // class is where we model out object and then we create objects from that class
{

    // Fields: defaulthealth and defaultclass, which store the default health and class of a player, respectively.
    public float defaulthealth = 100f;
    public string defaultclass = "Warrior";

    public void PlayerInfo() 
    {
        Debug.Log($">>> Player Info! Class: '{defaultclass}', Health: '{defaulthealth}'");
    }
}