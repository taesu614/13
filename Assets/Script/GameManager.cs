using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ġƮ ui ��ŷ ���ӿ���
public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] NotificatonPanel notificationPanel;
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        InputCheatKey();
#endif
    }

    void InputCheatKey()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))  //���߿� ��ư���� �۵��ϰ� �ٲٱ�
        {
            TurnManager.OnAddCard.Invoke(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            TurnManager.Inst.EndTrun();
        }
    }
    
    public void StartGame()
    {
        StartCoroutine(TurnManager.Inst.StartGameCo());
    }

    public void Notification(string message)
    {
        notificationPanel.Show(message);
    }
}