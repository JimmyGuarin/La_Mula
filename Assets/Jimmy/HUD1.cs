using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD1 : MonoBehaviour {

    public int encholadas;
    public static HUD1 instancia;
    public Text TextAtrapadas;

    // Use this for initialization
    void Start()
    {

        encholadas = 0;
        instancia = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Encholar()
    {
        encholadas++;
        TextAtrapadas.text = ""+encholadas;

    }
}
