using UnityEditor;
using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(menuName = "Polyrogue/Character Data", fileName = "New Character Data")]
    public class CharacterData : ScriptableObject
    {
        //public CharacterData(float acceleration, float maxSpeed, float dashForce, float dashCooldown, float dashDuration, int health, int shieldHealth, float takenKnockbackMultiplier, float invincibilityDuration,
        //    float attackCooldown, int attackDamage, float attackSize, float attackKnockback, float projectileSpeed)
        //{
        //    this.acceleration = acceleration;
        //    this.maxSpeed = maxSpeed;
        //    this.dashForce = dashForce;
        //    this.dashCooldown = dashCooldown;
        //    this.dashDuration = dashDuration;
        //    this.health = health;
        //    this.shieldHealth = shieldHealth;
        //    this.takenKnockbackMultiplier = takenKnockbackMultiplier;
        //    this.invincibilityDuration = invincibilityDuration;
        //    this.attackCooldown = attackCooldown;
        //    this.attackDamage = attackDamage;
        //    this.attackSize = attackSize;
        //    this.attackKnockback = attackKnockback;
        //    this.projectileSpeed = projectileSpeed;
        //}
        
        [HideInInspector] public bool isModifier;
        
        [Header("Movement Settings")]
        [HideInInspector] public float acceleration;
        [HideInInspector] public ModifierType accelerationType;
        [HideInInspector] public float maxSpeed;
        [HideInInspector] public ModifierType maxSpeedType;
        [HideInInspector] public float dashForce;
        [HideInInspector] public ModifierType dashForceType;
        [HideInInspector] public float dashCooldown;
        [HideInInspector] public ModifierType dashCooldownType;
        [HideInInspector] public float dashDuration;
        [HideInInspector] public ModifierType dashDurationType;

        [Header("Health Settings")]
        [HideInInspector] public int health;
        [HideInInspector] public ModifierType healthType;
        [HideInInspector] public int shieldHealth;
        [HideInInspector] public ModifierType shieldHealthType;
        [HideInInspector] public float suddenKnockbackMultiplier;
        [HideInInspector] public ModifierType suddenKnockbackMultiplierType;
        [HideInInspector] public float invincibilityDuration;
        [HideInInspector] public ModifierType invincibilityDurationType;
        
        [Header("Attack Settings")]
        [HideInInspector] public float attackCooldown;
        [HideInInspector] public ModifierType attackCooldownType;
        [HideInInspector] public int attackDamage;
        [HideInInspector] public ModifierType attackDamageType;
        [HideInInspector] public float attackSize;
        [HideInInspector] public ModifierType attackSizeType;
        [HideInInspector] public float attackKnockback;
        [HideInInspector] public ModifierType attackKnockbackType;
        [HideInInspector] public float projectileSpeed;
        [HideInInspector] public ModifierType projectileSpeedType;
        
        public void Add(CharacterData other)
        {
            acceleration += other.acceleration;
            maxSpeed += other.maxSpeed;
            dashForce += other.dashForce;
            dashCooldown += other.dashCooldown;
            dashDuration += other.dashDuration;
            health += other.health;
            shieldHealth += other.shieldHealth;
            suddenKnockbackMultiplier += other.suddenKnockbackMultiplier;
            invincibilityDuration += other.invincibilityDuration;
            attackCooldown += other.attackCooldown;
            attackDamage += other.attackDamage;
            attackSize += other.attackSize;
            attackKnockback += other.attackKnockback;
            projectileSpeed += other.projectileSpeed;
        }
    }

    public enum ModifierType
    {
        Addition,
        Multiplication
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(CharacterData))]
    public class CharacterData_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CharacterData data = (CharacterData)target;
            Undo.RecordObject(data, "Modify CharacterData");
            
            data.isModifier = EditorGUILayout.Toggle("Is Modifier", data.isModifier);

            EditorGUILayout.LabelField("Movement Settings", EditorStyles.boldLabel);

            data.acceleration = EditorGUILayout.FloatField("Acceleration", data.acceleration);
            if (data.isModifier && data.acceleration != 0f)
            {
                data.accelerationType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.accelerationType);
                EditorGUILayout.LabelField("");
            }
            
            data.maxSpeed = EditorGUILayout.FloatField("Max Speed", data.maxSpeed);
            if (data.isModifier && data.maxSpeed != 0f)
            {
                data.maxSpeedType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.maxSpeedType);
                EditorGUILayout.LabelField("");
            }
            
            data.dashForce = EditorGUILayout.FloatField("Dash Force", data.dashForce);
            if (data.isModifier && data.dashForce != 0f)
            {
                data.dashForceType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.dashForceType);
                EditorGUILayout.LabelField("");
            }
            
            data.dashCooldown = EditorGUILayout.FloatField("Dash Cooldown", data.dashCooldown);
            if (data.isModifier && data.dashCooldown != 0f)
            {
                data.dashCooldownType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.dashCooldownType);
                EditorGUILayout.LabelField("");
            }
            
            data.dashDuration = EditorGUILayout.FloatField("Dash Duration", data.dashDuration);
            if (data.isModifier && data.dashDuration != 0f)
            {
                data.dashDurationType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.dashDurationType);
                EditorGUILayout.LabelField("");
            }
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("Health Settings", EditorStyles.boldLabel);
            
            data.health = EditorGUILayout.IntField("Health", data.health);
            if (data.isModifier && data.health != 0f)
            {
                data.healthType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.healthType);
                EditorGUILayout.LabelField("");
            }
            
            data.shieldHealth = EditorGUILayout.IntField("Shield Health", data.shieldHealth);
            if (data.isModifier && data.shieldHealth != 0f)
            {
                data.shieldHealthType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.shieldHealthType);
                EditorGUILayout.LabelField("");
            }
            
            data.suddenKnockbackMultiplier = EditorGUILayout.FloatField("Sudden Knockback Multiplier", data.suddenKnockbackMultiplier);
            if (data.isModifier && data.suddenKnockbackMultiplier != 0f)
            {
                data.suddenKnockbackMultiplierType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.suddenKnockbackMultiplierType);
                EditorGUILayout.LabelField("");
            }
            
            data.invincibilityDuration = EditorGUILayout.FloatField("Invincibility Duration", data.invincibilityDuration);
            if (data.isModifier && data.invincibilityDuration != 0f)
            {
                data.invincibilityDurationType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.invincibilityDurationType);
                EditorGUILayout.LabelField("");
            }
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("Attack Settings", EditorStyles.boldLabel);
            
            data.attackCooldown = EditorGUILayout.FloatField("Attack Cooldown", data.attackCooldown);
            if (data.isModifier && data.attackCooldown != 0f)
            {
                data.attackCooldownType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.attackCooldownType);
                EditorGUILayout.LabelField("");
            }
            
            data.attackDamage = EditorGUILayout.IntField("Attack Damage", data.attackDamage);
            if (data.isModifier && data.attackDamage != 0f)
            {
                data.attackDamageType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.attackDamageType);
                EditorGUILayout.LabelField("");
            }
            
            data.attackSize = EditorGUILayout.FloatField("Attack Size", data.attackSize);
            if (data.isModifier && data.attackSize != 0f)
            {
                data.attackSizeType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.attackSizeType);
                EditorGUILayout.LabelField("");
            }
            
            data.attackKnockback = EditorGUILayout.FloatField("Attack Knockback", data.attackKnockback);
            if (data.isModifier && data.attackKnockback != 0f)
            {
                data.attackKnockbackType = (ModifierType)EditorGUILayout.EnumPopup("Modifier type", data.attackKnockbackType);
                EditorGUILayout.LabelField("");
            }
            
            data.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed", data.projectileSpeed);
            if (data.isModifier && data.projectileSpeed != 0f)
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
