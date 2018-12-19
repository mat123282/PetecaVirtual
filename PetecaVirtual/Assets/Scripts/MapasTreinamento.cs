using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapasTreinamento {
    public List<Mapa> mapas;
    public int numeroMapas;

    public  MapasTreinamento() {
        
        Mapa mapa1 = new Mapa {
            titulo = "Movimento",
            dificuldade = 1,
            descricao = "Mapa inicial para aprender sobre as movimentações básicas do robo e verificar seu código.",
            Buidindex = 1,
            modoJogo = 1
        };

        mapas.Add(mapa1);

        Mapa mapa2 = new Mapa {
            titulo = "Curva logo a frente",
            dificuldade = 1,
            descricao = "Teste suas habilidades de rotação com as curvas que você encontrará aqui.",
            Buidindex = 2,
            modoJogo = 1
        };

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