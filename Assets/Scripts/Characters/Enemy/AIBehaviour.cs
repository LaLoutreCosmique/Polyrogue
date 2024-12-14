using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Enemy
{
    [CreateAssetMenu(menuName = "Polyrogue/AI Behaviour", fileName = "New AI Behaviour")]
    public class AIBehaviour : ScriptableObject
    {
        [Header("Behaviour settings")]
        [HideInInspector] public bool canMove;
        [HideInInspector] public float minTargetDistance;
        [HideInInspector] public float maxTargetDistance;
        
        [HideInInspector] public bool canAttack;
        [HideInInspector] public float maxAttackDistance;
        [HideInInspector] public bool timedAttacks;
        [HideInInspector] public float attackDuration;
        [HideInInspector] public float attackPauseDuration;

        [HideInInspector] public bool canDash;
        [HideInInspector] public bool doLateralDash;
        [HideInInspector] public float dashMinWaitingTime;
        [HideInInspector] public float dashMaxWaitingTime;
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(AIBehaviour))]
    public class AIBehaviour_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AIBehaviour data = (AIBehaviour)target;
            
            EditorGUILayout.LabelField("Behaviour selection", EditorStyles.boldLabel);
            
            data.canMove = EditorGUILayout.Toggle("Can Move", data.canMove);
            if (data.canMove)
            {
                data.minTargetDistance = EditorGUILayout.FloatField("Min Target Distance", data.minTargetDistance);
                data.minTargetDistance = Mathf.Clamp(data.minTargetDistance, 0, Mathf.Infinity);
                
                data.maxTargetDistance = EditorGUILayout.FloatField("Max Target Distance", data.maxTargetDistance);
                data.maxTargetDistance = Mathf.Clamp(data.maxTargetDistance, data.minTargetDistance, Mathf.Infinity);
            }
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            data.canAttack = EditorGUILayout.Toggle("Can Attack", data.canAttack);
            if (data.canAttack)
            {
                data.maxAttackDistance = EditorGUILayout.FloatField("Max Attack Distance", data.maxAttackDistance);
                if (data.maxAttackDistance <= data.maxTargetDistance)
                    EditorGUILayout.HelpBox("It is preferable for ‘Max Attack Distance’ to be greater than ‘Max Target Distance’.", MessageType.Warning);
                
                data.timedAttacks = EditorGUILayout.Toggle("Timed Attacks", data.timedAttacks);

                if (data.timedAttacks)
                {
                    data.attackDuration = EditorGUILayout.FloatField("Attack Duration", data.attackDuration);
                    data.attackPauseDuration = EditorGUILayout.FloatField("Attack Pause Duration", data.attackPauseDuration);
                }
            }
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            data.canDash = EditorGUILayout.Toggle("Can Dash", data.canDash);
            if (data.canDash)
            {
                data.doLateralDash = EditorGUILayout.Toggle("Do Lateral Dash", data.doLateralDash);
                data.dashMinWaitingTime = EditorGUILayout.FloatField("Dash Min Waiting Time", data.dashMinWaitingTime);
                data.dashMinWaitingTime = Mathf.Clamp(data.dashMinWaitingTime, 0.5f, Mathf.Infinity);
                
                data.dashMaxWaitingTime = EditorGUILayout.FloatField("Dash Max Waiting Time", data.dashMaxWaitingTime);
                data.dashMaxWaitingTime = Mathf.Clamp(data.dashMaxWaitingTime, data.dashMinWaitingTime, Mathf.Infinity);
            }
        }
    }
    #endif
}
