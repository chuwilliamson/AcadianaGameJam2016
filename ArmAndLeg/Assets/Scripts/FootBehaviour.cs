using UnityEngine;

public class FootBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_MaxOffset;
    [SerializeField]
    private float m_MaxTime;

    [SerializeField]
    private bool m_Extending;

    private Rigidbody2D m_ParentRigidbody2D;

    private Vector3 m_OriginalPosition;

    private float m_CurrentTime;

    public bool extending
    {
        get { return m_Extending; }
        set { m_Extending = value; }
    }

    // Use this for initialization
    void Start()
    {
        m_ParentRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
        m_OriginalPosition = transform.localPosition;

        foreach (var footBehaviour in transform.parent.GetComponentsInChildren<FootBehaviour>())
        {
            if (footBehaviour == this)
                continue;

            if (!footBehaviour.m_Extending)
                m_Extending = true;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!m_ParentRigidbody2D)
            return;

        if (m_ParentRigidbody2D.velocity == Vector2.zero)
            return;

        var angle =
            Mathf.Atan2(
                m_ParentRigidbody2D.velocity.y,
                m_ParentRigidbody2D.velocity.x) * (180f / Mathf.PI);
        angle += 90f;

        transform.eulerAngles =
                new Vector3(
                    transform.eulerAngles.x,
                    transform.eulerAngles.y,
                    angle);

        angle = (transform.localEulerAngles.z - 90f) * Mathf.Deg2Rad;
        if (!m_Extending)
        {
            m_CurrentTime += Time.deltaTime;
            var distance = (m_CurrentTime / m_MaxTime) * m_MaxOffset;

            transform.localPosition =
                new Vector3(
                    m_OriginalPosition.x
                    + Mathf.Cos(angle) * distance,
                    m_OriginalPosition.y
                    + Mathf.Sin(angle) * distance,
                    transform.localPosition.z);
        }
        else
        {
            m_CurrentTime -= Time.deltaTime;
            var distance = (m_CurrentTime / m_MaxTime) * m_MaxOffset;

            transform.localPosition =
                new Vector3(
                    m_OriginalPosition.x
                    + Mathf.Cos(angle) * distance,
                    m_OriginalPosition.y
                    + Mathf.Sin(angle) * distance,
                    transform.localPosition.z);
        }

        if (Mathf.Abs(m_CurrentTime) > m_MaxTime)
        {
            m_CurrentTime = m_Extending ? -m_MaxTime : m_MaxTime;
            m_Extending = !m_Extending;
        }
    }
}
