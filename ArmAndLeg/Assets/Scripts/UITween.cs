using UnityEngine;
using System.Collections;

public class UITween : MonoBehaviour {

    public AnimationCurve ac;
    public void DoIt()
    {
        StartCoroutine(TweenIt());
    }
 
    void StopTween()
    {
        StopAllCoroutines();
    }
    IEnumerator TweenIt()
    {
        float timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime;
            transform.localScale = new Vector3(1, 1, 1) * ac.Evaluate(timer);
            yield return null;
        }
        timer = 0;
        yield return null;

    }

}
