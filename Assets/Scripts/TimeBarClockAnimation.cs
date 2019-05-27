using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarClockAnimation : MonoBehaviour
{
    private RectTransform tr;
    private int angle;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<RectTransform>();
        angle = 15;
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        while (true)
        {
            tr.localEulerAngles += Vector3.forward * angle;
            if (tr.localRotation.z > 0)
                angle = -15;
            else if (tr.localRotation.z < 0)
                angle = 15;
            yield return new WaitForSeconds (0.1f);
        }
    }
}
