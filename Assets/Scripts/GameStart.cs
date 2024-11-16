using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;
    public void StartGame() {
        TowerWeapon.upgradeBonus = 0;
        SceneManager.LoadScene("MainScene");
    }
    public void ReturnToMain() {
        TowerWeapon.upgradeBonus = 0;
        SceneManager.LoadScene("HomeScene");
    }
    public void TurnOnCanvas1() {
        canvas1.SetActive(true);
    }
    public void TurnOffCanvas1() {
        canvas1.SetActive(false);        
    }
    public void TurnOnCanvas2() {
        canvas2.SetActive(true);
    }
    public void TurnOffCanvas2() {
        canvas2.SetActive(false);        
    }
    public void GameQuit() {
        Application.Quit();
    }
}
