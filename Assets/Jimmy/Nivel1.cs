using UnityEngine;
using System.Collections;


[System.Serializable]
public class Nivel1 : MonoBehaviour {


    public bool objetivo1;
    private bool objetivo2;
    private bool objetivo3;
    private bool objetivo4;
    private bool objetivo5;
    private HUD1 hud;

    public static Nivel1 instancia;

    // Use this for initialization
    void Start () {

        instancia = this;
        hud = HUD1.instancia;
        /// xml
	}
	
	// Update is called once per frame
	void Update () {


        //Objetivo1
        if ((int)hud.encholadas == 2 && !objetivo1)
        {
            objetivo1 = true;
            CompletarObjetivo(1);
        }
        //Objetivo2
        if ((int)hud.cascosAtrapados == 10 && !objetivo2)
        {
            objetivo2 = true;
            CompletarObjetivo(2);
        }
        //Objetivo3
        if ((int)hud.dinamitasEsquivadas == 20 && !objetivo3)
        {
            objetivo3 = true;
            CompletarObjetivo(3);
        }
        //Objetivo4
        if ((int)hud.imanesAtrapados == 10 && !objetivo4)
        {
            objetivo4 = true;
            CompletarObjetivo(4);
        }

        //Objetivo5
        if ((int)hud.encholadas == 10 && hud.perdidas==0&&!objetivo5)
        {
            objetivo5 = true;
            CompletarObjetivo(5);
        }
    }


    public void CompletarObjetivo(int objetivo)
    {
        string nombre = "";
        string descripcion = "";
       
        switch (objetivo)
        {

            case 1:
                nombre = "Mula Servicial";
                descripcion = "Captura 20 barriles ";
                break;
            case 2:
                nombre = "Cabezadura";
                descripcion = "Atrapa 10 cascos.";
                break;
            case 3:
                nombre = "Anti Dinamitas";
                descripcion = "Esquiva 20 dinamitas.";
                break;
            case 4:
                nombre = "Magnetizado";
                descripcion = "Atrapa 10 imanes.";
                break;
            case 5:
                nombre = "Manos Rápidas";
                descripcion = "Atrapa los 10 primeros barriles.";
                break;
        }
        HUD1.instancia.MostrarObjetivo(nombre,descripcion,objetivo-1);
    }
}
