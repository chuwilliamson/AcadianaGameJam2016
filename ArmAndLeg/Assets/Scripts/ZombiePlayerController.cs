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
        var LeftArm = new Arm();
        var RightArm = new Arm();

        var LeftLeg = new Leg();
        var RightLeg = new Leg();

        m_Inventory.AddLimb(LeftArm);
        m_Inventory.AddLimb(RightArm);

        m_Inventory.AddLimb(LeftLeg);
        m_Inventory.AddLimb(RightLeg);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_Inventory.AddLimb(new Arm());
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector2 dir = new Vector3(h, v).normalized;
        Vector2 pos = transform.position;

        transform.position = pos + dir * m_Speed * Time.deltaTime;
        float vel = (pos - (Vector2)transform.position).magnitude;

        m_anim.SetFloat("Horizontal", h);
        m_anim.SetFloat("Vertical", h);

        // UNCOMMENT FOR ANGULAR ROTATION

        //if (h == 0f && v == 0f)
        //    return;

        //var to = new Vector2(0, -1);
        //var from = new Vector2(h, v);

        //var angle = Vector2.Angle(from, to);
        //var cross = Vector3.Cross(from, to);

        //if (cross.z > 0)
        //    angle = 360 - angle;

        //transform.eulerAngles =
        //    new Vector3(
        //        transform.eulerAngles.x,
        //        transform.eulerAngles.y,
        //        Mathf.Abs(angle));
    }


    public float m_Speed;

    [SerializeField]
    private Inventory m_Inventory;

    public Inventory inventory
    {
        get { return m_Inventory; }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        var limbBehaviour = collider.GetComponent<LimbBehaviour>();
        if (limbBehaviour && limbBehaviour.canPickUp)
        {
            m_Inventory.AddLimb(limbBehaviour.limb);
            Destroy(limbBehaviour.gameObject);
        }
    }
}
