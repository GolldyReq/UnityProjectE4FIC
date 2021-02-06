using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (m_player != null)
        {
            Vector3 PlayerPos = m_player.transform.position;
            this.transform.position = new Vector3(PlayerPos.x, PlayerPos.y + 45, PlayerPos.z - 15);
            transform.LookAt(m_player.transform);
        }
    }


    public void setPlayer(GameObject player)
    {
        this.m_player = player;
    }
}
