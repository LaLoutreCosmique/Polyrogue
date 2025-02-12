using UnityEngine;

namespace Characters
{
    public class Shield : MonoBehaviour, IDamageable
    {
        [SerializeField] Character m_Parent;

        public IDamageable.DamageableTag GetTag()
        {
            return m_Parent.damageableTag;
        }

        public void Hurt(int damage, float knockbackForce, Vector2 knockbackDir)
        {
            m_Parent.rb2d.AddForce(knockbackDir * (knockbackForce * m_Parent.Data.suddenKnockbackMultiplier), ForceMode2D.Impulse);

            if (!m_Parent.m_InvincibilityCooldown.HasEnded)
            {
                //print(m_Parent.name + " : NO DAMAGE TAKEN!");
                return;
            }
            
            m_Parent.Data.shieldHealth -= damage;
            m_Parent.m_InvincibilityCooldown.Start();
            m_Parent.OnTakeDamage();
            
            if (m_Parent.Data.shieldHealth <= 0) Die();
        }

        public void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
