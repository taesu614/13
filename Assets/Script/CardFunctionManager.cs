using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardFunctionManager : MonoBehaviour
{
    private Dictionary<string, Action> cardEffects = new Dictionary<string, Action>();

    //����Ǵ� ������ ����
    [SerializeField] TMP_Text costTMP;  //��꿡 ���� ���̹Ƿ� num = int.Parse(costTMP); �صѰ�
    [SerializeField] TMP_Text rcostTMP;
    [SerializeField] TMP_Text gcostTMP;
    [SerializeField] TMP_Text bcostTMP;
    [SerializeField] GameObject cardPrefab;  //�� �ڽ�Ʈ X ī�忡 ���� �ڽ�Ʈ O

    ItemSO itemSO;
    Card card;
    public CostManager costManager;
    CardManager cardManager;

    // Start is called before the first frame update
    void Start()
    {
        // ī�� �̸��� ����� ��Ī�Ͽ� Dictionary�� ����
        cardEffects["Moon"] = AddCost;
        cardEffects["TheHangedMan"] = BoostMyBooster;
        cardEffects["TheDevil"] = () => Combo(myscore); // ���ٽ� ��� ����
    }

    // ���÷� ���� �޼����
    private void AddCost()
    {
        //��, TheMoon, �ڽ�Ʈ�� 1 ����ϴ�
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

    // ī�� ���� �޼���
    public void UseSelectCard(string cardName)
    {
        if (cardEffects.TryGetValue(cardName, out Action cardEffect))
        {
            // �ش� ī���� ��� ����
            cardEffect();
        }
        else
        {
            Debug.Log("Card not found.");
        }
    }

    // ���÷� ������ ����
    private int myscore = 100;
}