using System;
using System.Collections;
using Characters.Player.Inputs;
using Characters.Player.Upgrade;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Characters.Player
{
    public class Player : Character
    {
        InputManager m_InputManager;

        public event Action<int> OnUpgradeInputPerformed;

        bool m_IsAttacking;

        [Header("Debug")]
        [SerializeField] bool m_DebugAim;
        [SerializeField] bool m_DebugVelocity;
        
        // UPGRADES
        bool spamAttack = false; // Reset attack cooldown on button released

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
            if (spamAttack) m_AttackCooldown.Complete();
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

        public void EnableInput()
        {
            m_InputManager.EnableInputs();
        }
        
        public void DisableInput()
        {
            m_InputManager.DisableInputs();
        }

        public override void Die()
        {
            base.Die();
            m_InputManager.DisableInputs();
            SceneManager.LoadScene("Scenes/Death");
        }

        public void EnableUpgradeInputs(bool value)
        {
            if (value)
            {
                m_InputManager.GameInputsEnabled = false;
                m_InputManager.UpgradeInputsEnabled = true;
            }
            else
            {
                m_InputManager.GameInputsEnabled = true;
                m_InputManager.UpgradeInputsEnabled = false;
            }
        }

        public void UpgradeStats(UpgradeCardData card)
        {
            if (card.modifier)
                m_currentData.AddModifier(card.modifier);

            foreach (var upgrade in card.special)
            {
                spamAttack = upgrade.type switch
                {
                    SpecialUpgradeType.SpamAttack => upgrade.active,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public void SelectUpgrade(int value)
        {
            OnUpgradeInputPerformed?.Invoke(value);
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
