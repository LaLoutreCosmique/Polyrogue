using System;
using UnityEngine;
using DG.Tweening;

namespace UI.Upgrade
{
    public class UpgradeArrows : MonoBehaviour
    {
        [SerializeField] RectTransform[] m_Arrows;
        [SerializeField] float m_Scale;
        [SerializeField] float m_AnimDuration;
        [SerializeField] float m_Offset;

        Vector3[] m_InitialPositions;
        
        void Awake()
        {
            m_InitialPositions = new Vector3[m_Arrows.Length];
            
            for (int i = 0; i < m_Arrows.Length; i++)
            {
                m_Arrows[i].localScale = Vector3.zero;
                m_InitialPositions[i] = m_Arrows[i].position;
            }
        }

        public void Display(int index)
        {
            
            m_Arrows[index].DOScale(m_Scale, m_AnimDuration).SetUpdate(true);

            switch (index)
            {
                case 0:
                    m_Arrows[index].DOMoveX(m_Arrows[index].position.x - m_Offset, m_AnimDuration).SetEase(Ease.OutExpo).SetUpdate(true); // Left
                    break;
                case 1:
                    m_Arrows[index].DOMoveX(m_Arrows[index].position.x + m_Offset, m_AnimDuration).SetEase(Ease.OutExpo).SetUpdate(true); // Right
                    break;
                case 2:
                    m_Arrows[index].DOMoveY(m_Arrows[index].position.y + m_Offset, m_AnimDuration).SetEase(Ease.OutExpo).SetUpdate(true); // Up
                    break;
                case 3:
                    m_Arrows[index].DOMoveY(m_Arrows[index].position.y - m_Offset, m_AnimDuration).SetEase(Ease.OutExpo).SetUpdate(true); // Down
                    break;
            }
        }

        public void Hide(int nbArrow, int selected)
        {
            Sequence sequence = DOTween.Sequence().SetUpdate(true);
            
            switch (selected)
            {
                case 0:
                    sequence.Append(m_Arrows[selected].DOMoveX(m_Arrows[selected].position.x - m_Offset, m_AnimDuration).SetEase(Ease.OutExpo)); // Horizontal
                    break;
                case 1:
                    sequence.Append(m_Arrows[selected].DOMoveX(m_Arrows[selected].position.x + m_Offset, m_AnimDuration).SetEase(Ease.OutExpo)); // Horizontal
                    break;
                case 2:
                    sequence.Append(m_Arrows[selected].DOMoveY(m_Arrows[selected].position.y + m_Offset, m_AnimDuration).SetEase(Ease.OutExpo)); // Vertical
                    break;
                case 3:
                    sequence.Append(m_Arrows[selected].DOMoveY(m_Arrows[selected].position.y - m_Offset, m_AnimDuration).SetEase(Ease.OutExpo)); // Vertical
                    break;
            }

            sequence.Append(m_Arrows[0].DOMove(m_Arrows[0].position, m_AnimDuration));
            
            for (int i = 0; i < nbArrow; i++)
            {
                sequence.Join(m_Arrows[i].DOMove(m_InitialPositions[i], m_AnimDuration).SetEase(Ease.InExpo));
                sequence.Join(m_Arrows[i].DOScale(0, m_AnimDuration * 1.5f));
            }
        }
    }
}
