using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapasTreinamento {
    public List<Mapa> mapas;
    public int numeroMapas;

    public  MapasTreinamento() {
        Mapa mapa1 = new Mapa();
        mapa1.titulo = "Movimento";
        mapa1.dificuldade = 1;
        mapa1.descricao = "Mapa inicial para aprender sobre as movimentações básicas do robo e verificar seu código.";
        mapa1.Buidindex = 1;
        mapa1.modoJogo = 1;
        
        mapas.Add(mapa1);

        Mapa mapa2 = new Mapa();
        mapa2.titulo = "Curva logo a frente";
        mapa2.dificuldade = 1;
        mapa2.descricao = "Teste suas habilidades de rotação com as curvas que você encontrará aqui.";
        mapa2.Buidindex = 2;
        mapa2.modoJogo = 1;

        mapas.Add(mapa2);

        numeroMapas = mapas.Count;
    }

    //    Mapa mapa = new Mapa();
    //    mapa.titulo = "Movimento";
    //    mapa.dificuldade = 1;
    //    mapa.descricao = "Mapa inicial para aprender sobre as movimentações básicas do robo e verificar seu código.";
    //    mapa.Buidindex = 1;

    //    mapas.Add(mapa);
}