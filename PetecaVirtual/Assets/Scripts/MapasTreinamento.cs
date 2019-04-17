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

            new Mapa {
                titulo = "Mapa Inicial",
                dificuldade = 1,
                descricao = "Aprenda os movimentos mais básicos da plataforma.",
                buildIndex = 4,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa {
                titulo = "Aprenda a se virar",
                dificuldade = 1,
                descricao = "Aprenda os movimentos mais básicos da plataforma.",
                buildIndex = 5,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa {
                titulo = "Zipado",
                dificuldade = 2,
                descricao = "Por que? Porque esse mapa é muito apertado.",
                buildIndex = 6,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa {
                titulo = "Tá bem?",
                dificuldade = 1,
                descricao = "O mapa parece uma resposta pra isso.",
                buildIndex = 7,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa {
                titulo = "Movimento",
                dificuldade = 2,
                descricao = "Mapa inicial para aprender sobre as movimentações " +
                            "básicas do robo e verificar seu código.",
                buildIndex = 1,
                modoJogo = ModoDeJogo.Solo
            },
        
            new Mapa{
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