using UnityEngine;
using UnityRoyale;
using UnityEngine.AI;
using System.Collections;
public enum AIState
{
    Idle,
    Seek,
    Attack,
    Die,
}
public class MyAIBase : MonoBehaviour
{
    public AIState state = AIState.Idle;
    public MyAIBase target = null;//¹¥»÷Ä¿±ê
    public float lastBlowTime = -1000f;
    public GameObject projectile;
    public Transform firePos;
    public virtual void OnIdle()
    {
        state = AIState.Idle;
    }
    public virtual void OnSeek()
    {
        state = AIState.Seek;
        this.transform.LookAt(target.transform);
    }
    public virtual void OnAttack()
    {
        state = AIState.Attack;
    }
    public virtual void DealBlow()
    {
        lastBlowTime = Time.time;
    }

    public virtual void OnDie()
    {
        state = AIState.Die;

    }
}

