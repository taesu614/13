using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificatonPanel : MonoBehaviour
{
    [SerializeField] TMP_Text notificationTMP;

    public void Show(string message)
    {
        notificationTMP.text = message;
        Sequence sequence = DOTween.Sequence()  //��Ʈ������ ȭ�� ����?
        .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))   //���ʰ� ��ȭ�Ǵ���
        .AppendInterval(0.9f)   //���ʰ� �����ִ���
        .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
    }

    void Start() => ScaleZero();

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;

    [ContextMenu("ScaleZero")]
    public void ScaleZero() => transform.localScale = Vector3.zero;
}
