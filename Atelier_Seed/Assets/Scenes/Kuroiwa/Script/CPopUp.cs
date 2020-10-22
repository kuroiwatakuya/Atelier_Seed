using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CPopUp : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public GameObject Hint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                Hint.SetActive(true);
            }
        }
    }
    
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Ended)
            {
                Hint.SetActive(false);
            }
        }
    }
}