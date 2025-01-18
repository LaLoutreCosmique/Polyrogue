using System;
using UnityEditor;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "Polyrogue/Character Data", fileName = "New Character Data")]
    public class CharacterData : ScriptableObject
    {
        [Header("Movement Settings")]
        public float acceleration;
        public float maxSpeed;
        public float dashForce;
        public float dashCooldown;
        public float dashDuration;

        [Header("Health Settings")]
        public float health;
        public float shieldHealth;
        public float suddenKnockbackMultiplier;
        public float invincibilityDuration;
        
        [Header("Attack Settings")]
        public float attackCooldown;
        public float attackDamage;
        public float attackSize;
        public float attackKnockback;
        public float projectileSpeed;

        DataWrapper[] m_Datas;

#if UNITY_EDITOR
        void OnValidate()
        {
            if (health != Mathf.Floor(health))
                health = Mathf.Round(health);
            if (shieldHealth != Mathf.Floor(shieldHealth))
                shieldHealth = Mathf.Round(shieldHealth);
            if (attackDamage != Mathf.Floor(attackDamage))
                attackDamage = Mathf.Round(attackDamage);
        }
#endif
        
        void OnEnable()
        {
            Init();
        }

        protected virtual void Init()
        {
            m_Datas = new[]
            {
                new DataWrapper(() => acceleration, val => acceleration = val),
                new DataWrapper(() => maxSpeed, val => maxSpeed = val),
                new DataWrapper(() => dashForce, val => dashForce = val),
                new DataWrapper(() => dashCooldown, val => dashCooldown = val),
                new DataWrapper(() => dashDuration, val => dashDuration = val),
                new DataWrapper(() => health, val => health = val),
                new DataWrapper(() => shieldHealth, val => shieldHealth = val),
                new DataWrapper(() => suddenKnockbackMultiplier, val => suddenKnockbackMultiplier = val),
                new DataWrapper(() => invincibilityDuration, val => invincibilityDuration = val),
                new DataWrapper(() => attackCooldown, val => attackCooldown = val),
                new DataWrapper(() => attackDamage, val => attackDamage = val),
                new DataWrapper(() => attackSize, val => attackSize = val),
                new DataWrapper(() => attackKnockback, val => attackKnockback = val),
                new DataWrapper(() => projectileSpeed, val => projectileSpeed = val)
            };
        }

        public void AddModifier(CharacterDataModifier modifier)
        {
            for (int i = 0; i < m_Datas.Length; i++)
            {
                if (modifier.m_Datas[i].Value != 0f)
                {
                    switch (modifier.floatsModifierType[i])
                    {
                        case ModifierType.Addition:
                            this.m_Datas[i].Value += modifier.m_Datas[i].Value;
                            break;
                        case ModifierType.Multiplication:
                            this.m_Datas[i].Value *= modifier.m_Datas[i].Value;
                            break;
                    }
                }
            }
        }
    }
    
    public class DataWrapper
    {
        Func<float> getter;
        Action<float> setter;

        public DataWrapper(Func<float> getter, Action<float> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public float Value
        {
            get => getter();
            set => setter(value);
        }
    }
}
