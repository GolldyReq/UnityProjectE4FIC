using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public static MenuManager m_Instance;
    public static MenuManager Instance { get { return m_Instance; } }

    bool m_IsReady;
    public bool IsReady { get { return m_IsReady; } }
    //public List<GameObject> m_Panels = new List<GameObject>();
    public struct Panel
    {
        public Panel(GameManager.GAME_STATE name, GameObject panel)
        {
            NameData = name;
            PanelData = panel;
        }
        public Panel(string name, GameObject panel)
        {
            NameData = (GameManager.GAME_STATE)Enum.Parse(typeof(GameManager.GAME_STATE), name);
            PanelData = panel;
        }
        public GameManager.GAME_STATE NameData { get; private set; }
        public GameObject PanelData { get; private set; }
    }

    public List<Panel> m_Panels = new List<Panel>();
    public List<UIStruct.UIPanel> m_UIPanels = new List<UIStruct.UIPanel>();

    EventSystem eventSystem;


    public event System.Action OnStartPlayButtonHasBeenClicked;
    public event System.Action OnExitButtonHasBeenClicked;
    public event System.Action OnSelectPlayerButtonHasBeenClicked;


    void GameStateChange(GameManager.GAME_STATE state)
    {
        //Debug.Log("State : " + GameManager.m_Instance.m_State);
        foreach (Panel panel in m_Panels)
        {
            panel.PanelData.SetActive(panel.NameData == state);
        }
        //Debug.Log("State : " + GameManager.m_Instance.m_State);
    }


    void GamePhaseChange(GameController.PHASEACTION phase)
    {
        //Debug.Log("State : " + GameManager.m_Instance.m_State);
        //Debug.Log("Changement phase");
        foreach (UIStruct.UIPanel panel in m_UIPanels)
        {
            panel.PanelData.SetActive(panel.NameData == phase);
        }
        //Debug.Log("State : " + GameManager.m_Instance.m_State);
    }

    private void Awake()
    {

        m_IsReady = false;
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);

        foreach (Transform p in GameObject.Find("Pannels").transform)
        {
            //Debug.Log("Ajout du panneau : " + p.gameObject.name);
            try
            {
                m_Panels.Add(new Panel(p.gameObject.name, p.gameObject));
            }
            catch (Exception e)
            {
                try
                {
                    m_UIPanels.Add(new UIStruct.UIPanel(p.gameObject.name, p.gameObject));
                }
                catch (Exception e2)
                {
                    Debug.Log("Impossible d'assigner le panneau " + p.gameObject.name);
                }
            }
        }

        eventSystem = GameObject.FindObjectOfType<EventSystem>();

        //Debug.Log("Menu Manager : OK !");
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnGameStateChange += GameStateChange;
        GameController.Instance.OnGamePhaseChange += GamePhaseChange;
        m_IsReady = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButtonHasBeenClicked()
    {
        if (OnStartPlayButtonHasBeenClicked != null) OnStartPlayButtonHasBeenClicked();
    }
    public void ExitButtonHasBeenClicked()
    {
        if (OnExitButtonHasBeenClicked != null) OnExitButtonHasBeenClicked();
    }
    public void SelectPlayerButtonHasBeenClicked()
    {
        if (OnSelectPlayerButtonHasBeenClicked != null) OnSelectPlayerButtonHasBeenClicked();
    }

}

