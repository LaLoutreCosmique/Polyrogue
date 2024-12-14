using System.Collections.Generic;
using UnityEngine;

namespace Phase
{
    [CreateAssetMenu(menuName = "Polyrogue/Waves/Database", fileName = "New Phase Database")]
    public class PhaseDatabase : ScriptableObject
    {
        public List<PhaseData> data;
    }
}
