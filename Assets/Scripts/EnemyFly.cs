using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;

public class EnemyFly : MonoBehaviour
{
    
    //скрипт для летающего врага
    
    public Transform player;

    public float Hp;

    public float needUp;
    public float SpeedMove;

    public bool phase;

    public  Vector3 vectorMove;

    public float timePause;


    public void Start()
    {

        phase = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Hp = 50;


    }

    public void Update()
    {

        MOving();//передвижение врага

    }
    public void MOving()//передвижение врага

    {
        if (!phase)//если фаза к игроку еще не готова то враг взлетает
        {
            vectorMove = new Vector3(transform.position.x, needUp, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, vectorMove, SpeedMove * Time.deltaTime);
            transform.LookAt(player.position);

          

        }


        if (transform.position.y >= needUp)//включение фазы к игроку
        {
            
            Invoke(nameof(resetMove), timePause);
        }

        if (phase)//если фаза готова то враг летит в игрока
        {





            SpeedMove = 7f;
            transform.position = Vector3.MoveTowards(transform.position, player.position, SpeedMove * Time.deltaTime);
            transform.LookAt(player.position);

        }

    }


    public void resetMove()//включение фазы к игроку
    {
        phase = true;
     
    }
    private void OnCollisionEnter(Collision collision)//если враг попадает в игрока то наносит урон, если в пулю то получает урон, если в стену то снова взлетает
    {

        if (collision.gameObject.layer == 8)
        {
            phase=false;
            Hp -= 25;
            Destroit();
           
        }
        if (collision.gameObject.layer == 7)
        {

            player.GetComponent<PlayerMovement>().TakeDamage();

            
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 10)
        {

            phase=false;
            timePause = 0; 




        }


        }

    public void Destroit() //уничножение
    {
        if (Hp <= 0)
        {
            player.GetComponent<PlayerMovement>().pointKill += 1;
            Destroy(gameObject);
        }
    }




}
