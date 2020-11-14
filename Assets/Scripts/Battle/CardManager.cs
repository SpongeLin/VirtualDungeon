using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager instance { get; private set; }

    public List<CardData> handCards;
    public List<CardData> deck;
    public List<CardData> cemetery;
    public List<CardData> banish;

    public CardViewControl cardViewControl;

    public BattleCardDragControl battleCardDragControl;

    public CardView currentDragCard = null;


    public void Awake()
    {
        instance = this;
        handCards = new List<CardData>();
        deck = new List<CardData>();
        cemetery = new List<CardData>();
        banish = new List<CardData>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Update()
    {
        CheckMouseUp();
    }


    public void GameUpdate()
    {
        foreach (CardData card in handCards)
            card.canUse = CheckCardCanUse(card);
        cardViewControl.GameUpdate();
    }

    public void MouseDownCard(CardView cardVIew)
    {
        battleCardDragControl.MouseDown(cardVIew);
    }
    void CheckMouseUp()
    {
        if (currentDragCard != null)
            if (Input.GetMouseButtonUp(0))
            {
                battleCardDragControl.MouseUp();
            }
    }



    bool CheckCardCanUse(CardData card)
    {
        if (!FieldManager.instance.playerTurn)
            return false;
        if (!OrderManager.instance.IsEmptyStack())
            return false;
        if (FieldManager.instance.currentActionCharacter == null)
            return false;
        if (FieldManager.instance.currentActionCharacter.energy < card.cardCost)
            return false;
        if (card.banCount >= 1)
            return false;
        if (!CardTargetExist(card))
            return false;
        // check card target exist!
        return true;
    }
    bool CardTargetExist(CardData card)
    {
        if (FieldManager.instance.GetConditionChar(card.charFilters.ToArray()) == null)
            return false;
        return true;
    }


    public void UseCard(CardData card)
    {
        Debug.Log("Use Card!!!!!!!!");


        FieldManager.instance.currentActionCharacter.ReduceEnergy(card.cardCost);
        if (FieldManager.instance.currentActionCharacter.magicPoint >= card.oriMagicConsume)
        {
            FieldManager.instance.CharLoseMagic(FieldManager.instance.currentActionCharacter, card.oriMagicConsume);
            card.magicCheck = true;
        }
        if(FieldManager.instance.currentActionCharacter == card.linkChar && card.linkChar != null)
        {
            card.linkCheck = true;
        }

        if (card.exhasutCount >= 1) {
            CardInfo cardif = TriggerManager.instance.GetTriggerInfo<CardInfo>();
            cardif.SetInfo(card);
            cardif.GoTrigger(TriggerType.CardExhaust);
        }


        UseCardInfo cif = TriggerManager.instance.GetTriggerInfo<UseCardInfo>();
        cif.SetInfo(card);

        cif.GoTrigger(TriggerType.UseCardCheck);

        cif.GoTrigger(TriggerType.UseCardAfter);
        OrderManager.instance.AddOrder(new sysOrder.UseCardOrder(card));
        cif.GoTrigger(TriggerType.UseCardBefore);

        //OrderManager.instance.AddOrder(new sysOrder.DiscardOrder(card));
        if (card.exhasutCount >= 1)
            DiscardHandCard(card, true);
        else
            DiscardHandCard(card);
    }
    public void RealUseCard(CardData card)
    {
        for(int i = card.cardEffects.Count - 1; i >= 0; i--)
        {
            if (card.cardEffects[i].CanUseCardEffect())
                card.cardEffects[i].UseCardEffect();
        }
        card.Clear();
    }



    public void Draw()
    {
        if (deck.Count != 0)
        {
            CardData drawCard = deck[deck.Count-1];
            deck.Remove(drawCard);
            EnterHand(drawCard);

            CardInfo cif = TriggerManager.instance.GetTriggerInfo<CardInfo>();
            cif.SetInfo(drawCard, CardPos.Deck, CardPos.Hand);
            cif.GoTrigger(TriggerType.CardMove); 
            cif.GoTrigger(TriggerType.Draw);

        }
        else
        {
            if (cemetery.Count != 0)
            {
                ReShuffle();
                Draw();
            }
            else
            {
                Debug.Log("there is not any card...");
            }
        }
        //        cardViewControl.HandCardPosUpdate();
    }
    public void DiscardHandCard(CardData card,bool exhaust=false)
    {
        if(GetCardPosition(card) == CardPos.Hand)
        {
            ExitHand(card);
            if (exhaust)
            {
                banish.Add(card);

                CardInfo cif = TriggerManager.instance.GetTriggerInfo<CardInfo>();
                cif.SetInfo(card, CardPos.Hand, CardPos.Banish);
                cif.GoTrigger(TriggerType.CardMove);
            }
            else
            {
                cemetery.Add(card);

                CardInfo cif = TriggerManager.instance.GetTriggerInfo<CardInfo>();
                cif.SetInfo(card, CardPos.Hand, CardPos.Cemetery);
                cif.GoTrigger(TriggerType.CardMove);
            }
        }
        else
        {
            Debug.LogWarning("The card not in hand will be discard form hand");
        }
    }

    void EnterHand(CardData card,CardPos from = CardPos.Deck)
    {

        handCards.Add(card);
        cardViewControl.OpenCard(card);

        //cardViewControl.HandCardPosUpdate();
    }
    void ExitHand(CardData card)
    {
        handCards.Remove(card);
        cardViewControl.CloseCard(card);

        cardViewControl.HandCardPosUpdate();
    }


    void ReShuffle()
    {
        //pop a wait order and Shuffle animation
        //and pop a draw order again

        foreach (CardData cd in cemetery)
            deck.Add(cd);
        cemetery.Clear();
        Shuffle();
    }
    public void Shuffle()
    {
        if (deck.Count == 0 || deck.Count==1) return;
        int num = deck.Count;
        for (int i = 0; i < 35; i++)
        {
            int r1 = 0;
            int r2 = 0;
            while (r1 == r2)
            {
                r1 = Random.Range(0, num);
                r2 = Random.Range(0, num);
            }
            CardData temp = deck[r1];
            deck[r1] = deck[r2];
            deck[r2] = temp;
        }
    }

    public void CardBurst(CardData card)
    {
        card.Burst();

        CardInfo cif = TriggerManager.instance.GetTriggerInfo<CardInfo>();
        cif.SetInfo(card, 1);
        cif.GoTrigger(TriggerType.CardBurst);
    }



    public CardPos GetCardPosition(CardData card)
    {
        if (handCards.Contains(card))
            return CardPos.Hand;
        if (deck.Contains(card))
            return CardPos.Deck;
        if (cemetery.Contains(card))
            return CardPos.Cemetery;
        if (banish.Contains(card))
            return CardPos.Banish;


        return CardPos.Null;
    }
    CardPos RemoveOriPos(CardData card)
    {
        CardPos pos = GetCardPosition(card);
        if (pos == CardPos.Hand)
            ExitHand(card);
        else if (pos == CardPos.Deck)
            deck.Remove(card);
        else if (pos == CardPos.Cemetery)
            cemetery.Remove(card);
        else if (pos == CardPos.Banish)
            banish.Remove(card);


        return pos;
    }
    public void CardMove(CardData card,CardPos to,int condition=0)
    {
        CardPos from = GetCardPosition(card);
        if (from == to) return;
        RemoveOriPos(card);

        if (to == CardPos.Hand)
            EnterHand(card);
        else if (to == CardPos.Deck)
        {
            deck.Add(card);
            if (condition == 0) { Shuffle(); }
        }
        else if (to == CardPos.Cemetery)
            cemetery.Add(card);
        else if (to == CardPos.Banish)
            banish.Add(card);


        CardInfo cif = TriggerManager.instance.GetTriggerInfo<CardInfo>();
        cif.SetInfo(card, from,to);
        cif.GoTrigger(TriggerType.CardMove);
        if (from == CardPos.Deck && to == CardPos.Hand)
            cif.GoTrigger(TriggerType.Draw);
    }

}
