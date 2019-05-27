using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCreater : MonoBehaviour
{
    [SerializeField] private Score score;
    [SerializeField] private Slider timer;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3[] pos;
    [SerializeField] private GameObject enemyPrefeb;
    [SerializeField] private GameObject[] enemy;
    [SerializeField] private Text timeText;
    public int count;
    [SerializeField] private TimeManager timeManager;
    private bool createCheck;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        enemy = new GameObject[20];
        for(int i = 0; i < 20; i++)
        {
            enemyPrefeb.GetComponent<Main_Enemy>().player = player;
            enemyPrefeb.GetComponent<Main_Enemy>().score = score;
            enemyPrefeb.GetComponent<Main_Enemy>().time = timer;
            enemyPrefeb.GetComponent<Main_Enemy>().creater = GetComponent<EnemyCreater>();
            enemyPrefeb.GetComponent<Main_Enemy>().timeManager = timeManager;
            enemy[i] = Instantiate(enemyPrefeb);
            enemyPrefeb.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(count < 20 && !createCheck)
        {
            createCheck = true;
            StartCoroutine("Create");
        }
        timeText.text = TimeTextChange(Time.time);
    }

    private string TimeTextChange(float time)
    {
        int hour = (int)time / 3600;
        int minute = (int)time % 3600 / 60;
        int second = (int)time % 60;
        return hour.ToString() + ":" + minute.ToString("D2") + ":" + second.ToString("D2");
    }

    IEnumerator Create()
    {
        int rand;
        while(count < 20)
        {
            rand = Random.Range(0, 4);
            for(int i = 0; i < 20; i++)
            {
                if (!enemy[i].activeSelf)
                {
                    enemy[i].transform.position = pos[rand];
                    enemy[i].GetComponent<Main_Enemy>().life = 5 + (int)(Time.time / 30);
                    enemy[i].GetComponent<Main_Enemy>().point = 5 + (int)(Time.time / 30);
                    enemy[i].SetActive(true);
                    enemy[i].GetComponent<Main_Enemy>().land = false;
                    count++;
                    break;
                }
            }
            yield return new WaitForSeconds(1);
        }
        createCheck = false;
    }
}
