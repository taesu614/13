using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;  //랜덤 턴 삭제 시 삭제할 것

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    public CostManager costManager;
    void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField] [Tooltip("시작 턴 모드를 정합니다")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("카드 배분이 매우 빨라집니다")] bool fastMode;
    [SerializeField] [Tooltip("시작 카드 개수를 정합니다")] int startCardCount;

    [Header("Properties")]  //일반적 상황
    public bool isLoading;  //게임 끝나면 isLoading을 true로 하면 카드와 엔티티 클릭 방지
    public bool myTurn;

    enum ETurnMode { Random, My, Other }    //턴 수 정하는 부분 적 선제공격 가능성으로 삭제하지 않음
    WaitForSeconds delay05 = new WaitForSeconds(0.5f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAddCard;
    public static event Action<bool> OnTurnStarted; 
    void GameSetup() //턴 수 정하는 부분 적 선제공격 가능성으로 삭제하지 않음
    {
        if (fastMode)
            delay05 = new WaitForSeconds(0.05f);

        switch (eTurnMode)
        {
            case ETurnMode.Random:
                myTurn = Random.Range(0, 2) == 0;
                break;
            case ETurnMode.My:
                myTurn = true;
                break;
            case ETurnMode.Other:
                myTurn = false;
                break;
        }
    }

    public IEnumerator StartGameCo()
    {
        GameSetup();
        isLoading = true;

        for (int i = 0; i < startCardCount; i++)
        {
            yield return delay05;
            OnAddCard?.Invoke(false);
            yield return delay05;
            OnAddCard?.Invoke(true);
        }
        StartCoroutine(StartTurnCo());
    }

    IEnumerator StartTurnCo()
    {
        isLoading = true;
        if (myTurn)
        {
            costManager.CostSet();
            costManager.ShowCost();
            GameManager.Inst.Notification("나의 턴");
        }
            

        yield return delay07;
        OnAddCard?.Invoke(myTurn);
        yield return delay07;
        isLoading = false;
        OnTurnStarted?.Invoke(myTurn);
    }

    public void EndTrun()
    {
        myTurn = !myTurn;
        StartCoroutine(StartTurnCo());

    }
}
