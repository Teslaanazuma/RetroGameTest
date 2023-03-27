using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
//скрипт для передвижение игрока

#if UNITY_ANDROID
    public FixedJoystick mJoystick;

#endif
    [HideInInspector]
    public CharacterController mCharacterController;
  
    [Space]
    public bool GamemodePC;// если играть с пк то включать этот режим для пережвижение клавишами

    [Space]
    [Space]
    public float walkSpeed;
    public float mRunSpeed;
    public float mRotationSpeed;

    [Space]
    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);


    [Space]
    [Tooltip("Gravity")]
   
    public float mGravity = -30.0f;
    public Transform gravityCheck;
    public bool Isground;
    public LayerMask Ground;

    public float GroundDistance;
    [Space]
    [Tooltip("Жизни")]
    public float MaxHp;
    public float Hp;
    public Image HpImage;
    [Space]
    [Tooltip("Атака персонажа")]
    public float MaxPower;
    public float Power;
    public Image powerImage;
    public float radiosPower;
    public LayerMask enemy;
    public GameObject CheckHit;
    

    [Space]
    [Tooltip("счетчик убийств")]
    public Text pointKillt;
    public Text pointKilltMenu;
    public int pointKill;
    public GameObject Meny;


    void Start()
    {
        CheckHit.SetActive(false);
         mCharacterController = GetComponent<CharacterController>();
        Power = 50f;
        Hp = MaxHp;
    }

  


    
    void Update()//статы и передвижение
    {
        
        powerImage.fillAmount = Power / MaxPower; 
        HpImage.fillAmount = Hp /MaxHp;
        pointKillt.text = pointKill.ToString();
        pointKilltMenu.text = pointKill.ToString();
        Move();

      


    }
   




    public void Move()//передвижение при помощи джойстика но не разворот, камера управляет разворотом персонажа
    {


#if UNITY_ANDROID
        float h = mJoystick.Horizontal;
        float v = mJoystick.Vertical;
#endif



        float speed = walkSpeed;


        if (GamemodePC)
        {
             h = Input.GetAxis("Horizontal");
             v = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = mRunSpeed;
            }



        }
    
     

        Isground = Physics.CheckSphere(gravityCheck.position, GroundDistance, Ground);
       
      

       
        mCharacterController.Move(transform.forward * v * speed * Time.deltaTime);
        mCharacterController.Move(transform.right * h * speed * Time.deltaTime);
      
        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;
        Vector3 right = transform.TransformDirection(Vector3.right).normalized;
        right.y = 0.0f;
      

       
        mVelocity.y += mGravity * Time.deltaTime;
        mCharacterController.Move(mVelocity * Time.deltaTime);

        if (mCharacterController.isGrounded && mVelocity.y < 0) 
            mVelocity.y = 0f;
    }
  
    public void Ulta()// улта если сила равна 100
    {
        if (Power >= 100)
        {

            Power -= 100;

            Collider[] nearEnemy = Physics.OverlapSphere(transform.position, radiosPower, enemy);
            pointKill += nearEnemy.Length;
            for (int i = 0; i < nearEnemy.Length; i++)
            {

                Destroy(nearEnemy[i].gameObject);



            }

        }
      
    }
    //методы для получения урона или добавления бонусов
    public void Doc()
    {
        if (Hp < MaxPower)
        {
            Hp += 15;
        }
    }
    public void DocMega()
    {
        if (Hp < MaxPower)
        {
            Hp += 50;
        }
    }


    public void TakeDamage()
    {
        Hp -= 15;
        
        if (Hp <= 0)
        {
            GameOver();
        }
        else if (Hp>= MaxHp)
        {
            Hp = MaxHp;

        }

    }
    public void MinusPower()
    {
        if(Power > 0)
        {
            Power -= 25;

        }
    }
    public void PlusPower()
    {
        if (Power < MaxPower)
        {
            Power += 50;
        }


    }
    








    //окончания игры

    public void GameOver()
    {


        Meny.GetComponent<Meny>().GameOver();











    }
    //включение индикатора попадания


    public void CheckHitOn()
    {


        CheckHit.SetActive(true);

        Invoke(nameof(CheckHitOff), 0.5f);

    }
    private void CheckHitOff()
    {
        CheckHit.SetActive(false);


    }




}
