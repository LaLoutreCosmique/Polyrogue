using UnityEngine;

namespace Characters
{
    public interface IDamageable
    {
        DamageableTag GetTag();
        void Hurt(int damage, float knockbackForce, Vector2 knockbackDir);
        void Die();
        
        enum DamageableTag
        {
            Player,
            Enemy,
            Projectile
        }
    }
}
