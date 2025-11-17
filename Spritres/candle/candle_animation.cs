using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class candle_animation : MonoBehaviour
{

    public GameObject candle;
    public SpriteRenderer[] fire;
    public int currframe = 0;
    public float FireAnimSpeed;
    public float CandleMovAnimSpeed;

    // Start is called before the first frame update
    void Start()
    {
        fire[0].enabled = false;
        fire[1].enabled = false;
        fire[2].enabled = false;
        StartCoroutine(DelayedAction());
    }

    void Update()
    {
        candle.transform.position += new Vector3(0, -0.05f * Time.deltaTime);
        //candle.transform.position = new Vector2(transform.position.x, transform.position.y - 1f );
        if (currframe > 2)
        {
            currframe = 0;
            
        }
    }
    IEnumerator DelayedAction()
    {

        
        while (true)
        {
            if (currframe > 2)
            { currframe = 0; Debug.Log("BOB"); }
            fire[0].enabled = false;
            fire[1].enabled = false;
            fire[2].enabled = false;
            fire[currframe].enabled = true;

           

            yield return new WaitForSeconds(FireAnimSpeed); // Wait for 3 seconds (scaled time)
            currframe = currframe + 1;
            if (currframe > 2)
            { currframe = 0;  }
        }
        
    }

}
