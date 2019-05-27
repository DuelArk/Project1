using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float timeScale;
    public float deltaTime;

    // Start is called before the first frame update
    void Start()
    {
        timeScale = 1;
        deltaTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime * timeScale;
    }
}
