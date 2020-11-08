using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour{
    public float firerate;
    public float firerange;
    public float hitforce;
    public int laserdamage;
    private LineRenderer laserline;
    private bool laserlineenabled;
    private WaitForSeconds laserduration;
    private float nextfire;
    // Start is called before the first frame update
    void Start()
    {
        laserline = GetComponent<LineRenderer>();
    }


    void Fire()
    {
        Transform cam = Camera.main.transform;
        nextfire = Time.time + firerate;
        Vector3 rayorigin = cam.position;
    laserline.SetPosition(0, transform.up * -10);
        RaycastHit hit;
        if (Physics.Raycast(rayorigin, cam.forward, out hit, firerange))
        {
            laserline.SetPosition(1, hit.point);
            cubebehaviour cubecrtl = hit.collider.GetComponent<cubebehaviour>();
            if (cubecrtl != null)
            {
                if (hit.rigidbody != null)
                {
                hit.rigidbody.AddForce(-hit.normal * hitforce);
                cubecrtl.Hit(laserdamage);
                }
            }
        }
        else
        {
            laserline.SetPosition(1, cam.forward * firerange);
        }

        StartCoroutine("LaserFX");
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    private IEnumerator LaserFX()
    {
        laserline.enabled = true;
        yield return laserduration;
        laserline.enabled = false;
    }
}
