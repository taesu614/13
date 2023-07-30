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
        Sequence sequence = DOTween.Sequence()  //도트윈으로 화면 띄우기?
        .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))   //몇초간 변화되는지
        .AppendInterval(0.9f)   //몇초간 보여주는지
        .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
    }

    void Start() => ScaleZero();

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;

    [ContextMenu("ScaleZero")]
    public void ScaleZero() => transform.localScale = Vector3.zero;
}
