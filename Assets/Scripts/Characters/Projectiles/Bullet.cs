using System.Collections;
using UnityEngine;

namespace Characters.Projectiles
{
    public class Bullet : Projectile
    {
        [SerializeField] float m_DestroyTime;
        
        public override void Init(Character sender, Vector2 direction, float projectileSpeed)
        {
            base.Init(sender, direction, projectileSpeed);
            
            GetComponent<Rigidbody2D>().AddForce(direction * m_Speed, ForceMode2D.Impulse);
            StartCoroutine(DestroyBullet());
        }

        IEnumerator DestroyBullet()
        {
            yield return new WaitForSeconds(m_DestroyTime);
            Destroy(gameObject);
        }
    }
}
