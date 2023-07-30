using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardFunctionManager : MonoBehaviour
{
    private Dictionary<string, Action> cardEffects = new Dictionary<string, Action>();

    //예상되는 변수들 모음
    [SerializeField] TMP_Text costTMP;  //계산에 쓰일 것이므로 num = int.Parse(costTMP); 해둘것
    [SerializeField] TMP_Text rcostTMP;
    [SerializeField] TMP_Text gcostTMP;
    [SerializeField] TMP_Text bcostTMP;
    [SerializeField] GameObject cardPrefab;  //내 코스트 X 카드에 써진 코스트 O

    ItemSO itemSO;
    Card card;
    public CostManager costManager;
    CardManager cardManager;

    // Start is called before the first frame update
    void Start()
    {
        // 카드 이름과 기능을 매칭하여 Dictionary에 저장
        cardEffects["Moon"] = AddCost;
        cardEffects["TheHangedMan"] = BoostMyBooster;
        cardEffects["TheDevil"] = () => Combo(myscore); // 람다식 사용 예시
    }

    // 예시로 만든 메서드들
    private void AddCost()
    {
        //달, TheMoon, 코스트를 1 얻습니다
        int cost = int.Parse(costTMP.text);
        cost++;
        costManager.CostSetNewCost(cost);
    }

    private void BoostMyBooster()
    {
        Debug.Log("BoostMyBooster()");
    }

    private void Combo(int score)
    {
        Debug.Log("Combo(" + score + ")");
    }

    // 카드 실행 메서드
    public void UseSelectCard(string cardName)
    {
        if (cardEffects.TryGetValue(cardName, out Action cardEffect))
        {
            // 해당 카드의 기능 실행
            cardEffect();
        }
        else
        {
            Debug.Log("Card not found.");
        }
    }

    // 예시로 선언한 변수
    private int myscore = 100;
}