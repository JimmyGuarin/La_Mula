using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class Serializable {

    public static Nivel1 nivel1Serializable;

    public static void Save()
    {
        nivel1Serializable = Nivel1.instancia;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, Serializable.nivel1Serializable);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            Serializable.nivel1Serializable = (Nivel1)bf.Deserialize(file);
            file.Close();
        }
    }

}
