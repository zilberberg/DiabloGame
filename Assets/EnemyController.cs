using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float attackRadius = 2f;
    public float attackCooldown = 1f;
    public float attackCounter = 1f;

    private Animator anim;

    [SerializeField]
    private float dmg = 150f;
    [SerializeField]
    private float life = 100f;

    [SerializeField]
    private GameObject lootWrapper;

    private bool isDead = false;
    Transform target;
    NavMeshAgent agent;
    private int maxLootCount = 5;
    private int minLootCount = 3;
    private float minDropRange = 0f;
    private float maxDropRange = 3f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerMgr.instance.gameObject.transform;
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0 && !isDead)
        {
            isDead = true;
            GetComponent<Rigidbody>().isKinematic = true;
            agent.isStopped = true;
            agent.radius = 0;
            agent.SetDestination(gameObject.transform.position);
            anim.SetBool("die", true);
            StartCoroutine("InitDeath", 4f);
            InitLoot();
            return;
        }

        if (life <= 0)
        {
            return;
        }

        float distance = Vector3.Distance(target.position, transform.position);
        
        if (distance <= lookRadius && !agent.isStopped)
        {
            anim.ResetTrigger("attack");
            agent.SetDestination(target.position);
        }

        attackCounter += Time.deltaTime;
        if (distance <= attackRadius)
        {
            // attack player
            if (attackCounter >= attackCooldown && !agent.isStopped)
            {
                AttackPlayer();
            }
        }
    }

    private void InitLoot()
    {
        int lootCount = UnityEngine.Random.Range(minLootCount, maxLootCount);
        for (int i = 0; i < lootCount; i++)
        {
            float randomPositionX = UnityEngine.Random.Range(-maxDropRange, maxDropRange);
            float randomPositionZ = UnityEngine.Random.Range(-maxDropRange, maxDropRange);

            Item currentItem = LootController.instance.GetRandomItem();
            Vector3 itemPosition = new Vector3(transform.position.x + randomPositionX, 0.15f, transform.position.z + randomPositionZ);
            GameObject newLootObject = Instantiate(currentItem.GetLootObject(), itemPosition, Quaternion.identity);

            Vector3 wrapperPosition = new Vector3(transform.position.x + randomPositionX, 0f, transform.position.z + randomPositionZ);
            GameObject lootWrapperObject = Instantiate(lootWrapper, wrapperPosition, Quaternion.identity);
            lootWrapperObject.GetComponent<LootItemController>().SetLoot(currentItem);

            newLootObject.transform.SetParent(lootWrapperObject.transform);
        }
        
    }

    private void AttackPlayer()
    {
        attackCounter = 0;
        
        agent.isStopped = true;
        agent.SetDestination(transform.position);
        anim.SetTrigger("attack");
        StartCoroutine("LockMovement", 2f);
        PlayerMgr.instance.SetHit(dmg);
    }

    private IEnumerator LockMovement(float duration)
    {
        yield return new WaitForSeconds(duration);
        agent.isStopped = false;
    }

    private IEnumerator InitDeath(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void SetDamageTaken(float damage)
    {
        life -= damage;
    }
}
