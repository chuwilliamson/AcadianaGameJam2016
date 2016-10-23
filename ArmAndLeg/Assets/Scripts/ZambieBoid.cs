using UnityEngine;
using System.Collections;


public class ZambieBoid : MonoBehaviour
{
    public GameObject target;
    private Vector3 position;
    private Vector3 velocity;
    private Vector3 seek;
    float seekFactor = 1f;
    float mass = 5f;

    void Awake()
    {
        velocity = transform.position.normalized;
        target = FindObjectOfType<ZombiePlayerController>().gameObject;
    }
    void Update()
    {        
        Vector3 desired = Vector3.Normalize(target.transform.position - transform.position);        
        
        seek = seekFactor * (desired - velocity) / mass; 
    }

    void LateUpdate()
    {
        velocity += seek;
        GetComponent<Rigidbody2D>().velocity = velocity;
        transform.up = velocity * -1f;
    }
}
 
 




