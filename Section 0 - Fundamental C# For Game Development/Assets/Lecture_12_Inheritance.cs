using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lecture_12_Inheritance : MonoBehaviour
{
    void Start()
    {
        Wizard wizard = new Wizard();
        
        wizard.Health_Permission_Gate = 250;
        wizard.Class_Permission_Gate = "Wizard";
        wizard.PlayerInfo(); // >>> Player Info! Class: 'Wizard', Health: '250'
        
        wizard.WizardMana = 100f;
        wizard.WizardCastSpeed = 0.85f;
        wizard.WizardPower(); // >>> Wizard Power! Mana: 100, Cast Speed: 0.85
        wizard.Attack(); // Shoot Fire Ball
    }

    void Update()
    {
        
    }
}

public class Wizard : PlayerBluePrint // this means that Wizard have inherited from the Player
                                      // Now this child class inherited all properties from the parent class
{

    // you can understand thing like this "the son have all the powers from his dad
    // super strong, immortal ... but he also have the laze eyes which his dad doesn't have"
    // Inheritance goes downward

    // [*] you can't inherit "private" properties from the parent class

    private float defaultwizardmana = 80f;
    private float defaultwizardcastspeed = 1.2f;
    
    public Wizard() 
    {
        this.Class_Permission_Gate = "Wizard";
        this.Health_Permission_Gate = 75f;
    }
    public float WizardMana
    {
        get { return this.defaultwizardmana; }
        set { this.defaultwizardmana = value; }
    }

    public float WizardCastSpeed
    {
        get { return this.defaultwizardcastspeed; }
        set { this.defaultwizardcastspeed = value; }
    }
    public void WizardPower() {
        Debug.Log($">>> Wizard Power! Mana: {defaultwizardmana}, Cast Speed: {defaultwizardcastspeed}");
    }

    public override void Attack() // "override" allows you to change the original virtual function in the parent class
    {
        Debug.Log("Shoot Fire Ball");
    }
}