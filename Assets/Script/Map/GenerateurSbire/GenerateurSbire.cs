using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateurSbire : MonoBehaviour
{

    private float m_wait;
    [SerializeField] GameObject m_Spawner;
    // Start is called before the first frame update
    void Start()
    {
        m_wait = 30;
        if (GameManager.Instance != null && GameManager.Instance.m_State != GameManager.GAME_STATE.Play)
            StartCoroutine(WaitForGame());
        //StartCoroutine(StartGenerateur());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WaitForGame()
    {
        while(GameManager.Instance.m_State != GameManager.GAME_STATE.Play)
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(StartGenerateur());
    }
    IEnumerator StartGenerateur()
    {
        //Remplacer true par jeux en mode : play
        while (true)
        {
            StartCoroutine(GenerateSbire());
            yield return new WaitForSeconds(m_wait) ;
        }
    }

    IEnumerator GenerateSbire()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject Sbire = Instantiate(Resources.Load("Creep/SbireCaC/Prefabs/SbireCaC") as GameObject);
            Sbire.transform.position = m_Spawner.transform.position;
            yield return new WaitForSeconds(1.5f);
        }
    }

}
