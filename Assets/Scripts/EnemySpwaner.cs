using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
    [SerializeField]
    private Transform[] wayPoints; // 현재 스테이지의 이동 경로
    private Wave currentWave; // 현재 웨이브 정보
    [SerializeField]
    private WaveSystem waveSystem;
    private int enemyIndex = 0;
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private Gold PlayerGold;
    private List<Enemy> enemyList; // 현재 맵에 존재하는 모든 적의 정보

    public List<Enemy> EnemyList => enemyList;
    [SerializeField]
    private GameObject enemyHPSliderPrefab;
    [SerializeField]
    private Transform canvasTransform;

    private void Awake() {
        enemyList = new List<Enemy>();
    }
    public void StartWave(Wave wave) {
        currentWave = wave;
        StartCoroutine("StartWaveWithDelay");
    }
    private IEnumerator StartWaveWithDelay() { // 버튼을 누르면 1초후에 적 생성
        yield return new WaitForSeconds(1f);
        StartCoroutine("SpawnEnemy");
    }
    private IEnumerator SpawnEnemy() {

        int spawnEnemyCount = 0;
        while(spawnEnemyCount < currentWave.maxEnemyCount) {
            
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]); // 적 오브젝트 생성
            Enemy enemy = clone.GetComponent<Enemy>(); // 방금 생성된 적의 Enemy 컴포넌트 호출

            enemy.Setup(this, wayPoints); // wayPoint 정보를 매개변수로 사용하여 Setup() 호출
            enemyList.Add(enemy); // 리스트에 적의 정보 저장

            SpawnEnemyHPSlider(clone);

            spawnEnemyCount++;
            enemyIndex++;
            if(enemyIndex == currentWave.enemyPrefabs.Length) {
                 enemyIndex = enemyIndex - 1;  
            }

            yield return new WaitForSeconds(currentWave.spawnTime);
        }                      
    }
    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold) {    
        if(type == EnemyDestroyType.Arrive) {            
            if(waveSystem.roundNum == 10 || waveSystem.roundNum == 20) { // 10, 20단계 일때는 보스이므로 처지 못할 시 즉시 패배 
                playerHP.TakeDamage(20);
            }
            else{
                playerHP.TakeDamage(1);
            }
        }
        else if(type == EnemyDestroyType.kill) {
            PlayerGold.CurrentGold += gold; // 적의 종류에 따라 사망 시 골드 획득
        }    
        enemyList.Remove(enemy); // 리스트에서 사망하는 적 정보 삭제
        Destroy(enemy.gameObject); // 적 오브젝트 삭제
        if(enemyList.Count == 0 && waveSystem.roundNum == 20) {
            GameManager.instance.SetGameVictory(); // 마지막 라운드 클리어시 승리
        }
    }
    private void SpawnEnemyHPSlider(GameObject enemy) {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<EnemySlider>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}

