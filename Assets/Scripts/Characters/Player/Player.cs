using System.Collections;
using Characters.Player.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Characters.Player
{
    public class Player : Character
    {
        InputManager m_InputManager;

        bool m_IsAttacking;

        [Header("Debug")]
        [SerializeField] bool m_DebugAim;
        [SerializeField] bool m_DebugVelocity;

        protected override void Awake()
        {
            base.Awake();
            m_InputManager = new InputManager(this);
            damageableTag = IDamageable.DamageableTag.Player;
        }

        public override void Attack()
        {
            base.Attack();
            m_IsAttacking = true;
        }

        public void StopAttack()
        {
            m_IsAttacking = false;
            m_AttackCooldown.Complete();
        }

        protected override void OnAttackRecovered()
        {
            base.OnAttackRecovered();
            if (m_IsAttacking) base.Attack();
        }

        public void OnMove(Vector2 dir)
        {
            m_MoveDirection = dir;
        }
        
        public void OnMoveStart()
        {
            m_IsMoving = true;
        }
        
        public void OnMoveCanceled()
        {
            m_IsMoving = false;
        }
        
        public void OnRotate(Vector2 dir)
        {
            m_RotateInput = dir;
            float targetAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            m_RotateDirection = Quaternion.Euler(0, 0, -targetAngle);
        }

        public override void Die()
        {
            base.Die();
            m_InputManager.DisableInputs();
            SceneManager.LoadScene("Scenes/Death");
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if (m_DebugAim)
            {
                Vector3 inputRotateV3 = new Vector3(m_RotateInput.x, m_RotateInput.y, 0);
                Gizmos.DrawLine(transform.position, transform.position + inputRotateV3 * 100);      
            }

            if (m_DebugVelocity)
            {
                Gizmos.DrawLine(transform.position, transform.position + (Vector3)rb2d.velocity);
            }
        }
#endif
    }
}
