using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreater : MonoBehaviour
{
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private Main_Player player;
    [SerializeField] private Timer timer;
    [SerializeField] private Vector3[] point;
    public bool[] pointCheck;
    [SerializeField] private GameObject itemObj;
    private GameObject[] item;
    [SerializeField] private Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        item = new GameObject[3];
        pointCheck = new bool[5];

        itemObj.GetComponent<Item>().itemCreater = GetComponent<ItemCreater>();
        itemObj.GetComponent<Item>().timeManager = timeManager;
        itemObj.GetComponent<Item>().player = player;
        itemObj.GetComponent<Item>().timer = timer;
        for (int i = 0; i < 3; i++)
        {
            itemObj.GetComponent<Item>().itemNum = i;
            item[i] = Instantiate(itemObj);
            item[i].SetActive(false);
            NewItem(i);
        }
    }

    public void NewItem(int num)
    {
        int rand = Random.Range(0, 5);
        int rand2 = Random.Range(0, 100);
        if (!pointCheck[rand])
        {
            item[num].GetComponent<Item>().itemPosNum = rand;
            if (rand2 < 25)
            {
                item[num].GetComponent<Item>().itemID = 0;
                item[num].GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
            else if(rand2 >= 25 && rand2 < 40)
            {
                item[num].GetComponent<Item>().itemID = 1;
                item[num].GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else if(rand2 >= 40 && rand2 < 70)
            {
                item[num].GetComponent<Item>().itemID = 2;
                item[num].GetComponent<SpriteRenderer>().sprite = sprites[2];
            }
            else if(rand2 >= 70 && rand2 < 85)
            {
                item[num].GetComponent<Item>().itemID = 3;
                item[num].GetComponent<SpriteRenderer>().sprite = sprites[3];
            }
            else if(rand2 >= 85 && rand2 < 100)
            {
                item[num].GetComponent<Item>().itemID = 4;
                item[num].GetComponent<SpriteRenderer>().sprite = sprites[4];
            }
            pointCheck[rand] = true;
            item[num].transform.position = point[rand];
            item[num].SetActive(true);
        }
        else
        {
            NewItem(num);
        }
    }
}
