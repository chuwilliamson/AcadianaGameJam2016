using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 25; i++)
            Spawn(prefab);
    }
    
    
    void Spawn(GameObject go)
    {
        float randx = Random.Range(-25f, 25f);
        float randy = Random.Range(-25f, 25f);
        Vector3 randPos = new Vector3(randx, randy, 0);
        GameObject thing = Instantiate(go, randPos, Quaternion.identity, transform) as GameObject;
        thing.name = "Zambie";

    }

    // Update is called once per frame
    public GameObject prefab;
}
