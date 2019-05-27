using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Enemy : MonoBehaviour
{
    private Transform tr;
    private Animator ani;
    public bool land;
    private int layerMask;
    private float gravity;
    public GameObject player;
    public float speed;
    public bool attackCheck;
    public int life;
    public Slider time;
    public Score score;
    public EnemyCreater creater;
    public int point;
    public TimeManager timeManager;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        ani = GetComponent<Animator>();
        gravity = 3;
        speed = 1;
        life = 5;
        point = life;
        Physics2D.IgnoreLayerCollision(10, 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Fall();
    }

    private void Move()
    {
        if (!land)
        {
            tr.Translate(Vector3.down * Time.deltaTime * gravity);
        }
        if (land)
            Tracking();

        ani.speed = timeManager.timeScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            tr.localScale = new Vector3(tr.localScale.x * -1, 1, 1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //바닥체크
        if (collision.gameObject.tag == "floor")
        {
            land = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            land = false;
        }
    }

    private void Fall()
    {
        //추락
        layerMask = (1 << 9) | (1 << 10);
        layerMask = ~layerMask;
        RaycastHit2D hit = Physics2D.Raycast(tr.position, Vector2.down, 0.5f, layerMask);
        if (hit.collider == null)
        {
            //ani.SetBool("JumpDown", true);
            land = false;
        }
    }

    private void Tracking()
    {
        float distanceX = tr.position.x - player.transform.position.x;
        float distanceY = tr.position.y - player.transform.position.y;
        ani.SetBool("MoveCheck", true);
        if(Mathf.Abs(distanceY) < 0.8f)
        {
            if(Mathf.Abs(distanceX) > 0.6f)
            {
                if (distanceX < 0)
                {
                    tr.localScale = Vector3.one;
                    tr.Translate(Vector3.right * timeManager.deltaTime);
                }
                else
                {
                    tr.localScale = new Vector3(-1, 1, 1);
                    tr.Translate(Vector3.right * -1 * timeManager.deltaTime);
                }
            }
            else
            {
                ani.SetBool("MoveCheck", false);
                Attack();
            }
        }
        else
        {
            tr.Translate(Vector3.right * tr.localScale.x * timeManager.deltaTime);
        }
    }

    private void Attack()
    {
        if (!attackCheck)
        {
            attackCheck = true;
            ani.SetBool("AttackCheck", true);
            Invoke("AttackFalse", 2);
        }
    }

    private void AttackFalse()
    {
        attackCheck = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack" && !ani.GetBool("Hit"))
        {
            ani.SetBool("Hit", true);
            life -= player.GetComponent<Main_Player>().atk;
        }

        if (collision.tag == "Boom")
        {
            life = 0;
        }

        if (life <= 0)
        {
            gameObject.SetActive(false);
            time.value += point;
            score.add(point);
            creater.count--;
        }
    }
}
