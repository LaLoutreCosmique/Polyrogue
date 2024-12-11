using Characters;
using UnityEngine;

namespace UI
{
    public class OffscreenIndicator : MonoBehaviour
    {
        [SerializeField] Character m_Target;
        UnityEngine.Camera m_Camera;

        void OnEnable()
        {
            m_Target.OnChangePosition += OntargetMoves;
        }

        void OnDisable()
        {
            m_Target.OnChangePosition -= OntargetMoves;
            print("détruit !");
        }

        void OntargetMoves(Vector2 targetPosition)
        {
            // Check if visible
        }
    }
}
