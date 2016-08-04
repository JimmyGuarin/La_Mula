using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasNiveles : MonoBehaviour {


    public float BeginDragY;
    public Animator PanelObjetivos;
    public int nivel;

    public GameObject[] objetivosNivel1;
    public GameObject[] logros;
    public Text[] textosAcumulados;
    public Text textDinero;
    
    private static int dinero;
    private string objetoTiendaActivo;
    private int valorObjetivoTienda;

    public GameObject panelSaldoInsuficiente;
    public GameObject panelSaldoSuficiente;

    public Button[] botonesObjetosTienda;


    // Use this for initialization
    void Start () {

        Debug.Log(Application.persistentDataPath);
        Serializable.Load();
        if (Serializable.niveles!= null)
        {
            VerificarObjetivosCompletados();
            textosAcumulados[0].text = "" + (int)Serializable.niveles.logros.metrosRecorridos;
            textosAcumulados[1].text = "" + (int)Serializable.niveles.logros.objetivosDerribados;
            dinero = PlayerPrefs.GetInt("Dinero");           
        }
        else
        {
            Serializable.niveles = new ManejadorNiveles();
            PlayerPrefs.SetInt("Dinero", 0);          
            Serializable.Save();
            dinero = 0;
        }

        objetoTiendaActivo = "";
        dinero = 1000; //Dinero Ficticio
        textDinero.text = "" + dinero;
    }
	

	// Update is called once per frame
	void Update () {	
	}


    public void DesplegarObjetivos(Animator anim)
    {     
        PanelObjetivos = anim;
        PanelObjetivos.SetBool("ocultar", false);
        anim.enabled = true;
         
    }


    public void Evento()
    {
        BeginDragY = Input.mousePosition.y;
    }


    public void Evento2()
    {
        if (Input.mousePosition.y <= BeginDragY)
        {
            PanelObjetivos.SetBool("ocultar", true);
            PanelObjetivos = null;
        }  
    }


    public void IrAtras()
    {
        if (PanelObjetivos != null)
        {
            PanelObjetivos.SetBool("ocultar", true);
        }
    }

    public void ComenzarNivel(int nivel)
    {
        PanelObjetivos.SetBool("ocultar", true);
        SceneManager.LoadSceneAsync(1);
    }


    public void VerificarObjetivosCompletados()
    {
        foreach(Nivel n in Serializable.niveles.niveles)
        {
            for(int i = 0; i < n.objetivos.Length; i++)
            {
                if (n.objetivos[i].estado)
                    objetivosNivel1[i].GetComponent<Image>().color = Color.white;
            }
        }

        for(int i = 0; i < Serializable.niveles.logros.logros.Length; i++)
        {
            if (Serializable.niveles.logros.logros[i].estado == true)
                logros[i].GetComponent<RawImage>().color = Color.white;
        }
    }


    public void IntentarComprarEnTienda(int Valor)
    {
        if (dinero>Valor)
        {
            panelSaldoSuficiente.SetActive(true);
            valorObjetivoTienda = Valor;
        }
        else
        {          
            panelSaldoInsuficiente.SetActive(true);
        }
    }


    public void ModificarObjetoTienda(string nombreObjeto)
    {
        objetoTiendaActivo = nombreObjeto;
    }

    public void RealizarCompra()
    {
        dinero -= valorObjetivoTienda;
        textDinero.text = ""+dinero;
        PlayerPrefs.SetInt(objetoTiendaActivo, 1);
    }


    public void VerificarObjetosComprados()
    {
        if(PlayerPrefs.GetInt("EmpezarCasco")>0)
        {
            botonesObjetosTienda[0].interactable = false;
            botonesObjetosTienda[0].transform.GetChild(0).GetComponent<Text>().text = "Comprado";
        }
    }

}
