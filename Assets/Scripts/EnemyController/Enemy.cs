using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType { Nomal, WaveBoss, boss };

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType;

    public RoundTimer roundTimer;
    public WaveBossSpawner waveBossSpawner;
    public EnemySpawner enemySpawner;
    public Rewarder rewarder;
    public GameStateCheck gameStateCheck;

    [Header("Enemy")]
    public EnemyData enemyData;
    public BoxCollider2D box;
    public Animator anim;
    public float enemyHp;
    public float curhp;
    public string enemyName;
    public float enemySpeed;
    public Transform[] wayPoint;
    public GameObject wayPointTrs;
    public Transform target;
    public int wayPointIndex = 0;
    public GameObject healthBarBack;
    public Image healthBarFill;
    public bool isDie;

    [Header("Boss")]
    public TextMeshProUGUI bossTimeText;
    public float bosstime;

    public float EnemyHp 
    { get { return enemyHp; } set { value = enemyHp; } }

    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        wayPointTrs = GameObject.Find("WayPoints");
    }

    private void Start()
    {
        enemyName = enemyData.enemyName;
        enemyHp = enemyData.enemyHp;
        enemySpeed = enemyData.enemySpeed;
        curhp = enemyHp;
        bosstime = 60f;

        healthBarFill.fillAmount = 1;

        wayPoint = new Transform[wayPointTrs.transform.childCount];
        for (int i = 0; i < wayPoint.Length; i++)
        {
            wayPoint[i] = wayPointTrs.transform.GetChild(i);
        }

        target = wayPoint[wayPointIndex];
    }

    private void Update()
    {
        if (isDie) return;
        move();
        hpBar();
        if (enemyType == EnemyType.WaveBoss)
        {
            waveBossTimer();
        }
        if (enemyType == EnemyType.boss)
        {
            bossTimer();
        }
    }

    private void move()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemySpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            nextMove();
        }
    }

    private void hpBar()
    {
        healthBarFill.fillAmount = curhp / enemyHp;
    }

    private void nextMove()
    {
        if (wayPointIndex >= wayPoint.Length -1)
        {
            wayPointIndex = 0;
        }
        else
        {
            wayPointIndex++;
        }

        target = wayPoint[wayPointIndex];
    }

    public void takeDamage(int damage)
    {
        curhp -= damage;

        if (curhp <= 0)
        {
            die();
        }
    }

    private void die()
    {
        switch (enemyType) 
        {
            case EnemyType.Nomal:
                GameManager.Instance.myGold += 1;
                break;
            case EnemyType.WaveBoss:
                GameManager.Instance.myCoin += 2;
                waveBossSpawner.wavebossDelay = 25f;
                break;
            case EnemyType.boss:
                GameManager.Instance.myGold += 300;
                GameManager.Instance.myCoin += 4;
                roundTimer.bossRound = false;
                roundTimer.sec = 15f;
                enemySpawner.enemySpawndelay = 0.85f;
                break;
        }

        anim.SetBool("isDie", true);
        isDie = true;
        Destroy(gameObject, 0.5f);
        rewarder.rewardGold += 50;
        rewarder.rewardGem += 10;
        rewarder.rewardPaper += 20;
        rewarder.rewardExp += 1;
    }

    private void waveBossTimer()
    {
        bosstime -= Time.deltaTime;
        bossTimeText.text = bosstime.ToString("F1")+"s";

        if (bosstime <= 0)
        {
            Destroy(this.gameObject);
            waveBossSpawner.wavebossDelay = 25;
        }
    }

    private void bossTimer()
    {
        bosstime -= Time.deltaTime;
        bossTimeText.text = bosstime.ToString("F1") + "s";

        if (bosstime <= 0)
        {
            gameStateCheck.gameOver = true;
        }
    }
}