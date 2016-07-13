using UnityEngine;
using System.Collections;

public class CanvasNiveles : MonoBehaviour {


    public float BeginDragY;
    public Animator PanelObjetivos;

	// Use this for initialization
	void Start () {
	
        
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
        Debug.Log(Input.mousePosition);
        BeginDragY = Input.mousePosition.y;
    }

    public void Evento2()
    {

        if (Input.mousePosition.y <= BeginDragY)
        {
            Debug.Log(Input.mousePosition);
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
}
