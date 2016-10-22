using UnityEngine;
using System.Collections;
using Items;

public class ZombiePlayerController : MonoBehaviour
{
    public GameObject armPrefab;
    public GameObject legPrefab;
    // Use this for initialization

    void Start()
    {
        GameObject armLeftobj = Instantiate(armPrefab, transform) as GameObject;
        GameObject armRightobj = Instantiate(armPrefab, transform) as GameObject;

        var leftArmComponent = armLeftobj.AddComponent<LimbBehaviour>();
        var rightArmComponent = armRightobj.AddComponent<LimbBehaviour>();


        Arm LeftArm = new Arm();
        Arm RightArm = new Arm();

        leftArmComponent.Init(LeftArm);
        rightArmComponent.Init(RightArm);


        m_Inventory.AddLimb(LeftArm);
        m_Inventory.AddLimb(RightArm);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;
        Vector2 pos = transform.position;
        transform.position = pos + dir * m_Speed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_Inventory = new Inventory();
        }
    }

    
    public float m_Speed;

    [SerializeField]
    private Inventory m_Inventory;

    public Inventory inventory
    {
        get { return m_Inventory; }
    }
}
