using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 25; i++)
            Spawn(prefab);
    }
    [SerializeField]
    public Rect boundary;
    void Spawn(GameObject go)
    {
        float randx = Random.Range(boundary.xMin, boundary.xMax);
        float randy = Random.Range(boundary.yMin, boundary.yMax);
        Vector3 randPos = new Vector3(randx, randy, 0);
        GameObject thing = Instantiate(go, randPos, Quaternion.identity, transform) as GameObject;
        thing.name = "yeaaaaa";

    }

    // Update is called once per frame
    public GameObject prefab;
}
