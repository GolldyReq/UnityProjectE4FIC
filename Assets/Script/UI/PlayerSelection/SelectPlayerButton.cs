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
        Player.name = "Character";
        Player.transform.parent = GameObject.Find("Player").transform;
        Debug.Log("Personnage chargé");

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
}
