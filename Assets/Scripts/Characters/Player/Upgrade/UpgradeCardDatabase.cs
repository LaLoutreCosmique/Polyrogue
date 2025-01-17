using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player.Upgrade
{
    [CreateAssetMenu(menuName = "Polyrogue/UpgradeCard/UpgradeCardDatabase", fileName = "New Upgrade Card Database")]
    public class UpgradeCardDatabase : ScriptableObject
    {
        public List<UpgradeCardData> commonCards;
    }
}
