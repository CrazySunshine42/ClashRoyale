using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class MyProjectile : MonoBehaviour
    {
        public MyAIBase caster;//投掷物释放者
        public MyAIBase target;//投掷物目标
        public float progress = 0;//飞行进度
        public float speed = 1;//飞行速度
    }
}
