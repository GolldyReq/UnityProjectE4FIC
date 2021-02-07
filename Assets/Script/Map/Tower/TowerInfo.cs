using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour
{
    private void Update()
    {
        //rotation
        Transform parent = this.transform.parent.transform;
        transform.localEulerAngles = new Vector3(90, 180 - parent.localEulerAngles.y, 0);

        //Update
        Tower actual = parent.gameObject.GetComponent<Tower>();
        if (actual)
        {
            transform.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = (float)Mathf.Clamp(actual.getPv(), 0, actual.getPvMax()) / actual.getPvMax();
        }
    }
}
