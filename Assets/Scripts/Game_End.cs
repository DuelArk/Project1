using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_End : MonoBehaviour
{
    public Text scroeText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1;
            Application.LoadLevel(1);
        }else if (Input.GetKeyDown(KeyCode.Q))
        {
            Time.timeScale = 1;
            Application.LoadLevel(0);
        }
    }
}
