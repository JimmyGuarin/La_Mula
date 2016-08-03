using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HUD1 : MonoBehaviour
{

    
    
    public static HUD1 instancia;
    public Text persistencia;

   public Text TextAtrapadas;
   public Text textoAtrapadasFinal;
   public Text TextOroBarrilesNormales;

   public Text TextAtrapadasOro;
   public Text TextAtrapadasOroFinal;
   public Text TextOroBarrilesOro;

   public Text textoPerdidasFinal;
   public Text textoPerdidas;

   public Text textoOroTotal;

   public Text textoVelocidad;
   public Text textoDistancia;
   public GameObject panelDerrota;
   public int velocidad;
   public GameObject BonusIman;
   public GameObject Mula;


   private int valorBarrilOro = 50;
   private int valorBarrilNormal = 5;
   private bool corriendo;
   private Nivel nivelActual;


    //Propiedades para verificar objetivos
    [HideInInspector]
    public int dinamitasEsquivadas;
    [HideInInspector]
    public int cascosAtrapados;
    [HideInInspector]
    public int imanesAtrapados;
    [HideInInspector]
    public int BarrilesDorados;
    [HideInInspector]
    public float distancia;
    [HideInInspector]
    public int encholadas;
    [HideInInspector]
    public int perdidas;

    public GameObject panelObjetivos;

    public void Awake()
    {
        //DontDestroyOnLoad(this);
        velocidad = 30;
        distancia = 0;
        persistencia.text = Application.persistentDataPath;
    }

    // Use this for initialization
    void Start()
    {

        Restablecer();
        textoVelocidad.text = "30 Km/h";
        corriendo = false;



        if(Serializable.niveles == null)
        {
            MostrarPanelDerrota();
            
            Time.timeScale = 0;
        }
        nivelActual = (Nivel)Serializable.niveles.niveles[0];

        if (instancia == null)
        {
            instancia = this;
            IniciarJuego();
            //MostrarPanelDerrota();

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
            Serializable.niveles.logros.metrosRecorridos += d;
        }

        Objetivo obj = nivelActual.VerificarObjetivos();
        if (obj!= null)
        {
            MostrarObjetivo(obj,0);
        }
        obj = Serializable.niveles.logros.VerificarLogros();
        if (obj != null)
        {
            MostrarObjetivo(obj,1);
        }


    }

    public void Encholar()
    {
        encholadas++;
        TextAtrapadas.text = "" + encholadas;
        Handheld.Vibrate();

    }

    public void EncholarDorado()
    {
        BarrilesDorados++;
        TextAtrapadasOro.text = "" + BarrilesDorados;
        Handheld.Vibrate();
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
        Serializable.Save();
        Time.timeScale = 1;
        Destroy(this.gameObject);
        SceneManager.LoadScene(0);

    }


    public void Restablecer()
    {
        perdidas = 0;
        encholadas = 0;
        BarrilesDorados = 0;
        distancia = 0;
        dinamitasEsquivadas = 0;
        imanesAtrapados = 0;
        cascosAtrapados = 0;
        textoPerdidas.text = "0";
        TextAtrapadas.text = "0";
        textoVelocidad.text = "30 Km/h";
    }


    public void Reiniciar()
    {
        corriendo = true;
        Destroy(this.gameObject);
        SceneManager.LoadScene(1);
        
    }

    public void Perder()
    {
        corriendo = false;
        Invoke("MostrarPanelDerrota", 1);

    }

    public void MostrarPanelDerrota()
    {
        textoDistancia.text = "0 Mts.";
        textoPerdidasFinal.text = textoPerdidas.text;
        textoAtrapadasFinal.text = TextAtrapadas.text;
        TextAtrapadasOroFinal.text = TextAtrapadasOro.text;

        int oroConseguido = encholadas * valorBarrilNormal + BarrilesDorados * valorBarrilOro;

        TextOroBarrilesNormales.text = "" + encholadas * valorBarrilNormal;
        TextOroBarrilesOro.text= "" + BarrilesDorados * valorBarrilOro;
        textoOroTotal.text = "" + oroConseguido;

        panelDerrota.SetActive(true);
        BonusIman.SetActive(false);
        BonusIman.GetComponentInChildren<Slider>().value = 1;
        Time.timeScale = 0;
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


    public void MostrarObjetivo(Objetivo obj, int tipo)
    {
        //Objetivo
        if (tipo == 0)
        {
            panelObjetivos.transform.GetChild(2).gameObject.SetActive(true);
            panelObjetivos.transform.GetChild(3).gameObject.SetActive(false);
        }

        //Logro
        if (tipo == 1)
        {
            panelObjetivos.transform.GetChild(2).gameObject.SetActive(true);
            panelObjetivos.transform.GetChild(3).gameObject.SetActive(false);
        }

        panelObjetivos.transform.GetChild(0).GetComponent<Text>().text = obj.nombre;
        panelObjetivos.transform.GetChild(1).GetComponent<Text>().text = obj.descripcion;
        panelObjetivos.GetComponent<Animation>().Play();
    }



}
