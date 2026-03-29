using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UnityRoyale
{
    public class CardManagerX : MonoBehaviour
    {
        public static CardManagerX instance;

        #region Deckpage
        //DeckPage的属性，这部分字段需要在DeckPage加载完后动态赋值
        //public Transform[] cards;//活动牌
        //public GameObject[] cardPrefabs;//卡牌预制体
        public GameObject[] cardPos;
        public Transform canvas;
        public Transform startPos, endPos;//起始位置和结束位置
        #endregion
        private Transform previewCard;//预览卡牌
        public MeshRenderer forbidenAreaRenderer;

        private void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        async void Start()
        {
            //StartCoroutine(CreateCard(0.5f));
            //StartCoroutine(PreviewToCard(0, 1f));

            //StartCoroutine(CreateCard(1.5f));
            //StartCoroutine(PreviewToCard(1, 2f));

            //StartCoroutine(CreateCard(2.5f));
            //StartCoroutine(PreviewToCard(2, 3f));

            //StartCoroutine(CreateCard(3.5f));
            //加载出牌区UI，创建卡牌必须在出牌区创建完毕再执行
            //由于await是异步等待，被放在showpageAsync的lambda表达式写的回调函数里所以要给该lambda表达式加上async关键字
            UIPage.ShowPageAsync<DeckPage>(async() =>
            {
                await CreateCard(0.5f);
                await PreviewToCard(0, 0.5f);

                await CreateCard(0.5f);
                await (PreviewToCard(1, 0.5f));

                await CreateCard(0.5f);
                await PreviewToCard(2, 0.5f);

                await CreateCard(0.5f);
            });
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        public async Task CreateCard(float delay)
        {
            //yield return new WaitForSeconds(delay);
            await new WaitForSeconds(delay);//这里会创建一个Task,在await时c#会返回这个Task对象，所以返回值类型不能写void

            int iCard = Random.Range(0, MyCardModelS1.instance.list.Count);
            MyCardS1 card = MyCardModelS1.instance.list[iCard];
            //GameObject cardPrefab = Resources.Load<GameObject>(card.cardPrefab);
            //GameObject cardPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Length)];
            //previewCard = Instantiate(cardPrefab, startPos.position, Quaternion.identity, canvas).transform;


            //由于是异步实例化，所以我们不能直接获取到创建的卡牌对象
            //我们需要等待异步实例化完毕，同时又不能阻塞unity程序的执行（会造成卡顿）
            //所以我们要用C#的异步等待
            //在addressable系统中，InstantiateAsync == Resources.Load + Instantiate
            //await异步等待必须卸载支持异步的方法里面 --- 必须声明该方法为异步方法 
            //用了异步就可以不再使用协程了，前提是我们要引入支持协程的所有功能的一个库
            GameObject cardPrefab = await Addressables.InstantiateAsync(card.cardPrefab,canvas,false).Task;//异步等待实例化预制体完毕
            previewCard = cardPrefab.transform;
            previewCard.position = startPos.position;
            previewCard.localScale = Vector3.one * 0.2f;
            previewCard.DOMove(endPos.position, 0.5f);


            previewCard.DOScale(new Vector3(0.7f, 0.7f, 0.7f), 0.5f);
            //将数据存入前端

            previewCard.GetComponent<MyCardView>().data = card;
        }
        //public IEnumerator PreviewToCard(int i, float delay)
        public async Task PreviewToCard(int i, float delay)
        {
            //yield return new WaitForSeconds(delay);
            await new WaitForSeconds(delay);
            previewCard.DOMove(cardPos[i].transform.position, 0.5f);
            previewCard.DOScale(Vector3.one, 0.5f);

            previewCard.GetComponent<MyCardView>().index = i;
        }

    }
}
