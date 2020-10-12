using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    //***********
    // 対象タグ
    //***********
    public string PlayerTag;

    //UI
    public GameObject ClearUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == PlayerTag)
        {
            ClearUI.SetActive(true);
        }
    }
}
