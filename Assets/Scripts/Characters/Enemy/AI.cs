using UnityEngine;
using Cooldown = Utilities.Cooldown;
using Random = UnityEngine.Random;

namespace Characters.Enemy
{
    public class AI
    {
        Enemy m_Parent;
        Character m_Target;
        AIBehaviour m_AIData;

        Cooldown m_AttackTimer;
        Cooldown m_AttackPause;

        public Vector2 m_Direction;
        bool m_TargetInAttackRange;
        bool m_TargetInRange;
        
        float TargetDistance => Vector2.Distance(m_Target.transform.position, m_Parent.transform.position);

        public AI(Enemy enemy, Character target, AIBehaviour AIData)
        {
            m_Parent = enemy;
            m_Target = target;
            m_AIData = AIData;

            m_AttackTimer = new Cooldown(m_AIData.attackDuration, cooldownOver:OnAttackTimerOver);
            m_AttackPause = new Cooldown(m_AIData.attackPauseDuration, cooldownOver:OnAttackPauseOver);
        }

        public void Start()
        {
            m_TargetInRange = TargetDistance > m_AIData.maxTargetDistance ||
                              TargetDistance < m_AIData.minTargetDistance;
            m_TargetInAttackRange = TargetDistance > m_AIData.maxAttackDistance;

            float speedMultiplier = Random.Range(0.7f, 1.3f);
            m_Parent.Data.maxSpeed *= speedMultiplier;
            
            MotionCalculations();
            Dash();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void Update()
        {
            m_AttackPause.Update();
            m_AttackTimer.Update();

            MotionCalculations();
        }

        void MotionCalculations()
        {
            // Calculate move direction
            Vector2 selfPos = m_Parent.transform.position;
            Vector2 targetPos = m_Target.transform.position;
            Vector2 targetDir = targetPos - selfPos;

            Vector2 normal = new Vector2(-targetDir.y, targetDir.x).normalized;
            
            Vector2 velocity = m_Parent.rb2d.velocity;
            Vector2 reflectedVelocity = Vector2.Reflect(velocity, normal);

            Vector2 directionToTarget = targetDir.normalized;

            m_Direction = Vector2.Dot(reflectedVelocity, directionToTarget) < 0 || m_Parent.rb2d.velocity.magnitude < 0.01f
                ? directionToTarget
                : reflectedVelocity.normalized;

            // Calculate aim rotation
            Vector3 targetVel = m_Target.rb2d.velocity;
            Vector3 aimTarget = m_Target.transform.position + targetVel * targetVel.magnitude / TargetDistance  / m_Parent.Data.projectileSpeed;
            Vector2 aimRotation = aimTarget - m_Parent.transform.position;
            m_Parent.SetRotateInput(aimRotation);
            
            // Set rotation
            float targetAngle = Mathf.Atan2(aimRotation.x, aimRotation.y) * Mathf.Rad2Deg;
            m_Parent.SetRotateDirection(Quaternion.Euler(0, 0, -targetAngle));
            
            // Set move direction
            if (TargetDistance > m_AIData.maxTargetDistance)
                m_Parent.m_MoveDirection = m_Direction.normalized;
            else if (TargetDistance < m_AIData.minTargetDistance)
                m_Parent.m_MoveDirection = -m_Direction.normalized;
            
            // Move distance
            if (TargetDistance < m_AIData.maxTargetDistance && TargetDistance > m_AIData.minTargetDistance && !m_TargetInRange)
                OnTargetInRange();
            else if ((TargetDistance > m_AIData.maxTargetDistance || TargetDistance < m_AIData.minTargetDistance) && m_TargetInRange)
                OnTargetLeftRange();
            
            // Attack distance
            if (TargetDistance < m_AIData.maxAttackDistance && !m_TargetInAttackRange)
                OnTargetInAttackRange();
            else if (TargetDistance > m_AIData.maxAttackDistance && m_TargetInAttackRange)
                OnTargetLeftAttackRange();
        }
        
        #region movement
        void OnTargetInRange()
        {
            m_TargetInRange = true;
            m_Parent.StopMove();
        }
        
        void OnTargetLeftRange()
        {
            m_TargetInRange = false;
            if (m_AIData.canMove)
                m_Parent.StartMove();
        }
        #endregion
        
        #region attack
        void Attack()
        {
            if (!m_AIData.canAttack || !m_TargetInAttackRange || m_AttackPause.IsRunning) return;
            
            m_Parent.Attack();

            if (m_AIData.timedAttacks && !m_AttackTimer.IsRunning)
                m_AttackTimer.Start();
        }
        
        public void OnAttackRecovered() { Attack(); }
        
        void OnTargetInAttackRange()
        {
            m_TargetInAttackRange = true;
            Attack();
        }

        void OnTargetLeftAttackRange()
        {
            m_TargetInAttackRange = false;
            m_AttackPause.Complete();
            m_AttackTimer.Complete();
        }

        void OnAttackTimerOver() { m_AttackPause.Start(); }

        void OnAttackPauseOver()
        {
            if (m_TargetInAttackRange)
                Attack();
        }
        #endregion

        void Dash()
        {
            if (!m_AIData.canDash) return;

            float newDashCd = Random.Range(m_AIData.dashMinWaitingTime, m_AIData.dashMaxWaitingTime);
            Vector2 dashDirection = m_Parent.m_MoveDirection;

            if (m_AIData.doLateralDash)
            {
                // 0 = dash to the right, 1 = dash to the left
                int randomDashDir = Random.Range(0, 2);

                dashDirection = randomDashDir switch
                {
                    1 => new Vector2(-dashDirection.y, dashDirection.x),
                    2 => new Vector2(dashDirection.y, -dashDirection.x),
                    _ => dashDirection
                };
            }
            
            m_Parent.Dash(dashDirection, newDashCd);
        }
        
        public void OnDashRecovered() { Dash(); }
    }
}
