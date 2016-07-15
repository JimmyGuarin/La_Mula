using UnityEngine;
using System.Collections;


[System.Serializable]
public class ManejadorNiveles{


    public ArrayList niveles;
    public Logros logros;
    public ManejadorNiveles()
    {
        logros = new Logros();
        niveles = new ArrayList();
        niveles.Add(new Nivel1());
    }




	
}



