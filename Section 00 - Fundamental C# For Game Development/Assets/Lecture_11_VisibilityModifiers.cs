using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_11_VisibilityModifiers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerGenerator Player = new PlayerGenerator();

        /* 
          Error: 'PlayerGenerator.defaulthealth' is inaccessible due to its protection level
            Player.defaulthealth = 500f;
        */

        print("[+] Player Class: " + Player.GetClassInfo()); // [+] Player Class: Farmer
        Player.SetHealth(500f);
        Player.PlayerInfo(); // >>> Player Info! Class: 'Farmer', Health: '500'
    }

}
public class PlayerGenerator
{
    private float defaulthealth = 100f; 
    // when set variable to "private" => we can only access it inside of the class where it's created
    // which mean it can helps us to prevent accidentally changing the value of variable 
    private string defaultclass = "Farmer";

    public PlayerGenerator() { }
    public PlayerGenerator(string playerclass, float playerhealth)
    {
        this.defaultclass = playerclass;
        this.defaulthealth = playerhealth;
    }

/*****************************************************************************************/

    // it can prevent us from changing stuff, but how can we access to it
    public string GetClassInfo() // public means it can me access and modify even in other class
    {
        return this.defaultclass; // so now instances are abled to gather the default class but don't have permission to change it
    } // => we call this getter

    // however we still want to change it, but still want to prevent accidentally changes

    public void SetClassInfo(string NewClass)
    { 
        this.defaultclass = NewClass; // only by access to this function can we change the player class
    } // we call this setter

    public string Class_Permission_Gate
    {
        get 
        {
            return this.defaultclass;
        }
        set 
        {
            this.defaultclass = value;
        }
    }
/*****************************************************************************************/
    public float GetHealth() 
    {
        return this.defaulthealth;
    }
    public void SetHealth(float NewHealth) 
    { 
        this.defaulthealth = NewHealth; 
    }

    // However we have another shorter way to write this

    public float Health_Permission_Gate
    {
        get { // need it or not, you need to able to access it first and then you get your modify permission
              // because of that you have should have can have get only, but you can't use set only
            return this.defaulthealth;
        }
        set {
            this.defaulthealth = value; // what is this "value"
        }
    }
    /*
      PlayerGenerator Player = new PlayerGenerator();
      Player.Health_Permission_Gate = 5; => in this case "value" is 5
     */
    public void PlayerInfo()
    {
        Debug.Log($">>> Player Info! Class: '{defaultclass}', Health: '{defaulthealth}'");
    }
}