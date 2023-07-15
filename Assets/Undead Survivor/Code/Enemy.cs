using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
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
}
