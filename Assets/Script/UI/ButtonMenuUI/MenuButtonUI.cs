using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonUI : MonoBehaviour
{
    public void OnMouseEnter()
    {
        AudioManager.Instance.Play("Sound/GUIEffect/ButtonMouseOver" , 5f);
      
    }

    public void OnMouseClick()
    {
        AudioManager.Instance.Play("Sound/GUIEffect/ButtonMouseClick", 5f);
    }
}
