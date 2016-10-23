using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIFlavorText : MonoBehaviour
{

    void Awake()
    {
        var inventory = FindObjectOfType<ZombiePlayerController>().inventory;
        inventory.armCountEvent.AddListener(SayIt);
        inventory.legCountEvent.AddListener(SayIt);
    }
    
    public List<string> thingsToSay;
    void SayIt(int num)
    {
        int randomindex = Random.Range(0, thingsToSay.Count - 1);
        if (randomindex < 0)
            return;
        string thingToSay = thingsToSay[randomindex];
        GetComponent<Text>().text = thingToSay;
    }


}
