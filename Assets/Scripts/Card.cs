using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
public class Card : MonoBehaviour
{
    public bool isFlipped = false; // 카드를 뒤집었는지 여부 확인
    private bool isFlipping = false; // 뒤집는 도중에 카드를 다시 뒤집기 불가 하기 위한 변수
    public int randomIndex = 0;
    private int cardIndex;
    private string cardName;
    private static int rerollChance = 1;
    [SerializeField]
    private Sprite[] cardSprite; // 16가지 카드 배경
    [SerializeField]
    private Sprite basicCard; // 기본 카드 뒷 배경
    private double[] probabilities = {0.1, 0.1, 0.1, 0.1, 0.1, 0.11, 0.11, 0.11, 0.051, 0.051,
                                     0.051, 0.008, 0.005, 0.003, 0.0008, 0.0002}; // 16장 카드의 각각 등장할 확률
    [SerializeField]
    private static List<int> selectedCardIndices = new List<int>(); // 이미 선택된 카드 인덱스 저장
    [SerializeField]
    private Text cardNameText;
    public void CardClick(GameObject clickedButton) { // 카드 클릭시 카드 이미지 변경
        if(!isFlipped && !isFlipping) {
            isFlipped = true;
            isFlipping = true;
            clickedButton.transform.DORotate(new Vector3(0, 90, 0), 0.2f) // 0.2초간 목표 Scale까지 뒤집음
            .OnComplete(() => {
                randomIndex = GetUniqueRandomIndex();
                clickedButton.GetComponent<Image>().sprite = cardSprite[randomIndex]; // 절반 뒤집었으니 이미지 변경

                clickedButton.transform.DORotate(Vector3.zero, 0.2f) // 나머지 0.2초간 다시 뒤집음
                .OnComplete(() => {
                    isFlipping = false; // 뒤집는 상태가 아니므로 다시 상태 변경
                    CheckCardNum(randomIndex); // 카드 족보 확인
                    CheckCard();
                    cardNameText.text = cardName;                    
                });
            });            
        }        
    }
    public void FlipToBack() { // 카드 초기화를 위한 메서드
        GetComponent<Image>().sprite = basicCard;
        isFlipped = false;
    }
    private int GetUniqueRandomIndex() { // 카드를 뒤집기 전 랜덤으로 인덱스를 가져오는 함수
        int randomIndexNum = -1;
        double randomValue = Random.value; // 0 과 1 사이의 랜덤 숫자를 가져옴
        double cumulativeProbabiility = 0.0;
        
        for(int i = 0; i < probabilities.Length; i++) {
            cumulativeProbabiility += probabilities[i];
            if(randomValue < cumulativeProbabiility) {
                randomIndexNum = i;
                break;
            }
        }
        selectedCardIndices.Add(randomIndexNum);
        return randomIndexNum;
    }
    public void Reroll(GameObject clickedButton) { // 1회 리롤 함수
        if(isFlipped && !isFlipping && rerollChance == 1) {
            isFlipping = true;
            clickedButton.transform.DORotate(new Vector3(0, 90, 0), 0.2f) // 0.2초간 목표 Scale까지 뒤집음
            .OnComplete(() => {
                randomIndex = GetUniqueRandomIndex();
                clickedButton.GetComponent<Image>().sprite = cardSprite[randomIndex]; // 절반 뒤집었으니 이미지 변경

                clickedButton.transform.DORotate(Vector3.zero, 0.2f) // 나머지 0.2초간 다시 뒤집음
                .OnComplete(() => {
                    isFlipping = false; // 뒤집는 상태가 아니므로 다시 상태 변경
                    CheckCardNum(randomIndex); // 카드 족보 확인 
                    CheckCard();
                    cardNameText.text = cardName;
                });
            });
            rerollChance--;
        }
    }
    public static void ResetSelectedCards() {
        selectedCardIndices.Clear(); // 모든 카드를 초기화할 때 중복 리스트를 마찬가지로 초기화 
        rerollChance = 1; // 리롤 기회도 초기화 
    }
    // 카드의 족보를 알기 위한 함수
    public void CheckCardNum(int randomIndex) {
        if(randomIndex <= 4) cardIndex = 0;
        else if (randomIndex > 4 && randomIndex <= 7) cardIndex = 1;
        else if (randomIndex > 7 && randomIndex <= 9) cardIndex = 2;
        else if (randomIndex == 10) cardIndex = 3;
        else if (randomIndex == 11) cardIndex = 4;
        else if (randomIndex == 12) cardIndex = 5;
        else if (randomIndex == 13) cardIndex = 6;
        else if (randomIndex == 14) cardIndex = 7;
        else cardIndex = 8;
    }
    private void CheckCard() {
        if(cardIndex == 0) cardName = "Common";
        else if(cardIndex == 1) cardName = "Rare";
        else if(cardIndex == 2) cardName = "Ancient";
        else if(cardIndex == 3) cardName = "Relic";
        else if(cardIndex == 4) cardName = "Ultimate";
        else if(cardIndex == 5) cardName = "Legend";
        else if(cardIndex == 6) cardName = "Epic";
        else if(cardIndex == 7) cardName = "Myth";
        else cardName = "Transcendent";
    }
}

