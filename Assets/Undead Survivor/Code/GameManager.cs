using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //GameManager���� Player ������ �����ϱ� ����
    public static GameManager Instance;
    public Player player;

    void Awake()
    {
        Instance = this;
    }
}