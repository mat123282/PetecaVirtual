using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapa {

    public string titulo;
    [Range(1, 5, order = 1)]
    public int dificuldade;
    [Multiline(1), Tooltip("Descreve o nivel")]
    public string descricao;
    public int Buidindex;
    public int modoJogo;

    public Mapa() {

    }

}
