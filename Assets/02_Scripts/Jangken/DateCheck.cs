using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateCheck : MonoBehaviour
{
    System.DateTime now;
    int nowMonth;
    int nowDay;
    
    void Start()
    {
        now = System.DateTime.Now;

        nowMonth = now.Month;
        nowDay = now.Day;
    }

    
    void Update()
    {
        
    }
}
