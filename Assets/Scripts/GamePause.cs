using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public Button muteButton;
    public GameObject gamePausePanel;
    public Sprite muteSprite;
    public Sprite unMuteSprite;
    private bool isMute = false;
    public void GamePauseNow() {
        gamePausePanel.SetActive(true);
        if(GameManager.instance != null) {
            GameManager.instance.gameSpeed = Time.timeScale;
        }
        Time.timeScale = 0;
    }
    public void BackToGame() {
        gamePausePanel.SetActive(false);
        if(GameManager.instance != null) {
            Time.timeScale = GameManager.instance.gameSpeed;
        }
        else Time.timeScale = 1;        
    }
    public void MuteAll() {
        isMute = !isMute;
        Image buttonImage = muteButton.GetComponent<Image>();
        if(buttonImage != null) {
            buttonImage.sprite = isMute ? muteSprite : unMuteSprite; // 음소거 여부에 따른 이미지 변경
        }
        AudioListener.volume = isMute ? 0 : 1; // 음소거 전환
    }
}
