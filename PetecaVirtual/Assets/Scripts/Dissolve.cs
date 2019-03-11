using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dissolve : MonoBehaviour {
    private Material mat = null;
    private float height;
    private float progress;
    bool running;
    
    private void Start() {
        mat = GetComponent<MeshRenderer>().material;
        height = mat.GetFloat("_dissolveSize");
    }
    
    public void Disolver(int obj,int pts) {
        if (running == false) {
            running = true;
            var tracker = FindObjectOfType<ModeTrackingScript>();
            if (!(tracker == null))
                switch (obj) {
                    case 1:
                        tracker.pontuacaoRoboVermelho += pts;
                        break;
                    case 2:
                        tracker.pontuacaoRoboAzul += pts;
                        break;
                }

            mat?.SetFloat("_dissolveSize", height);
            progress = 0;
            StartCoroutine(Diss());
            Debug.Log($"pontos adicionados ({pts})");
        }
    }

    IEnumerator Diss() {

        yield return new WaitForFixedUpdate();
        progress = progress + 2 * (0.01f + (Time.deltaTime * (progress)));
        mat.SetFloat("_progress", progress);
        if (progress < 1) {
            StartCoroutine(Diss());
        }
        else {
            mat?.SetFloat("_dissolveSize", 0);
            progress = 0;
            yield return null;
            //running = false;
            Destroy(this.gameObject);
        }


    }
}
