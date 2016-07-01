using UnityEngine;
using System.Collections;

public class circulo : MonoBehaviour
{
    public int segments;
    public float xradius;
    public float yradius;
    LineRenderer line;


    public int objetivo;
    public bool mover;
    public ArrayList vectores;

    void Start()
    {
        vectores = new ArrayList();

        line = gameObject.GetComponent<LineRenderer>();
        line.SetVertexCount(segments + 1);
        line.useWorldSpace = false;
        CreatePoints();
        objetivo = segments;
        if(mover)
        transform.position = (Vector3)vectores[0];
    }
    void CreatePoints()
    {
        float x;
        float y;
        float z = 0f;
        float angle = 90f;
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            if(mover)
            line.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / segments);
            if(mover)
            vectores.Add(new Vector3(x, y, z) + transform.position);
        }
    }

    public void Update()
    {

        if (mover)
        {
            if (transform.position == (Vector3)vectores[objetivo])
            {
                if (objetivo > 0)
                    objetivo--;
                else
                {
                    objetivo = segments;
                }
            }
            else
            {
                Move();
            }

        }

        


    }




    public void Move()
    {

        Debug.Log("posicion"+transform.position);
        Debug.Log("Destino" + vectores[objetivo]);
        transform.position=Vector3.MoveTowards(transform.position, (Vector3)vectores[objetivo], 20 * Time.deltaTime);


    }
}