using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject entityPrefab;
    [SerializeField] List<Entity> myEntities;
    [SerializeField] List<Entity> otherEntites;
    [SerializeField] Entity myEmptyEntity;
    [SerializeField] Entity myBossEntity;
    [SerializeField] Entity otherBossEntity;

    const int MAX_ENTITY_COUNT = 6; //엔티티 최대 개수 및 정렬
    public bool IsFullMyEntities => myEntities.Count >= MAX_ENTITY_COUNT && !ExistMyEmptyEntity;
    bool IsFullOtherEntities => otherEntites.Count >= MAX_ENTITY_COUNT;
    bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);
    int MyEmptyEntityIndex => myEntities.FindIndex(x => x == myEmptyEntity);

    WaitForSeconds delay1 = new WaitForSeconds(1);

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
        if (!myTurn)
            StartCoroutine(AICo());
    }

    IEnumerator AICo()  //적 턴 시작시 AI 코루틴 시작됨
    {
        yield return delay1;

        //공격로직
        TurnManager.Inst.EndTrun();
    }

    void EntityAlignment(bool isMine)   //엔티티 정렬
    {
        float targetY = isMine ? -1f : 4.15f;
        var targetEntities = isMine ? myEntities : otherEntites;

        for(int i = 0; i < targetEntities.Count; i++)
        {
            float targetX = (targetEntities.Count - 1) * -3.4f + i * 6.8f;

            var targetEntity = targetEntities[i];
            targetEntity.originPos = new Vector3(targetX, targetY, 0);
            targetEntity.MoveTransform(targetEntity.originPos, true, 0.5f);
            targetEntity.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    public void InsertMyEmptyEntity(float xPos) //마우스 필드 드래그 시 엔티티 리스트 위치 변경용인듯? 
    {
        if (IsFullMyEntities)
        {
            return;
        }
        if (!ExistMyEmptyEntity)
            myEntities.Add(myEmptyEntity);

        Vector3 emptyentityPos = myEmptyEntity.transform.position;
        emptyentityPos.x = xPos;
        myEmptyEntity.transform.position = emptyentityPos;

        int _emptyEntityIndex = MyEmptyEntityIndex;
        myEntities.Sort((entity1, entity2) => entity1.transform.position.x.CompareTo(entity2.transform.position.x));
        if (MyEmptyEntityIndex != _emptyEntityIndex)
            EntityAlignment(true);
    }

    public bool SpawnEntity(bool isMine, Item item, Vector3 spawnPos)
    {
        if (isMine)
        {
            if (IsFullMyEntities || !ExistMyEmptyEntity)
                return false;
        }
        else
        {
            if (IsFullMyEntities)
                return false;
        }

        var entityObject = Instantiate(entityPrefab, spawnPos, Utils.QI);
        var entity = entityObject.GetComponent<Entity>();

        if (isMine)
            myEntities[MyEmptyEntityIndex] = entity;
        else
            otherEntites.Insert(Random.Range(0, otherEntites.Count), entity);

        entity.isMine = isMine;
        entity.Setup(item);
        EntityAlignment(isMine);

        return true;
    }

    public void RemoveMyEmptyEntity()
    {
        if (!ExistMyEmptyEntity)
            return;

        myEntities.RemoveAt(MyEmptyEntityIndex);
        EntityAlignment(true);
    }
}
