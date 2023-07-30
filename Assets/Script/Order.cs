using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour  //�ؽ�Ʈ �Ž� ���δ� ���� ������ �ȵǾ� ��ũ��Ʈ�� ������
{
    [SerializeField] Renderer[] backRenderers;
    [SerializeField] Renderer[] middleRenderers;
    [SerializeField] string sortingLayerName;
    int originOrder;

    public void SetOriginOrder(int originOrder) //ī�� Ȯ���
    {
        this.originOrder = originOrder;
        SetOrder(originOrder);
    }

    public void SetMostFrontOrder(bool isMostFront) //ī�� Ȯ���
    {
        SetOrder(isMostFront ? 100 : originOrder);
    }

    public void SetOrder(int order)
    {
        int mulOrder = order * 10;

        foreach (var renderer in backRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder;
        }

        foreach (var renderer in middleRenderers)
        {
            renderer.sortingLayerName = sortingLayerName;
            renderer.sortingOrder = mulOrder + 1;
        }
    }
}
