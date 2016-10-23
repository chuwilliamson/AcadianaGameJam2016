using UnityEngine;
using System.Collections;

public class TimeToLiveBehaviour : MonoBehaviour {
    IEnumerator Timer()
    {
        while(ttl > 0)
        {
            ttl--;
        }
        Destroy(gameObject);
        yield return null;
    }

    public float ttl = 2f;
}
