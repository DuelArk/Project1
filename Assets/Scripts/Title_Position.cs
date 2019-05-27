using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Position : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject startBtn;
    [SerializeField] private GameObject exitBtn;

    // Start is called before the first frame update
    void Start()
    {
        title.transform.position = new Vector3(Screen.width / 2, Screen.height * 0.75f, 0);
        float btnDistance = startBtn.GetComponent<RectTransform>().sizeDelta.y * 1.5f;
        startBtn.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        exitBtn.transform.position = startBtn.transform.position + Vector3.down * btnDistance;
    }
}
