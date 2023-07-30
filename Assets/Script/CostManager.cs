using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CostManager : MonoBehaviour
{
    [SerializeField] TMP_Text costTMP;  //계산에 쓰일 것이므로 num = int.Parse(costTMP); 해둘것
    [SerializeField] TMP_Text rcostTMP;
    [SerializeField] TMP_Text gcostTMP;
    [SerializeField] TMP_Text bcostTMP;
    public static CostManager Inst { get; private set; }
    void Awake() => Inst = this;
    private int mycost = 0;
    private int hasmycost;
    private bool can;
    int rcost = 0;
    int gcost = 0;
    int bcost = 0;

    public void ShowCost()
    {
        costTMP.text = hasmycost.ToString();
        rcostTMP.text = rcost.ToString();
        gcostTMP.text = gcost.ToString();
        bcostTMP.text = bcost.ToString();
    }

    public void CostSet()  //코스트 늘려주기
    {
        if(mycost < 5)
            mycost++;

        hasmycost = mycost;
    }

    public void CostSetNewCost(int cost)
    {
        hasmycost = cost;
        ShowCost();
    }

    public bool CompareCost(Card card)  //코스트 비교
    {
        Debug.Log(card.item.cost);
        Debug.Log(hasmycost);
        if (hasmycost < card.item.cost)
        {
            can = false;
        }
        else
        {
            can = true;
        }

        return can;
    }

    public void SubtractCost(Card card)
    {
        hasmycost -= card.item.cost;
        if(card.item.color == 'R')
        {
            rcost++;
        }
        else if(card.item.color == 'G')
        {
            gcost++;
        }
        else if(card.item.color == 'B')
        {
            bcost++;
        }

        Debug.Log("R: " +rcost + ", G: " + gcost + ", B: "+ bcost);
    }
}
