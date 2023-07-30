using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;  //���� �� ���� �� ������ ��

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    public CostManager costManager;
    void Awake() => Inst = this;

    [Header("Develop")]
    [SerializeField] [Tooltip("���� �� ��带 ���մϴ�")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("ī�� ����� �ſ� �������ϴ�")] bool fastMode;
    [SerializeField] [Tooltip("���� ī�� ������ ���մϴ�")] int startCardCount;

    [Header("Properties")]  //�Ϲ��� ��Ȳ
    public bool isLoading;  //���� ������ isLoading�� true�� �ϸ� ī��� ��ƼƼ Ŭ�� ����
    public bool myTurn;

    enum ETurnMode { Random, My, Other }    //�� �� ���ϴ� �κ� �� �������� ���ɼ����� �������� ����
    WaitForSeconds delay05 = new WaitForSeconds(0.5f);
    WaitForSeconds delay07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAddCard;
    public static event Action<bool> OnTurnStarted; 
    void GameSetup() //�� �� ���ϴ� �κ� �� �������� ���ɼ����� �������� ����
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
            GameManager.Inst.Notification("���� ��");
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
