using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Projectiles
{
    public abstract class Projectile : MonoBehaviour, IDamageable
    {
        Character m_Sender;
        Vector2 m_Direction;
        [SerializeField] protected float m_Speed;
        int m_Damage;
        float m_Knockback;

        public float Speed => m_Speed;
        
        public virtual void Init(Character sender, Vector2 direction, float projectileSpeed)
        {
            m_Sender = sender;
            m_Direction = direction;
            m_Speed = projectileSpeed;
            m_Damage = sender.m_currentData.attackDamage;
            m_Knockback = sender.m_currentData.attackKnockback;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == null) return;
            IDamageable target = other.GetComponent<IDamageable>();
            if (target == null) return;
            IDamageable.DamageableTag targetTag = target.GetTag();
            if (targetTag == m_Sender.m_Tag || targetTag == IDamageable.DamageableTag.Projectile) return;

            target.Hurt(m_Damage, m_Knockback, m_Direction);
            Destroy(gameObject);
        }

        public IDamageable.DamageableTag GetTag()
        {
            return IDamageable.DamageableTag.Projectile;
        }

        public void Hurt(int damage, float knockbackForce, Vector2 knockbackDir)
        {
            return;
        }

        public void Die()
        {
            return;
        }
    }
}
