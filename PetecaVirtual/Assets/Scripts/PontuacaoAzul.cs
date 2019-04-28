using UnityEngine;

public class PontuacaoAzul : MonoBehaviour {

    private CaracteristicasScript Valores;
    private ModeTrackingScript tracker;

    void Start() {
        Valores = FindObjectOfType<CaracteristicasScript>();
        tracker = FindObjectOfType<ModeTrackingScript>();
        if (tracker == null) tracker = new ModeTrackingScript();
    }

    private void OnTriggerEnter(Collider objetoDeColisao){
        switch (objetoDeColisao.tag) {
            case "Cilindro_amarelo":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Cilindro_amarelo);
                break;
            case "Cilindro_anil":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Cilindro_anil);
                break;
            case "Cilindro_magenta":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Cilindro_magenta);
                break;
            case "Cilindro_verde":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Cilindro_verde);
                break;
            case "Cubo_amarelo":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Cubo_amarelo);
                break;
            case "Cubo_anil":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Cubo_anil);
                break;
            case "Cubo_magenta":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Cubo_magenta);
                break;
            case "Cubo_verde":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Cubo_verde);
                break;
            case "Esfera_amarelo":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Esfera_amarelo);
                break;
            case "Esfera_anil":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Esfera_anil);
                break;
            case "Esfera_magenta":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Esfera_magenta);
                break;
            case "Esfera_verde":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.Esfera_verde);
                break;
            case "PrismaTriangular_amarelo":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.PrismaTriangular_amarelo);
                break;
            case "PrismaTriangular_anil":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.PrismaTriangular_anil);
                break;
            case "PrismaTriangular_magenta":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.PrismaTriangular_magenta);
                break;
            case "PrismaTriangular_verde":
                objetoDeColisao.GetComponent<Dissolve>().Dissolver(2, Valores.PrismaTriangular_verde);
                break;
        }
    }
}
