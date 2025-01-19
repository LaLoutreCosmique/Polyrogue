using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player.Inputs
{
    public class InputManager
    {
        Player m_Player;
        
        PlayerControls m_Controls;
        PlayerControls.PlayerActions m_PlayerActions;
        PlayerControls.UpgradeActions m_UpgradeActions;

        public bool GameInputsEnabled
        {
            get => m_PlayerActions.enabled;
            set
            {
                if (value)
                    m_PlayerActions.Enable();
                else
                    m_PlayerActions.Disable();
            }
        }

        public bool UpgradeInputsEnabled
        {
            get => m_UpgradeActions.enabled;
            set
            {
                if (value)
                    m_UpgradeActions.Enable();
                else
                    m_UpgradeActions.Disable();
            }
        }

        public InputManager(Player player)
        {
            m_Player = player;
            m_Controls = new PlayerControls();
            m_PlayerActions = m_Controls.Player;
            m_UpgradeActions = m_Controls.Upgrade;

            UpgradeInputsEnabled = false;
            EnableInputs();
        }

        public void EnableInputs()
        {
            m_PlayerActions.Movement.performed += ctx => m_Player.OnMove(ctx.ReadValue<Vector2>());
            m_PlayerActions.Movement.started += _ => m_Player.OnMoveStart();
            m_PlayerActions.Movement.canceled += _ => m_Player.OnMoveCanceled();
            
            m_PlayerActions.Rotation.performed += ctx => m_Player.OnRotate(ctx.ReadValue<Vector2>());
            m_PlayerActions.Attack.performed += _ => m_Player.Attack();
            m_PlayerActions.Attack.canceled += _ => m_Player.StopAttack();
            m_PlayerActions.Dash.performed += _ => m_Player.Dash(m_Player.m_MoveDirection);

            m_UpgradeActions.Skip.performed += _ => m_Player.SelectUpgrade(-1);
            m_UpgradeActions.Left.performed += _ => m_Player.SelectUpgrade(0);
            m_UpgradeActions.Right.performed += _ => m_Player.SelectUpgrade(1);
            m_UpgradeActions.Up.performed += _ => m_Player.SelectUpgrade(2);
            m_UpgradeActions.Down.performed += _ => m_Player.SelectUpgrade(3);
            
            m_Controls.Enable();
        }
        
        public void DisableInputs()
        {
            m_Controls.Disable();
            
            m_PlayerActions.Movement.performed -= ctx => m_Player.OnMove(ctx.ReadValue<Vector2>());
            m_PlayerActions.Movement.started -= _ => m_Player.OnMoveStart();
            m_PlayerActions.Movement.canceled -= _ => m_Player.OnMoveCanceled();
            
            m_PlayerActions.Rotation.performed -= ctx => m_Player.OnRotate(ctx.ReadValue<Vector2>());
            m_PlayerActions.Attack.performed -= _ => m_Player.Attack();
            m_PlayerActions.Attack.canceled -= _ => m_Player.StopAttack();
            m_PlayerActions.Dash.performed -= _ => m_Player.Dash(m_Player.m_MoveDirection);
            
            m_UpgradeActions.Skip.performed -= _ => m_Player.SelectUpgrade(-1);
            m_UpgradeActions.Left.performed -= _ => m_Player.SelectUpgrade(0);
            m_UpgradeActions.Right.performed -= _ => m_Player.SelectUpgrade(1);
            m_UpgradeActions.Up.performed -= _ => m_Player.SelectUpgrade(2);
            m_UpgradeActions.Down.performed -= _ => m_Player.SelectUpgrade(3);
        }
    }
}
