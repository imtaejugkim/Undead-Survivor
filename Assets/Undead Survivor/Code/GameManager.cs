using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //GameManager에는 Player 정보를 저장하기 위함
    public static GameManager Instance;
    public PoolManager pool;
    public Player player;

    void Awake()
    {
        Instance = this;
    }
}
