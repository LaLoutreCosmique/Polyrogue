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
        public int health;
        public int shieldHealth;
        public float suddenKnockbackMultiplier;
        public float invincibilityDuration;
        
        [Header("Attack Settings")]
        public float attackCooldown;
        public int attackDamage;
        public float attackSize;
        public float attackKnockback;
        public float projectileSpeed;

        [HideInInspector] public FloatWrapper[] floatParameters;
        [HideInInspector] public int[] intParameters;

        void OnEnable()
        {
            Init();
        }

        protected virtual void Init()
        {
            floatParameters = new[]
            {
                new FloatWrapper(() => acceleration, val => acceleration = val),
                new FloatWrapper(() => maxSpeed, val => maxSpeed = val),
                new FloatWrapper(() => dashForce, val => dashForce = val),
                new FloatWrapper(() => dashCooldown, val => dashCooldown = val),
                new FloatWrapper(() => dashDuration, val => dashDuration = val),
                new FloatWrapper(() => suddenKnockbackMultiplier, val => suddenKnockbackMultiplier = val),
                new FloatWrapper(() => invincibilityDuration, val => invincibilityDuration = val),
                new FloatWrapper(() => attackCooldown, val => attackCooldown = val),
                new FloatWrapper(() => attackSize, val => attackSize = val),
                new FloatWrapper(() => attackKnockback, val => attackKnockback = val),
                new FloatWrapper(() => projectileSpeed, val => projectileSpeed = val)
            };

            intParameters = new[]
            {
                health,
                shieldHealth,
                attackDamage
            };
        }

        public void AddModifier(CharacterDataModifier modifier)
        {
            Debug.Log("First Acceleration : " + acceleration);
            for (int i = 0; i < floatParameters.Length; i++)
            {
                if (modifier.floatParameters[i].Value != 0f)
                {
                    switch (modifier.floatsModifierType[i])
                    {
                        case ModifierType.Addition:
                            this.floatParameters[i].Value += modifier.floatParameters[i].Value;
                            break;
                        case ModifierType.Multiplication:
                            this.floatParameters[i].Value *= modifier.floatParameters[i].Value;
                            break;
                    }
                }
            }
            Debug.Log("Array Acceleration : " + floatParameters[0]);
            Debug.Log("Second Acceleration : " + acceleration);
            
            for (int i = 0; i < intParameters.Length; i++)
            {
                if (modifier.intParameters[i] != 0)
                {
                    switch (modifier.intsModifierType[i])
                    {
                        case ModifierType.Addition:
                            this.intParameters[i] += modifier.intParameters[i];
                            break;
                        case ModifierType.Multiplication:
                            this.intParameters[i] *= modifier.intParameters[i];
                            break;
                    }
                }
            }
        }
    }
    
    public class FloatWrapper
    {
        Func<float> getter;
        Action<float> setter;

        public FloatWrapper(Func<float> getter, Action<float> setter)
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
