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
    public Text textoDistancia;
    public GameObject panelDerrota;
    public int velocidad;
    public GameObject BonusIman;
    public GameObject Mula;
    private bool corriendo;
    private float distancia;

    public void Awake()
    {
        DontDestroyOnLoad(this);
        velocidad = 30;
        distancia = 0;
    }





    // Use this for initialization
    void Start()
    {

        perdidas = 0;
        encholadas = 0;
        textoVelocidad.text = "30 Km/h";
        corriendo = false;

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

    public void Update()
    {


        if (corriendo)
        {
            float d = (velocidad * 5) / 18;
            d *= Time.deltaTime;
            distancia += d;
            textoDistancia.text = "" + ((int)distancia)+" Mts";


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

    public void CambioVelocidad()
    {
        Debug.Log("entra");
        Time.timeScale += 0.1f;
        velocidad += 10;
        textoVelocidad.text = "" + velocidad + " Km/h";
    }

    public void IniciarJuego()
    {
        Time.timeScale = 1f;
        corriendo = true;
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
        corriendo = false;
        Invoke("MostrarPanelDerrota", 1);

    }

    public void MostrarPanelDerrota()
    {
        Time.timeScale = 0;
        perdidas = 0;
        encholadas = 0;
        distancia = 0;
        textoDistancia.text = "0 Mts.";
        textoPerdidasFinal.text = textoPerdidas.text;
        textoAtrapadasFinal.text = TextAtrapadas.text;
        textoPerdidas.text = "0";
        TextAtrapadas.text = "0";
        textoVelocidad.text = "30 Km/h";
        panelDerrota.SetActive(true);
        BonusIman.SetActive(false);
        BonusIman.GetComponentInChildren<Slider>().value = 1;
    }
    public void MostrarPanelBonus()
    {
        BonusIman.SetActive(true); BonusIman.GetComponentInChildren<Slider>().value = 1;
        StartCoroutine("disminuirBonusIman");




    }

    IEnumerator disminuirBonusIman()
    {

        Slider slider = BonusIman.GetComponentInChildren<Slider>();
        while (slider.value > 0)
        {

            slider.value -= (float)(0.01 * Time.timeScale);
            yield return new WaitForSeconds(0.1f * Time.timeScale);
        }

        GameObject.Find("Burra").GetComponent<MovimientoAcelerometro>().iman.SetActive(false);
        BonusIman.SetActive(false);
        yield return null;

    }


}
