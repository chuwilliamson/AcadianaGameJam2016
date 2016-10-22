using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class UIInventory : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        ogString = GetComponent<Text>().text;
        FindObjectOfType<ZombiePlayerController>().inventory.armCountEvent.AddListener(UpdateText);	
	}
    string ogString;
    void UpdateText(int amount)
    {        
        GetComponent<Text>().text = ogString + " "+ amount.ToString();
        
    }
	// Update is called once per frame
	
}
