using UnityEngine;
using UnityEngine.UI;

public class TowerInfoViewer : MonoBehaviour
{
    [SerializeField]
    private Text textDamage;
    [SerializeField]
    private Text textRate;
    [SerializeField]
    private Text textRange;
    [SerializeField]
    private Image towerImage;
    private TowerWeapon currentTower;
    [SerializeField]
    private TowerAttackRange towerAttackRange;
    private bool isInfoPanelAcitve = false;

    private void Awake() {
        OffTowerInfo();
    }
    public void OnTowerInfo(Transform towerWeapon) {
        currentTower = towerWeapon.GetComponent<TowerWeapon>();
        gameObject.SetActive(true);
        isInfoPanelAcitve = true;
        UpdateTowerInfo();
        towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);

        if(currentTower.TowerImage != null) {
            towerImage.sprite = currentTower.TowerImage;
            towerImage.enabled = true;
        }
        else {
            towerImage.enabled = false;
        }
    }
    private void Update() {
        if(isInfoPanelAcitve && currentTower != null) {
            UpdateTowerInfo(); // 실시간 스탯 반영 
        }
    }
    public void OffTowerInfo() {
        gameObject.SetActive(false); // 타워 정보 Off
        isInfoPanelAcitve = false;        
    }
    private void UpdateTowerInfo() {
        // 스탯 정보 출력
        textDamage.text = "Damage : " + currentTower.Damage;
        textRange.text = "Range : " + currentTower.Range;
        textRate.text = "Rate : " + currentTower.Rate;
    }
    public void SellTower() {
        currentTower.Sell();
        OffTowerInfo();
    }
}
