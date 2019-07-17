using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card
{
    public enum AbilityType
    {
        NO_ABILITY,
        INSTANT_ACTIVE,
        DOUBLE_ATTACK,
        SHIELD,
        PROVOCATION,
        REGENERATION_EACH_TURN,
        COUNTER_ATTACK
    }

    public string Name;
    public Sprite Logo;
    public int Attack, Defense, Manacost;
    public bool CanAttack;
    public bool IsPlaced;

    public List<AbilityType> Abilities;

    public bool IsSpell;

    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
    }
    public bool HasAbility
    {
        get
        {
            return Abilities.Count > 0;
        }
    }
    public bool IsProvocation
    {
        get
        {
            return Abilities.Exists(x => x == AbilityType.PROVOCATION);
        }
    }

    public int TimesDealedDamage;

    public Card(string name, string logoPath, int attack, int defense, int manacost, AbilityType abilityType = 0)
    {
        Name = name;
        Logo = Resources.Load<Sprite>(logoPath);
        Attack = attack;
        Defense = defense;
        Manacost = manacost;
        CanAttack = false;
        IsPlaced = false;

        Abilities = new List<AbilityType>();

        if (abilityType != 0)
            Abilities.Add(abilityType);

        TimesDealedDamage = 0;
    }

    public Card(Card card)
    {
        Name = card.Name;
        Logo = card.Logo;
        Attack = card.Attack;
        Defense = card.Defense;
        Manacost = card.Manacost;
        CanAttack = false;
        IsPlaced = false;

        Abilities = new List<AbilityType>(card.Abilities);

        TimesDealedDamage = 0;
    }

    public void GetDamage(int dmg)
    {
        if (dmg > 0)
        {
            if (Abilities.Exists(x => x == AbilityType.SHIELD))
                Abilities.Remove(AbilityType.SHIELD);
            else
                Defense -= dmg;
        }
    }

    public Card GetCopy()
    {
        return new Card(this);
    }
}

public class SpellCard : Card
{
    public enum SpellType
    {
        NO_SPELL,
        HEAL_ALLY_FIELD_CARDS,
        DAMAGE_ENEMY_FIELD_CARDS,
        HEAL_ALLY_HERO,
        DAMAGE_ENEMY_HERO,
        HEAL_ALLY_CARD,
        DAMAGE_ENEMY_CARD,
        SHIELD_ON_ALLY_CARD,
        PROVOCATION_ON_ALLY_CARD,
        BUFF_CARD_DAMAGE,
        DEBUFF_CARD_DAMAGE
    }

    public enum TargetType
    {
        NO_TARGET,
        ALLY_CARD_TARGET,
        ENEMY_CARD_TARGET
    }

    public SpellType Spell;
    public TargetType SpellTarget;
    public int SpellValue;

    public SpellCard(string name, string logoPath, int manacost, SpellType spellType = 0,
                     int spellValue = 0, TargetType targetType = 0) : base(name, logoPath, 0, 0, manacost)
    {
        IsSpell = true;

        Spell = spellType;
        SpellTarget = targetType;
        SpellValue = spellValue;
    }

    public SpellCard(SpellCard card) : base (card)
    {
        IsSpell = true;

        Spell = card.Spell;
        SpellTarget = card.SpellTarget;
        SpellValue = card.SpellValue;
    }

    public new SpellCard GetCopy()
    {
        return new SpellCard(this);
    }
}

public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}

public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        CardManager.AllCards.Add(new Card("ebalo", "Sprites/Cards/ebalo", 5, 5, 6));
        CardManager.AllCards.Add(new Card("buldiga", "Sprites/Cards/buldiga", 4, 3, 5));
        CardManager.AllCards.Add(new Card("hmm", "Sprites/Cards/hmm", 3, 3, 4));
        CardManager.AllCards.Add(new Card("micro", "Sprites/Cards/micro", 2, 1, 2));
        CardManager.AllCards.Add(new Card("pominki", "Sprites/Cards/pominki", 8, 1, 7));
        CardManager.AllCards.Add(new Card("pomoika", "Sprites/Cards/pomoika", 1, 1, 1));

        CardManager.AllCards.Add(new Card("provocation", "Sprites/Cards/provocation", 1, 2, 3, Card.AbilityType.PROVOCATION));
        CardManager.AllCards.Add(new Card("regeneration", "Sprites/Cards/regen", 4, 2, 5, Card.AbilityType.REGENERATION_EACH_TURN));
        CardManager.AllCards.Add(new Card("doubleAttack", "Sprites/Cards/doubleAttack", 3, 2, 4, Card.AbilityType.DOUBLE_ATTACK));
        CardManager.AllCards.Add(new Card("instantActive", "Sprites/Cards/instantActive", 2, 1, 2, Card.AbilityType.INSTANT_ACTIVE));
        CardManager.AllCards.Add(new Card("shield", "Sprites/Cards/shield", 5, 1, 7, Card.AbilityType.SHIELD));
        CardManager.AllCards.Add(new Card("counterAttack", "Sprites/Cards/counterAttack", 3, 1, 1, Card.AbilityType.COUNTER_ATTACK));

        CardManager.AllCards.Add(new SpellCard("HEAL_ALLY_FIELD_CARDS", "Sprites/Cards/healAllyCards", 2,
            SpellCard.SpellType.HEAL_ALLY_FIELD_CARDS, 2, SpellCard.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new SpellCard("DAMAGE_ENEMY_FIELD_CARDS", "Sprites/Cards/damageEnemyCards", 2,
            SpellCard.SpellType.DAMAGE_ENEMY_FIELD_CARDS, 2, SpellCard.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new SpellCard("HEAL_ALLY_HERO", "Sprites/Cards/healAllyHero", 2,
            SpellCard.SpellType.HEAL_ALLY_HERO, 2, SpellCard.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new SpellCard("DAMAGE_ENEMY_HERO", "Sprites/Cards/damageEnemyHero", 2,
            SpellCard.SpellType.DAMAGE_ENEMY_HERO, 2, SpellCard.TargetType.NO_TARGET));
        CardManager.AllCards.Add(new SpellCard("HEAL_ALLY_CARD", "Sprites/Cards/healAllyCard", 2,
            SpellCard.SpellType.HEAL_ALLY_CARD, 2, SpellCard.TargetType.ALLY_CARD_TARGET));
        CardManager.AllCards.Add(new SpellCard("DAMAGE_ENEMY_CARD", "Sprites/Cards/damageEnemyCard", 2,
            SpellCard.SpellType.DAMAGE_ENEMY_CARD, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));
        CardManager.AllCards.Add(new SpellCard("SHIELD_ON_ALLY_CARD", "Sprites/Cards/shieldOnAllyCard", 2,
            SpellCard.SpellType.SHIELD_ON_ALLY_CARD, 0, SpellCard.TargetType.ALLY_CARD_TARGET));
        CardManager.AllCards.Add(new SpellCard("PROVOCATION_ON_ALLY_CARD", "Sprites/Cards/provocationOnAllyCard", 2,
            SpellCard.SpellType.PROVOCATION_ON_ALLY_CARD, 0, SpellCard.TargetType.ALLY_CARD_TARGET));
        CardManager.AllCards.Add(new SpellCard("BUFF_CARD_DAMAGE", "Sprites/Cards/buffCardDamage", 2,
            SpellCard.SpellType.BUFF_CARD_DAMAGE, 2, SpellCard.TargetType.ALLY_CARD_TARGET));
        CardManager.AllCards.Add(new SpellCard("DEBUFF_CARD_DAMAGE", "Sprites/Cards/debuffCardDamage", 2,
            SpellCard.SpellType.DEBUFF_CARD_DAMAGE, 2, SpellCard.TargetType.ENEMY_CARD_TARGET));
    }
}