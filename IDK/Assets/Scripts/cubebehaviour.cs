using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubebehaviour : MonoBehaviour{
    public float scalemax;
    public float scalemin;
    public float maxorbitspeed;
    public float orbitspeed;
    private Transform orbitanchor;
    private Vector3 orbitdirection;
    public float growingspeed;
    private bool isscaled=false;
    private Vector3 maxscale;
    public int health;
    private bool isalive = true;

    void CubeSettings()
    {
        orbitanchor = Camera.main.transform;
        float x = Random.Range(-1, 1f);
        float y = Random.Range(-1, 1f);
        float z = Random.Range(-1, 1f);
        orbitdirection = new Vector3(x, y, z);

        orbitspeed = Random.Range(.5f, maxorbitspeed);

        float scale = Random.Range(scalemin, scalemax);
        maxscale = new Vector3(scale, scale, scale);

    }

    void ScaleObject()
    {
        if(transform.localScale != maxscale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxscale, Time.deltaTime * growingspeed);
            isscaled = true;
        }
    }

    public bool Hit(int hitdamage)
    {
        health -= hitdamage;
        if(health>=0 && isalive)
        {
            StartCoroutine("DestroyCube");
            return true;
        }
        return false;
    }

    private IEnumerator DestroyCube()
    {
        isalive = false;
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void Update()
    {
        RotateCube();
        if (!isscaled)
        {
            ScaleObject();
        }
    }

    private void RotateCube()
    {
        transform.RotateAround(orbitanchor.position, orbitdirection, orbitspeed * Time.deltaTime);
        transform.Rotate(orbitdirection * 30 * Time.deltaTime);
    }
    private void Start()
    {
        CubeSettings();
    }
}
