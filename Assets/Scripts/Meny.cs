using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class Meny : MonoBehaviour
{
    public GameObject Resume;
    public GameObject panelmeny;
    bool OnOffPanel;


    private void Start()
    {
        panelmeny.SetActive(false);
    }





    public void RestrartGame()//метод для перезагрузки сцены
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
      
    }




    public void GameOver()//если конец игры
    {

        Panel();
        Resume.SetActive(false);
    }



    public void Panel()//включение паузы во время открытия меню
    {

        if (OnOffPanel == false)
        {
            Time.timeScale = 0f;
        }
        if (OnOffPanel == true)
        {
            Time.timeScale = 1.0f;
        }

        OnOffPanel = !OnOffPanel;
        panelmeny.SetActive(OnOffPanel);




    }












}
