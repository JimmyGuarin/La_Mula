using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD1 : MonoBehaviour {

    public int encholadas;
    public int perdidas;
    public static HUD1 instancia;
    public Text TextAtrapadas;
    public Text textoPerdidas;
    public Text textoVelocidad;

    // Use this for initialization
    void Start()
    {
        perdidas = 0;
        encholadas = 0;
        instancia = this;
        textoVelocidad.text = "30 Km/h";
    }

   

    public void Encholar()
    {
        encholadas++;
        TextAtrapadas.text = ""+encholadas;

    }

    public void Perdida()
    {
        perdidas++;
        textoPerdidas.text = "" + perdidas;
    }

    public void CambioVelocidad(int valor)
    {
        textoVelocidad.text = "" + valor + " Km/h";
    }


}
