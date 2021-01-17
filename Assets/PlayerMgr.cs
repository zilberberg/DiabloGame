using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMgr : MonoBehaviour
{
    public LayerMask clickableSurface;

    private NavMeshAgent myAgent;

    private Animator anim;

    private float moveUpdateFactor = 0.2f;
    private float moveUpdateCounter = 0.2f;

    public GameObject healthBar;
    private Image healthImage;
    public GameObject manaBar;
    private Image manaImage;

    [SerializeField]
    private float maxHealth = 3000f;

    #region Singleton

    public static PlayerMgr instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        healthImage = healthBar.GetComponent<Image>();
        manaImage = manaBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !myAgent.isStopped)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, clickableSurface))
            {
                anim.ResetTrigger("attack");
                myAgent.SetDestination(hitInfo.point);
            }
        }        
    }

    public void SetHit(float dmg)
    {
        float dmgTaken = (dmg / maxHealth);
        healthImage.fillAmount -= dmgTaken;
    }
}
