using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_14_StaticVariablesAndFunctions : MonoBehaviour
{
    // Main problem for us is the complexity when we need to use a function from other class

    private PlayerBluePrint Player;

    void Start()
    {
        Player = GameObject.Find("The Player").GetComponent<PlayerBluePrint>();
        Player.Attack(); // Create an object, add script and then access to this with a bunch of code just to access Attach() function

        // UltimateMagic() is a static function, so we can call it directly like this without object reference
        PlayerBluePrint.UltimateMagic(); // Alahuaba!!! Booom
        print("Player Hidden Skill: " + PlayerBluePrint.HiddenMagic); // Player Hidden Skill: KameHameHaaaaaa
    }

    void Update()
    {
        
    }
}
