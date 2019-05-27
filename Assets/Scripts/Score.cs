using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        text = GetComponent<Text>();
    }

    public int get()
    {
        return score;
    }

    public void add(int num)
    {
        score += num;
        text.text = score.ToString();
    }
}
