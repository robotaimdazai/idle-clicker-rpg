using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GIKIT
{

    public class LoadingProgress : MonoBehaviour
    {
        [SerializeField] Slider slider = null;



        public void SetValue(float valNormalized)
        {
            slider.value = valNormalized;
        }

        public void SetSmoothProgress(float smoothTime = 2f)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothProgressCoroutine(smoothTime));
        }

        IEnumerator SmoothProgressCoroutine(float smoothTime)
        {
            float value = 0f;
            while (value<=1f)
            {
                float currentVal = Mathf.Lerp(0,1f,value);
                if (value<1)
                {
                    value += Time.deltaTime / smoothTime;
                }
                SetValue(value);

                yield return null;
            }
        }
    }

}

