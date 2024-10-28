using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("# Game Info")]
    [SerializeField]
    private Text textPlayerHP;
    [SerializeField]
    private PlayerHP playerHP; // 플레이어 체력
    [SerializeField]
    private Text textGold;
    [SerializeField]
    private Gold playerGold;
    [SerializeField]
    private Text textDamage;
    [SerializeField]
    private Text textNeedsGold;
    [SerializeField]
    private Text roundText;
    [SerializeField]
    private Text cardName;    
    
    [Header("# Game Component")]
    [SerializeField]
    TowerManager towerManager;
    [SerializeField]
    WaveSystem waveSystem;
    public static GameManager instance;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject gameVictoryPanel;
    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }
    
    private void Update() {
        textPlayerHP.text = playerHP.CurrentHP + "/" + playerHP.MaxHP;
        textGold.text = playerGold.CurrentGold.ToString();
        textDamage.text = "Damage : +" + TowerWeapon.upgradeBonus.ToString();
        textNeedsGold.text = towerManager.upgradeGold.ToString();
        roundText.text = "Round : " + waveSystem.roundNum.ToString();
    }
    public void SetGameOver() {
        Invoke("ShowGameOverPanel", 1f);
    }
    public void SetGameVictory() {
        Invoke("ShowGameVictoryPanel", 1f);
    }
    private void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }
    private void ShowGameVictoryPanel() {
        gameVictoryPanel.SetActive(true);
    }
}
