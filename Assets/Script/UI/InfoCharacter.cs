using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //rotation
        Transform parent = this.transform.parent.transform;
        transform.localEulerAngles = new Vector3(90, 180 - parent.localEulerAngles.y, 0);

        //Update
        Character actual = parent.gameObject.GetComponent<Character>();
        if(actual)
        {
            transform.GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = (float)Mathf.Clamp(actual.getPv(), 0, actual.getPvMax()) / actual.getPvMax();
        }
    }
}
