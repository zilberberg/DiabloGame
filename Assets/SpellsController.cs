using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpellsController : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent myAgent;
    public LayerMask clickableSurface;

    [SerializeField]
    private List<GameObject> spellsObject = new List<GameObject>();

    private float skillTimer = 0f;

    private bool isSkillTriggered = false;

    public float rotationSpeed = 20f;
    private Vector3 newDirection;
    private bool isManualRotation = false;
    private Vector3 pointerPoint;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        myAgent = GetComponent<NavMeshAgent>();
        ResetSkills();
    }

    private void ResetSkills()
    {
        for (int i = 0; i < spellsObject.Count; i++)
        {
            spellsObject[i].GetComponent<SpellManager>().GetSpell().SetCurrentCooldown();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isManualRotation)
        {
            Vector3 targetDirection = pointerPoint - transform.position;
            float singleStep = rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            if (Vector3.Angle(newDirection, targetDirection) < 1)
            {
                isManualRotation = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // try activate first skill
            ActivateSkill(spellsObject[0]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // try activate first skill
            ActivateSkill(spellsObject[1]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // try activate first skill
            ActivateSkill(spellsObject[2]);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // try activate first skill
            ActivateSkill(spellsObject[3]);
        }
    }

    public void ActivateSkill(GameObject spellObject)
    {
        if (!spellObject.GetComponent<SpellManager>().GetSpell().IsSpellReady())
        {
            return;
        }

        Spell spell = spellObject.GetComponent<SpellManager>().GetSpell();

        StartCoroutine("LockMovement", spell.movementLockDuration);
        spell.PutOnCooldown();
        anim.SetTrigger("attack");

        if (spell.GetSpellEffect())
        {
            if (spell.IsPointerPosition())
            {
                Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(myRay, out hitInfo, 100, clickableSurface))
                {
                    Vector3 targetPosition = hitInfo.point;
                    if (spell.spellCastDistanceRadius < Vector3.Distance(hitInfo.point, transform.position))
                    {
                        targetPosition = transform.position + (hitInfo.point - transform.position).normalized * spell.spellCastDistanceRadius;
                    }

                    GameObject spellEffectObject = Instantiate(spell.GetSpellEffect(), targetPosition, Quaternion.identity);
                    spellEffectObject.GetComponent<EffectDamageController>().SetEffectDamage(50);
                    pointerPoint = hitInfo.point;
                }
            }
        }
        isManualRotation = true;
    }

    private IEnumerator LockMovement(float duration)
    {
        myAgent.isStopped = true;
        myAgent.SetDestination(transform.position);
        yield return new WaitForSeconds(duration);
        myAgent.isStopped = false;
    }
}
