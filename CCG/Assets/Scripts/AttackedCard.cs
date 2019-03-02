using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCard : MonoBehaviour, IDropHandler {

    public void OnDrop(PointerEventData eventData)
    {
        if (!GameManagerScr.Instance.IsPlayerTurn)
            return;

        CardController attacker = eventData.pointerDrag.GetComponent<CardController>(),
                       defender = GetComponent<CardController>();

        if (attacker &&
            attacker.Card.CanAttack &&
            defender.Card.IsPlaced)
        {
            if (GameManagerScr.Instance.EnemyFieldCards.Exists(x => x.Card.IsProvocation) &&
                !defender.Card.IsProvocation)
                return;

            GameManagerScr.Instance.CardsFight(attacker, defender);
        }
    }
}