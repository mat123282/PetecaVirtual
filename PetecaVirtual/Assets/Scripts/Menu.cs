using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    // FIGURAS
    public Texture2D LogoPetecaVirtual; // Logo do PetVirtual em .psd
    public Texture2D LogoPeteca;
    public Texture2D LogoPetecaDesafios;
    public Texture2D LogoVirtualVex;

    // Tracker
    public GameObject Tracker;

    // GUI DESIGN
    public GUISkin Skin; // Design para a GUI
    public GUISkin ControlSkin; // Design para a GUI
    public GUISkin Transparent; // Design para a GUI
    public GUISkin ConteudoMenu;
    public GUISkin ConteudoBotoes;

    // TOOLBAR
    private string[] Topicos = { "Inicio", "Treinamento", "Mapas", "Updates & Créditos" }; // String dos topicos do Toolbar
    private int BotaoSelecionadoToolbar = 0;

    // JANELAS
    private bool mostrarJanelaUpdate;
    private bool mostrarJanelaIrParaMapa;
    private bool botaoTempo = false;
    private bool botaoPontuacao = false;

    private string PegarVersao = "";
    private WWW VersaoUpdate;
    private Rect janelaUpdate;
    private Rect janelaIrParaMapa;

    // CONFIGURACOES COM O SITE
    public string textoNoticias = "Carregando...";
    public string textoLogAtualizacao = "Carregando...";
    private ModeTrackingScript tracker;
    private WWW siteNoticias;
    private WWW siteLogAtualizacao;

    public Vector2 ScrollPosition = Vector2.down;

    public string stringToEdit = "1000";

    public WWW VersaoUpdate1
    {
        get
        {
            return VersaoUpdate;
        }

        set
        {
            VersaoUpdate = value;
        }
    }

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    IEnumerator Start()
    {
        
        tracker = Tracker.GetComponent<ModeTrackingScript>();
        siteNoticias = new WWW("http://www.sorocaba.unesp.br/Home/PaginaDocentes/PET-ECA/petecavirtualnoticias.txt");
        while (!siteNoticias.isDone) yield return null;
        yield return siteNoticias;
        StartCoroutine(CarregarVersaoUpdate());
        StartCoroutine(CarregarLogUpdate());
        textoNoticias = siteNoticias.text;
        janelaUpdate = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 200);
        janelaIrParaMapa = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 200);
        mostrarJanelaUpdate = false;
        mostrarJanelaIrParaMapa = false;
    }

    /// <summary>
    /// Main loop
    /// </summary>
    void Update() {

    }

    /// <summary>
    /// Draw the Menu GUI
    /// </summary>
    void OnGUI()
    {
        GUI.skin = Transparent; // Design para as imagens
        GUI.Box(new Rect(20, 30, 440, 88), LogoPetecaVirtual); // Coloca o Logo do PetecaVirtual
        GUI.Label(new Rect(Screen.width - 230, 20, 210, 80), "Página Inicial"); // Coloca o escrito "Página Inicial" no canto superior direito
        GUI.Label(new Rect(20, Screen.height - 40, 100, 35), Constants.VERSION, "versionlabel"); // Coloca a versão do programa no canto inferior esquerdo

        GUI.skin = Skin;  // Design para o Grupo e o Toolbar

        #region GroupJanelas
        GUI.BeginGroup(new Rect(20, 100, Screen.width - 40, Screen.height - 150)); // Cria um retângulo para colocar os conteudos do Menu
        BotaoSelecionadoToolbar = GUI.Toolbar(new Rect(40, 0, Screen.width - 150, 50), BotaoSelecionadoToolbar, Topicos); // Criar uma Toolbar e devolve o index do botao selecionado.

        GUI.skin = ControlSkin; // Design para o conteudo

        if (BotaoSelecionadoToolbar == 0) { //Inicio
            GUI.Box(new Rect(5, 65, 400, 350), "Notícias", "score");

            if (siteNoticias==null) {//sit.isDone
                GUI.Box(new Rect(10, 90, 390, 320), "Carregando...");
            } else {
                GUI.Box(new Rect(10, 90, 390, 320), textoNoticias, "news");
            }
            GUI.Box(new Rect(415, 65, 300, 120), "Documentação", "score");
            GUI.Label(new Rect(415, 90, 290, 110), "A documentação do PetecaVirtual fornece ajuda completa sobre todos os recursos disponíveis para se utilizar " +
                "na programação do seu robô.");
            bool help = GUI.Button(new Rect(415, 155, 130, 25), "Ver Documentação");

            GUI.Box(new Rect(730, 65, 300, 120), "Website", "score");
            GUI.Label(new Rect(735, 90, 290, 110), "Visite o site do PETECA para descobrir mais sobre o grupo e ficar ligado no que cada programa tem a oferecer.");
            bool site = GUI.Button(new Rect(735, 155, 130, 25), "Ver o site");

            if (help)
                Application.OpenURL("https://sites.google.com/site/virtualvex/knowledge-base"); // MUDAR O SITE
            if (site)
                Application.OpenURL("http://www.sorocaba.unesp.br/#!/peteca");

        }
        else if (BotaoSelecionadoToolbar == 1) { //Treinamento

            ScrollPosition = GUI.BeginScrollView(
                                new Rect(15, 65, 800, 400), ScrollPosition,
                                new Rect(0, 0, 750, MapasTreinamento.numeroMapas*72),
                                false, true);

            GUI.skin = ConteudoBotoes;

            for (int i = 0; i < MapasTreinamento.numeroMapas; i++)
            {
                Mapa mapa = MapasTreinamento.mapas[i];
                bool pressed=GUI.Button(new Rect(0, 70*i, 780, 60), 
                                "<b>1. "+mapa.titulo+"</b> \nModo: Solo\tDificuldade: "+
                                mapa.dificuldade+" de 5\n"+mapa.descricao);

                if (pressed==true)
                {
                    if (mapa.buildIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        tracker.MapaEscolhido = mapa.buildIndex;
                        tracker.ModoJogo = mapa.modoJogo;
                    }
                    else
                    {
                        print("seu adm errou a build index");
                    }
                }
            }
           
            GUI.EndScrollView();

            GUI.skin = ControlSkin; // Design para o conteudo

            bool VerificaMapaSelecionado = GUI.Button(new Rect(975, 370, 100, 30), "Selecionar");
            if (VerificaMapaSelecionado) {
                mostrarJanelaIrParaMapa = true;
            }  
            
            GUI.BeginGroup(new Rect(850, 90, 350, 250));
            GUI.skin = ConteudoMenu;
            GUI.Box(new Rect(5, 5, 340, 240), "Modo de Partida");

            GUI.skin = ControlSkin; // Design para o conteudo
            botaoTempo = GUI.Toggle(new Rect(50, 60, 220, 30), botaoTempo, "<size=18>Por tempo</size>");
            botaoPontuacao = GUI.Toggle(new Rect(50, 160, 220, 30), botaoPontuacao, "<size=18>Por pontuação</size>");

            if ((botaoTempo == true) && (botaoPontuacao == true)) {
                tracker.TipoPontuacao = 2;
                GUI.Label(new Rect(90, 110, 70, 60), "<size=18>Tempo: </size>");
                tracker.TempoTotal = int.Parse(GUI.TextField(new Rect(160, 115, 60, 20), tracker.TempoTotal.ToString()));
            } else if ((botaoTempo == true)) {
                tracker.TipoPontuacao = 0;
                GUI.Label(new Rect(90, 110, 70, 60), "<size=18>Tempo: </size>");
                tracker.TempoTotal = int.Parse(GUI.TextField(new Rect(160, 115, 60, 20), tracker.TempoTotal.ToString()));
            } else if ((botaoPontuacao == true)) {
                tracker.TipoPontuacao = 1;
                tracker.TempoTotal = 0;
            } else {
                tracker.TipoPontuacao = 1;
                tracker.TempoTotal = 0;
            }

            GUI.EndGroup();           
        }
        else if (BotaoSelecionadoToolbar == 2) { //mapas
            //GUI.skin = ControlSkin;
            //ScrollPosition = GUI.BeginScrollView(new Rect(15, 65, 800, 400), ScrollPosition, new Rect(0, 0, 750, 600), false, true);

            // Make four buttons - one in each corner. The coordinate system is defined
            // by the last parameter to BeginScrollView.
            //GUI.Button(new Rect(0, 0, 100, 20), "Top-left");
            //GUI.Button(new Rect(700, 0, 100, 20), "Top-right");
            //GUI.Button(new Rect(0, 540, 100, 20), "Bottom-left");
            //GUI.Button(new Rect(700, 540, 100, 20), "Bottom-right");

            // End the scroll view that we began above.
            //GUI.EndScrollView();

            //GUI.Button(new Rect(900, 200, 130, 50), "Botao1");
            //GUI.Button(new Rect(1100, 200, 130, 50), "Botao2");
            //GUI.Button(new Rect(1000, 300, 130, 50), "Botao3");
        }
        else if (BotaoSelecionadoToolbar == 3) { //Updates
            GUI.Box(new Rect(5, 65, 400, 350), "Notas de Atualização", "score");
            if (siteLogAtualizacao != null)//caso o site já tenha sido declarado...
            {
                if (!siteLogAtualizacao.isDone)
                {
                    GUI.Box(new Rect(10, 90, 390, 320), "Carregando...");
                }
                else
                {
                    GUI.Box(new Rect(10, 90, 390, 320), textoLogAtualizacao, "news");
                }
            }

            GUI.Box(new Rect(415, 65, 300, 120), "Verificar Atualizações", "score");
            GUI.Label(new Rect(415, 90, 290, 110), "Tenha sempre o PetaVirtual atualizado para correção de bugs e novas ferramentas.");

            bool VerificarAtualizacao = GUI.Button(new Rect(420, 155, 150, 25), "Verificar");
            if (VerificarAtualizacao) {
                mostrarJanelaUpdate = true;
            }

            GUI.Box(new Rect(735, 65, 400, 150), "Créditos", "score");
            GUI.Label(new Rect(740, 90, 390, 140), "O PetecaVirtual foi desenvolvido pelo programa PETECA-Desafios. O objetivo do PetecaVirtual é promover competições de robótica para os alunos de Engenharia de Controle " +
                "e Automação no campus de Sorocaba da UNESP. " +
                "Agradecemos ao projeto open-source VirtualVex. Para mais informações sobre o VirtualVex, " +
                "você pode acessar o site através do link: https://sites.google.com/site/virtualvex/.\n" +
                "Créditos da música: Esley Joubert Callai Bitencourt.");


            GUI.skin = Transparent; // Design para o conteudo
            GUI.Box(new Rect(735, 250, 150, 225), LogoPeteca);
            GUI.Box(new Rect(885, 250, 150, 225), LogoPetecaDesafios);
            GUI.Box(new Rect(1035, 300, 150, 225), LogoVirtualVex);
        }

        GUI.skin = ControlSkin;

        if (mostrarJanelaUpdate) {
            janelaUpdate = GUI.Window(1, janelaUpdate, VerificarAtualizacao, "Verificar Atualizações");
        }
        if (mostrarJanelaIrParaMapa)
        {
            janelaIrParaMapa = GUI.Window(2, janelaUpdate, VerificarIrParaMapa, "Ir para o Mapa?");
        }
       

        GUI.EndGroup(); // Termina o Group
        #endregion

        bool exit = GUI.Button(new Rect(Screen.width - 150, Screen.height - 60, 120, 40), "Sair PetecaVirtual");
        if (exit) {
            Application.Quit();
        }
    }

    /// <summary>
    /// Access the update server
    /// </summary>
    /// <returns></returns>
    IEnumerator CarregarVersaoUpdate()
    {
        VersaoUpdate1 = new WWW("http://www.sorocaba.unesp.br/Home/PaginaDocentes/PET-ECA/petecavirtualversaoatual.txt");
        while (!VersaoUpdate1.isDone) yield return null;
        yield return VersaoUpdate1;
        PegarVersao = VersaoUpdate1.text;
    }
    IEnumerator CarregarLogUpdate()
    {
        VersaoUpdate1 = new WWW("http://www.sorocaba.unesp.br/Home/PaginaDocentes/PET-ECA/petecavirtualversaoatual.txt");
        siteLogAtualizacao = new WWW("http://www.sorocaba.unesp.br/Home/PaginaDocentes/PET-ECA/petecavirtualnotasatualizacao.txt");
        yield return siteLogAtualizacao;
        textoLogAtualizacao = siteLogAtualizacao.text;
    }
    void VerificarAtualizacao(int windowID) {
        string textoDaJanela = "Tentando conectar com o servidor...";
        if (VersaoUpdate1!=null)
        {
            if (Constants.VERSION.Equals(PegarVersao)) {
                textoDaJanela = "Nenhuma atualização encontrada. Você possui a versão \nmais atualizada do PetecaVirtual em seu computador.";
            } else {
                textoDaJanela = "Atualização disponível: " + PegarVersao;
                bool download = GUI.Button(new Rect(30, 50, 100, 25), "Download");
                if (download) {
                    Application.OpenURL("https://sites.google.com/site/virtualvex/downloads"); // ALTERAR O SITE DEPOIS
                }
            }
        }
        GUI.Label(new Rect(30, 30, Screen.width - 30, Screen.height - 30), textoDaJanela);
        bool Voltar = GUI.Button(new Rect(30, 150, 80, 25), "Voltar") || GUI.Button(new Rect(355, 0, 40, 18), "", "close");
        if (Voltar) {
            mostrarJanelaUpdate = false;
        }
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }
    void VerificarIrParaMapa(int windowID) {
        GUI.Label(new Rect(30, 30, 300, 150), "Você deseja ir para o Mapa? Verifique se as " +
            "configurações colocadas estão de acordo com a sua preferência.");
        bool irParaMapa = GUI.Button(new Rect(30, 150, 80, 25), "Sim");
        bool voltar = GUI.Button(new Rect(140, 150, 80, 25), "Não") || GUI.Button(new Rect(355, 0, 40, 18), "", "close");
        if (voltar) {
            mostrarJanelaIrParaMapa = false;
        } else if(irParaMapa) {
            mostrarJanelaIrParaMapa = false;
            Tracker.GetComponent<ModeTrackingScript>().IniciarMapa();
        }
    }
}
