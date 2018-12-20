using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa {

    public string titulo;                           //Titulo do mapa
    [Multiline(3), Tooltip("Descreve o nivel")]     //
    public string descricao;                        //Descrição do mapa
    [Range(1, 5, order = 1)]                        //
    public int dificuldade=1;                       //Dificuldade do mapa (1 por padrão)
    public int buildIndex;                          //Referencia a cena do jogo na build
    public ModoDeJogo modoJogo;                     //Modo de jogo inerente ao mapa

    public Mapa() {}

}
 public enum ModoDeJogo
{
    Nenhum,
    Solo,
    Competição,
    Arena
}