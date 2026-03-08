using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
namespace UnityRoyale
{
    public class CPUEnemy : MonoBehaviour
    {
        public float interval = 5f;//出牌时间间隔
        public Transform[] range = new Transform[2];
        // Start is called before the first frame update
        async void Start()
        {
            await DealCards();

        }
        // Update is called once per frame
        void Update()
        {

        }
        async Task DealCards()
        {
            while (true)
            {
                await new WaitForSeconds(interval);
                //yield return new WaitForSeconds(interval);
                List<MyCardS1> cardList = MyCardModelS1.instance.list;
                MyCardS1 cardData = cardList[Random.Range(0,cardList.Count)];
                List<MyPlaceableView> viewList = await MyPlaceableMgr.instance.OnCardTransition(cardData, this.transform,
                    new Vector3(Random.Range(range[0].position.x, range[1].position.x), 0, Random.Range(range[0].position.z, range[1].position.z)),Placeable.Faction.Opponent);
                //添加敌方 小兵
                int count = viewList.Count;
                for (int i = 0; i < count; i++)
                {
                    MyPlaceableView view = viewList[i];
                    MyPlaceableMgr.instance.enemy.Add(view);
                    MyPlaceableMgr.instance.allUnitAIList.Add(view);
                }
                
            }
        }

    }
}
