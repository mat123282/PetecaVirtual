﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontuacaoVermelho : MonoBehaviour {

    public GameObject Caracteristicas;
    public GameObject Tracker;

    private CaracteristicasScript Valores;
    private ModeTrackingScript tracker;

    // Use this for initialization
    void Start () {
        Tracker = GameObject.Find("Mode Tracker");
        Valores = Caracteristicas.GetComponent<CaracteristicasScript>();
        tracker = Tracker.GetComponent<ModeTrackingScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Cilindro_amarelo") {
            tracker.pontuacaoRoboVermelho += Valores.Cilindro_amarelo;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cilindro_anil") {
            tracker.pontuacaoRoboVermelho += Valores.Cilindro_anil;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cilindro_magenta") {
            tracker.pontuacaoRoboVermelho += Valores.Cilindro_magenta;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cilindro_verde") {
            tracker.pontuacaoRoboVermelho += Valores.Cilindro_verde;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cubo_amarelo") {
            tracker.pontuacaoRoboVermelho += Valores.Cubo_amarelo;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cubo_anil") {
            tracker.pontuacaoRoboVermelho += Valores.Cubo_anil;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cubo_magenta") {
            print(tracker.pontuacaoRoboVermelho);
            tracker.pontuacaoRoboVermelho += Valores.Cubo_magenta;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Cubo_verde") {
            tracker.pontuacaoRoboVermelho += Valores.Cubo_verde;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Esfera_amarelo") {
            tracker.pontuacaoRoboVermelho += Valores.Esfera_amarelo;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Esfera_anil") {
            tracker.pontuacaoRoboVermelho += Valores.Esfera_anil;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Esfera_magenta") {
            tracker.pontuacaoRoboVermelho += Valores.Esfera_magenta;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "Esfera_verde") {
            tracker.pontuacaoRoboVermelho += Valores.Esfera_verde;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "PrismaTriangular_amarelo") {
            tracker.pontuacaoRoboVermelho += Valores.PrismaTriangular_amarelo;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "PrismaTriangular_anil") {
            tracker.pontuacaoRoboVermelho += Valores.PrismaTriangular_anil;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "PrismaTriangular_magenta") {
            tracker.pontuacaoRoboVermelho += Valores.PrismaTriangular_magenta;
            Destroy(objetoDeColisao.gameObject);
        } else if (objetoDeColisao.tag == "PrismaTriangular_verde") {
            tracker.pontuacaoRoboVermelho += Valores.PrismaTriangular_verde;
            Destroy(objetoDeColisao.gameObject);
        }
    }
}
