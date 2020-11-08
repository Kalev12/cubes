using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class spawnscript : MonoBehaviour{
    public GameObject spawner;
    public int TotalSpawner;
    public float Timetospawn;
    private GameObject[] SpawnerList;
    private bool PositionSet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnloop());
        SpawnerList = new GameObject[TotalSpawner];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private bool SetPosition()
    {
        Transform cam = Camera.main.transform;
        transform.position = cam.forward * 10;
        return true;
    }

    private IEnumerator ChangePosition()
    {
        yield return new WaitForSeconds(0.2f);
        if (!PositionSet)
        {
            if (VuforiaBehaviour.Instance.enabled)
            {
                SetPosition();
            }
        }
    }

    private IEnumerator Spawnloop()
    {
        StartCoroutine(ChangePosition());
        yield return new WaitForSeconds(1f);
        int i = 0;
        while (i < TotalSpawner - 1)
        {
            SpawnerList[i] = SpawnElement();
            i++;
            yield return new WaitForSeconds(Random.Range(Timetospawn,Timetospawn*3));
        }
        
    }
    private GameObject SpawnElement()
    {
        GameObject spawnobject = Instantiate(spawner, (Random.insideUnitSphere * 4)+transform.position,transform.rotation)as GameObject;
        float scale = Random.Range(0.5f, 2f);
        spawnobject.transform.localScale = new Vector3(scale, scale, scale);
        return spawnobject;
    }
}
