using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitUpgrader unitUpgrader;
    public IUnitUpgrader iUnitUpgrader;

    public string unitName;
    public int attackDamage;
    public int attackSpeed;
    public float attackRange;
    public int lastUpgrade;
    public EUnitGrade eUnitGrade;

    public UnitAI unitAI;
    public UnitSkill unitSkill;
    public Animator anim;
    public BoxCollider box;

    public GameObject target;
    public List<Collider> targets = new List<Collider>();
    public float rotationSpeed;
    public Coroutine attackCoroutine;

    public virtual void Init(UnitData unitData)
    {
        unitAI = new UnitAI();
        unitAI.init(this);

        anim = this.GetComponent<Animator>();
        box = this.GetComponent<BoxCollider>();

        unitUpgrader = GameObject.Find("UnitUpgrader").GetComponent<UnitUpgrader>();
        iUnitUpgrader = unitUpgrader;

        unitName = unitData.unitName;
        attackDamage = unitData.unitDamage;
        attackSpeed = unitData.attackSpeed;
        attackRange = unitData.attackRange;
        eUnitGrade = unitData.unitGrade;

        switch (eUnitGrade)
        {
            case EUnitGrade.C
            : lastUpgrade = iUnitUpgrader.getUpgradeLevel()[1]; 
                break;
            case EUnitGrade.B: 
                lastUpgrade = iUnitUpgrader.getUpgradeLevel()[1]; 
                break;
            case EUnitGrade.S: 
                lastUpgrade = iUnitUpgrader.getUpgradeLevel()[2]; 
                break;
            case EUnitGrade.SS: 
                lastUpgrade = iUnitUpgrader.getUpgradeLevel()[3];
                break;
        }

        iUnitUpgrader.missUpgrade(lastUpgrade, this);
    }

    private void Update()
    {
        unitAI.State();
    }

    public GameObject targetEnemy()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackRange,
             LayerMask.GetMask("Enemy"));

        target = null;

        float minDistance = Mathf.Infinity;

        foreach (Collider coll in colls)
        {
            Enemy enemy = coll.GetComponent<Enemy>();

            if (enemy == null || enemy.isDie == true)
                continue;

            float distance = Vector3.Distance(transform.position, coll.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                target = coll.gameObject;
            }
        }

        return target;
    }

    public void attack()
    {
        if (target != null)
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack());
            }
        }
        else
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
            }
        }
    }

    IEnumerator Attack()
    {
        while (true) 
        {
            if (target == null)
            {
                yield break;
            }

            Enemy enemy = target.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.takeDamage(attackDamage);
            }

            yield return new WaitForSeconds(1 / attackSpeed);
        }
    }

    public void lookEnemy()
    {
        if (target == null) return;

        Vector3 dir = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void changeAnim(eUnitAI curState)
    {
        switch(curState) 
        {
            case eUnitAI.eAI_CREATE:
                anim.SetBool("isAttack", false);
                break;
            case eUnitAI.eAI_SEARCH:
                anim.SetBool("isAttack", false);
                break;
            case eUnitAI.eAI_ATTACK:
                anim.SetBool("isAttack", true);
                break;
            case eUnitAI.eAI_RESET:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}