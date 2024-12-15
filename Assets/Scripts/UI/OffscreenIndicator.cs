using Characters;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class OffscreenIndicator : MonoBehaviour
    {
        UnityEngine.Camera m_MainCamera;
        RectTransform m_CanvasRect;
        Character m_Target;
    
        RectTransform m_ArrowRect;
        Image m_Icon;
        const float EdgeOffset = 20f;

        Color m_IconColor;

        public void Init(UnityEngine.Camera cam, RectTransform canvas, Character target)
        {
            m_MainCamera = cam;
            m_CanvasRect = canvas;
            m_Target = target;
            
            m_ArrowRect = GetComponent<RectTransform>();
            m_Icon = GetComponent<Image>();
            m_Target.OnDie += Destroy;
            m_IconColor = m_Icon.color;
        }

        void OnGUI()
        {
            Vector3 enemyViewportPos = m_MainCamera.WorldToViewportPoint(m_Target.transform.position);

            // Visible test
            if (enemyViewportPos.z > 0 && (enemyViewportPos.x < 0 || enemyViewportPos.x > 1 || enemyViewportPos.y < 0 || enemyViewportPos.y > 1))
            {
                Vector3 cameraPos =
                    new Vector3(m_MainCamera.transform.position.x, m_MainCamera.transform.position.y, m_Target.transform.position.z);
                float dist = Vector3.Distance(m_Target.transform.position, cameraPos);
                m_IconColor.a = Mathf.InverseLerp(20f, 150f, dist);

                Vector3 arrowScreenPosition = FindEdgePosition(enemyViewportPos);

                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_CanvasRect, arrowScreenPosition, null, out Vector2 localCanvasPos))
                {
                    m_ArrowRect.anchoredPosition = localCanvasPos;

                    // Rotate
                    Vector3 direction = (m_Target.transform.position - m_MainCamera.transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    m_ArrowRect.rotation = Quaternion.Euler(0, 0, angle - 90);
                }
            }
            else
            {
                m_IconColor.a = 0f;
            }

            m_Icon.color = m_IconColor;
        }

        Vector3 FindEdgePosition(Vector3 viewportPos)
        {
            Vector3 edgePos = viewportPos;

            bool isLeft = viewportPos.x < 0f;
            bool isRight = viewportPos.x > 1f;
            bool isBottom = viewportPos.y < 0f;
            bool isTop = viewportPos.y > 1f;

            // Apply offset
            if (isLeft || isRight)
            {
                // Clamp height
                edgePos.y = Mathf.Clamp(viewportPos.y, EdgeOffset / Screen.height, 1f - EdgeOffset / Screen.height);
            }

            if (isTop || isBottom)
            {
                // Clamp width
                edgePos.x = Mathf.Clamp(viewportPos.x, EdgeOffset / Screen.width, 1f - EdgeOffset / Screen.width);
            }
        
            Vector3 screenPos = m_MainCamera.ViewportToScreenPoint(edgePos);

            // Adjusts positions
            if (isLeft) screenPos.x = EdgeOffset;
            if (isRight) screenPos.x = Screen.width - EdgeOffset;
            if (isBottom) screenPos.y = EdgeOffset;
            if (isTop) screenPos.y = Screen.height - EdgeOffset;

            return screenPos;
        }

        void Destroy(Character target)
        {
            m_Target.OnDie -= Destroy;
            Destroy(gameObject);
        }
    }
}
