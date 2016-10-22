using UnityEngine;
using System.Collections;
using Items;
public class EnemyController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject armLeftobj = BuildLimb(armPrefab, new Arm(), true);
        GameObject armRightobj = BuildLimb(armPrefab, new Arm());
        GameObject legRightobj = BuildLimb(legPrefab, new Leg(), true);
        GameObject legLeftobj = BuildLimb(legPrefab, new Leg());
    }

    GameObject BuildLimb(GameObject template, Limb limb, bool flipX = false, bool flipY = false)
    {
        GameObject obj = Instantiate(template, transform) as GameObject;
        LimbBehaviour objComp = obj.AddComponent<LimbBehaviour>();
        objComp.Init(limb);
        obj.GetComponent<SpriteRenderer>().flipX = flipX;
        obj.GetComponent<SpriteRenderer>().flipY = flipY;
        m_Inventory.AddLimb(limb);

        return obj;
    }

    // Update is called once per frame
    public Inventory m_Inventory;
    public GameObject armPrefab;
    public GameObject legPrefab;

    public void OnCollisionEnter2D(Collision2D collision)
    {        
        m_Inventory.PopArm(transform);
    }
}
