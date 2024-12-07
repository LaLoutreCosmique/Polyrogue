using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "Polyrogue/Character Data", fileName = "New Character Data")]
    public class CharacterData : ScriptableObject
    {
        //public CharacterData(float acceleration, float maxSpeed, float dashForce, float dashCooldown, float dashDuration, int health, int shieldHealth, float takenKnockbackMultiplier, float invincibilityDuration,
        //    float attackCooldown, int attackDamage, float attackSize, float attackKnockback, float projectileSpeed)
        //{
        //    this.acceleration = acceleration;
        //    this.maxSpeed = maxSpeed;
        //    this.dashForce = dashForce;
        //    this.dashCooldown = dashCooldown;
        //    this.dashDuration = dashDuration;
        //    this.health = health;
        //    this.shieldHealth = shieldHealth;
        //    this.takenKnockbackMultiplier = takenKnockbackMultiplier;
        //    this.invincibilityDuration = invincibilityDuration;
        //    this.attackCooldown = attackCooldown;
        //    this.attackDamage = attackDamage;
        //    this.attackSize = attackSize;
        //    this.attackKnockback = attackKnockback;
        //    this.projectileSpeed = projectileSpeed;
        //}
        
        [Header("Movement Settings")]
        public float acceleration = 30f;
        public float maxSpeed = 10f;
        public float dashForce = 10f;
        public float dashCooldown = 3f;
        public float dashDuration = 0.2f;

        [Header("Health Settings")]
        public int health = 1;
        public int shieldHealth = 1;
        public float takenKnockbackMultiplier = 1f;
        public float invincibilityDuration = 0f;
        
        [Header("Attack Settings")]
        public float attackCooldown = 0.1f;
        public int attackDamage = 1;
        public float attackSize = 1f;
        public float attackKnockback = 1f;
        public float projectileSpeed = 1f;
        
        public void Add(CharacterData other)
        {
            acceleration += other.acceleration;
            maxSpeed += other.maxSpeed;
            dashForce += other.dashForce;
            dashCooldown += other.dashCooldown;
            dashDuration += other.dashDuration;
            health += other.health;
            shieldHealth += other.shieldHealth;
            takenKnockbackMultiplier += other.takenKnockbackMultiplier;
            invincibilityDuration += other.invincibilityDuration;
            attackCooldown += other.attackCooldown;
            attackDamage += other.attackDamage;
            attackSize += other.attackSize;
            attackKnockback += other.attackKnockback;
            projectileSpeed += other.projectileSpeed;
        }
    }
}