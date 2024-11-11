using System.Collections;
using UnityEngine;

public enum EnemyDestroyType { kill = 0, Arrive }
public class Enemy : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0; 
    private Movement2D movement2D; 
    private EnemySpwaner enemySpawner;
    [SerializeField]
    private int gold = 10;

    public void Setup(EnemySpwaner enemySpawner, Transform[] wayPoints) {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine(OnMove());
    }

    private IEnumerator OnMove() {
        // 이동 루프
        while (true) {
            // 현재 목표 지점과의 거리 계산
            float distanceToTarget = Vector3.Distance(transform.position, wayPoints[currentIndex].position);

            // 목표 지점에 도달했으면 다음 지점으로 이동
            if (distanceToTarget <= 0.1f) { 
                NextMoveTo();
            }

            yield return null;
        }
    }

    private void NextMoveTo() {
        // 다음 지점으로 이동
        if (currentIndex < wayPointCount - 1) {
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        } else {
            // 마지막 지점 도달 시 적 삭제 처리
            gold = 0;
            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type) {
        movement2D.StopMoving();
        enemySpawner.DestroyEnemy(type, this, gold);
    }
}