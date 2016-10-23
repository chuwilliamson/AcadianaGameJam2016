using UnityEngine;
using System.Collections;


public class ZambieBoid : MonoBehaviour
{
    private IMovement movementType;
    public GameObject target;
    public float mass;
    
    void Start()
    {
        SetMovement(new Seek());  
    }
    void Update()
    {
        movementType.Move(gameObject, target);
    }

    void SetMovement(IMovement movement)
    {
        movementType = movement;
    }


}
public interface IMovement
{
    void Move(GameObject entity, GameObject target);
}

public class Linear : IMovement
{
    public void Move(GameObject go, GameObject target)
    {
        go.transform.position += new Vector3(1, 0, 0);
    }
}
public class Seek : IMovement
{
    private float maxForce = 5f;
    private float mass = 5f;
    
    public void Move(GameObject go, GameObject target)
    { 
        float seekFactor = 5f;
        Vector3 Velocity = go.transform.position.normalized;
        
        Vector3 DesiredVelocity = Vector3.Normalize(target.transform.position - go.transform.position);

        Vector3 SeekForce = seekFactor * (DesiredVelocity - Velocity).normalized;

        go.transform.position += SeekForce * Time.deltaTime;
        
    }
}




