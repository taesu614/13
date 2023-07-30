using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Entity : MonoBehaviour //�ش� ������ ���� ���ڸ� ���� ��ȹ �Ƹ� �ٸ� ItemSO�� �����
{
    [SerializeField] Item item;
    [SerializeField] SpriteRenderer entity;
    [SerializeField] SpriteRenderer charater;
    [SerializeField] TMP_Text healthTMP;

    public int attack;
    public int health;
    public bool isMine;
    public bool isBossOrEmpty;
    public Vector3 originPos;
    int liveCount;

    void Start()
    {
        TurnManager.OnTurnStarted += OnTurnStarted;
    }

    void OnDestroy()
    {
        TurnManager.OnTurnStarted -= OnTurnStarted;   
    }

    void OnTurnStarted(bool myTurn)
    {
        if (isBossOrEmpty)
            return;

        if (isMine == myTurn)
            liveCount++;
    }
    public void Setup(Item item)
    {
        health = int.Parse(healthTMP.text);

        this.item = item;
    }

    public void MoveTransform(Vector3 pos, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
            transform.DOMove(pos, dotweenTime);
        else
            transform.position = pos;
    }
}
