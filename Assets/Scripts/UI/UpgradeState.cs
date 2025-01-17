using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Player;
using Characters.Player.Upgrade;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class UpgradeState : MonoBehaviour
    {
        Player m_Player;
        
        public event Action OnFinishState;

        [SerializeField] UpgradeCardDatabase m_InitialUpgradeCards;
        UpgradeCardDatabase m_CurrentUpgradeCards;
        [SerializeField] Image bg;
        [SerializeField] float bgAlpha;
        [SerializeField] TextMeshProUGUI testTxt;
        [SerializeField] public int nbCardsToDraw;

        UpgradeCardData[] cardsToChoose;
        bool isStarted;
        bool canChooseCard;

        public void Init(Player player)
        {
            m_Player = player;

            m_CurrentUpgradeCards = Instantiate(m_InitialUpgradeCards);
        }
        
        public void StartState()
        {
            isStarted = true;
            
            m_Player.m_InvincibilityCooldown.Start();
            m_Player.DisableInput();
            
            DrawCards(nbCardsToDraw);
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
                    print("Card found, redraw...");
                    i--;
                    continue;
                }
                
                pickedCards.Add(pickedCard);
            }

            cardsToChoose = pickedCards.ToArray();
            DisplayCards();
        }

        void DisplayCards()
        {
            testTxt.text = cardsToChoose[0].description + " - " + cardsToChoose[1].description;
            canChooseCard = true;
        }

        void OnCardChosen(int result)
        {
            switch (result)
            {
                case -1:
                    print("SKIPPED");
                    break;
                case >= 0 when result < cardsToChoose.Length:
                    print("CHOSEN : " + cardsToChoose[result]);
                    break;
            }

            canChooseCard = false;
            StopState();
        }

        void StopState()
        {
            isStarted = false;

            cardsToChoose = null;
            testTxt.text = "";
            m_Player.EnableInput();
            
            OnFinishState?.Invoke();
        }
        
        void Update()
        {
            if (isStarted && Input.GetKeyDown(KeyCode.Space))
                StopState();
            
            if (isStarted && bg.color.a < bgAlpha)
                bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, bg.color.a + Time.deltaTime * 5);
            else if (!isStarted && bg.color.a >= 0f)
                bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, bg.color.a - Time.deltaTime * 10);
        }
    }
}
