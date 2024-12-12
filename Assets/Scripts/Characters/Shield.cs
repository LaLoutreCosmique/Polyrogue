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
            m_Parent.rb2d.AddForce(knockbackDir * (knockbackForce * m_Parent.m_currentData.takenKnockbackMultiplier), ForceMode2D.Impulse);

            if (!m_Parent.m_InvincibilityCooldown.HasEnded)
            {
                //print("NO DAMAGE TAKEN!");
                return;
            }
            
            m_Parent.m_currentData.shieldHealth -= damage;
            m_Parent.m_InvincibilityCooldown.Start();
            
            if (m_Parent.m_currentData.shieldHealth <= 0) Die();
        }

        public void Die()
        {
            gameObject.SetActive(false);
        }
    }
}
