using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    private enum Type
    {
        Arm,
        Leg,
    }

    [SerializeField]
    private Type m_Type;

    private string ogString;

    // Use this for initialization
    private void Awake()
    {
        ogString = GetComponent<Text>().text;

        var inventory = FindObjectOfType<ZombiePlayerController>().inventory;

        switch (m_Type)
        {
        case Type.Arm:
            inventory.armCountEvent.AddListener(UpdateText);
            break;
        case Type.Leg:
            inventory.legCountEvent.AddListener(UpdateText);
            break;

        default:
            break;
        }
    }

    private void UpdateText(int amount)
    {
        GetComponent<Text>().text = ogString + " " + amount;
    }
}
