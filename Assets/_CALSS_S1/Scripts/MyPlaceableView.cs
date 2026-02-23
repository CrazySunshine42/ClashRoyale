using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class MyPlaceableView : MonoBehaviour
    {
        public MyPlaceable data;
        public float dieDuaration = 5f;//死亡溶解总事件，按秒
        public float dieProgress = 0f;//当前溶解进度
        private void OnDestroy()
        {
            MyPlaceableMgr.instance.RemovePlaceableViewList(this);
        }
    }
}
