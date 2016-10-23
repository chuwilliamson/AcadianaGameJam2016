using System.Collections;

using Items;

using UnityEngine;

public class ZombiePlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator m_Animator;

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
    void Update()
    {
        if (Input.GetMouseButton(0))
            ;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.position += h * m_Speed * -transform.right * Time.deltaTime;
        transform.position += v * m_Speed * -transform.up * Time.deltaTime;

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 deltaPosition = mousePosition - transform.position;

        angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * (180f / Mathf.PI);

        if (Input.GetMouseButton(1))
            transform.eulerAngles =
                new Vector3(
                    transform.eulerAngles.x,
                    transform.eulerAngles.y,
                    angle + 90f);
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

    IEnumerator ShootLimb()
    {
        yield return false;
    }
}
