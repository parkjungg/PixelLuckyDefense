using System.Collections;
using UnityEngine;

public enum EnemyDestroyType {kill = 0, Arrive}
public class Enemy : MonoBehaviour
{
    private int wayPointCount; // 이동 경로 개수
    private Transform[] wayPoints; // 이동 경로 정보
    private int currentIndex = 0; // 현재 목표지점 인덱스
    private Movement2D movement2D; // 적 이동 제어
    private EnemySpwaner enemySpawner;
    [SerializeField]
    private int gold = 10; // 적 사망 시 획득 가능한 골드

    
    public void Setup(EnemySpwaner enemySpawner, Transform[] wayPoints) {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        // 적 이동 경로 WayPoints 정보 설정
        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // 적의 위치를 첫 번째 wayPoint 위치로 설정
        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove() {
        // 다음 이동 방향 설정
        NextMoveTo();

        // 적의 현재위치와 목표위치의 거리가 0.02 * movement2D.MoveSpeed 보다 작을 때 if 조건문 실행 
        while(true) {
            if(Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed) {
                NextMoveTo();
            }
            yield return null;
        }
    }
    private void NextMoveTo() {
        // 아직 이동할 wayPoints가 남아있다면 실행
        if(currentIndex < wayPointCount - 1) {
            // 적의 위치를 목표 위치로 설정
            transform.position = wayPoints[currentIndex].position;
            // 이동 방향 설정 → 다음 목표 지점(wayPoints) 
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        // 만약 현재 위치가 마지막 wayPoints라면 적 오브젝트 삭제
        else {
            gold = 0; // 목표 지점이라면 골드를 지급하지 않음 
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type) {
        movement2D.StopMoving();
        enemySpawner.DestroyEnemy(type, this, gold);
    }
}
