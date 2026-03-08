using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class MyProjectileMgr : MonoBehaviour
    {
        public static MyProjectileMgr instance = null;
        public List<MyProjectile> myProjList = new List<MyProjectile>();
        public List<MyProjectile> enemyProjList = new List<MyProjectile>();
        public List<MyProjectile> allProjList = new List<MyProjectile>();
        public void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            UpdateProjList(allProjList);
        }
        /// <summary>
        /// 更新所有投掷的飞行对象
        /// </summary>
        /// <param name="prijList">投掷物列表</param>
        public void UpdateProjList(List<MyProjectile> prijectileList)
        {
            List<MyProjectile> removeItem = new List<MyProjectile>();
            for (int i = 0; i < prijectileList.Count; i++)
            {
                MyProjectile proj = prijectileList[i];
                if (proj.target == null)
                {
                    removeItem.Add(proj);
                    Destroy(proj.gameObject);
                    continue;
                }
                proj.progress += Time.deltaTime * proj.speed;
                proj.transform.position = Vector3.Lerp(proj.caster.transform.position, proj.target.transform.position + Vector3.up, proj.progress);
                if (proj.progress >= 1f)
                {
                    MyUnitAI casterAI = proj.caster as MyUnitAI;
                    MyAIBase targetAI = proj.target;
                    casterAI.OnDealDamage();
                    //死亡处理
                    if (targetAI.GetComponent<MyPlaceableView>().data.hitPoints <= 0)
                    {
                        MyPlaceableMgr.instance.OnEnterDie(targetAI);
                    }
                    Destroy(proj.gameObject);
                    removeItem.Add(proj);
                }
            }
            //int count = removeItem.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    MyProjectile item = removeItem[i];
            //    MyPlaceableMgr.instance.RemoveProjectileList(item);
            //}
            foreach (var item in removeItem)
            {
                RemoveProjectileList(item);
            }
        }
        public void RemoveProjectileList(MyProjectile item)
        {
            allProjList.Remove(item);
            myProjList.Remove(item);
        }
    }
}
