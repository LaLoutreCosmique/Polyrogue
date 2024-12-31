using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player.Inputs
{
    public class InputManager
    {
        Player m_Player;
        
        PlayerControls m_Controls;
        PlayerControls.PlayerActions m_PlayerActions;

        public InputManager(Player player)
        {
            m_Player = player;
            m_Controls = new PlayerControls();
            m_PlayerActions = m_Controls.Player;

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
        }
    }
}
