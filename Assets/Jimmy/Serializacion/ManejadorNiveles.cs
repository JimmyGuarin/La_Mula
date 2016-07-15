using UnityEngine;
using System.Collections;


[System.Serializable]
public class ManejadorNiveles{


    public ArrayList niveles;
    public static ManejadorNiveles instancia;


    public ManejadorNiveles()
    {
        instancia = this;
        niveles = new ArrayList();
        niveles.Add(new Nivel1());
    }




	
}



