using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallController : MonoBehaviour
{
    public Transform goalTransform;
    public SliderManager sliderManager;
    Rigidbody rb;
    float goalZvalue;
    public float power;
    Vector3 startPos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        goalZvalue = goalTransform.position.z;
        startPos = transform.position;
        
    }

    public void Shoot()
    {
        Vector3 shootDirection = new Vector3(SliderManager._horizontalSliderValue, SliderManager._verticalSliderValue, goalZvalue) * power;
        rb.AddForce(shootDirection, ForceMode.VelocityChange); // Impulse ile kuvvet uygula
        Invoke("AgainSlider", 3f);
        Invoke("ResetBall", 3f);

    }

    void AgainSlider()
    {
        sliderManager.ActivateAgain();
    }

   void ResetBall()
    {
        transform.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }
}
