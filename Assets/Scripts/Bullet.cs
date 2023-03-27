using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Скрипт для пули

    [Header("Назваание тэга врага")]
    public string vrag; //тэг врага
    public Transform player;//положение игрока

    [Header("Враги")]
    public Transform nearObj;//Ближайший враг
    public GameObject[] enemy;//массив для врагов
    GameObject closset;//Ближайший враг(найденный)
    [Header("Щанс")]
    public float ranResht;//(щанс рандома)
   



    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        enemy = GameObject.FindGameObjectsWithTag(vrag);
    }

  
    
     GameObject FindCloses()//Поиск ближайшего врага
    {
        float distance = Mathf.Infinity;
        
        Vector3 position = transform.position;
        
        foreach (GameObject go in enemy)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closset = go;
                distance = curDistance;
            }
        }
        return closset;

    }

    

    private void OnCollisionEnter(Collision collision)
    {


        
        if (collision.gameObject.layer == 9)//(прикосновение с врагом)
        {

            player.GetComponent<PlayerMovement>().CheckHitOn();
            Rechashet();
            if (ranResht >= 90)
            {
                NearEnemy();
                if (ranResht >= 95)
                {
                    player.GetComponent<PlayerMovement>().DocMega();
                }
                if (ranResht >= 100)
                {
                    player.GetComponent<PlayerMovement>().PlusPower();
                }
                
            }
            if (ranResht <= 50)
            {

                Destroy(gameObject);

            }else if (ranResht <= 30)
            {


               


            }

            }
        if (collision.gameObject.layer == 10)// для оптимизации можно добавить чтоб обьект исчезал при попадании в стену
        {

            


              //  Destroy(gameObject);
          
       
        
        
        
        }
    
    
    
    }

    public void NearEnemy()//метод для рекошета
    {
       
 
       if(enemy.Length == 0)
        {
           
            Destroy(gameObject);


        }
        if (enemy.Length >= 1)
        {
            if (enemy[0]!=null)             
            {
                transform.LookAt(enemy[0].transform.position);
                transform.position = enemy[0].transform.position;


            }
            else
            {
                Destroy(gameObject);
            }

          




        }



    }

    









        public void Rechashet()//(выявления щанса рекошета)
    {


        ranResht = Random.Range(0, 100);
     
        if (player.GetComponent<PlayerMovement>().Hp <= 50)
        {
            ranResht += 20;
            if (player.GetComponent<PlayerMovement>().Hp <= 40)
            {
                ranResht += 30;
                if (player.GetComponent<PlayerMovement>().Hp <= 30)
                {
                    ranResht += 90;

                }
            }
        }


    }











}
