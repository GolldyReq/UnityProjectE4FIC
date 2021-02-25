using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public int m_pv,m_pv_max;
    public bool RedTeam;
    public bool BlueTeam;

    // Start is called before the first frame update
    void Start()
    {
        m_pv = 500;
        m_pv_max = m_pv;

    }

    // Update is called once per frame
    void Update()
    {
        if(m_pv <= 0 )
        {
            //Mettre les 2 generateurs de sbire en SerializeField et les detruire
            Destroy(gameObject);
            //Reload du nav mesh Surface
        }
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
