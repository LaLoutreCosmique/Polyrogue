using UnityEngine;

namespace Characters.Player.Upgrade
{
    [CreateAssetMenu(menuName = "Polyrogue/UpgradeCard/UpgradeCardData", fileName = "New Upgrade Card Data")]
    public class UpgradeCardData : ScriptableObject
    {
        public string cardName;
        public string description;
        public CharacterDataModifier modifier;
        public SpecialUpgrade[] special;
        
        [System.Serializable]
        public class SpecialUpgrade
        {
            [SerializeField]
            public SpecialUpgradeType type;
    
            [SerializeField]
            public bool active;
        }
    }

    public enum SpecialUpgradeType
    {
        SpamAttack
    }
}
