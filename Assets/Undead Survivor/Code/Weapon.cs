using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public int speed;


    void Start()
    {
        Init();
    }
    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                break;
        }

        // Test Code
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 5);
        }
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0) 
        {
            Batch();
        }
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                break;
        }
    }

    void Batch()
    {
        for (int index = 0; index < count; index++) 
        {
            Transform bullet;
            if(index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            // 레벨 업 시, 웨폰의 위치를 초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            // Rotate 함수로 계산된 각도 적용
            bullet.Rotate(rotVec);
            // Translate 함수로 자신의 위쪽으로 이동, 이동방향은 Space.World 기준
            bullet.Translate(bullet.up *  1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1); // -1 is Infinity Per.
        }
    }
}
