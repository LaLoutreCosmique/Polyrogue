using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Player;
using Characters.Player.Upgrade;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Upgrade
{
    public class UpgradeState : MonoBehaviour
    {
        Player m_Player;
        
        public event Action OnFinishState;

        [SerializeField] UpgradeCardDatabase m_InitialUpgradeCards; // DON'T TOUCH THIS (pls)
        [SerializeField] Image m_Background;
        [SerializeField] float m_BackgourndAlpha;
        [SerializeField] int m_NbCardsToDraw;
        [SerializeField] UpgradeCard[] m_CardsVisuals;
        [SerializeField] UpgradeArrows m_Arrows;

        UpgradeCardDatabase m_CurrentUpgradeCards;
        UpgradeCardData[] cardsToChoose;
        bool isStarted;

        public void Init(Player player)
        {
            m_Player = player;

            m_Player.OnUpgradeInputPerformed += OnCardChosen;
            m_CurrentUpgradeCards = Instantiate(m_InitialUpgradeCards);
        }

        void OnDisable()
        {
            m_Player.OnUpgradeInputPerformed -= OnCardChosen;
        }

        public void StartState()
        {
            isStarted = true;
            
            m_Player.m_InvincibilityCooldown.Start();
            
            DrawCards(m_NbCardsToDraw);
        }

        void DrawCards(int nbCards)
        {
            List<UpgradeCardData> pickedCards = new ();
            for (int i = 0; i < nbCards; i++)
            {
                // IT PICK ONLY COMMON CARDS (for now ?)
                int drawnIndex = Random.Range(0, m_CurrentUpgradeCards.commonCards.Count);
                UpgradeCardData pickedCard = m_CurrentUpgradeCards.commonCards[drawnIndex];
                
                if (pickedCards.Any(c => c.cardName == pickedCard.cardName))
                {
                    i--;
                    continue;
                }
                
                pickedCards.Add(pickedCard);
            }

            cardsToChoose = pickedCards.ToArray();
            StartCoroutine(DisplayCards());
        }

        IEnumerator DisplayCards()
        {
            for (int i = 0; i < m_NbCardsToDraw; i++)
            {
                print(m_CardsVisuals[i].name);
                m_CardsVisuals[i].Display(cardsToChoose[i]);
                m_Arrows.Display(i);
                yield return new WaitForSecondsRealtime(0.1f);
            }
            
            m_Player.EnableUpgradeInputs(true);
        }

        void HideCards(int selected)
        {
            for (int i = 0; i < m_NbCardsToDraw; i++)
            {
                if (i == selected)
                    m_CardsVisuals[i].ValidAnim();
                else
                    m_CardsVisuals[i].Hide();
            }
            
            m_Arrows.Hide(m_NbCardsToDraw, selected);
        }

        void OnCardChosen(int result)
        {
            switch (result)
            {
                case -1:
                    break;
                case >= 0 when result < cardsToChoose.Length:
                    print( "[" + result + "] CHOSEN : " + cardsToChoose[result].cardName);
                    m_Player.UpgradeStats(cardsToChoose[result]);
                    if (cardsToChoose[result].special.Length > 0)
                        RemoveCardFromDatabase(cardsToChoose[result]);
                    break;
                default:
                    return;
            }

            HideCards(result);
            cardsToChoose = null;
            StopState();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void StopState()
        {
            isStarted = false;

            cardsToChoose = null;
            m_Player.EnableUpgradeInputs(false);
            
            OnFinishState?.Invoke();
        }

        void RemoveCardFromDatabase(UpgradeCardData card)
        {
            // There's only common cards for now so it's simple
            m_CurrentUpgradeCards.commonCards.Remove(card);
        }
        
        void Update()
        {
            if (isStarted && m_Background.color.a < m_BackgourndAlpha)
                m_Background.color = new Color(m_Background.color.r, m_Background.color.g, m_Background.color.b, m_Background.color.a + Time.deltaTime * 5);
            else if (!isStarted && m_Background.color.a >= 0f)
                m_Background.color = new Color(m_Background.color.r, m_Background.color.g, m_Background.color.b, m_Background.color.a - Time.deltaTime * 10);
        }
    }
}
