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
    [SerializeField]
    private Text speedText;    
    
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
    [SerializeField]
    private AudioManager audioManager;
    [SerializeField]
    private AudioSource backgroundMusic;
    private bool gameSpeedOn = false;
    public float gameSpeed = 1;
    void Awake() {
        if(instance == null) {
            instance = this;
        }
        gameSpeed = 1;
        Time.timeScale = 1;
        Application.targetFrameRate = 60;
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
        backgroundMusic.mute = true;
        audioManager.GameOverSound();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    private void ShowGameVictoryPanel() {
        backgroundMusic.mute = true;
        audioManager.GameVictorySound();
        gameVictoryPanel.SetActive(true);
        Time.timeScale = 0;
    }
    // 게임 속도 조정 → 최대 2배 조정 가능(토글 형식)
    public void SetGameSpeed() {
        if(!gameSpeedOn) {
            Time.timeScale = 2;
            gameSpeed = 2;
            gameSpeedOn = true;
            speedText.text = "x2";
        }
        else {
            Time.timeScale = 1;
            gameSpeed = 1;
            gameSpeedOn = false;
            speedText.text = "x1";
        }        
    }
}
