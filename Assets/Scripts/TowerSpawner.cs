using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{     
    [SerializeField]
    private List<GameObject> cards; // 카드 버튼 오브젝트들을 리스트에 삽입
    [SerializeField]
    private GameObject selectCardPanel;
    [SerializeField]
    private GameObject[] towerPrefab;
    [SerializeField]
    private int towerBuildGold = 10; // 타워 건설에 필요한 골드
    [SerializeField]
    private Gold playerGold;
    [SerializeField]
    private Card card;
    private Transform tileTransform;
    [SerializeField]
    private EnemySpwaner enemySpwaner;
    private CanvasGroup selectCardPanelCanvasGroup;
    public int cardIndex;
    public bool isPanelOn = false;

    private void Awake() {
        selectCardPanelCanvasGroup = selectCardPanel.GetComponent<CanvasGroup>();
    }

    public void SpawnTower(Transform tileTransform) {
        Tile tile = tileTransform.GetComponent<Tile>();
        if(tile.isBuildTower == true) {
            return; // 해당 위치에 타워가 있다면 함수를 빠져나가 Panel이 뜨지 않게끔 함(중복 방지)
        }
        if(towerBuildGold > playerGold.CurrentGold) {
            return;
        } // 타워를 건설할 만큼의 골드가 없다면 건설을 못하게 함
        this.tileTransform = tileTransform;   
        tile.isBuildTower = true;
        ShowCardPanel();
        isPanelOn = true;            
    }
    public void ShowCardPanel() { 
        selectCardPanel.SetActive(true);
        StartCoroutine("FadeInPanel");
    }
    private IEnumerator FadeInPanel() { // 패널 등장 애니메이션
        float duration = 0.5f; // 애니메이션 효과 시간 
        float elapsed = 0f;

        selectCardPanelCanvasGroup.alpha = 0f;
        while(elapsed < duration) {
            elapsed += Time.deltaTime;
            selectCardPanelCanvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        selectCardPanelCanvasGroup.alpha = 1f;
    }
    private IEnumerator FadeOutPanel() { // 패널 소실 애니메이션
        float duration = 0.5f; 
        float elapsed = 0f;

        while(elapsed < duration) {
            elapsed += Time.deltaTime;
            selectCardPanelCanvasGroup.alpha = Mathf.Clamp01(1f - (elapsed / duration));
            yield return null;
        }
        selectCardPanelCanvasGroup.alpha = 0f;
        selectCardPanel.SetActive(false);
        ResetCards(); // 카드 조합을 선택하고 카드들을 초기화해서 다음번에 완전히 새로운 카드들이 되도록 함
    }
    public void CloseCardPanel() {
        if(card.randomIndex >= 0 && card.randomIndex < towerPrefab.Length && card.isFlipped) {
            Tile tile = tileTransform.GetComponent<Tile>();
            Vector3 position = tileTransform.position + Vector3.back; // 선택한 타일의 위치에 타워 건설(타일보다 z축 -1의 위치에 배치)
            GameObject clone = Instantiate(towerPrefab[card.randomIndex], position, Quaternion.identity);
            clone.GetComponent<TowerWeapon>().Setup(enemySpwaner, playerGold, tile);
        }
        if(card.isFlipped) {
            StartCoroutine("FadeOutPanel");
            isPanelOn = false;
            playerGold.CurrentGold -= towerBuildGold; // 타워 건설에 필요한 골드만큼 감소
        }
       
    }
    private void ResetCards() { // 카드 초기화 메서드
        foreach(GameObject card in cards) {
            card.GetComponent<Card>().FlipToBack();
        }
        Card.ResetSelectedCards();
    }
}
