using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController m_Instance;
    public static GameController Instance { get { return m_Instance; } }

    public enum PHASEACTION { None, ChampionSelection , PlayerStat }
    public PHASEACTION m_Phase;
    public event Action<PHASEACTION> OnGamePhaseChange;

    public void ChangePhase(PHASEACTION phase)
    {
        m_Phase = phase;
        if (OnGamePhaseChange != null)
            OnGamePhaseChange(m_Phase);
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (MenuManager.Instance.IsReady == false)
            yield return null;
        while (GameManager.Instance.IsReady == false)
            yield return null;
        ChangePhase(PHASEACTION.None);
        //Debug.Log("Game controller OK !");
    }
    void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

}
