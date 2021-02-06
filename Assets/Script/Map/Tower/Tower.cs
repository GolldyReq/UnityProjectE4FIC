using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public int m_pv;

    // Start is called before the first frame update
    void Start()
    {
        m_pv = 1500;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void TakeDammage(Tower tour , int dammage)
    {
        tour.m_pv = tour.m_pv - dammage;
    }
}
