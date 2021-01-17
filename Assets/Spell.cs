using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spell")]
public class Spell : ScriptableObject
{
    [Header("About this spell")]
    public string spellName = "New Spell";
    public string spellDescription = "Spell Description";
    public Sprite spellSprite;
    public enum Element { Fire, Water, Earth, Air, Arcane, Light };
    public Element element;

    [Header("Spell Stats")]
    public float currentCooldown = 0f;
    public float maxCooldown;
    public GameObject spellEffect;
    public bool isOnPointer;
    public float spellDamageRadius;
    public float spellCastDistanceRadius;
    public float movementLockDuration;

    public void PutOnCooldown()
    {
        CooldownManager.instance.StartCooldown(this);
    }

    public bool IsSpellReady()
    {
        if (currentCooldown <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void SetCurrentCooldown()
    {
        currentCooldown = 0f;
    }

    public GameObject GetSpellEffect()
    {
        return spellEffect;
    }

    public bool IsPointerPosition()
    {
        return isOnPointer;
    }
}
