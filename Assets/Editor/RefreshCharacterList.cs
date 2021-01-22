using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class RefreshCharacterList : MonoBehaviour
{
    [MenuItem("Tools/Update Character List")]
    public static void UpdateCharacterList()
    {
        string file = Application.persistentDataPath + "/Characters/CharactersList.txt";
        string[] dir = Directory.GetDirectories("Assets/Resources/Characters", "*", SearchOption.TopDirectoryOnly);
        FileStream vidage = File.Open(file, FileMode.Truncate);
        vidage.Close();
        
        foreach (string folder in dir)
        {
            string path = folder.Replace("\\", "/");
            string CharacterName = path.Split('/')[path.Split('/').Length - 1];
            CharacterName = CharacterName + '\n';
            FileStream fs = File.Open(file, FileMode.Append);
            Byte[] info = new UTF8Encoding(true).GetBytes(CharacterName + "\n");
            fs.Write(info, 0, CharacterName.Length);
            fs.Close();
        }
        Debug.Log("[INFO] Character List has been update ! ");
    }


    [MenuItem("Tools/Print Character List")]
    public static void PrintCharacterList()
    {
        string file = Application.persistentDataPath + "/Characters/CharactersList.txt";
        StreamReader sr = new StreamReader(file);

        while (sr.Peek() >= 0)
        {
            Debug.Log(sr.ReadLine());
        }

        sr.Close();
    }
}
