using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    [SerializeField] private Slider timer;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text scoreText;
    private Game_End gameEnd;

    // Start is called before the first frame update
    void Start()
    {
        gameEnd = gameOver.GetComponent<Game_End>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.value == 0)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
            gameEnd.scroeText.text = scoreText.text;
        }
    }
}
