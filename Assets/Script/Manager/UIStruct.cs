using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStruct 
{
    public struct UIPanel
    {
        public UIPanel(GameController.PHASEACTION name, GameObject panel)
        {
            NameData = name;
            PanelData = panel;
        }
        public UIPanel(string name, GameObject panel)
        {
            NameData = (GameController.PHASEACTION)Enum.Parse(typeof(GameController.PHASEACTION), name);
            PanelData = panel;
        }
        public GameController.PHASEACTION NameData { get; private set; }
        public GameObject PanelData { get; private set; }
    }
}
