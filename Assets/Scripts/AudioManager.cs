using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] buttonClickClip;
    [Range(0f, 1f)]
    public float volume = 0.4f;

    private void Awake() {
        // AudioSource가 없으면 추가
        if (GetComponent<AudioSource>() == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        } else {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.volume = volume;
    }

    public void ButtonPressedSound() { // 기본 버튼 클릭시
        audioSource.clip = buttonClickClip[0];
        audioSource.volume = volume;
        audioSource.Play();
    }
    public void ButtonPressedSound1() { // 카드 뒤집을시
        audioSource.clip = buttonClickClip[1];
        audioSource.volume = volume;
        audioSource.Play();
    }
    public void ButtonPressedSound2() { // 카드 결정시
        audioSource.clip = buttonClickClip[2];
        audioSource.volume = volume;
        audioSource.Play(); 
    }
    public void ButtonPressedSound3() { // 시작 버튼 클릭시
        audioSource.clip = buttonClickClip[3];
        audioSource.volume = volume;
        audioSource.Play(); 
    }
    public void TilePressedSound() { // 타일 선택시
        audioSource.clip = buttonClickClip[0];
        audioSource.volume = volume;
        audioSource.Play();
    }
    public void GameOverSound() { // 게임 오버시
        audioSource.clip = buttonClickClip[4];
        audioSource.volume = volume;
        audioSource.Play();
    }
    public void GameVictorySound() { // 게임 승리시
        audioSource.clip = buttonClickClip[5];
        audioSource.volume = volume;
        audioSource.Play();
    }

}