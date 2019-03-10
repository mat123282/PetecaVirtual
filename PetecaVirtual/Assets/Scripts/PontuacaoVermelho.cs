using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontuacaoVermelho : MonoBehaviour {


    private CaracteristicasScript Valores;
    private ModeTrackingScript tracker;

    void Start() {
        Valores = FindObjectOfType<CaracteristicasScript>();
        tracker = FindObjectOfType<ModeTrackingScript>();
    }

    private void OnTriggerEnter(Collider objetoDeColisao) {
        switch (objetoDeColisao.tag) {
            case "Cilindro_amarelo":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Cilindro_amarelo;
                break;
            case "Cilindro_anil":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Cilindro_anil;
                break;
            case "Cilindro_magenta":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Cilindro_magenta;
                break;
            case "Cilindro_verde":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Cilindro_verde;
                break;
            case "Cubo_amarelo":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Cubo_amarelo;
                break;
            case "Cubo_anil":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Cubo_anil;
                break;
            case "Cubo_magenta":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Cubo_magenta;
                break;
            case "Cubo_verde":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Cubo_verde;
                break;
            case "Esfera_amarelo":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Esfera_amarelo;
                break;
            case "Esfera_anil":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Esfera_anil;
                break;
            case "Esfera_magenta":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Esfera_magenta;
                break;
            case "Esfera_verde":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.Esfera_verde;
                break;
            case "PrismaTriangular_amarelo":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.PrismaTriangular_amarelo;
                break;
            case "PrismaTriangular_anil":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.PrismaTriangular_anil;
                break;
            case "PrismaTriangular_magenta":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.PrismaTriangular_magenta;
                break;
            case "PrismaTriangular_verde":
                //Destroy(objetoDeColisao.gameObject);
                objetoDeColisao.GetComponent<Dissolve>()?.Disolver();
                tracker.pontuacaoRoboVermelho += Valores.PrismaTriangular_verde;
                break;
        }
    }
}
