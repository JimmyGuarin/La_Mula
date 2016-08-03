using UnityEngine;
using System.Collections;


[System.Serializable]
public class ManejadorNiveles{


    public ArrayList niveles;
    public Logros logros;
    public int version;
    public ManejadorNiveles()
    {
        version = 2;
        logros = new Logros();
        niveles = new ArrayList();
        niveles.Add(new Nivel1());
    }

    public ManejadorNiveles(ManejadorNiveles mN)
    {
        version = 2;
        logros = mN.logros;
        niveles = mN.niveles;
    }



	
}



