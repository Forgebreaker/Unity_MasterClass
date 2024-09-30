using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lecture_18_ListsAndGeneric : MonoBehaviour
{
    // Using array, we can put item in them, accessing those item via index

    public GameObject[] Itemlist; // an array of GameObject type variables
    // of course this size is 0 from the start, unless you change it in the editor
    // and yeah! it is not flexible at all ! we can't simply add more value nor remove ....
    // that why we have List<Type>, we can use it just by import System.Collections.Generic

    // Error: public List ItemList; <=> you need to add type for the list using Generic
    public List<GameObject> ItemList; // a list of GameObject type variables
    // <GameObject> is called Generic, just simply enter the type that we want to get from our object, same as with our array
    // Generic is a construct that enables you to create classes, methods, delegates, and interfaces that work with various data types without specifying the actual type

    public List<string> ItemList2 = new List<string>(/*you can enter the Size here*/); // size in this case is optional, resizable
    public string[] ItemList3 = new string[10]; // you can't leave the size empty
    void Start()
    {
        ItemList2.Add("Tran Hung Thinh"); // add value into the list
        ItemList2.Add("Big Box");
        ItemList2.Add("Kum Kum Lord");
        print(ItemList2.Count); // output: 3
        ItemList2.Remove(ItemList2[2]);
        ItemList2.Remove("Big Box");
        print(ItemList2.Count); // output: 1

        for (int i = 0; i < ItemList2.Count; i++) 
        { 
            print(ItemList2[i]);
        }
    }

    void Update()
    {
        
    }
}
