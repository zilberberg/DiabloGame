using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyManager : MonoBehaviour
{
    [SerializeField]
    private float spellDuration;

    private Animator spellAnimator;
    // Start is called before the first frame update
    void Start()
    {
        spellAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (spellAnimator)
        {
            if (spellAnimator.GetCurrentAnimatorClipInfo(0).Length > 0)
            {
                if (spellAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Destroy"))
                {
                    Destroy(gameObject);
                }
            }            
        } else
        {
            spellDuration -= Time.deltaTime;
            if (spellDuration <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
