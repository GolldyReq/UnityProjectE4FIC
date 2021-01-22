using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{
   

    //Recuperer le nombre de Personnage jouable 
    public static void CountPlayer()
    {

        int i = 200;// Screen.width / 2;
        int j = Screen.height/2 ;
        GameObject SelectionPlayerPannel = GameObject.Find("SelectionPlayer");
        Debug.Log("Width :  " + Screen.width + " Height : " + Screen.height);
        string file = Application.persistentDataPath + "/Characters/CharactersList.txt";
        StreamReader sr = new StreamReader(file);

        while(sr.Peek() >= 0)
        {
            /*
            string path = folder.Replace("\\", "/");
            string CharacterName = path.Split( '/' )[path.Split('/').Length-1];
            */
            string CharacterName = sr.ReadLine();
            
            GameObject PlayerSelection = GameObject.Instantiate(Resources.Load("UI/GUI/SelectPlayer/Character")) as GameObject;
            PlayerSelection.name = CharacterName;
            PlayerSelection.transform.parent = SelectionPlayerPannel.transform;
            RectTransform Pos = PlayerSelection.GetComponent<RectTransform>();
            Pos.position = new Vector3(i, j, 0);

            //Chargement de l'icone
            try
            {
                Button button = PlayerSelection.GetComponentInChildren<Button>();
                Image icone = button.GetComponent<Image>();
                string iconePath = "Characters/" + CharacterName + "/Icone/icone";
                Sprite sprite = Resources.Load<Sprite>(iconePath);
                icone.sprite = sprite;
            }catch(Exception e) { Debug.Log("Impossible de charger l'icone du personnage : " + CharacterName); }
            //Chargement du nom
            PlayerSelection.GetComponentInChildren<Text>().text = CharacterName;

            i += 200;
            //j -= 200;
        }
        sr.Close();
    }
}
