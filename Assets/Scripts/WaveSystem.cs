using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpwaner enemySpawner;
    private int currentWaveIndex = -1;
    public int roundNum = 0;
    private bool isPressed = false;
    [SerializeField]
    private AudioManager audioManager;
    public void StartWave() {
        if(isPressed) return;
        if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1) {            
            if(!isPressed) {
                isPressed = true;
                currentWaveIndex++;
                audioManager.ButtonPressedSound3();
                enemySpawner.StartWave(waves[currentWaveIndex]);              
            }            
            if (currentWaveIndex == 0) {
                roundNum = 1;
            }          
            else {
                roundNum++;
            }            
        }
    }
    public void EndWave() {
        isPressed = false;
    }  
}

[System.Serializable]
public struct Wave {
    public float spawnTime; // 현재 웨이브 적 생성 주기
    public int maxEnemyCount; // 현재 웨이브 적 등장 숫자 
    public GameObject[] enemyPrefabs; // 적 종류
}
