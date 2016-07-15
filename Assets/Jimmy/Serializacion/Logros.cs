using UnityEngine;
using System.Collections;

[System.Serializable]
public class Logros{

    public int objetivosDerribados;
    public float metrosRecorridos;
    public Objetivo[] logros;


    public Logros()
    {
        objetivosDerribados = 0;
        metrosRecorridos = 0;
        logros = new Objetivo[2];
        logros[0] = new Objetivo("Mula Trotamundos", "Recorre 5.000 metros");
        logros[1] = new Objetivo("Contra el Mundo", "Derriba 10 obstáculos con el casco");
    }


    public Objetivo VerificarLogros()
    {
        if ((int)metrosRecorridos==5000 && !logros[0].estado)
        {
            logros[0].estado= true;
            return logros[0];
        }
        if (objetivosDerribados == 10 && !logros[1].estado)
        {
            logros[1].estado = true;
            return logros[1];
        }
        return null;
    }
}
