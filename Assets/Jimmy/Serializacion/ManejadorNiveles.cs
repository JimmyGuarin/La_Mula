using UnityEngine;
using System.Collections;


[System.Serializable]
public class ManejadorNiveles{


    public ArrayList niveles;
    public ManejadorNiveles()
    {
    
        niveles = new ArrayList();
        niveles.Add(new Nivel1());
    }




	
}



