using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnEnemy : MonoBehaviour
{
    //префабы и сколько нужно врагов на сцене

    public GameObject prefabBoss;
    public int MuchBoss;
    public GameObject prefabFly;
    public int MuchFly;

    
    // радиус и появлиение монстров

    public float radios;
    public float TimeBetweenWaves;
    bool alreadyWave;

    
  



    public void Update()
    {
        Waves();
        
    }

    private void Waves()//метод для волн
    {
        
        
        
        if (!alreadyWave)
        {
              


            for(int i = 0;i< MuchBoss; i++)
            {
                float transrandomx = Random.Range(-radios, radios);
                float transrandomy = Random.Range(-radios, radios);
                Vector3 posBoss =  new Vector3(transrandomx,transform.position.y,transrandomy);
                
                GameObject Boss = Instantiate(prefabBoss);
                Boss.transform.position = posBoss;

            }
          
            
            for (int i = 0; i < MuchFly; i++)
            {
                float transrandomx = Random.Range(-radios, radios);
                float transrandomy = Random.Range(-radios, radios);
               
                Vector3 posFly = new Vector3(transrandomx, 3f, transrandomy);

                GameObject Fly = Instantiate(prefabFly);
                Fly.transform.position = posFly;

            }


            Invoke(nameof(ResetWave), TimeBetweenWaves);

            alreadyWave = true;
        }



    }
    private void ResetWave()//проверка если время между волнами уже ниже 2 то включается уже возрастание количество монстров
    {
        if (TimeBetweenWaves > 2.2f)
        {
            TimeBetweenWaves -= 0.1f;
            
        }
        if(TimeBetweenWaves <= 2.2f)
        {
            MuchFly += 4;
            MuchBoss += 1;
        }
        


        alreadyWave = false;


    }

    











}
