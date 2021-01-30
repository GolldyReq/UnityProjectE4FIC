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

    public static void UpdateManaAndHealth(int pv, int pvmax, int pm, int pmmax)
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

    public static void UpdateCoolDownPlayer(float tA , float tZ ,float tE , float tR)
    {
        try
        {
            GameObject Apannel = GameObject.Find("A");
            GameObject Zpannel = GameObject.Find("Z");
            GameObject Epannel = GameObject.Find("E");
            GameObject Rpannel = GameObject.Find("R");
            if(tA > 0)
                Apannel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = tA.ToString("0.0");
            else
                Apannel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "";
            if (tZ > 0)
                Zpannel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = tZ.ToString("0.0");
            else
                Zpannel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "";
            if (tE > 0)
                Epannel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = tE.ToString("0.0");
            else
                Epannel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "";
            if (tR > 0)
                Rpannel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = tR.ToString("0.0");
            else
                Rpannel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>().text = "";


        }
        catch(Exception e) { }
    }

    public static void UpdatePlayerItem()
    {
        try
        {

        }
        catch (Exception e) { }// Debug.Log("Problem with GUI player item"); }
    }

}
