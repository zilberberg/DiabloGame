using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellManager : MonoBehaviour
{
    [SerializeField]
    private Spell spell;

    [SerializeField]
    private GameObject imageObj;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = gameObject.GetComponent<Button>();
        imageObj.GetComponent<Image>().sprite = spell.spellSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (!spell.IsSpellReady())
        {
            button.interactable = false;
        } else
        {
            button.interactable = true;
        }
    }

    public Spell GetSpell()
    {
        return spell;
    }
}
