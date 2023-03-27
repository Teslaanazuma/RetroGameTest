using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
   
    
    //скрипт проверяет 4 зоны на карте
    //Там где меньше всего врагов туда и телепортируется в случайное место
    
    
    
    
    
    
    public LayerMask Enemy;
    public LayerMask ocean;
    public bool needteleport;
    public Transform CheckGround;
    //Zons
    public Transform zonaOne;
    public Transform zonaTwo;
    public Transform zonaTree;
    public Transform zonaFour;


    public int[] Container;

    public int numOne;
    public int numTwo;
    public int numTree;
    public int numFour;





    public float radiosZons;



    public void Start()
    {
        needteleport = false;
        CheckGround = GetComponent<PlayerMovement>().gravityCheck;
       // TeleportObj();
    }









    public void Update()
    {
        needteleport = Physics.CheckSphere(CheckGround.position, 1, ocean);//если игрок за картой



        if (needteleport)
        {
            TeleportObj();//телепорт



        }

        //TeleportObj();




    }
    private void TeleportObj()
    {




        //создание 4 зон и проверка сколько там врагов
        Collider[] ColliderOne = Physics.OverlapSphere(zonaOne.position, radiosZons, Enemy);
        Collider[] ColliderTwo = Physics.OverlapSphere(zonaTwo.position, radiosZons, Enemy);
        Collider[] ColliderTree = Physics.OverlapSphere(zonaTree.position, radiosZons, Enemy);
        Collider[] ColliderFour = Physics.OverlapSphere(zonaFour.position, radiosZons, Enemy);

        //присваивание массиву сколько врагов в каждой зоне
        Container[0] = ColliderOne.Length;
        Container[1] = ColliderTwo.Length;
        Container[2] = ColliderTree.Length;
        Container[3] = ColliderFour.Length;

        //включается проверка для того чтоб узнать, где всего менешь врагов
        int minValue = Container[0];

        for(int i = 0; i < Container.Length; i++)
        {
            if (Container[i] < minValue)
            {
                minValue = Container[i];
            }


        }

        //рандоманая позиция
        int posRand = Random.Range(0,25);



        //после того как узнали где само меньше врагов телепортируется игрок но добавляется рандном к позиции

        if (minValue == ColliderOne.Length)
        {
            transform.position = new Vector3(zonaOne.position.x+ posRand, 2, zonaOne.position.z+ posRand);
        }
        if (minValue == ColliderTwo.Length)
        {
            transform.position = new Vector3(zonaTwo.position.x + posRand, 2, zonaTwo.position.z + posRand);
        }
        if (minValue == ColliderTree.Length)
        {
            transform.position = new Vector3(zonaTree.position.x + posRand, 2, zonaTree.position.z + posRand);
        }

        if (minValue == ColliderFour.Length)
        {
            transform.position = new Vector3(zonaFour.position.x + posRand, 2, zonaFour.position.z + posRand);
        }







        needteleport = false;




    }


    void OnDrawGizmosSelected()
    {
        //чисто для удобства 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(zonaOne.position, radiosZons);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(zonaTwo.position, radiosZons);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(zonaTree.position, radiosZons);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(zonaFour.position, radiosZons);


    }








}
