using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Characters.Enemy
{
    public class Enemy : Character
    {
        AI m_AI;
        [SerializeField] AIBehaviour m_AIData;
        [SerializeField] Character m_Target;
        
        [Header("Debug")]
        [SerializeField] bool m_DebugRanges;
        [SerializeField] bool m_DebugAIVision;

        protected override void Awake()
        {
            base.Awake();
            m_AI = new AI(this, m_Target, m_AIData);
            m_Tag = IDamageable.DamageableTag.Enemy;
        }

        void Start()
        {
            m_AI.Start();
        }

        protected override void Update()
        {
            base.Update();
            m_AI.Update();
        }
        
        public override void Die()
        {
            base.Die();
            Destroy(gameObject);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.transform.CompareTag("Player")) return;
            
            Vector2 dir = other.transform.position - transform.position;
            other.collider.GetComponent<IDamageable>()
                .Hurt(m_currentData.attackDamage, m_currentData.attackKnockback, dir);
        }

        public void SetRotateDirection(Quaternion dir)
        {
            m_RotateDirection = dir;
        }

        public void SetRotateInput(Vector2 dir)
        {
            m_RotateInput = dir;
        }

        public void StartMove()
        {
            m_IsMoving = true;
        }

        public void StopMove()
        {
            m_IsMoving = false;
        }

        protected override void OnAttackRecovered()
        {
            base.OnAttackRecovered();
            m_AI.OnAttackRecovered();
        }
        
        protected override void OnDashRecovered()
        {
            base.OnDashRecovered();
            m_AI.OnDashRecovered();
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (m_DebugRanges)

            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position, m_AIData.maxAttackDistance);
                
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.position, m_AIData.minTargetDistance);
                
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position, m_AIData.maxTargetDistance);
            }

            if (m_DebugAIVision && m_AI != null)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)m_AI.m_Direction);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)rb2d.velocity);
            }
        }
#endif
    }
}
