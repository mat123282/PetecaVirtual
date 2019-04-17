using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System;

//a tag abaixo adiciona uma descrição ao script
/// <summary>
/// Esse Script se mantem com a troca de cenários,
/// tornando o objeto que o carrega persistente.
///Sendo usado para manter as variaves globais do jogo
/// </summary>
public class ModeTrackingScript : MonoBehaviour {
    public GUISkin skin;
    
    public int MapaEscolhido = 0;                   // 0 - Menu. 1,2,3 - Mapas
    public ModoDeJogo ModoJogo = 0;                 // 0 - Nada. 1 - Solo. 2 - Batalha. 3 - Arena. 4 - Cooperativo
    public int TipoPontuacao = 0;                   // 0 - Por tempo. 1 - Por pontos. 2 - Por tempo e pontuacao

    public float TempoTotal = 0;                    //Tempo total inicial, em segundos
    public static float DEFAULT_INITIAL_TIME = 60;  //tempo inicial padrão
    public int pontuacaoRoboVermelho = 0;           //
    public int pontuacaoRoboAzul = 0;               //

    private GameObject tracker;                     //referência Objeto que carrega esse script
    public GameObject Pecas;                        //

    private float tempoInicio;                      //
    private float tempo;                            //
    private string segundosContador = "";           //
    private bool mostrarArquivo = false;            //
    private bool primeiraVez = true;                //
    private bool fimDeJogo = false;                 //
    private bool JogoPausado = true;                //

    private static ModeTrackingScript ModeTrack;
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);        //Torna obj persistente a troca de cenas

        if (ModeTrack == null) {
            ModeTrack = this;
        } else {
            Destroy(gameObject);
        }
    }

    NamedPipeServer PServer1;
    NamedPipeServer PServer2;


    void Start() {
        DontDestroyOnLoad(transform.gameObject);
        tracker = FindObjectOfType<ModeTrackingScript>().gameObject; //encontra o objeto que tem esse script

        PServer1 = new NamedPipeServer(@"\\.\pipe\PetServerPipe0", 0);//sread
        PServer2 = new NamedPipeServer(@"\\.\pipe\PetServerPipe1", 1);//swrite
        PServer1.Start();
        PServer2.Start();
        NamedPipeServer.OnDataReceived += SeverResponse;

        SceneManager.sceneLoaded += OnSceneLoaded;
        Time.timeScale = 0;                         //
        mostrarArquivo = false;                     //
        QualitySettings.vSyncCount = 0;             //
        Application.targetFrameRate = 60;           //
    }

    public static event EventHandler<int> OnMoveCommand;

    private void SeverResponse(object sender, string readValue)
    {
        var v = readValue.Split(';');
        if (v.Length == 3)
        {
            if (v[1] == "MOVE")
            {
                //Debug.Log("Movendo" + ((v[0] == "R1") ? "Robo Vermelho" : "Robo Azul")+" para ");
                //Debug.Log(((v[0][1]-49)<<2) | (v[2][0]-49) );
                var st = "Movendo" + ((v[0] == "R1") ? "Robo Vermelho" : "Robo Azul");
                //PServer2.SendMessage(st, PServer2.clientse);
                OnMoveCommand?.Invoke(PServer2, ((v[0][1]-49)<<2) | (v[2][0]-49) );
            }
        }

        //PServer2.SendMessage(readValue, PServer2.clientse);
    }

    private void OnApplicationQuit()
    {
        PServer1.StopServer();
        PServer2.StopServer();
        Debug.Log("SeverStoped");
    }


    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex!=0)// se a cena aberta não for o menu
        {//atualiza a ref de peças
            ConfsIniciais();

            Pecas = SceneManager.GetSceneByBuildIndex(MapaEscolhido).GetRootGameObjects().Where(o => o.name == "Pecas").ToArray().First();
        }
    }

    void Update() {
        // 0 - Por tempo. 1 - Por pontos. 2 - Por tempo e pontuacao
        if (Input.GetKeyDown(KeyCode.P) == true) {
            JogoPausado = !JogoPausado;
            Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
            if (primeiraVez == true) {
                ControlaTempo();
                primeiraVez = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.B) == true) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            Time.timeScale = 0;
            ReiniciarPontuacao();
            fimDeJogo = false;
            JogoPausado = true;
        }

        if (TipoPontuacao == 0) {
            tempo = tempoInicio - Time.time;
            if (tempo < 0) {
                tempo = 0;
                Time.timeScale = 0;
                fimDeJogo = true;
            } else if (Pecas != null) {
                if (Pecas.transform.childCount == 0) {
                    Time.timeScale = 0;
                    fimDeJogo = true;
                }
            }
        }
        else if (TipoPontuacao == 1) {
            tempo = Time.time - tempoInicio;

            if (Pecas != null) {
                if (Pecas.transform.childCount == 0) {
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
            }

            if (Pecas != null) {
                if (Pecas.transform.childCount == 0) {
                    Time.timeScale = 0;
                    fimDeJogo = true;
                }
            }
        }

        segundosContador = tempo.ToString("f3");
    }

    void OnGUI() {
        GUI.depth = 1;
        GUI.skin = skin;
              
        if (SceneManager.GetActiveScene().buildIndex != 0) {  //Se a scene não for o menu
            GUI.Box(new Rect(0, 0, Screen.width - 150, 25), "");                            //
            bool botaoArquivo = GUI.Button(new Rect(0, 0, 100, 25), "Arquivo");             //Arquivo
            bool botaoReiniciar = GUI.Button(new Rect(100, 0, 100, 25), "Reiniciar");       //Reniciar
            bool botaoPausar = GUI.Button(new Rect(200, 0, 120, 25), "Pausar/Despausar");   //Pausar/Despausar

            #region Interface
            GUI.BeginGroup(new Rect(Screen.width - 150, 0, 200, 80));                       //----------------
            if (ModoJogo == ModoDeJogo.Solo) {
                GUI.Box(new Rect(0, 0, 150, 50), "");
                GUI.contentColor = Color.white;                                             //
                GUI.Label(new Rect(20, 25, 100, 25), "<b>Pontuação: </b>");                 //Solo:<pontos>
                GUI.Label(new Rect(90, 25, 100, 25), pontuacaoRoboVermelho.ToString());     //
            } else if(ModoJogo == ModoDeJogo.Cooperacao) {
                GUI.Box(new Rect(0, 0, 150, 50), "");
                GUI.contentColor = Color.white;                                             //
                GUI.Label(new Rect(20, 25, 100, 25), "<b>Pontuação: </b>");                 //Solo:<pontos>
                GUI.Label(new Rect(90, 25, 100, 25), (pontuacaoRoboAzul + pontuacaoRoboVermelho).ToString());     //
            } else if(ModoJogo == ModoDeJogo.Competição) {
                GUI.Box(new Rect(0, 0, 150, 80), "");
                GUI.contentColor = Color.red;                                               //
                GUI.Label(new Rect(20, 25, 100, 25), "<b>Vermelho: </b>");                  //Vermelho:<pontos>
                GUI.Label(new Rect(90, 25, 100, 25), pontuacaoRoboVermelho.ToString());     //

                GUI.contentColor = Color.blue;                                              //
                GUI.Label(new Rect(45, 50, 100, 25), "<b>Azul: </b>");                      //Azul:<pontos>
                GUI.Label(new Rect(90, 50, 100, 25), pontuacaoRoboAzul.ToString());         //
            } else {
                GUI.Box(new Rect(0, 0, 150, 80), "");
                GUI.contentColor = Color.red;                                               //
                GUI.Label(new Rect(20, 25, 100, 25), "<b>Vermelho: </b>");                  //Vermelho:<pontos>
                GUI.Label(new Rect(90, 25, 100, 25), pontuacaoRoboVermelho.ToString());     //

                GUI.contentColor = Color.blue;                                              //
                GUI.Label(new Rect(45, 50, 100, 25), "<b>Azul: </b>");                      //Azul:<pontos>
                GUI.Label(new Rect(90, 50, 100, 25), pontuacaoRoboAzul.ToString());         //
            }
           
            GUI.Label(new Rect(35, 0, 50, 25), "<b>Tempo: </b>");                           //Tempo:<tempo>
            GUI.Label(new Rect(90, 0, 50, 25), segundosContador);                           //
            GUI.EndGroup();                                                                 //----------------
            #endregion

            GUI.contentColor = Color.white;
            GUI.skin = skin;
            
            if (botaoArquivo == true) mostrarArquivo = !mostrarArquivo;
            if (mostrarArquivo) {

                bool botaoMenuPrincipal = GUI.Button(new Rect(0, 25, 150, 25), "Menu Principal");
                bool botaoSair = GUI.Button(new Rect(0, 50, 150, 25), "Sair");

                if (botaoMenuPrincipal) {
                    ReiniciarPontuacao();
                    MapaEscolhido = 0;
                    SceneManager.LoadScene(0, LoadSceneMode.Single);

                } else if (botaoSair) {
                    Application.Quit();
                }
            }

            if (botaoReiniciar == true) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
                Time.timeScale = 0;
                ReiniciarPontuacao();
                fimDeJogo = false;
                JogoPausado = true;
            }

            if (botaoPausar == true) {
                JogoPausado = !JogoPausado;
                Time.timeScale = (Time.timeScale == 0) ? 1 : 0;
                if (primeiraVez == true) {
                    ControlaTempo();
                    primeiraVez = false;
                }
            }
            
            if (fimDeJogo == true) {
                GUI.Label(new Rect(Screen.width/2 - 200, Screen.height/2 - 50, 400, 100),
                    "<color=#ffa500ff><size=50><b>Fim de Partida</b></size></color>");

                if (ModoJogo == ModoDeJogo.Solo) {
                    if (TipoPontuacao == 0) {
                        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 20, 550, 100),
                        "<color=#ffa500ff><size=30><b>Você finalizou o jogo com: " +
                        pontuacaoRoboVermelho + $" ponto{((pontuacaoRoboVermelho != 1) ? ("s") : "")}!</b></size></color>");
                    } else if (TipoPontuacao == 1) {
                        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 20, 550, 100),
                        "<color=#ffa500ff><size=30><b>Você finalizou o jogo com: " +
                        segundosContador + " segundos!</b></size></color>");
                    } else if(TipoPontuacao == 2) {
                        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 20, 550, 100),
                        "<color=#ffa500ff><size=30><b>Você finalizou o jogo com: " +
                        pontuacaoRoboVermelho + $" ponto{((pontuacaoRoboVermelho != 1) ? ("s") : "")}!" +
                        $"em" + segundosContador + "segundos</b></size></color>");
                    }
                    
                } else if (ModoJogo == ModoDeJogo.Cooperacao) {
                    if (TipoPontuacao == 0) {
                        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 20, 550, 100),
                        "<color=#ffa500ff><size=30><b>Você finalizou o jogo com: " +
                        (pontuacaoRoboVermelho + pontuacaoRoboAzul) + $" ponto(s)!</b></size></color>");
                    } else if (TipoPontuacao == 1) {
                        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 20, 550, 100),
                        "<color=#ffa500ff><size=30><b>Você finalizou o jogo com: " +
                        segundosContador + " segundos!</b></size></color>");
                    } else if (TipoPontuacao == 2) {
                        GUI.Label(new Rect(Screen.width / 2 - 275, Screen.height / 2 + 20, 550, 100),
                        "<color=#ffa500ff><size=30><b>Você finalizou o jogo com: " +
                        (pontuacaoRoboVermelho + pontuacaoRoboAzul) + $" pontos!"+
                        $"em" + segundosContador + "segundos</b></size></color>");
                    }
                } else if (ModoJogo == ModoDeJogo.Competição) {
                    if (pontuacaoRoboAzul > pontuacaoRoboVermelho) {
                        GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 20, 400, 100),
                            "<color=#ffa500ff><size=30><b>O time " + "Azul" +
                            " ganhou!" + "</b></size></color>");
                    } else {
                        GUI.TextArea(new Rect(Screen.width / 2 - 200, Screen.height / 2 + 20, 400, 100),
                            "<color=#ffa500ff><size=30><b>O time " + "Vermelho" +
                            " ganhou!" + "</b></size></color>");
                    }                   
                }
            } else {
                if (JogoPausado == true) {
                    GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 50, 400, 100),
                        "<color=#ffa500ff><size=50><b>Partida Pausada</b></size></color>");
                }
            }

        }
    }

    public void IniciarMapa()
    {
        SceneManager.LoadScene(MapaEscolhido, LoadSceneMode.Single);
        ConfsIniciais();
    }

    private void ConfsIniciais()
    {
        mostrarArquivo = false;
        fimDeJogo = false;
        JogoPausado = true;
        primeiraVez = true;
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
        if (TipoPontuacao == 1) {
            tempoInicio = Time.time;
        } else {
            tempoInicio = Time.time + TempoTotal;
        }

    }



    
}
