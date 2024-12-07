using UnityEngine;

namespace Utilities
{
    public class TargetFollow : MonoBehaviour
    {
        [SerializeField] Transform m_target;
        Vector3 m_offset;

        void Update()
        {
            m_offset.z = Mathf.Abs(m_target.position.z) + 1;
            transform.position = m_target.position + m_offset;
        }
    }
}
