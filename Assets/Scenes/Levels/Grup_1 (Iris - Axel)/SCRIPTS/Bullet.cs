using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeDestroy=1;
    public List<string> omitTag;
    public GameObject defaultSplash;
    public List<GameObject> splash;
    public List<string> tags;
    private Dictionary<string, GameObject> spawns;

    void Start() {
        spawns = new Dictionary<string, GameObject>();
        StartCoroutine(DestoyObj(timeDestroy));
        for (int i = 0;i<splash.Count;i++) {
            spawns.Add(tags[i],splash[i]);
        }
    }

    IEnumerator DestoyObj(float tim)
    {
        yield return new WaitForSeconds(tim);
        Destroy(this.gameObject);

    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (string tagsOmit in omitTag)
        {
            if (collision.gameObject.tag.CompareTo(tagsOmit) == 0)
            {
                return;
            }
        }
        GameObject spawnEffect = defaultSplash;
        try {
            spawnEffect = spawns[collision.gameObject.tag];
        }
        catch (Exception e) {
            Debug.Log("El tag "+ collision.gameObject.tag+" no esta asignado, usando default");
        }
       
       
        Instantiate(spawnEffect,transform.position,transform.rotation);
        Destroy(this.gameObject);
    }
}
