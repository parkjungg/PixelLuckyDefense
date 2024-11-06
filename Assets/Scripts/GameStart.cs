
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject canvas1;
    public void StartGame() {
        SceneManager.LoadScene("MainScene");
    }
    public void ReturnToMain() {
        SceneManager.LoadScene("HomeScene");
    }
    public void TurnOnCanvas1() {
        canvas1.SetActive(true);
    }
    public void TurnOffCanvas1() {
        canvas1.SetActive(false);        
    }
}
