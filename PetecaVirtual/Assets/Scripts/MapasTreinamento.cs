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
                buildIndex = 1,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa {
                titulo = "Aprenda a se virar",
                dificuldade = 1,
                descricao = "Aprenda os movimentos mais básicos da plataforma.",
                buildIndex = 2,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa {
                titulo = "Zipado",
                dificuldade = 2,
                descricao = "Por que? Porque esse mapa é muito apertado.",
                buildIndex = 3,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa {
                titulo = "Movimento",
                dificuldade = 2,
                descricao = "Mapa inicial para aprender sobre as movimentações " +
                            "básicas do robo e verificar seu código.",
                buildIndex = 4,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa{
                titulo = "Curva logo a frente",
                dificuldade = 3,
                descricao = "Teste suas habilidades de rotação " +
                            "com as curvas que você encontrará aqui.",
                buildIndex = 5,
                modoJogo = ModoDeJogo.Solo
            },

            new Mapa{
                titulo = "Indo direto ao ponto",
                dificuldade = 2,
                descricao = "Com ajuda de um amigo, é claro.",
                buildIndex = 6,
                modoJogo = ModoDeJogo.Cooperacao
            },

            new Mapa{
                titulo = "Cuidado ai em baixo",
                dificuldade = 3,
                descricao = "Desça as peças para o primeiro andar para consquistar pontos.",
                buildIndex = 7,
                modoJogo = ModoDeJogo.Cooperacao
            },

            new Mapa{
                titulo = "Parceria",
                dificuldade = 3,
                descricao = "Ambos os lados devem cooperar para alcançar os objetivos.",
                buildIndex = 8,
                modoJogo = ModoDeJogo.Cooperacao
            },

            new Mapa{
                titulo = "Nível 1",
                dificuldade = 1,
                descricao = "Um puzzle tão fácil que dá até vergonha se perder.",
                buildIndex = 9,
                modoJogo = ModoDeJogo.Labirinto
            },

            new Mapa{
                titulo = "Nível 2",
                dificuldade = 2,
                descricao = "Esse puzzle será impossível, eu acho.",
                buildIndex = 10,
                modoJogo = ModoDeJogo.Labirinto
            },
        };

    public static int numeroMapas => mapas.Count;
}