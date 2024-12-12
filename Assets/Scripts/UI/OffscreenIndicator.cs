using System;
using Characters;
using UnityEngine;

public class OffscreenIndicator : MonoBehaviour
{
    [SerializeField] UnityEngine.Camera mainCamera;
    [SerializeField] RectTransform canvasRect;
    [SerializeField] Character m_Target;
    
    RectTransform arrowRect;
    const float EdgeOffset = 20f;

    void Awake()
    {
        Init();
    }

    public void Init()
    {
        arrowRect = GetComponent<RectTransform>();
        m_Target.OnDie += () => Destroy(gameObject);
    }

    void Update()
    {
        Vector3 enemyViewportPos = mainCamera.WorldToViewportPoint(m_Target.transform.position);

        // Visible test
        if (enemyViewportPos.z > 0 && (enemyViewportPos.x < 0 || enemyViewportPos.x > 1 || enemyViewportPos.y < 0 || enemyViewportPos.y > 1))
        {
            Vector3 arrowScreenPosition = FindEdgePosition(enemyViewportPos);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, arrowScreenPosition, null, out Vector2 localCanvasPos))
            {
                arrowRect.anchoredPosition = localCanvasPos;

                // Rotate
                Vector3 direction = (m_Target.transform.position - mainCamera.transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrowRect.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }
        else
        {
            arrowRect.anchoredPosition = new Vector2(-9999, -9999);
        }
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
        
        Vector3 screenPos = mainCamera.ViewportToScreenPoint(edgePos);

        // Adjusts positions
        if (isLeft) screenPos.x = EdgeOffset;
        if (isRight) screenPos.x = Screen.width - EdgeOffset;
        if (isBottom) screenPos.y = EdgeOffset;
        if (isTop) screenPos.y = Screen.height - EdgeOffset;

        return screenPos;
    }
}
