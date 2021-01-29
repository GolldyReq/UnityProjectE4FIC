using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateMap()
    {

        transform.position = new Vector3(0, 0, 0);
        //Construction Map
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                //A modifier pour chercher des tiles random
                GameObject tile = Instantiate(Resources.Load("Map/Tile/BasicTile")) as GameObject;
                tile.transform.parent = transform;
                tile.transform.position = new Vector3(i * 10, 0, j * 10);
                tile.name = i.ToString() + "_" + j.ToString();
                
                //tile.AddComponent<NavMeshModifier>();
                NavMeshModifier modifier = tile.GetComponent<NavMeshModifier>();
                if (modifier)
                {
                    //Attribution Layer
                    modifier.overrideArea = true;
                    modifier.area = 04; //Ground   *
                }

            }
        }

        //Ajout des tourelles et Spawner de creeps
    }
}
