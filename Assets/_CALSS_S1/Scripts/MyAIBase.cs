using UnityEngine;
using UnityEngine.AddressableAssets;
using DG.Tweening;
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
    public MyAIBase target = null;//攻击目标
    public float lastBlowTime = -1000f;
    //public GameObject projectile;通过可寻址资源引用实例化
    public AssetReference projectile;
    public Transform firePos;
    public virtual void OnIdle()
    {
        state = AIState.Idle;
    }
    public virtual void OnSeek()
    {
        state = AIState.Seek;
        this.transform.DOLookAt(target.transform.position,0.5f);
    }
    public virtual void OnAttack()
    {
        state = AIState.Attack;
        this.transform.DOLookAt(target.transform.position, 0.5f);
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

