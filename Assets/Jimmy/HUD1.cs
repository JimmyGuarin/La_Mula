using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD1 : MonoBehaviour {

    public int encholadas;
    public static HUD1 instancia;
    public Text TextAtrapadas;
    public Text textoToques;


    // Use this for initialization
    void Start()
    {

        encholadas = 0;
        instancia = this;
    }

   

    public void Encholar()
    {
        encholadas++;
        TextAtrapadas.text = ""+encholadas;

    }





    float deltaPosSum;
    public GUIText t, t2;
    void Update() {
        
    }
}
