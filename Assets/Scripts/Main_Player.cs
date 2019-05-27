using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main_Player : MonoBehaviour
{
    private Transform tr;
    private Animator ani;
    private Rigidbody2D rig;
    public float speed;
    private bool jumpCheck = false;
    public float gravity;
    public bool land;
    public bool attackCheck = false;
    [SerializeField] private Slider slider;
    private float timeScale;
    public int atk;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        ani = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        gravity = 3;
        floorRaycastCheck();
        atk = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
    }

    //이동 메소드
    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (!attackCheck)
            {
                int dir = (int)Input.GetAxisRaw("Horizontal");
                tr.localScale = new Vector3(dir, 1, 1);
                if (!jumpCheck)
                {
                    ani.SetBool("MoveCheck", true);
                }
                tr.Translate(Vector3.right * dir * Time.deltaTime * speed);
            }
        }
        else
        {
            ani.SetBool("MoveCheck", false);
        }
        if (!land)
        {
            tr.Translate(Vector3.down * Time.deltaTime * gravity);
        }
    }

    //점프 메소드
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.X) && !jumpCheck && !attackCheck && !ani.GetBool("Hit"))
        {
            jumpCheck = true;
            StartCoroutine("JumpCoroutine");
        }
    }

    //점프 코루틴
    IEnumerator JumpCoroutine()
    {
        int count = 0;
        ani.SetBool("JumpUp", true);
        while(count < 40)
        {
            tr.Translate(Vector3.up * Time.deltaTime * 3);
            count++;
            yield return new WaitForSeconds(Time.deltaTime * 0.1f);
        }
        ani.SetBool("JumpUp", false);
        ani.SetBool("JumpDown", true);
        land = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //바닥과 충돌시 착지
        if (collision.gameObject.tag == "floor" && !land)
        {
            ani.SetBool("JumpDown", false);
            jumpCheck = false;
            land = true;
            //아래에 바닥이 없으면 떨어짐
            floorRaycastCheck();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //아래에 바닥이 없으면 떨어짐
        floorRaycastCheck();
    }

    //공격 메소드
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !jumpCheck && !ani.GetBool("Hit"))
        {
            attackCheck = true;
            ani.SetBool("AttackCheck", true);
        }
    }
    
    //레이캐스트로 바닥체크
    public void floorRaycastCheck()
    {
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        RaycastHit2D hit = Physics2D.Raycast(tr.position, Vector2.down, 0.5f, layerMask);
        if (hit.collider == null && !jumpCheck)
        {
            ani.SetBool("JumpDown", true);
            jumpCheck = true;
            land = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //맞았을때
        if (collision.tag == "EnemyAttack")
        {
            if (!jumpCheck && !attackCheck && !ani.GetBool("Hit"))
            {
                float distance = collision.transform.position.x - tr.position.x;
                if (distance < 0)
                    tr.localScale = new Vector3(-1, 1, 1);
                else
                    tr.localScale = Vector3.one;

                ani.SetBool("Hit", true);
            }
            slider.value -= 1;
        }
    }
}
