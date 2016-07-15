using UnityEngine;
using System.Collections;


[System.Serializable]
public abstract class Nivel {

    public string nombre;
    public Objetivo[] objetivos;
    public abstract Objetivo VerificarObjetivos();


}
