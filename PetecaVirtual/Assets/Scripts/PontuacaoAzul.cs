using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontuacaoAzul : MonoBehaviour {

    public GameObject Caracteristicas;
    public GameObject Tracker;

    private CaracteristicasScript Valores;
    private ModeTrackingScript tracker;

    // Use this for initialization
    void Start()
    {
        Tracker = GameObject.Find("Mode Tracker");
        Valores = Caracteristicas.GetComponent<CaracteristicasScript>();
        tracker = Tracker.GetComponent<ModeTrackingScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Cilindro_amarelo") {
            tracker.pontuacaoRoboAzul += Valores.Cilindro_amarelo;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cilindro_anil") {
            tracker.pontuacaoRoboAzul += Valores.Cilindro_anil;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cilindro_magenta") {
            tracker.pontuacaoRoboAzul += Valores.Cilindro_magenta;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cilindro_verde") {
            tracker.pontuacaoRoboAzul += Valores.Cilindro_verde;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cubo_amarelo") {
            tracker.pontuacaoRoboAzul += Valores.Cubo_amarelo;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cubo_anil") {
            tracker.pontuacaoRoboAzul += Valores.Cubo_anil;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cubo_magenta") {
            print(tracker.pontuacaoRoboAzul);
            tracker.pontuacaoRoboAzul += Valores.Cubo_magenta;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cubo_verde") {
            tracker.pontuacaoRoboAzul += Valores.Cubo_verde;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Esfera_amarelo") {
            tracker.pontuacaoRoboAzul += Valores.Esfera_amarelo;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Esfera_anil") {
            tracker.pontuacaoRoboAzul += Valores.Esfera_anil;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Esfera_magenta") {
            tracker.pontuacaoRoboAzul += Valores.Esfera_magenta;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Esfera_verde") {
            tracker.pontuacaoRoboAzul += Valores.Esfera_verde;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "PrismaTriangular_amarelo") {
            tracker.pontuacaoRoboAzul += Valores.PrismaTriangular_amarelo;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "PrismaTriangular_anil") {
            tracker.pontuacaoRoboAzul += Valores.PrismaTriangular_anil;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "PrismaTriangular_magenta") {
            tracker.pontuacaoRoboAzul += Valores.PrismaTriangular_magenta;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "PrismaTriangular_verde") {
            tracker.pontuacaoRoboAzul += Valores.PrismaTriangular_verde;
            Destroy(objetoDeColisao.gameObject);
        }
    }
}
