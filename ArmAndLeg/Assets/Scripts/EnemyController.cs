using Items;

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Inventory m_Inventory;

    public GameObject armPrefab;
    public GameObject legPrefab;

    // Use this for initialization
    void Start()
    {
        var armLeftobj = BuildLimb(armPrefab, new Arm(), true);
        var armRightobj = BuildLimb(armPrefab, new Arm());
        var legRightobj = BuildLimb(legPrefab, new Leg(), true);
        var legLeftobj = BuildLimb(legPrefab, new Leg());
    }

    private GameObject BuildLimb(Object template, Limb limb, bool flipX = false, bool flipY = false)
    {
        var obj = Instantiate(template, transform) as GameObject;
        if (!obj)
            return null;

        var objComp = obj.AddComponent<EnemyLimbBehaviour>();

        objComp.Init(limb);

        obj.GetComponent<SpriteRenderer>().flipX = flipX;
        obj.GetComponent<SpriteRenderer>().flipY = flipY;

        m_Inventory.AddLimb(limb);

        return obj;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        m_Inventory.PopArm(transform.position);
    }
}
