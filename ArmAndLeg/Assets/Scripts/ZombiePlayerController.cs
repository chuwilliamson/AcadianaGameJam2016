using UnityEngine;
using System.Collections;
using Items;

public class ZombiePlayerController : MonoBehaviour
{
    public Animator m_anim;
    // Use this for initialization
    void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
    void Start()
    {


        Arm LeftArm = new Arm();
        Arm RightArm = new Arm(); 

        m_Inventory.AddLimb(LeftArm);
        m_Inventory.AddLimb(RightArm);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 dir = new Vector3(h, v).normalized;
        Vector2 pos = transform.position; 

        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_Inventory = new Inventory();
        }

        transform.position = pos + dir * m_Speed * Time.deltaTime;
        float vel = (pos - (Vector2)transform.position).magnitude;
        GetComponent<SpriteRenderer>().flipY = (v > 0) ? true : false;
        
        m_anim.SetFloat("Speed", vel);
    }

    
    public float m_Speed;

    [SerializeField]
    private Inventory m_Inventory;

    public Inventory inventory
    {
        get { return m_Inventory; }
    }
}
