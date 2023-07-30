using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer colorimg;
    [SerializeField] SpriteRenderer costcolor;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text costTMP;  //��꿡 ���� ���̹Ƿ� num = int.Parse(costTMP); �صѰ�
    [SerializeField] TMP_Text acitveTMP;
    [SerializeField] Sprite cardFront;  //�������Ҽ���?
    [SerializeField] Sprite cardBack;   //22

    public Item item;
    bool isFront;   //delete
    public PRS originPRS;
    public string functionname;

    public void Setup(Item item, bool isMine)
    {
        this.item = item;
        colorimg.sprite = this.item.colorimg;
        costcolor.sprite = this.item.costcolor;
        character.sprite = this.item.sprite;
        nameTMP.text = this.item.name;
        costTMP.text = this.item.cost.ToString();
        acitveTMP.text = this.item.active;
        functionname = this.item.functionname;

        if(this.item.color == 'R')  //���� ���� �����ϸ� �۾� �� �ٲ�
        {
            nameTMP.color = new Color32(255, 88, 88, 255);
            costTMP.color = new Color32(255, 88, 88, 255);
        }
        else if (this.item.color == 'G')
        {
            nameTMP.color = new Color32(88, 255, 88, 255);
            costTMP.color = new Color32(88, 255, 88, 255);
        }
        if (this.item.color == 'B')
        {
            nameTMP.color = new Color32(88, 88, 255, 255);
            costTMP.color = new Color32(88, 88, 255, 255);
        }
    }

    void OnMouseOver()
    {
        CardManager.Inst.CardMouseOver(this);
    }

    void OnMouseExit()
    {
        CardManager.Inst.CardMouseExit(this);    
    }
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if(useDotween)  //��Ʈ�� �Ἥ �ε巴�� �����̰��ϱ�
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }

    void OnMouseDown()
    {
        CardManager.Inst.CardMouseDown();
    }

    void OnMouseUp()
    {
        CardManager.Inst.CardMouseUp();
    }
}