using System.Collections.Generic;

/// <summary>
/// Uma classe com valores estáticos, 
/// assim que inicializada seus valores podem ser acessados globalmente.
/// </summary>
/// <example>
/// Para acessar um mapa dos Mapas de treinamento, basta usar:
/// <c>... new MapasCompeticoes();</c> para inicializar
/// <c> MapasCompeticoes.mapas[valor] </c> buscar valor
/// </example>
public class MapasCompeticoes {
    public static List<Mapa> mapas =
        new List<Mapa>(){

            new Mapa(){//Mapa1
                titulo = "Movimento",
                dificuldade = 1,
                descricao = "Mapa inicial para aprender sobre as movimentações" +
                            " básicas do robo e verificar seu código.",
                buildIndex = 1
            },

            new Mapa(){//Mapa2
                titulo = "Curva logo a frente",
                dificuldade = 1,
                descricao = "Teste suas habilidades de rotação com " +
                            "as curvas que você encontrará aqui.",
                buildIndex = 2
            }
        };
    public int numeroMapas => mapas.Count;
}