using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CanvasNiveles : MonoBehaviour {


    public float BeginDragY;
    public Animator PanelObjetivos;
    public int nivel;

	// Use this for initialization
	void Start () {

        nivel = 0;
        Serializable.Load();
        Debug.Log(Serializable.nivel1Serializable.objetivo1);
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
}
