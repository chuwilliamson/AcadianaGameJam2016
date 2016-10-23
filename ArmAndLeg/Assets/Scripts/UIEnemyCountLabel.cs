using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIEnemyCountLabel : MonoBehaviour
{

    void Awake()
    {
        og = GetComponent<Text>().text;
        UpdateLabel();
    }
    string og;
    void Start()
    {
        UpdateLabel();
        FindObjectOfType<ObjectSpawner>().onCombatUpdate.AddListener(UpdateLabel);
    }

    void UpdateLabel()
    { 
        GetComponent<Text>().text = og + "<color=#ffa500ff>" + FindObjectOfType<ObjectSpawner>().numEnemies + "</color>";


    }
}
