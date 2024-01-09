using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curvesControl : MonoBehaviour
{
    public Material[] materials;
    float currentValueHorizontal;
    float currentValueVertical;
    public float lerpTime;
    bool isComplete = true;
    float[] horizontalCurve = { 0.001f, 0, -0.001f };

    // Start is called before the first frame update
    void Start()
    {
        currentValueHorizontal = 0;
        currentValueVertical = -0.0007f;
        foreach (Material mat in materials)
        {
            mat.SetFloat("_sideWays_Strength", currentValueHorizontal);
            mat.SetFloat("_backward_Strength", currentValueVertical);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "curveSignal")
    //    {
    //        if (isComplete)
    //        {
    //            StartCoroutine(changeCurves());
    //        }
    //    }
    //}
    //private IEnumerator changeCurves()
    //{
    //    float elapsedTime = 0;
    //    int randomHorizontalCurve = Random.Range(0, 3);
    //    Debug.Log("change Curve" + randomHorizontalCurve);
    //    while (elapsedTime < lerpTime)
    //    {
    //        isComplete = false;
    //        currentValueHorizontal = Mathf.Lerp(currentValueHorizontal, horizontalCurve[randomHorizontalCurve], elapsedTime / lerpTime);
    //        elapsedTime += Time.deltaTime;
    //        foreach (Material mat in materials)
    //        {
    //            mat.SetFloat("_sideWays_Strength", currentValueHorizontal);
    //        }
    //        yield return null;
    //    }
    //    currentValueHorizontal = horizontalCurve[randomHorizontalCurve];
    //    isComplete = true;
    //}
}
