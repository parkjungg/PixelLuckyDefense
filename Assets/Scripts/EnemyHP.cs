using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP; // 적의 기본 체력
    private float currentHP; // 현재 체력
    private bool isDie = false; 
    private Enemy enemy;
    private Movement2D movement2D;
    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    
    [SerializeField]
    Animator anim;

    private void Awake() {
        currentHP = maxHP; // 적의 현재 체력을 기본 체력으로 설정
        enemy = GetComponent<Enemy>();
        movement2D = GetComponent<Movement2D>();
    }

    public void TakeDamage(float damage) {
        if(isDie == true) return;

        currentHP -= damage;
        if(currentHP <= 0) {
            isDie = true;
            anim.SetBool("isDeath", true);
            anim.SetTrigger("4_Death");
            movement2D.StopMoving();
            StartCoroutine("DelayedOnDie");
        }
    } 
    private IEnumerator DelayedOnDie() {
        yield return new WaitForSeconds(1f);
        enemy.OnDie(EnemyDestroyType.kill);
        
    }
}
