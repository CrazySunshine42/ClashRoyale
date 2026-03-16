using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityRoyale;
public partial class MyPlaceable
{
    public Placeable.Faction faction = Placeable.Faction.None;
    //浅拷贝
    public MyPlaceable Clone()
    {
        return this.MemberwiseClone() as MyPlaceable;
    }
}
namespace UnityRoyale
{

    public class MyPlaceableMgr : MonoBehaviour
    {
        public static MyPlaceableMgr instance;
        public List<MyPlaceableView> mine = new List<MyPlaceableView>();
        public List<MyPlaceableView> enemy = new List<MyPlaceableView>();
        public List<MyPlaceableView> allUnitAIList = new List<MyPlaceableView>();



        public MyAIBase aiHisTower;
        public MyAIBase aiMineTower;
        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {
            enemy.Add(aiHisTower.GetComponent<MyPlaceableView>());
            mine.Add(aiMineTower.GetComponent<MyPlaceableView>());
        }

        // Update is called once per frame
        void Update()
        {
            UpdateUnitAIState(allUnitAIList);
        }

        private bool IsInAttackRange(Vector3 myPos, Vector3 targetPos, float attackRange)
        {
            return Vector3.Distance(myPos, targetPos) <= attackRange;
        }

        /// <summary>
        /// 此方法为玩家敌人通用方法，玩家找敌人，敌人找玩家，所以要传一个当前角色类型进去
        /// </summary>
        /// <param name="faction"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private MyAIBase FindNearestEnemy(Vector3 myPos, Placeable.Faction faction)
        {
            MyAIBase nearest = null;
            List<MyPlaceableView> units = faction == Placeable.Faction.Player ? enemy : mine;
            int count = units.Count;
            float x = float.MaxValue;
            for (int i = 0; i < count; i++)
            {
                float d = Vector3.Distance(units[i].transform.position, myPos);
                if (d < x)
                {
                    x = d;
                    nearest = units[i].GetComponent<MyAIBase>();
                }
            }
            return nearest;
        }
        /// <summary>
        /// 根据兵种数据创建到场地中
        /// </summary>
        /// <param name="cardData">配置数据</param>
        /// <param name="parent">父节点</param>
        /// <param name="faction">兵种</param>
        public async Task<List<MyPlaceableView>> OnCardTransition(MyCardS1 cardData, Transform parent, Vector3 pos, Placeable.Faction faction)
        {
            List<MyPlaceableView> viewList = new List<MyPlaceableView>();
            List<MyPlaceable> myPlaceables = MyPlaceableModel.instance.list;
            MyPlaceable p = null;
            for (int i = 0; i < cardData.placeablesIndices.Length; i++)
            {
                int unitID = cardData.placeablesIndices[i];
                for (int j = 0; j < myPlaceables.Count; j++)
                {
                    if (myPlaceables[j].id == unitID)
                    {
                        p = myPlaceables[j];
                        break;
                    }
                }

                Vector3 offset = cardData.relativeOffsets[i];
                //Profiler.BeginSample("Creat unit by Resources");
                //GameObject cardPrefab = Resources.Load<GameObject>(faction == Placeable.Faction.Player ? p.associatedPrefab : p.alternatePrefab);
                //GameObject cardEntity = Instantiate(cardPrefab, previewHolder, false);
                //cardEntity.transform.localPosition = offset;
                //GameObject cardEntity = Instantiate(cardPrefab, parent, false);

                //由于instantiateAsync是异步的，会造成性能分析器在前一个EndSample还没执行到的时候，就执行了下一个BeginSample
                //unity不允许性能分析器的Begin/End数量不匹配，所以报错了 
                //Profiler.BeginSample("Creat unit by Addressables");
                string prefabName = faction == Placeable.Faction.Player ? p.associatedPrefab : p.alternatePrefab;
                GameObject cardEntity = await Addressables.InstantiateAsync(prefabName, parent, false).Task;
                //AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(cardEntity);
                //await handle.Task;
                //if (handle.Status == AsyncOperationStatus.Succeeded)
                //{
                //    Debug.Log($"加载状态{handle.Result.name}");
                //}
                //else
                //{
                //    Debug.LogError($"加载失败: {handle.OperationException}");
                //}
                //Profiler.EndSample();
                cardEntity.transform.localPosition = offset;
                cardEntity.transform.position = pos + offset;
                MyPlaceable playP2 = p.Clone();
                playP2.faction = faction;
                MyPlaceableView view = cardEntity.GetComponent<MyPlaceableView>();
                view.data = playP2;
                viewList.Add(view);
            }
            return viewList;
        }

        /// <summary>
        /// 更新所有可移动AI状态
        /// </summary>
        /// <param name="unitAIList">己方或敌方或全部可移动AI列表</param>
        public void UpdateUnitAIState(List<MyPlaceableView> unitAIList)
        {
            for (int i = 0; i < unitAIList.Count; i++)
            {
                MyPlaceableView myplacebale = unitAIList[i];
                MyPlaceable data = myplacebale.data;
                MyAIBase myAIBase = myplacebale.GetComponent<MyAIBase>();
                if (myAIBase is MyUnitAI)
                {
                    myAIBase = myAIBase as MyUnitAI;
                }
                //按照游戏单位当前的状态执行状态机
                //执行状态内的动作
                //执行状态检测
                //执行状态转移

                switch (myAIBase.state)
                {
                    case AIState.Idle:
                        if (myAIBase is MyBuidingAI)
                        {
                            //如果国王塔具有攻击能力，直接跳转到attack状态
                            break;
                        }
                        myAIBase.target = FindNearestEnemy(myAIBase.transform.position, data.faction);
                        if (myAIBase.target != null)
                        {
                            //myAIBase.transform.LookAt(myAIBase.target.transform);
                            myAIBase.OnSeek();
                        }
                        break;
                    case AIState.Seek:
                        if (myAIBase.target == null)
                        {
                            myAIBase.state = AIState.Idle;
                            break;
                        }
                        if (IsInAttackRange(myAIBase.transform.position, myAIBase.target.transform.position, data.attackRange))
                        {
                            myAIBase.OnAttack();
                        }
                        //当在attack范围内时将状态转换为attack
                        break;
                    case AIState.Attack:
                        if (myAIBase.target == null)
                        {
                            myAIBase.OnIdle();
                            break;
                        }
                        //执行攻击动作,检测敌人是否仍然在攻击范围内
                        //如果在攻击间隔内则不攻击
                        if (Time.time >= myAIBase.lastBlowTime + data.attackRatio)
                        {
                            myAIBase.DealBlow();
                            var targetData = myAIBase.target.GetComponent<MyPlaceableView>();
                            if (targetData.data.hitPoints <= 0)
                            {
                                //myAIBase.target.OnDie();
                                myAIBase.state = AIState.Idle;
                                OnEnterDie(myAIBase.target);
                            }
                        }
                        break;
                    case AIState.Die:
                        Color color = myplacebale.data.faction == Placeable.Faction.Player ? Color.red : Color.blue;
                        var rds = myAIBase.GetComponentsInChildren<Renderer>();
                        myplacebale.dieProgress += Time.deltaTime * (1 / myplacebale.dieDuaration);
                        foreach (var rd in rds)
                        {
                            rd.material.SetColor("_EdgeColor", color * 8);
                            rd.material.SetFloat("_DissolveFactor", myplacebale.dieProgress);
                        }
                        //血量归零时将状态转换为die
                        break;
                }
            }
        }
        public void RemovePlaceableViewList(MyPlaceableView item)
        {
            allUnitAIList.Remove(item);
            if (item.data.faction == Placeable.Faction.Player)
            {
                mine.Remove(item);
            }
            else if (item.data.faction == Placeable.Faction.Opponent)
            {
                enemy.Remove(item);
            }
        }
        public void OnEnterDie(MyAIBase target)
        {
            if (target.state == AIState.Die)
                return;
            target.state = AIState.Die;
            Debug.Log($"{gameObject.name} is dead!!!!");
            target.GetComponent<MyPlaceableView>().data.hitPoints = 0;
            NavMeshAgent nav = target.GetComponent<NavMeshAgent>();
            Animator ani = target.GetComponent<Animator>();
            if (ani != null)
            {
                ani.SetTrigger("IsDead");
            }
            if (nav != null)
            {
                nav.isStopped = true;
            }
            MyPlaceableView aiView = target.GetComponent<MyPlaceableView>();
            Color color = aiView.data.faction == Placeable.Faction.Player ? Color.red : Color.blue;
            var rds = target.GetComponentsInChildren<Renderer>();
            aiView.dieProgress = 0;
            foreach (var rd in rds)
            {
                rd.material.SetColor("_EdgeColor", color * 8);
                rd.material.SetFloat("_DissolveFactor", aiView.dieProgress);
            }
            //Addressables.ReleaseInstance(target.gameObject);
            Destroy(target.gameObject, aiView.dieDuaration);
        }

    }
}
