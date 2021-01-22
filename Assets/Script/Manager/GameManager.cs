using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }

    public enum GAME_STATE { MenuPrincipal, SelectionPlayer , Play, Wait, Pause, Loading, Victory, GameOver, Equipe }
    public GAME_STATE m_State;

    public event Action<GAME_STATE> OnGameStateChange;
    bool m_IsReady;
    public bool IsReady { get { return m_IsReady; } }

    public bool IsPlaying { get { return m_State == GAME_STATE.Play; } }
    public void ChangeState(GAME_STATE state)
    {
        m_State = state;
        if (OnGameStateChange != null)
            OnGameStateChange(m_State);
    }

    private void Awake()
    {
        m_IsReady = false;
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (MenuManager.Instance.IsReady == false)
            yield return null;


        MenuManager.Instance.OnStartPlayButtonHasBeenClicked += OnStartPlayButtonHasBeenClicked;
        MenuManager.Instance.OnExitButtonHasBeenClicked += OnExitButtonHasBeenClicked;
        MenuManager.Instance.OnSelectPlayerButtonHasBeenClicked += OnSelectPlayerButtonHasBeenClicked;
        ChangeState(GAME_STATE.MenuPrincipal);
        m_IsReady = true;
        //Debug.Log("Game Manager : OK !");

    }


    //Boutton lancant la partie
    private void OnStartPlayButtonHasBeenClicked()
    {
        ChangeState(GAME_STATE.Play);
        GameController.Instance.ChangePhase(GameController.PHASEACTION.PlayerStat);
    }


    //Boutton lancant le jeu pour passer à l'écran de selection du perso
    private void OnSelectPlayerButtonHasBeenClicked()
    {
        //Debug.Log("J'appuie la sur le bouton");
        ChangeState(GAME_STATE.SelectionPlayer);
        GameController.Instance.ChangePhase(GameController.PHASEACTION.ChampionSelection);
        SelectPlayer.CountPlayer();
    }


    //Boutyon retour au menu principal


    //Boutyon pause



    //Boutton pour reprendre le jeu




    //Boutton pour quitter le jeu 
    private void OnExitButtonHasBeenClicked()
    {
        Application.Quit();
    }

}
