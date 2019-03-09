using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dissolve : MonoBehaviour
{
    public bool diss;
    public bool undiss;
    private Material mat;
    private float height;
    private float progress;
    bool running;
    private void OnValidate()
    {
        if (diss) {
            diss = !diss;
            Disolver();
        }
        if (undiss)
        {
            undiss = !undiss;
            UnDisolver();
        }
    }
    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        height = mat.GetFloat("_dissolveSize");
        Debug.Log(height);
    }

    public void UnDisolver()
    {
        if (running == false)
        {
            mat?.SetFloat("_dissolveSize", height);
            progress = 0;
            running = true;
            StartCoroutine(UnDiss());
        }


    }

    IEnumerator UnDiss()
    {

        yield return new WaitForFixedUpdate();
        progress = progress + (0.01f + (Time.deltaTime * (progress)));
        mat.SetFloat("_progress", progress);
        if (progress < 1)
        {
            StartCoroutine(UnDiss());
        }
        else
        {
            mat?.SetFloat("_dissolveSize", 0.01f);
            progress = 0;
            yield return null;
            running = false;
        }


    }


    public void Disolver()
    {
        if (running == false)
        {
            mat?.SetFloat("_dissolveSize",height);
            progress = 1;
            running = true;
            StartCoroutine(Diss());
        }

        
    }

    IEnumerator Diss() {
        
        yield return new WaitForFixedUpdate();
        progress = progress -(0.01f+(Time.deltaTime*( progress)));
        mat.SetFloat("_progress", progress);
        if (progress > -0.01) {
            StartCoroutine(Diss());
        }
        else
        {
            mat?.SetFloat("_dissolveSize",0);
            progress = 1;
            yield return null;
            running = false;
        }
        

    }
}
