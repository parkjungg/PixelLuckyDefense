using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 20; // 플레이어 체력
    private float currentHP;
    [SerializeField]
    private Image imageScreen; // 적이 플레이어의 체력을 감소시키면 발생하는 화면 UI

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;    

    private void Awake() {
        currentHP = maxHP; // 초기 체력을 최대 체력으로 초기화
    }

    public void TakeDamage(float damage) {
        currentHP -= damage;    // 데미지 만큼 체력 감소
        StopCoroutine("HitAnimation");
        StartCoroutine("HitAnimation");
        if(currentHP <= 0) { // 체력이 0이라면 게임오버
            GameManager.instance.SetGameOver();
        }
    }
    private IEnumerator HitAnimation() {
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;

        while(color.a >= 0.0f) {
            color.a -= Time.deltaTime;
            imageScreen.color = color;

            yield return null;
        }
    }
}
