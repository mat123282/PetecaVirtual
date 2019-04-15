using System.Collections.Generic;

/// <summary>
/// Uma classe com valores estáticos, 
/// assim que inicializada seus valores podem ser acessados globalmente.
/// </summary>
/// <example>
/// Para acessar um mapa dos Mapas de treinamento, basta usar:
/// <c>... new MapasTreinamento();</c> para inicializar
/// <c> MapasTreinamento.mapas[valor] </c> buscar valor
/// </example>
public class MapasTreinamento {
    public static List<Mapa> mapas = 
        new List<Mapa>() {

            new Mapa {//Mapa1
                titulo = "Mapa Inicial",
                dificuldade = 1,
                descricao = "Aprenda os movimentos mais básicos da plataforma.",
                buildIndex = 4,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa {//Mapa1
                titulo = "Movimento",
                dificuldade = 2,
                descricao = "Mapa inicial para aprender sobre as movimentações " +
                            "básicas do robo e verificar seu código.",
                buildIndex = 1,
                modoJogo = ModoDeJogo.Solo
            },
        
            new Mapa{//Mapa2
                titulo = "Curva logo a frente",
                dificuldade = 2,
                descricao = "Teste suas habilidades de rotação " +
                            "com as curvas que você encontrará aqui.",
                buildIndex = 2,
                modoJogo = ModoDeJogo.Solo
            }
        };

    public static int numeroMapas => mapas.Count;
}