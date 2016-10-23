using System.Collections;

using Factories;

using Items;

using UnityEngine;

public class ZombiePlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;
    [SerializeField]
    private Rigidbody2D m_Rigidbody2D;

    [SerializeField]
    private Inventory m_Inventory;

    [SerializeField]
    private float m_Speed;

    public Inventory inventory
    {
        get { return m_Inventory; }
    }

    // Use this for initialization
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        var leftArm = new Arm();
        var rightArm = new Arm();

        var leftLeg = new Leg();
        var rightLeg = new Leg();

        m_Inventory.AddLimb(leftArm);
        m_Inventory.AddLimb(rightArm);

        m_Inventory.AddLimb(leftLeg);
        m_Inventory.AddLimb(rightLeg);
    }
    public Vector3 mousePosition;
    public float angle;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ShootLimb());
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        var totalVelocity = Vector3.zero;
        totalVelocity += h * m_Speed * -transform.right;
        totalVelocity += v * m_Speed * -transform.up;

        m_Rigidbody2D.velocity = totalVelocity;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 deltaPosition = mousePosition - transform.position;

        angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * (180f / Mathf.PI);

        if (Input.GetMouseButton(1))
            transform.eulerAngles =
                new Vector3(
                    transform.eulerAngles.x,
                    transform.eulerAngles.y,
                    angle + 90);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var limbBehaviour = other.GetComponent<LimbBehaviour>();
        if (limbBehaviour && limbBehaviour.canPickUp)
        {
            m_Inventory.AddLimb(limbBehaviour.limb);
            Destroy(limbBehaviour.gameObject);
        }
    }

    private IEnumerator ShootLimb()
    {
        var poppedLimb = m_Inventory.PopLimb();
        LimbObjectFactory.CreateWeapon(gameObject, poppedLimb);

        yield return false;
    }
}
