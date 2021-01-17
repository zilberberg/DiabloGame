using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDamageController : MonoBehaviour
{
    private float EffectDamage;
    private bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEffectDamage(float damage)
    {
        EffectDamage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            if (!isHit)
            {
                collision.collider.gameObject.GetComponent<EnemyController>().SetDamageTaken(EffectDamage);
                isHit = true;
            }
        }
    }
}
