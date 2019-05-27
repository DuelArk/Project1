using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Button : MonoBehaviour
{

    public void StartButton()
    {
        Application.LoadLevel(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
