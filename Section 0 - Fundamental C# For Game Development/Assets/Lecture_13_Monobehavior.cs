using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_13_Monobehavior : MonoBehaviour
    // Every script you want to attach on a game object, it needs to inherit from MonoBehavior
    // Start() or Update() is function that inherited from MonoBehavior class
{
    private BoxCollider GameObjectCollider;

    // Trying to create a reference of other Object, in this case is PlayerBluePrint
    private PlayerBluePrint Player;

    // However there is a shorter way

    [SerializeField] private PlayerBluePrint Player2;
    // now you can drag and drop "The Player" into this reference
    // public still allows you to modify in the editor, but we all know why we should not do that

    // you can also get the component in this game object, for example I want to get the boxcollider in order to disable it

    private BoxCollider Collider; // this also a reference, so things make sense now right
    [SerializeField] private Collider2D PlayerCollider; // Drag and drop "The Player" here to control its collider

    void Start()
    {

        print(gameObject.name); // Output: Lecture_13 
        // gameObject refers to the object that added this script
        // gameObject.name is the name of the object in Hierarchy
        
        print(transform.position);
        // transform refers to transform property of the gameObject
        // and you can access to rotation, position and scale from this component

        Player = GameObject.Find("The Player").GetComponent<PlayerBluePrint>();
        // GameObject is pre-defined in the UnityEngine which is base class for all entities in the scene
        // and then it find a gameObject that named "The Player"
        // after that it get the type of component "PlayerBluePrint" in this case is the name of the script I added to gameObject
        // so now the Player become a reference of the Object

        Player.Attack(); // Dealt Damage to enemy
        Player.Health_Permission_Gate = 1000f;
        
        Player2.PlayerInfo(); // >>> Player Info! Class: 'Farmer', Health: '1000'

        // Yeah yeah! I know, you might thing that "What the heck is going on here"
        // Remember! no "new" is used here, the Player reference is the pointer to the "The Player" gameobject's memory
        // => the Player2 which also is a pointer to that memory location, that's why we have the result

        Collider = gameObject.GetComponent<BoxCollider>();

    }

    void Update()
    {
        
    }
}
