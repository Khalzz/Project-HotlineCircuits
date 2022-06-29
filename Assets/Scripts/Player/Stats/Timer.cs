using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timer;

    int hrs;
    string hrsStr;

    int min;
    string minStr;

    float sec;
    string secStr;

    string timerStr;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<TextMeshProUGUI>();
        hrs = 0;
        min = 0;
        sec = 0;
    }

    // Update is called once per frame
    void Update()
    {

        

        sec += 1* Time.deltaTime;

        // here i just get the order of 00:00:00 when the hours, minutes or seconds are smaller than 9
        if (sec < 10) secStr = 0 + sec.ToString("F3"); else secStr = sec.ToString("F3");
        if (min < 10) minStr = 0 + min.ToString(); else minStr = min.ToString();
        if (hrs < 10) hrsStr = 0 + hrs.ToString(); else hrsStr = hrs.ToString();

        if (sec >= 60) 
        {
            sec = 0;
            min += 1;
        }
        if (min == 60)
        {
            min = 0;
            hrs += 1;
        }

        if (min > 0) 
        {
            timerStr = minStr + ":" + secStr;
        }
        else if (hrs > 0)
        {
            timerStr = hrsStr + ":" + minStr + ":" + secStr;
        }
        else 
        {
            timerStr = secStr;
        }

        timer.text = timerStr;
        


    }
}
