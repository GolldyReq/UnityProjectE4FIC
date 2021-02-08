using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayerButton : MonoBehaviour
{
    public void OnPlayerSelectedClick()
    {
        //Instanciation du personnage
        string playerName = transform.parent.gameObject.GetComponentInChildren<Text>().text;
        Debug.Log("Vous avez choisi : " + playerName);
        string pathPrefab = "Characters/" + playerName + "/Prefabs/" + playerName;
        Debug.Log("Chargement de : " + pathPrefab);
        GameObject Player = GameObject.Instantiate(Resources.Load(pathPrefab)) as GameObject;
        Player.name = "Player";
        //Player.transform.parent = GameObject.Find("PlayerSpawn").transform;
        Player.transform.parent = GameObject.Find("PlayerSpawn").transform.parent.transform;
        Player.transform.position = GameObject.Find("PlayerSpawn").transform.position;
        Debug.Log("Personnage chargé");

        //Désactiver la caméra du menu
        GameObject.Find("CameraMenu").SetActive(false);

        //Instanciatin de la caméra
        GameObject camera = new GameObject("CameraPlayer");
        camera.AddComponent<Camera>();
        camera.AddComponent<AudioListener>();
        camera.AddComponent<CameraTest>();
        //Lié le player à la camera
        camera.GetComponent<CameraTest>().setPlayer(Player);
        Player.GetComponent<Character>().setCamera(camera.GetComponent<Camera>());

       

        //Chargement du niveau
        MenuManager.Instance.PlayButtonHasBeenClicked();

    }

    public void OnMouseOverEnter()
    {
        gameObject.transform.parent.GetChild(0).GetComponent<Image>().color = Color.white;
    }
    public void OnMouseOverExit()
    {
        gameObject.transform.parent.GetChild(0).GetComponent<Image>().color = Color.gray;
    }

}
