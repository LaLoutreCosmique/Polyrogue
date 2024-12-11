using System;
using Characters;
using Characters.Player;
using UnityEngine;

namespace Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Character m_Player;

        [SerializeField] Vector3 m_Offset;
        [SerializeField] float m_SmoothSpeed = 5f;
        [SerializeField] float m_DirectionOffset = 5f;

        void Update()
        {
            Vector3 normalizedDirection = m_Player.m_RotateInput.normalized;
            Vector3 lookAtOffset = new Vector3(normalizedDirection.x, normalizedDirection.y, 0) * m_DirectionOffset;
            Vector3 desiredPosition = m_Player.transform.position + m_Offset + lookAtOffset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, m_SmoothSpeed * Time.deltaTime);
        
            transform.position = smoothedPosition;
        }
    }
}
