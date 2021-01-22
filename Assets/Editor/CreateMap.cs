using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CreateMap : MonoBehaviour
{
    [MenuItem("Tools/Generate Map")]
    public static void GenerateMap()
    {
        GameObject Map;
        //Si le GO Map n'existe pas déjà , on le crée  
        Map = GameObject.Find("Map");
        if(!Map)
        {
            Debug.Log("Création Game Object Map");
            Map = new GameObject("Map");
        }
        Map.transform.position = new Vector3(0, 0, 0);
        //Destruction ancienne map
        /*foreach(Transform child in Map.gameObject.transform)
        {
            DestroyImmediate(child.gameObject);
        }*/
        //Construction Map
        for (int i =0; i <5; i++)
        {
            for (int j = 0; j< 20; j++)
            {
                //A modifier pour chercher des tiles random
                GameObject tile = Instantiate(Resources.Load("Map/Tile/BasicTile")) as GameObject; 
                tile.transform.parent = Map.transform;
                tile.transform.position = new Vector3(i*10 , 0, j*10);
                tile.name = i.ToString() +"_"+ j.ToString();
                //tile.AddComponent<NavMeshModifier>();
                NavMeshModifier modifier = tile.GetComponent<NavMeshModifier>();
                if(modifier)
                    modifier.area = 03; //Ground                


            }
        }

        //Ajout des tourelles et Spawner de creeps



    }
}
