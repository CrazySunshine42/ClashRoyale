using UnityEngine;
using UnityEngine.AI;
using UnityRoyale;

internal class MyUnitAI:MyAIBase
{
    private NavMeshAgent nav;
    private Animator ani;
    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }
    public override void OnSeek()
    {
        base.OnSeek();
        nav.enabled = true;
        nav.isStopped = false;
        nav.destination = target.transform.position;
        
        ani.SetBool("IsMoving", true);
    }
    public override void OnAttack()
    {
        base.OnAttack();
        nav.isStopped = true;
        
        ani.SetBool("IsMoving",false);
    }
    public override void DealBlow()
    {
        base.DealBlow();
        ani.SetTrigger("Attack");
        //transform.forward = target.transform.position - transform.position;
    }
    public override void OnDie()
    {
        base.OnDie();
    }
    public void OnDealDamage()
    {
        if (this.target == null)
            return;
        var targetData = target.GetComponent<MyPlaceableView>().data;
        targetData.hitPoints -= this.GetComponent<MyPlaceableView>().data.damagePerAttack;
        if (targetData.hitPoints < 0)
        {
            targetData.hitPoints = 0;
            this.target = null;
        }
    }
    public void OnFireProject()
    {
        //实例化一个火球
        GameObject fireGo = Instantiate(projectile, firePos.position, Quaternion.identity,MyProjectileMgr.instance.transform);//放在手部位置，但是不以手部为父节点
        //设置投掷物的释放者（用于投掷物命中目标后的伤害结算）
        MyProjectile myProjectile = fireGo.GetComponent<MyProjectile>();
        myProjectile.caster = this;
        myProjectile.target = this.target;
        //myProjectile.target = target;
        //投掷物的飞行倍MyPlaceableMgr统一管理
        MyProjectileMgr.instance.myProjList.Add(myProjectile);
        MyProjectileMgr.instance.allProjList.Add(myProjectile);
    }
}
