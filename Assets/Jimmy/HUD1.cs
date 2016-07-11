using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HUD1 : MonoBehaviour
{

    public int encholadas;
    public int perdidas;
    public static HUD1 instancia;
    public Text TextAtrapadas;
    public Text textoAtrapadasFinal;
    public Text textoPerdidasFinal;
    public Text textoPerdidas;
    public Text textoVelocidad;
    public GameObject panelDerrota;
    public int velocidad;
    public GameObject BonusIman;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        velocidad = 30;
    }





    // Use this for initialization
    void Start()
    {

        perdidas = 0;
        encholadas = 0;
        textoVelocidad.text = "30 Km/h";
        
        if (instancia == null)
        {
            instancia = this;
            Time.timeScale = 0;

        }

        else
        {
            Time.timeScale = 1;
            Destroy(this.gameObject);
        }
        


    }

   

    public void Encholar()
    {
        encholadas++;
        TextAtrapadas.text = "" + encholadas;

    }

    public void Perdida()
    {
        perdidas++;
        textoPerdidas.text = "" + perdidas;
    }

    public void CambioVelocidad(int valor)
    {

        Time.timeScale += 0.2f;
        velocidad += 10;
        textoVelocidad.text = "" + velocidad + " Km/h";
    }

    public void IniciarJuego()
    {
        Time.timeScale = 1f;
    }

    public void Salir()
    {
        Application.Quit();

    }



    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }

    public void Perder()
    {
        Invoke("MostrarPanelDerrota", 1);

    }

    public void MostrarPanelDerrota()
    {
        Time.timeScale = 0;
        perdidas = 0;
        encholadas = 0;
        textoPerdidasFinal.text = textoPerdidas.text;
        textoAtrapadasFinal.text = TextAtrapadas.text;
        textoPerdidas.text = "0";
        TextAtrapadas.text = "0";
        textoVelocidad.text = "30 Km/h";
        panelDerrota.SetActive(true);
    }
    public void MostrarPanelBonus()
    {
        BonusIman.SetActive(true);
        StartCoroutine("disminuirBonusIman");

        
       

    }

    IEnumerator disminuirBonusIman()
    {
       
        Slider slider = BonusIman.GetComponentInChildren<Slider>();
        Debug.Log("entra");
        while (slider.value >0)
        {
            Debug.Log("entra2");
            slider.value -= (float)(0.01* Time.timeScale);
            yield return null;
        }


        BonusIman.SetActive(false);
    }

       
}
