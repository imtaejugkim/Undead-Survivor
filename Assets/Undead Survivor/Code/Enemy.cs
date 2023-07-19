using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {

        if (!isLive)
        {
            return;
        }

        //위치 차이 = 타겟 위치 - 나의 위치
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        
        //물리속도가 이동에 영향을 주지 않도록 제거
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
        {
            return;
        }

        // sprite renderer에서 플레이어가 왼쪽, 오른쪽 있는 경우 x 축비교 
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌한게 bullet 이라면
        if (!collision.CompareTag("Bullet"))
        {
            return;
        }

        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0)
        {
            // Live, Hit Action

        }
        else
        {
            // Die
            Dead();
        }
    }

    void Dead()
    {
        // 지금은 간단히 비활성화
        gameObject.SetActive(false);
    }
}
