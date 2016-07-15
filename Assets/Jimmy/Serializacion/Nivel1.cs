using UnityEngine;
using System.Collections;


[System.Serializable]
public class Nivel1 : Nivel {


    

    public Nivel1()
    {
        objetivos = new Objetivo[5];
        objetivos[0] = new Objetivo("Mula Servicial", "Captura 20 barriles");
        objetivos[1] = new Objetivo("Cabezadura", "Atrapa 10 cascos");
        objetivos[2] = new Objetivo("Anti Dinamitas", "Esquiva 20 dinamitas");
        objetivos[3] = new Objetivo("Magnetizado", "Atrapa 10 imanes");
        objetivos[4] = new Objetivo("Manos Rápidas", "Atrapa los 10 primeros barriles");

    }


    
    public override Objetivo VerificarObjetivos()
    {
        HUD1 hud = HUD1.instancia;

        //Objetivo1
        if ((int)hud.encholadas == 20 && !objetivos[0].estado)
        {
            objetivos[0].estado = true;
            return objetivos[0];
        }
        //Objetivo2
        if ((int)hud.cascosAtrapados == 5 && !objetivos[1].estado)
        {
            objetivos[1].estado = true;
            return objetivos[1];
        }
        //Objetivo3
        if ((int)hud.dinamitasEsquivadas == 10 && !objetivos[2].estado)
        {
            objetivos[2].estado = true;
            return objetivos[2];
        }
        //Objetivo4
        if ((int)hud.imanesAtrapados == 5 && !objetivos[3].estado)
        {
            objetivos[3].estado = true;
            return objetivos[3];
        }

        //Objetivo5
        if ((int)hud.encholadas == 5 && hud.perdidas == 0 && !objetivos[4].estado)
        {
            objetivos[4].estado = true;
            return objetivos[4];
        }

        return null;

    }


    
}
