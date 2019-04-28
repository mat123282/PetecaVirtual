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

            
        };

    public static int numeroMapas => mapas.Count;
}