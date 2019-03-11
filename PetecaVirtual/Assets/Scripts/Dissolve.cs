using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dissolve : MonoBehaviour {
    private Material materialDissolvido = null;
    private float progresso;
    bool running;
    
    private void Start() {
        materialDissolvido = GetComponent<MeshRenderer>().material;
        progresso = 0;
    }
    
    public void Dissolver(int obj,int pts) {
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
            progresso = 0;
            StartCoroutine(Diss());
        }
    }

    IEnumerator Diss() {
        yield return new WaitForFixedUpdate();
        progresso = progresso + 15 * (0.01f + (Time.deltaTime * (progresso)));
        materialDissolvido.SetFloat("_progresso_shader", progresso);
        if (progresso < 0.7) {
            StartCoroutine(Diss());
        } else {
            Destroy(this.gameObject);
            yield return null;
        }
    }
}
