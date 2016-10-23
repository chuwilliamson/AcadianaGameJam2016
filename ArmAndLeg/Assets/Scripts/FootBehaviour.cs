using UnityEngine;
using System.Collections;

public class FootBehaviour : MonoBehaviour
{
    private Rigidbody2D m_ParentRigidbody2D;

    // Use this for initialization
    void Start()
    {
        m_ParentRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_ParentRigidbody2D)
            return;

        var angle =
            Mathf.Atan2(
                m_ParentRigidbody2D.velocity.y,
                m_ParentRigidbody2D.velocity.x) * (180f / Mathf.PI);

        //if (m_ParentRigidbody2D.velocity)
    }
}
