using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class UIInventory : MonoBehaviour {

	// Use this for initialization
	void Awake ()
    {
        ogString = GetComponent<Text>().text;
        if(ogString.Contains("ARMS"))
        FindObjectOfType<EnemyController>().inventory.armCountEvent.AddListener(UpdateText);
        else
        {
            FindObjectOfType<EnemyController>().inventory.legCountEvent.AddListener(UpdateText);
        }
	}
    string ogString;
    void UpdateText(int amount)
    {        
        GetComponent<Text>().text = ogString + " "+ amount.ToString();
        
    }
	// Update is called once per frame
	
}
