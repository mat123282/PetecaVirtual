using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

//a tag abaixo adiciona uma descrição ao script
/// <summary>
/// Esse Script se mantem com a troca de cenários,
/// tornando o objeto que o carrega persistente.
///Sendo usado para manter as variaves globais do jogo
/// </summary>
public class ModeTrackingScript : MonoBehaviour
{
    public GUISkin skin;

    public int MapaEscolhido = 0;                   // 0 - Menu. 1,2,3 - Mapas
    public ModoDeJogo ModoJogo = 0;                 // 0 - Nada. 1 - Solo. 2 - Batalha. 3 - Arena
    public int TipoPontuacao = 0;                   // 0 - Por tempo. 1 - Por pontos. 2 - Por tempo e pontuacao

    public float TempoTotal = 0;                    //
    public int pontuacaoRoboVermelho = 0;           //
    public int pontuacaoRoboAzul = 0;               //

    private GameObject tracker;                     //referência Objeto que carrega esse script
    public GameObject Pecas;                        //

    private float tempoInicio;
    private float tempo;
    private string segundosContador = "";
    private bool mostrarArquivo = false;
    private bool primeiraVez = true;
    private bool fimDeJogo = false;

    void Start() {
        DontDestroyOnLoad(transform.gameObject);                    //Torna obj persistente a troca de cenas
        tracker = FindObjectOfType<ModeTrackingScript>().gameObject;//encontra o objeto que tem esse script
        Pecas = GameObject.Find("Pecas");

        Time.timeScale = 0;
        mostrarArquivo = false;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update() {

        if (TipoPontuacao == 0) { 
            tempo = tempoInicio - Time.time;
            if (tempo < 0) {
                tempo = 0;
                Time.timeScale = 0;
                fimDeJogo = true;
            }
        }
        else if (TipoPontuacao == 1) {
            tempo = Time.time - tempoInicio;

            if (Pecas != null)
            {
                if (Pecas.transform.childCount == 0)
                {
                    Time.timeScale = 0;
                    fimDeJogo = true;
                }
            }
            
            
        }
        else if (TipoPontuacao == 2) {
            tempo = tempoInicio - Time.time;
            if (tempo < 0) {
                tempo = 0;
                Time.timeScale = 0;
                fimDeJogo = true;
                if (Pecas != null) 
                {
                    if (Pecas.transform.childCount == 0)
                    {
                        Time.timeScale = 0;
                        fimDeJogo = true;
                    }
                }
            }
        }

        segundosContador = tempo.ToString("f3");
    }

    void OnGUI()
    {
        GUI.depth = 1;
        GUI.skin = skin;
              

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            GUI.Box(new Rect(0, 0, Screen.width - 150, 25), "");
            bool botaoArquivo = GUI.Button(new Rect(0, 0, 100, 25), "Arquivo");
            bool botaoReiniciar = GUI.Button(new Rect(100, 0, 100, 25), "Reiniciar");
            bool botaoPausar = GUI.Button(new Rect(200, 0, 120, 25), "Pausar/Despausar");

            GUI.BeginGroup(new Rect(Screen.width - 150, 0, 200, 80));
            GUI.Box(new Rect(0, 0, 150, 80), "");
            GUI.Label(new Rect(35, 0, 50, 25), "<b>Tempo: </b>");
            GUI.Label(new Rect(90, 0, 50, 25), segundosContador);
            GUI.contentColor = Color.red;
            GUI.Label(new Rect(20, 25, 100, 25), "<b>Vermelho: </b>");
            GUI.Label(new Rect(90, 25, 100, 25), pontuacaoRoboVermelho.ToString());
            GUI.contentColor = Color.blue;
            GUI.Label(new Rect(45, 50, 100, 25), "<b>Azul: </b>");
            GUI.Label(new Rect(90, 50, 100, 25), pontuacaoRoboAzul.ToString());
            GUI.EndGroup();
            GUI.contentColor = Color.white;
            GUI.skin = skin;
            
            if (botaoArquivo == true) {
                if (mostrarArquivo == true) {
                    mostrarArquivo = false;
                } else {
                    mostrarArquivo = true;
                }
            }
            if (botaoReiniciar == true) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
                Time.timeScale = 0;
                Pecas = SceneManager.GetSceneByBuildIndex(MapaEscolhido).GetRootGameObjects().Where(o => o.name == "Pecas").ToArray().First();
                ReiniciarPontuacao();
                fimDeJogo = false;
            }
            if (botaoPausar == true) {
                if (Time.timeScale == 0) {
                    Time.timeScale = 1;
                } else {
                    Time.timeScale = 0;
                }
                if (primeiraVez == true) {
                    ControlaTempo();
                    Pecas = SceneManager.GetSceneByBuildIndex(MapaEscolhido).GetRootGameObjects().Where(o => o.name == "Pecas").ToArray().First();
                    primeiraVez = false;
                }
            }

            if (mostrarArquivo) {
                bool botaoMenuPrincipal = GUI.Button(new Rect(0, 25, 150, 25), "Menu Principal");
                bool botaoSair = GUI.Button(new Rect(0, 50, 150, 25), "Sair");

                if (botaoMenuPrincipal) {
                    ReiniciarPontuacao();
                    SceneManager.LoadScene(0, LoadSceneMode.Single);

                } else if (botaoSair) {
                    Application.Quit();
                }
            }

            if (fimDeJogo == true) {
                GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height/2 - 50, 400, 100), "<color=#ffa500ff><size=50><b>Fim de Partida</b></size></color>");
                if (ModoJogo == ModoDeJogo.Solo) {
                    GUI.TextArea(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 20, 550, 100), "<color=#ffa500ff><size=30><b>Você finalizou o jogo em: " +
                        segundosContador + "</b></size></color>");
                }
                else if (ModoJogo == ModoDeJogo.Competição) {
                    if (pontuacaoRoboAzul > pontuacaoRoboVermelho) {
                        GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 20, 400, 100), "<color=#ffa500ff><size=30><b>O time " + "Azul" +
                        " ganhou!" + "</b></size></color>");
                    } else {
                        GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 20, 400, 100), "<color=#ffa500ff><size=30><b>O time " + "Vermelho" +
                        " ganhou!" + "</b></size></color>");
                    }
                    
                }
                else if (ModoJogo == ModoDeJogo.Arena) {
                    if (pontuacaoRoboAzul > pontuacaoRoboVermelho) {
                        GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 20, 400, 100), "<color=#ffa500ff><size=30><b>O time " + "Azul" +
                        " ganhou!" + "</b></size></color>");
                    } else {
                        GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 20, 400, 100), "<color=#ffa500ff><size=30><b>O time " + "Vermelho" +
                        " ganhou!" + "</b></size></color>");
                    }
                }
            }

        }
    }

    public void IniciarMapa() {
        SceneManager.LoadScene(MapaEscolhido, LoadSceneMode.Single);
        ControlaTempo();
    }

    private void ReiniciarPontuacao() {
        if (ModoJogo == ModoDeJogo.Solo) {
            pontuacaoRoboAzul = 0;
            pontuacaoRoboVermelho = 0;
            ControlaTempo();
        }
        fimDeJogo = false;
    }

    private void ControlaTempo() {
        if (TempoTotal == 0) {
            tempoInicio = Time.time;
        } else if (TempoTotal > 0) {
            tempoInicio = Time.time + TempoTotal;
        }
    }
}
