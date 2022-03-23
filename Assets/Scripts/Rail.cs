using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rail : MonoBehaviour
{
    public bool IsRunning = false;
    public float Speed = 0.5f;

    [SerializeField] RectTransform product = null;
    [SerializeField] float productStartPositionX;
    [SerializeField] float productEndPositionX;

    [SerializeField]RawImage topRailRawImage = null;
    [SerializeField]RawImage bottomRailRawImage = null;

    float xValue = 0;
    float productXValue=0;
    float productSpeedConstant = 1550f;


    void Start()
    {
        productXValue = productStartPositionX;
        product.anchoredPosition = new Vector2(productXValue, product.anchoredPosition.y);
    }

    void Update()
    {
        if (IsRunning)
        {
            xValue += Time.deltaTime * Speed;
            productXValue -= Time.deltaTime * productSpeedConstant * Speed;

            if (productXValue<=productEndPositionX)
            {
                productXValue = productStartPositionX;
            }
            

            if (xValue >= 0.5)
            {
                xValue = 0;
            }

            topRailRawImage.uvRect = new Rect(xValue,0,0.5f,1);
            bottomRailRawImage.uvRect = new Rect(xValue,0,0.5f,1);
            product.anchoredPosition = new Vector2(productXValue,product.anchoredPosition.y);
            
            
        }
       
    }
}
