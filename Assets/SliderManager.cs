using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SliderManager : MonoBehaviour
{
    [SerializeField] private Slider _horizontalSlider;
    [SerializeField] private Slider _verticalSlider;

    public float sliderVelocity = 0.1f; // enumla medium hard easy scriptable object
    WaitForSeconds cachedWait;
    private Coroutine currentCoroutine;
    private bool isHorizontalActive = true;
    [SerializeField] private UnityEvent onSlidersStopped;
    int index = 0;
    public static float _horizontalSliderValue;
    public static float _verticalSliderValue;

    void Start()
    {
        cachedWait = new WaitForSeconds(sliderVelocity);
        //currentCoroutine = StartCoroutine(SliderPlay(_horizontalSlider));
        ActivateAgain();
        //StartCoroutine(SliderPlay(_horizontalSlider));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SliderPlay(Slider currentSlider)
    {

        while (true)
        {

            for (int i=(int)currentSlider.minValue; i <= currentSlider.maxValue; i++)
            {
                currentSlider.value = i;
                yield return cachedWait;

                if (currentSlider.value == currentSlider.maxValue)
                {

                    for (int k = (int)currentSlider.maxValue; k >= currentSlider.minValue; k--)
                    {
                        currentSlider.value = k;

                        yield return cachedWait;
                    }

                }
            }
        }



    }
    public void OnSwitchSlider(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            index++;

            switch (index)
            {
                case 1:
                    _horizontalSliderValue = _horizontalSlider.value;
                   print(_horizontalSliderValue);
                    break;
                case 2:
                    _verticalSliderValue = _verticalSlider.value;
                    print(_verticalSliderValue);
                    onSlidersStopped?.Invoke(); // Event tetiklenir
                    StopAllCoroutines();
                   
                    return;

            }



            //if (index == 2)
            //{
            //    onSlidersStopped?.Invoke(); // Event tetiklenir
            //    StopAllCoroutines();
            //    return;
            //}

            // Mevcut Coroutine'i durdur
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);

            }

            // Eðer horizontal slider aktifse, vertical slider'a geç
            if (isHorizontalActive)
            {
                currentCoroutine = StartCoroutine(SliderPlay(_verticalSlider));
            }
            else
            {
                currentCoroutine = StartCoroutine(SliderPlay(_horizontalSlider));
            }

            // Slider'ýn durumunu tersine çevir
            isHorizontalActive = !isHorizontalActive;
        }
    }

    public void ActivateAgain()
    {
        index = 0;
        isHorizontalActive = true;
        currentCoroutine = StartCoroutine(SliderPlay(_horizontalSlider));
    }
}
