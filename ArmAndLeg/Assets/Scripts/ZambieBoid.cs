using UnityEngine;
using System.Collections;


public class ZambieBoid : MonoBehaviour
{
    private IMovement movementType;
    public GameObject target;
    public float mass;
    
    void Start()
    {
        SetMovement(new Linear());  
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
        Vector3 dir = target.transform.position - go.transform.position;
        Vector3 oldPos = go.transform.position;
        go.transform.position = oldPos + dir.normalized * Time.deltaTime;
    }
}
public class Seek : IMovement
{
    private float maxForce = 5f;
    private float mass = 5f;
    
    public void Move(GameObject go, GameObject target)
    { 
        float seekFactor = 5f;

        Vector3 v = go.transform.position.normalized;
        
        Vector3 dir = Vector3.Normalize(target.transform.position - go.transform.position);

        Vector3 oldPos = go.transform.position;
        
        Vector3 SeekForce = seekFactor * (dir  - v).normalized;

        go.transform.position = oldPos + dir.normalized * Time.deltaTime;
        
    }
}




