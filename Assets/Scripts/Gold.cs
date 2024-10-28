using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField]
    private int currentGold = 10; // 현재 골드
    public int CurrentGold {
        set => currentGold = Mathf.Max(0, value);
        get => currentGold;
    }
}
