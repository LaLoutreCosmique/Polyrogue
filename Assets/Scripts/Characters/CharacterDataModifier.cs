using System;
using UnityEditor;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "Polyrogue/Character Data Modifier", fileName = "New Character Data Modifier")]
    public class CharacterDataModifier : CharacterData
    {
        [Header("Movement Settings")]
        [HideInInspector] public ModifierType accelerationType;
        [HideInInspector] public ModifierType maxSpeedType;
        [HideInInspector] public ModifierType dashForceType;
        [HideInInspector] public ModifierType dashCooldownType;
        [HideInInspector] public ModifierType dashDurationType;

        [Header("Health Settings")]
        [HideInInspector] public ModifierType healthType;
        [HideInInspector] public ModifierType shieldHealthType;
        [HideInInspector] public ModifierType suddenKnockbackMultiplierType;
        [HideInInspector] public ModifierType invincibilityDurationType;
        
        [Header("Attack Settings")]
        [HideInInspector] public ModifierType attackCooldownType;
        [HideInInspector] public ModifierType attackDamageType;
        [HideInInspector] public ModifierType attackSizeType;
        [HideInInspector] public ModifierType attackKnockbackType;
        [HideInInspector] public ModifierType projectileSpeedType;

        [HideInInspector] public ModifierType[] floatsModifierType;
        [HideInInspector] public ModifierType[] intsModifierType;

        void OnEnable()
        {
            Init();
        }

        protected override void Init()
        {
            base.Init();
            
            floatsModifierType = new[]
            {
                accelerationType, 
                maxSpeedType, 
                dashForceType, 
                dashCooldownType, 
                dashDurationType,
                suddenKnockbackMultiplierType,
                invincibilityDurationType,
                attackCooldownType,
                attackSizeType,
                attackKnockbackType,
                projectileSpeedType
            };
            
            intsModifierType = new[]
            {
                healthType,
                shieldHealthType,
                attackDamageType
            };
        }
    }

    public enum ModifierType
    {
        Addition,
        Multiplication
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(CharacterDataModifier))]
    public class CharacterDataModifier_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CharacterDataModifier data = (CharacterDataModifier)target;
            Undo.RecordObject(data, "Modify CharacterData");

            EditorGUILayout.LabelField("Movement Settings", EditorStyles.boldLabel);

            data.acceleration = EditorGUILayout.FloatField("Acceleration", data.acceleration);
            if (data.acceleration != 0f)
            {
                data.accelerationType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.accelerationType);
                EditorGUILayout.LabelField("");
            }
            
            data.maxSpeed = EditorGUILayout.FloatField("Max Speed", data.maxSpeed);
            if (data.maxSpeed != 0f)
            {
                data.maxSpeedType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.maxSpeedType);
                EditorGUILayout.LabelField("");
            }
            
            data.dashForce = EditorGUILayout.FloatField("Dash Force", data.dashForce);
            if (data.dashForce != 0f)
            {
                data.dashForceType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.dashForceType);
                EditorGUILayout.LabelField("");
            }
            
            data.dashCooldown = EditorGUILayout.FloatField("Dash Cooldown", data.dashCooldown);
            if (data.dashCooldown != 0f)
            {
                data.dashCooldownType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.dashCooldownType);
                EditorGUILayout.LabelField("");
            }
            
            data.dashDuration = EditorGUILayout.FloatField("Dash Duration", data.dashDuration);
            if (data.dashDuration != 0f)
            {
                data.dashDurationType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.dashDurationType);
                EditorGUILayout.LabelField("");
            }
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("Health Settings", EditorStyles.boldLabel);
            
            data.health = EditorGUILayout.IntField("Health", data.health);
            if (data.health != 0f)
            {
                data.healthType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.healthType);
                EditorGUILayout.LabelField("");
            }
            
            data.shieldHealth = EditorGUILayout.IntField("Shield Health", data.shieldHealth);
            if (data.shieldHealth != 0f)
            {
                data.shieldHealthType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.shieldHealthType);
                EditorGUILayout.LabelField("");
            }
            
            data.suddenKnockbackMultiplier = EditorGUILayout.FloatField("Sudden Knockback Multiplier", data.suddenKnockbackMultiplier);
            if (data.suddenKnockbackMultiplier != 0f)
            {
                data.suddenKnockbackMultiplierType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.suddenKnockbackMultiplierType);
                EditorGUILayout.LabelField("");
            }
            
            data.invincibilityDuration = EditorGUILayout.FloatField("Invincibility Duration", data.invincibilityDuration);
            if (data.invincibilityDuration != 0f)
            {
                data.invincibilityDurationType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.invincibilityDurationType);
                EditorGUILayout.LabelField("");
            }
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("Attack Settings", EditorStyles.boldLabel);
            
            data.attackCooldown = EditorGUILayout.FloatField("Attack Cooldown", data.attackCooldown);
            if (data.attackCooldown != 0f)
            {
                data.attackCooldownType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.attackCooldownType);
                EditorGUILayout.LabelField("");
            }
            
            data.attackDamage = EditorGUILayout.IntField("Attack Damage", data.attackDamage);
            if (data.attackDamage != 0f)
            {
                data.attackDamageType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.attackDamageType);
                EditorGUILayout.LabelField("");
            }
            
            data.attackSize = EditorGUILayout.FloatField("Attack Size", data.attackSize);
            if (data.attackSize != 0f)
            {
                data.attackSizeType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.attackSizeType);
                EditorGUILayout.LabelField("");
            }
            
            data.attackKnockback = EditorGUILayout.FloatField("Attack Knockback", data.attackKnockback);
            if (data.attackKnockback != 0f)
            {
                data.attackKnockbackType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.attackKnockbackType);
                EditorGUILayout.LabelField("");
            }
            
            data.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed", data.projectileSpeed);
            if (data.projectileSpeed != 0f)
            {
                data.projectileSpeedType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.projectileSpeedType);
                EditorGUILayout.LabelField("");
            }
            
            if (GUI.changed)
            {
                EditorUtility.SetDirty(data);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}
