using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemID;
    public TimeManager timeManager;
    public Main_Player player;
    public Timer timer;
    public SpriteRenderer render;
    [SerializeField] private GameObject boomRange;
    private BoxCollider2D collider;
    public ItemCreater itemCreater;
    public int itemNum;
    public int itemPosNum;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreLayerCollision(10, 11);
    }

    private void Update()
    {
        if (Time.time % 1 < 0.5f)
            transform.position -= Vector3.up * Time.deltaTime * 0.2f;
        else
            transform.position += Vector3.up * Time.deltaTime * 0.2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            itemCreater.GetComponent<AudioSource>().Play();
            switch (itemID)
            {
                case 0:
                    EnemySlow();
                    break;
                case 1:
                    AtkUp();
                    break;
                case 2:
                    TimeUp();
                    break;
                case 3:
                    TimeUpPlus();
                    break;
                case 4:
                    Boom();
                    break;
            }
        }
    }

    private void EnemySlow()
    {
        timeManager.timeScale = 0.3f;
        Invoke("SlowRelease", 10);
        NextItemSet();
    }

    private void SlowRelease()
    {
        timeManager.timeScale = 1;
    }

    private void AtkUp()
    {
        player.atk += 1;
        NextItemSet();
    }

    private void TimeUp()
    {
        timer.slider.value += 5;
        NextItemSet();
    }

    private void TimeUpPlus()
    {
        timer.slider.value += 10;
        NextItemSet();
    }

    private void Boom()
    {
        boomRange.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = null;
        collider.isTrigger = true;
        StartCoroutine(BoomEnd());
    }

    IEnumerator BoomEnd()
    {
        while (boomRange.transform.localScale.x < 5)
        {
            boomRange.transform.localScale += (Vector3.right + Vector3.up) * Time.deltaTime * 5;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        boomRange.SetActive(false);
        boomRange.transform.localScale = Vector3.one;
        collider.isTrigger = false;
        NextItemSet();
    }

    private void NextItemSet()
    {
        Invoke("ItemCreate", 5);
        itemCreater.pointCheck[itemPosNum] = false;
        gameObject.SetActive(false);
    }

    private void ItemCreate()
    {
        itemCreater.NewItem(itemNum);
    }
}
