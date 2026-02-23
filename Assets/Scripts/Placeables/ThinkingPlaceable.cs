using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UnityRoyale
{
    public class ThinkingPlaceable : Placeable
    {
        [HideInInspector] public States state = States.Dragged;
        public enum States
        {
            Dragged, //when the player is dragging it as a card on the play field（玩家正在拖拽一张游戏单位卡牌，尚未放到游戏区域内的预览状态）
            Idle, //at the very beginning, when dropped （默认的空闲状态）
            Seeking, //going for the target（寻找目标状态）
            Attacking, //attack cycle animation, not moving（攻击状态）
            Dead, //dead animation, before removal from play field（死亡）
        }

        [HideInInspector] public AttackType attackType;
        public enum AttackType
        {
            Melee, // 近程攻击
            Ranged, // 远程攻击
        }

        [HideInInspector] public ThinkingPlaceable target;//攻击目标
        [HideInInspector] public HealthBar healthBar;//血条

        [HideInInspector] public float hitPoints;//血量值
        [HideInInspector] public float attackRange;//攻击范围
        [HideInInspector] public float attackRatio;//攻击速率
        [HideInInspector] public float lastBlowTime = -1000f;//上次打击时间
        [HideInInspector] public float damage;//攻击伤害值
		[HideInInspector] public AudioClip attackAudioClip;//攻击音效
        
        [HideInInspector] public float timeToActNext = 0f;//下一次造成伤害的事件

		//Inspector references
		[Header("Projectile for Ranged")]
		public GameObject projectilePrefab;//投掷物预制体
		public Transform projectileSpawnPoint;//投掷物的生成位置（弓箭手，法师的手部）

		private Projectile projectile;//projectilePrefab创建的投掷物实例
		protected AudioSource audioSource;//攻击音效

		public UnityAction<ThinkingPlaceable> OnDealDamage, OnProjectileFired;//攻击造成伤害的回调函数，投掷物发射的回调函数

        public virtual void SetTarget(ThinkingPlaceable t)
        {
            target = t;
            t.OnDie += TargetIsDead;
        }

        public virtual void StartAttack()
        {
            state = States.Attacking;
        }

        public virtual void DealBlow()
        {
            lastBlowTime = Time.time;
        }

		// 被Animation的Event调用
		public void DealDamage()
        {
			//only melee units play audio when the attack deals damage
			if(attackType == AttackType.Melee)
				audioSource.PlayOneShot(attackAudioClip, 1f);

			if(OnDealDamage != null)
				OnDealDamage(this);
		}

		// 被Animation的Event调用
		public void FireProjectile()
        {
			//ranged units play audio when the projectile is fired
			audioSource.PlayOneShot(attackAudioClip, 1f);

			if(OnProjectileFired != null)
				OnProjectileFired(this);
		}

        public virtual void Seek()
        {
            state = States.Seeking;
        }

        protected void TargetIsDead(Placeable p)
        {
            //Debug.Log("My target " + p.name + " is dead", gameObject);
            state = States.Idle;
            
            target.OnDie -= TargetIsDead;

            timeToActNext = lastBlowTime + attackRatio;
        }
        
        public bool IsTargetInRange()
        {
            return (transform.position-target.transform.position).sqrMagnitude <= attackRange*attackRange;
        }

        public float SufferDamage(float amount)
        {
            hitPoints -= amount;
            //Debug.Log("Suffering damage, new health: " + hitPoints, gameObject);
            if(state != States.Dead
				&& hitPoints <= 0f)
            {
                Die();
            }

            return hitPoints;
        }

		public virtual void Stop()
		{
			state = States.Idle;
		}

        protected virtual void Die()
        {
            state = States.Dead;
			audioSource.pitch = Random.Range(.9f, 1.1f);
			audioSource.PlayOneShot(dieAudioClip, 1f);

			if(OnDie != null)
            	OnDie(this);
        }
    }
}
