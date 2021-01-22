using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : MonoBehaviour
{

    private NavMeshSurface m_navMeshSurface;
    private bool m_CanCompute;
    private float m_CoolDownCompute;
    void Start()
    {
        m_navMeshSurface = GetComponent<NavMeshSurface>();
        if (m_navMeshSurface!=null)
            Debug.Log("NavMeshSurface init  : [OK] ");
        else
            Debug.Log("NavMeshSurface int : [ERREUR]");
        m_CanCompute = true;
        m_CoolDownCompute = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Calcul a chaque frame trop lourd 
        if (m_navMeshSurface && m_CanCompute)
        {
            Debug.Log("Compute nav mesh");
            m_navMeshSurface.BuildNavMesh();
            StartCoroutine(StartCoolDownComputeNavMesh());
        }
        */
    }

    protected IEnumerator StartCoolDownComputeNavMesh()
    {
        this.m_CanCompute = false;
        yield return new WaitForSeconds(m_CoolDownCompute);
        this.m_CanCompute = true;
    }
}
