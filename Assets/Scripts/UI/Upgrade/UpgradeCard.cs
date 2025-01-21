using Characters.Player.Upgrade;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.Upgrade
{
    public class UpgradeCard : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] CardDirection m_Direction;
        [SerializeField] float m_AnimDuration;
        
        [Header("Children References")]
        [SerializeField] TextMeshProUGUI m_Title;
        [SerializeField] TextMeshProUGUI m_Description;

        RectTransform m_Rect;
        Vector2 m_InitialPosition;

        void Awake()
        {
            m_Rect = GetComponent<RectTransform>();
            m_InitialPosition = m_Rect.position;
        }

        void OnEnable()
        {
            m_Rect.position = new Vector2(m_InitialPosition.x, m_Rect.position.y - Screen.height);
        }

        public void Display(UpgradeCardData data)
        {
            m_Title.text = data.cardName;
            m_Description.text = data.description;
            m_Rect.DOMove(m_InitialPosition, m_AnimDuration).SetEase(Ease.OutExpo).SetUpdate(true);
        }

        public void ValidAnim()
        {
            Sequence sequence = DOTween.Sequence().SetUpdate(true);

            switch (m_Direction)
            {
                case CardDirection.Left:
                    sequence.Append(m_Rect.DOMoveX(m_InitialPosition.x - Screen.width, m_AnimDuration));
                    break;
                case CardDirection.Right:
                    sequence.Append(m_Rect.DOMoveX(m_InitialPosition.x + Screen.width, m_AnimDuration));
                    break;
                case CardDirection.Up:
                    sequence.Append(m_Rect.DOMoveY(m_InitialPosition.y + Screen.height, m_AnimDuration));
                    break;
                case CardDirection.Down:
                    sequence.Append(m_Rect.DOMoveY(m_InitialPosition.y - Screen.height, m_AnimDuration));
                    break;
            }
            
            sequence.Append(m_Rect.DOMove(new Vector2(m_InitialPosition.x, m_Rect.position.y - Screen.height), 0));
        }

        public void Hide()
        {
            m_Rect.DOShakeRotation(m_AnimDuration, 60f, vibrato: 4, fadeOut: false).SetUpdate(true);
            Sequence sequence = DOTween.Sequence().SetUpdate(true);
            sequence.Append(m_Rect.DOScale(0, m_AnimDuration));
            sequence.Append(m_Rect.DOMove(new Vector2(m_InitialPosition.x, m_Rect.position.y - Screen.height), 0));
            sequence.Append(m_Rect.DOScale(1, 0));
        }
    }

    public enum CardDirection
    {
        Left,
        Right,
        Up,
        Down
    }
}
