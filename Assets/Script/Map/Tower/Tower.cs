using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public int m_pv,m_pv_max;

    // Start is called before the first frame update
    void Start()
    {
        m_pv = 500;
        m_pv_max = m_pv;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void TakeDammage(Tower tour , int dammage)
    {
        tour.m_pv = tour.m_pv - dammage;
    }

    public int getPvMax()
    {
        return m_pv_max;
    }
    public int getPv()
    {
        return m_pv;
    }
}
