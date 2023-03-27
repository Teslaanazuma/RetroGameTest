using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.UI;
public class CameraRotation : MonoBehaviour
{
  //скрипт для повората камеры
    public Transform player;
    public float mouseSensitivity;
    float cameraVerticalRotation = 0f;
    public FixedJoystick lookJoystick;
    public float mMinPitch;
    public float mMaxPitch;
    private float angleX = 0.0f;

    public Slider Slider;


    private void Start()
    {

        Sensa();

    }


    void LateUpdate()
    {

      
         UpdateMoveJoystick();

    }
    
    public void Sensa()
    {

        mouseSensitivity = Slider.value;
    }





    
    void UpdateMoveJoystick()
    {

        float mx, my;
        mx = lookJoystick.Horizontal * Time.deltaTime;
        my = lookJoystick.Vertical * Time.deltaTime;
        
        cameraVerticalRotation -= my;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;


        player.Rotate(Vector3.up * mx * mouseSensitivity);

       
        
        
        Vector3 eu = transform.rotation.eulerAngles;

        angleX -= my * mouseSensitivity;


        angleX = Mathf.Clamp(angleX, mMinPitch, mMaxPitch);

        eu.y += mx * mouseSensitivity;

        Quaternion newRot = Quaternion.Euler(angleX, eu.y, 0.0f);
        
        transform.rotation = newRot;

    }







}
