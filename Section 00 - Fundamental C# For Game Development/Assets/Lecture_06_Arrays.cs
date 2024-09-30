using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Lecture_06_Arrays : MonoBehaviour
{
     /*
     Class-level fields (declared outside any methods) are automatically initialized to their default values (like null for arrays). 

     Local variables (declared inside a method) must be explicitly assigned before use
     */
    int samplenumber; // as you can see, this one isn't assigned with a number so its default value is equal to 0
    void Start()
    {
        string[] Guild = { "", "BigBox", "KumKumLord", "Lana", "SickDuck" };
        Guild[0] = "Tran Hung Thinh"; // 0 in this case is the index of element in the array, the very first one

        string[] UselessList; // if you don't assign any thing to this array, this is an empty one, an array without a single slot
        
        string[] NameList = new string[5 /*this is array size*/];
        // a string array name "NameList" is set as a new string string array with 5 elements
        // all the element inside this array right now is equal to 0 just like the samplenumber above
        
        print(NameList.Length /* the total number of elements of the array */); // [Result] 5
        
        print(NameList[NameList.Length -1]); // Start from 0 right (^.^~) => the last one's index is 5 - 1 = 4 

        int counter = 0;
        
        foreach (string Name in Guild) // foreach is a special loop for array. Loop bases on the size array
        // Funfact: In C#, the loop variable in a foreach loop is read-only and cannot be assigned a new value directly.
                 // In this case, Name cannot be assigned
        {
            // Error: Name = "hehe";
            NameList[counter] = Name;
            print("[+] NameList appends: " + NameList[counter]);
            counter++;
        }

        print("The final name in the NameList: " + NameList[NameList.Length -1]);
    }


    void Update()
    {
        
    }
}
