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
    public void StartWave() {
        if(enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1) {
            currentWaveIndex++;
            enemySpawner.StartWave(waves[currentWaveIndex]);
            if (currentWaveIndex == 0) {
                roundNum = 1;
            }          
            else {
                roundNum++;
            }            
        }
    }  
}

[System.Serializable]
public struct Wave {
    public float spawnTime; // 현재 웨이브 적 생성 주기
    public int maxEnemyCount; // 현재 웨이브 적 등장 숫자 
    public GameObject[] enemyPrefabs; // 적 종류
}
