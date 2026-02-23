using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityRoyale
{
    public class MyCardView : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public MyCardS1 data;
        public int index;
        private bool isDragging = false;
        private Transform previewHolder;
        private Camera camera;
        public CanvasGroup canvasGroup;

        private void Awake()
        {
            camera = Camera.main;
            previewHolder = GameObject.Find("PreviewHolder").transform;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            this.transform.SetAsLastSibling();
            CardManagerX.instance.forbidenAreaRenderer.enabled = true;

        }
        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform.parent as RectTransform, eventData.position, null, out Vector3 posWorld);
            this.transform.position = posWorld;
            Ray ray = camera.ScreenPointToRay(eventData.position);
            bool canTransition = Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("PlayingField"));
            if (canTransition)
            {
                previewHolder.transform.position = hit.point;
                if (isDragging == false)
                {
                    Debug.Log("射线命中地面 && 卡牌没有变小兵");
                    previewHolder.gameObject.SetActive(true);
                    MyPlaceableMgr.instance.OnCardTransition(data, previewHolder, hit.point, Placeable.Faction.Player);
                    canvasGroup.alpha = 0;
                    isDragging = true;
                }
                else
                {
                    Debug.Log("射线命中地面 && 卡牌已经变小兵");
                }
            }
            else
            {
                if (isDragging)
                {
                    Debug.Log("鼠标没有命中地面（放回卡牌UI区域）");
                    canvasGroup.alpha = 1;
                    isDragging = false;
                    foreach (Transform item in previewHolder)
                    {
                        Destroy(item.gameObject);
                    }
                }
            }

        }
        public async void OnPointerUp(PointerEventData eventData)
        {
            Ray ray = camera.ScreenPointToRay(eventData.position);
            bool canTransition = Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, 1 << LayerMask.NameToLayer("PlayingField"));
            CardManagerX.instance.forbidenAreaRenderer.enabled = false;
            if (canTransition)
            {
                OnCardUsed();
                Destroy(this.gameObject);
                //这里的await只是异步等待，没有new一个task对象，所以本方法的返回值类型可以为void
                await CardManagerX.instance.PreviewToCard(index, 0.5f);
                await CardManagerX.instance.CreateCard(1f); 
            }
            else
            {
                this.transform.DOMove(CardManagerX.instance.cardPos[index].transform.position, 0.3f);
            }
        }
        private void OnCardUsed()
        {
            for (int i = previewHolder.childCount - 1; i >= 0; i--)
            {
                Transform item = previewHolder.GetChild(i);
                item.SetParent(MyPlaceableMgr.instance.transform, true);
                MyPlaceableView view = item.GetComponent<MyPlaceableView>();
                MyPlaceableMgr.instance.mine.Add(view);
                MyPlaceableMgr.instance.allUnitAIList.Add(view);
            }
        }
    }
}
