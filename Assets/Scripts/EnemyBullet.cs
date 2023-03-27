using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

  //скрипт поли врага
  
    
    public Transform player;
    public float speedfly;
   

    


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;//узнаем кто игрок
        
    }

    public void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,player.position,speedfly*Time.deltaTime);//летит к игроку
    


    }


    private void OnCollisionEnter(Collision collision)//если врезается в обьекты то исчезает, если в игрока то отнимает жизни
    {

        if (collision.gameObject.layer == 10)
        {

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 7)
        {

            player.GetComponent<PlayerMovement>().MinusPower();


            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 8)
        {

            Destroy(gameObject);
        }

    }





}
