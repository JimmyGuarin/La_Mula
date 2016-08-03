using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class Serializable {

    public static ManejadorNiveles niveles;
  
    public static void Save()
    {

        Debug.LogError("Error");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, Serializable.niveles);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {

            Debug.Log(Application.persistentDataPath);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            Serializable.niveles = (ManejadorNiveles)bf.Deserialize(file);
            file.Close();
            if (Serializable.niveles.version != 2)
            {
                Debug.Log("Entra");
                File.Delete(Application.persistentDataPath + "/savedGames.gd");
                ManejadorNiveles mN = new ManejadorNiveles(Serializable.niveles);
                Serializable.niveles = mN;
                Save();
            }
        }
        
    }

}
