using System;
using Characters.Projectiles;
using Unity.Mathematics;
using UnityEngine;
using Utilities;

namespace Characters
{
    public class Character : MonoBehaviour, IDamageable
    {
        public IDamageable.DamageableTag m_Tag;

        public event Action<Vector2> OnChangePosition;

        public Rigidbody2D rb2d;
        [SerializeField] CharacterData m_InitialData;
        [SerializeField] protected GameObject m_ProjectilePrefab;

        Cooldown m_DashCooldown;
        protected Cooldown m_DashDurationCooldown;
        protected Cooldown m_AttackCooldown;
        [HideInInspector] public Cooldown m_InvincibilityCooldown; // Start when hurt
        [HideInInspector] public CharacterData m_currentData;
        
        [HideInInspector] public Vector2 m_RotateInput;
        protected Quaternion m_RotateDirection;
        [HideInInspector] public Vector2 m_MoveDirection;
        protected bool m_IsMoving;

        protected virtual void Awake()
        {
            m_currentData = Instantiate(m_InitialData);

            m_DashCooldown = new Cooldown(m_InitialData.dashCooldown, OnDashRecovered);
            m_DashDurationCooldown = new Cooldown(m_InitialData.dashDuration);
            m_AttackCooldown = new Cooldown(m_InitialData.attackCooldown, cooldownOver:OnAttackRecovered);
            m_InvincibilityCooldown = new Cooldown(m_currentData.invincibilityDuration);
        }
        
        protected virtual void Update()
        {
            m_DashCooldown?.Update();
            m_DashDurationCooldown?.Update();
            m_AttackCooldown?.Update();
            m_InvincibilityCooldown?.Update();
        }
        
        void FixedUpdate()
        {
            if (rb2d.velocity.magnitude > 0.2f) OnChangePosition?.Invoke(transform.position);
            
            transform.rotation = m_RotateDirection;
            
            if (!m_DashDurationCooldown.HasEnded) return;

            Vector2 newForce;
            // Break when not moving
            if (!m_IsMoving)
                newForce = -rb2d.velocity;
            else
                newForce = m_MoveDirection * m_currentData.acceleration;

            // Speed limit
            if (rb2d.velocity.magnitude > m_currentData.maxSpeed)
            {
                newForce = (m_MoveDirection - rb2d.velocity);
            }
            
            rb2d.AddForce(newForce, ForceMode2D.Force);
        }
        
         public virtual void Attack()
        {
            if (!m_AttackCooldown.HasEnded) return;
            
            Vector3 spawnPosition = transform.position + new Vector3(m_RotateInput.normalized.x, m_RotateInput.normalized.y, 0) * transform.localScale.magnitude;
            GameObject newProjectileGo = Instantiate(m_ProjectilePrefab, spawnPosition, quaternion.identity);
            newProjectileGo.transform.localScale = transform.lossyScale * m_currentData.attackSize;
            Vector2 projectileDirection = m_RotateInput.normalized.magnitude == 0f ? Vector2.up : m_RotateInput;
            
            Projectile newProjectile = newProjectileGo.GetComponent<Projectile>();
            float projectileSpeed = newProjectile.Speed * m_currentData.projectileSpeed;
            newProjectile.Init(this, projectileDirection.normalized, projectileSpeed);
            
            m_AttackCooldown.Start();
        }
        
        public void Dash(Vector2 dashDirection, float newDashCD = 0f)
        {
            if (dashDirection.magnitude < 0.1f || !m_DashCooldown.HasEnded) return;

            if (newDashCD > 0f)
                m_DashCooldown.Start(newDashCD);
            else
                m_DashCooldown.Start();

            m_DashDurationCooldown.Start();
            rb2d.AddForce(dashDirection * m_currentData.dashForce, ForceMode2D.Impulse);
        }
        

        public void Hurt(int damage, float knockbackForce, Vector2 knockbackDir)
        {
            rb2d.AddForce(knockbackDir * (knockbackForce * m_currentData.takenKnockbackMultiplier), ForceMode2D.Impulse);

            if (!m_InvincibilityCooldown.HasEnded)
            {
                print(gameObject.name + " : NO DAMAGE TAKEN");
                return;
            }
            
            m_currentData.health -= damage;
            m_InvincibilityCooldown.Start();

            if (m_currentData.health <= 0) Die();
        }
        
        public IDamageable.DamageableTag GetTag() { return m_Tag; }

        protected virtual void OnDashRecovered()
        {
            Debug.Log(gameObject.name + " : Dash recovered");
        }
        
        protected virtual void OnAttackRecovered() { }

        public virtual void Die() { }
    }
}
