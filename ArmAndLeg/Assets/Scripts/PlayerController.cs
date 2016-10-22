using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;
        Vector2 pos = transform.position;
        transform.position = pos + dir * m_Speed * Time.deltaTime;
    }
    public float m_Speed;
    
}
