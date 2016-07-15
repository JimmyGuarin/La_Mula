using UnityEngine;
using System.Collections;


[System.Serializable]
public class Objetivo {

    public string nombre;
    public string descripcion;
    public bool estado;


    public Objetivo(string nombre,string descripcion)
    {
        this.descripcion = descripcion;
        this.nombre = nombre;
        estado = false;
    }


}
