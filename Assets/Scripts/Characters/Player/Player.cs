using Characters.Player.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters.Player
{
    public class Player : Character
    {
        InputManager m_InputManager;

        bool m_IsAttacking;

        protected override void Awake()
        {
            base.Awake();
            m_InputManager = new InputManager(this);
            m_Tag = IDamageable.DamageableTag.Player;
        }

        public override void Attack()
        {
            base.Attack();
            m_IsAttacking = true;
        }

        public void StopAttack()
        {
            m_IsAttacking = false;
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
            print("Oh non je suis mort");
        }
    }
}
