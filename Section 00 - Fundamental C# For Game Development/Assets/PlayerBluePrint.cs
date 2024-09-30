using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBluePrint : MonoBehaviour
{
    /*****************************************************************************************/

    // These lines below are for Lecture 12

    private float defaulthealth = 100f;

    private string defaultclass = "Farmer";

    public PlayerBluePrint() { }
    public PlayerBluePrint(string playerclass, float playerhealth)
    {
        this.defaultclass = playerclass;
        this.defaulthealth = playerhealth;
    }

    public string Class_Permission_Gate
    {
        get { return this.defaultclass; }
        set { this.defaultclass = value; }
    }

    public float Health_Permission_Gate
    {
        get { return this.defaulthealth; }
        set { this.defaulthealth = value; }
    }
    public void PlayerInfo()
    {
        Debug.Log($">>> Player Info! Class: '{defaultclass}', Health: '{defaulthealth}'");
    }

    public virtual void Attack() // virtual allows the child class to override this function to fit its situation 
    {
        Debug.Log("Dealt Damage to enemy");
    }

    /*****************************************************************************************/
    
    // These lines below are for Lecture 14

    // static variable is a class variable
    public static void UltimateMagic() 
    {
        Debug.Log("Alahuaba!!! Booom");
    }

    public static string HiddenMagic = "KameHameHaaaaaa";

    /*
    public int PlayerPower = 10; [note] this object should be static in order to be used in a static function
                                        because all of them should be in the class level
    public static void HideMagic() 
    {
        Debug.Log("Player Power" + PlayerPower);
        <=> An object reference is required for the non-static field, method, or property 'member'
    }
    */

    /*****************************************************************************************/

    // These lines below are for Lecture 16

    public delegate void PlayerDied(int a, int b); 
    // delegate is just like its name a delegation for group of function that return nothing (void), named PlayerDied
    // you can think that delegate is where you define the type of the function you want to subscribe to the event that will be created from this delegate

    public static event PlayerDied OnDied; // of course you can create many event from one delegate, each one is subscribed by many other functions
    // Event created from the delegate "PlayerDied"
    // OnDie is the event's name (like variable name)
    // when we call the event all that function subscribed to it will be called

    private void Start()
    {
        if (OnDied != null) 
        { 
            OnDied(6, 5); // Output: Printed after event was called
            // just use it like a normal function, call it when something happened, use its output ...
            // really useful for game manager
        }
    }
}
