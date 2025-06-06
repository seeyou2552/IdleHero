using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterManager : MonoBehaviour
{
    public CharacterStat minion;
    public Collider col;

    [Header("적-아군 구분 값")]
    public bool thisEnemy = false;

    [Header("적 탐색")]
    public bool findingEnemy = true; // 적 탐색 중인지
    public Transform enemyPosition;
    public Rigidbody rb;
    public float moveSpeed;
    public float originForward;
    public bool test = false;

    [Header("공격")]
    public CharacterStat enemyStat;
    public CharacterManager enemy;
    public bool attacking = false;
    public float attackDelay = 1f;
    public bool dead = false;

    void Awake()
    {
        originForward = transform.eulerAngles.y;
        minion = GetComponent<CharacterStat>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if (!attacking && !dead) MoveToEnemy(); // 공격 중이 아닐 때 이동

        if (enemyStat != null && enemyStat.character.health <= 0) // 타켓 죽었을 시 초기화
        {
            TargetClear();
        }

        if (attacking) attackDelay += Time.deltaTime;

        if (minion.character.type == AttackType.Range && !findingEnemy) // 원거리 공격
        {
            Attack();
        }
        else if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1.5f) && attackDelay >= 1f) // 근거리 공격
        {
            if (thisEnemy ? hit.collider.CompareTag("Player") : hit.collider.CompareTag("Enemy"))
            {
                Attack();
            }
            Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.red);
        }


    }

    void OnTriggerEnter(Collider other) // 적 탐색
    {
        if (enemy == null && (thisEnemy ? other.CompareTag("Player") : other.CompareTag("Enemy")))
        {
            Debug.Log("닿음");
            enemyPosition = other.transform;
            enemyStat = other.transform.root.GetComponent<CharacterStat>();
            enemy = other.transform.root.GetComponent<CharacterManager>();
            findingEnemy = false;
            col.enabled = false;
        }
    }



    void MoveToEnemy()
    {
        if (!enemyPosition)
        {

        }
        else
        {
            transform.LookAt(enemyPosition.position);
        }
        transform.position += transform.forward * moveSpeed * Time.deltaTime; ;
    }

    void Attack()
    {
        if (minion.character.type == AttackType.Range)
        {

        }
        else if (minion.character.type == AttackType.Melee)
        {
            Debug.Log("공격");
            enemy.Damaged(minion.character.pow);
        }
        attacking = true;
        attackDelay = 0f;

    }

    void Damaged(int pow)
    {
        minion.character.health -= pow;
        if (minion.character.health <= 0) DeadCharacter();
        minion.status.UpdateHPBar();
        Debug.Log(minion.character.health);
    }

    void TargetClear() // 타겟 초기화
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, originForward, transform.eulerAngles.z);
        enemyPosition = null;
        enemyStat = null;
        enemy = null;
        attacking = false;
        findingEnemy = true;
        col.enabled = true;
        attackDelay = 1f;
    }


    void DeadCharacter()
    {
        dead = true;
        float force = 20f;
        rb.AddForce(-transform.forward * force, ForceMode.Impulse);
    }


}
