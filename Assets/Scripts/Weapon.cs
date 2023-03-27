using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    //скрипт для оружия, самый не сложный
   
    public float range;
    public float lifeBull;

   
    
    
    public GameObject Bullet;
    public Transform muzzle;




    public void Shoot()//метод для стрельбы
    {
        
        GameObject bullet = Instantiate(Bullet, muzzle.position, muzzle.rotation);
      
        bullet.GetComponent<Rigidbody>().velocity = muzzle.forward * range;
      
        
        Destroy(bullet, lifeBull);
     

    }


  













}
