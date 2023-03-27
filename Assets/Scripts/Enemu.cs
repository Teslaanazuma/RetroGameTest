using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemu : MonoBehaviour
{

    //скрипт для наземного врага

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    public LayerMask Wall;
  
    
    public float health;

    public float rotation;

    //Fiev

    public float viewRadius;
  
    [Range(0, 360)]
    public float viewAngle;

   
    

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();



  
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
   
    
    public GameObject BullEnemy;
    public Transform muzzle;


    //States
    
    public bool playerInRadiosRange, playerInAttackRange;
 
    public int whatIsBullet;
 


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }


   
    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);




    }



    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }


    }
   

    void FindVisibleTargets()//проверка видит ли враг игрока, или он за стенкой
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, whatIsPlayer);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
         
            
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                playerInRadiosRange = false;


                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, Wall))
                {
                
                    
                    visibleTargets.Add(target);
                    playerInRadiosRange = true;

                }
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }







    private void Update()
    {
       
      
        playerInAttackRange = Physics.CheckSphere(transform.position, viewRadius, whatIsPlayer);//Если игрок в зоне аттаки



        Rot();// враг всегда поварачивается к игроку

        

        if (!playerInAttackRange) 
        {
            ChasePlayer();//если игрок вне зоны атаки то враг идет за ним
           
        }
      
        if (playerInAttackRange) 
        {
           AttackPlayer();//если игрок в зоне врага то идет атака
            
        }
    }



    public void Rot()//поворт в сторону игрока
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion LookRotathion = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, LookRotathion, Time.deltaTime * rotation);
    }




    private void ChasePlayer()//Слежка за игроком
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        
        
        

        
        agent.SetDestination(transform.position);

        if (!playerInRadiosRange)//проверка видит ли враг игрока, или он за стенкой
        {
            agent.SetDestination(player.position);

        }

        FindVisibleTargets();
    
        
       

        if (!alreadyAttacked)
        {
            
             
              GameObject bulEnemy = Instantiate(BullEnemy, muzzle.position, muzzle.rotation);

          
      
             
             
             



            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
   

        }
    }

    private void ResetAttack()// перезарядка врага
    {
        alreadyAttacked = false;
    }



    private void OnCollisionEnter(Collision collision)//если врага попадает пуля, то он получает урон
    {

        if (collision.gameObject.layer == whatIsBullet)
        {

            TakeDamage();
        }



    }



    public void TakeDamage()//получение урона
    {
        health -= 50;

        if (health <= 0)
            DestroyEnemy();;
    }
    
    
    
    
    private void DestroyEnemy()//если враг умирает от пули
    {
        player.GetComponent<PlayerMovement>().PlusPower();
        player.GetComponent<PlayerMovement>().pointKill += 1;
        Destroy(gameObject);
    
    }

  
    
 





}
