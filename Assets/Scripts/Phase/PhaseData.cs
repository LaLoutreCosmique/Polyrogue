#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

using UnityEngine;
using UnityEngine.UIElements;

namespace Phase
{
    [CreateAssetMenu(menuName = "Polyrogue/Phase/Phase data", fileName = "New Phase")]
    public class PhaseData : ScriptableObject
    {
        public Wave[] waves;
        //TODO possible abilities
    }

    [System.Serializable]
    public struct Wave
    {
        public GameObject enemyGo;
        public int amount;
        public float spawnCooldown;
        public bool waitForNext;
        public float timeBeforeNext; //TODO: hide when waitForNext is toggled (with property drawer?)
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(Wave))]
    public class Wave_UIE : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement(); 
            
            var enemyGoField = new PropertyField(property.FindPropertyRelative("enemyGo"));
            var amountField = new PropertyField(property.FindPropertyRelative("amount"));
            var spawnCooldownField = new PropertyField(property.FindPropertyRelative("spawnCooldown"));
            var waitForNextField = new PropertyField(property.FindPropertyRelative("waitForNext"));
            var timeBeforeNextField = new PropertyField(property.FindPropertyRelative("timeBeforeNext"));
            
            container.Add(enemyGoField);
            container.Add(amountField);
            container.Add(spawnCooldownField);
            container.Add(waitForNextField);
            container.Add(timeBeforeNextField);

            return container;
        }
    }
#endif
}