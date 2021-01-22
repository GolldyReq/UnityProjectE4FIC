using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatTool : MonoBehaviour
{


    //Initialise les statistiques du player
    public static void InitialisationPlayerStat(Sprite A , Sprite Z , Sprite E , Sprite R)
    {
        try
        {
            GameObject Apannel = GameObject.Find("A");
            GameObject Zpannel = GameObject.Find("Z");
            GameObject Epannel = GameObject.Find("E");
            GameObject Rpannel = GameObject.Find("R");
            Apannel.transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = A;
            Zpannel.transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = Z;
            Epannel.transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = E;
            Rpannel.transform.GetChild(1).gameObject.GetComponentInChildren<Image>().sprite = R;
        }
        catch (Exception e) { }// Debug.Log("Problem with icon"); }
       


    }

    public static void UpdateManaAndHealth(int pv, int pvmax , int pm , int pmmax)
    {
        try
        {
            GameObject Health = GameObject.Find("Health");
            GameObject Mana = GameObject.Find("Mana");

            Health.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)Mathf.Clamp(pv, 0, pvmax) / pvmax;
            Mana.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)Mathf.Clamp(pm, 0, pmmax) / pmmax;

            Health.transform.GetChild(2).GetComponent<Text>().text = pv + "/" + pvmax;
            Mana.transform.GetChild(2).GetComponent<Text>().text = pm + "/" + pmmax;
        }
        catch (Exception e) { }// Debug.Log("Problem with GUI health/mana bar"); }


    }

    public static void UpdatePlayerStat()
    {
        try
        {

        }
        catch (Exception e) { }// Debug.Log("Problem with GUI player stat"); }

    }


    public static void UpdatePlayerItem()
    {
        try
        {

        }
        catch (Exception e) { }// Debug.Log("Problem with GUI player item"); }
    }

}
