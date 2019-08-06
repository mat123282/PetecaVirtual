using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtLibControl
{

    public NamedPipeServer PServer1;
    public NamedPipeServer PServer2;

    public static event EventHandler<UserAction> OnCommandCalled;

    public static Queue<UserAction> userActions = new Queue<UserAction>();

    public struct UserAction
    {
        public string type;
        //0 -   Movement
        //1 -   Rotation
        //2 -   Pause
        //3 -   Restart
        //4 -   Claw
        public int target;
        //0 -   RedBot
        //1 -   BlueBot
        public float value;
        public UserAction(string type, int target, float value)
        { this.type = type; this.target = target; this.value = value; }

        public UserAction(string type, int target)
        { this.type = type; this.target = target; this.value = -1; }

        public UserAction(string type)
        { this.type = type; this.target = -1; this.value = -1; }

    }

    public void INIT()
    {
        PServer1 = new NamedPipeServer(@"\\.\pipe\PetServerPipe0", 0);//sread
        PServer2 = new NamedPipeServer(@"\\.\pipe\PetServerPipe1", 1);//swrite
        PServer1.Start();
        PServer2.Start();
        NamedPipeServer.OnDataReceived += SeverResponse;//sempre que onDataR.. for chamado , chama serverRes.. com o parâmetro 
    }


    private void SeverResponse(object sender, string readValue)
    {
        string st = $"Recebido->{readValue}";
        UserAction action = new UserAction();

        var v = readValue.Split(' ');

        switch (v[0])
        {
            case "MOVE":
                if (v.Length == 3)
                {
                    st = $"Movendo {((v[1] == "R0") ? "Robo Vermelho" : "Robo Azul")} {float.Parse(v[2])} unidades";

                    action = new UserAction("move", (v[1] == "R1") ? 1 : 0, float.Parse(v[2]));
                }
                break;
            case "ROTATE":
                if (v.Length == 3)
                {
                    st = $"Rotacionando {((v[1] == "R0") ? "Robo Vermelho" : "Robo Azul")} {float.Parse(v[2])} unidades";

                    action = new UserAction("rot", (v[1] == "R1") ? 1 : 0, float.Parse(v[2]));
                }
                break;
            case "PAUSE":

                st = $"Pausando...";
                action = new UserAction("pause");

                break;
            case "RESTART":

                st = $"Reininciando o nivel...";
                action = new UserAction("restart");

                break;
            case "GARRA":
                if (v.Length == 2)
                {
                    st = $"Agindo na garra do {((v[1] == "R0") ? "Robo Vermelho" : "Robo Azul")}";
                    action = new UserAction("garra", (v[1] == "R1") ? 1 : 0);
                }
                break;
            case "CAM"://não implementada
                if(v.Length == 2)
                {
                    st = $"Alternando para camera {v[1]}";
                    action = new UserAction("cam", -1, float.Parse(v[1]));
                }
                break;
            case "WAIT"://não implementada
                if (v.Length == 2)
                {
                    st = $"Segurando a fila por  {v[1]} segundos";
                    action = new UserAction("hold", -1, float.Parse(v[1]));
                }
                break;
            case "getTIME":

                action = new UserAction("getTIME");

                break;
            case "DEQUEUE":
                userActions.Clear();
                break;
            default:
                break;
        }
        userActions.Enqueue(action);

        //Debug.Log($"{st}\n{userActions.Count} ações na fila");

        if (userActions.Count == 1)
            MoveActionQueue();

        //OnCommandReceived?.Invoke(PServer2, action);
        //PServer2.SendMessage(readValue, PServer2.clientse);
        //Debug.Log(readValue);
    }

    public static void DeQueueAction()
    {

        if (userActions.Count > 0)
            userActions.Dequeue();
        if (userActions.Count > 0)
            MoveActionQueue();

        
    }

    public static void MoveActionQueue()
    {
        //Debug.Log($"{userActions.Count-1} ações na fila- tipo:{userActions.Peek().type}, valor:{userActions.Peek().value}");
        OnCommandCalled?.Invoke(null, userActions.Peek());
    }

    public void END()
    {
        PServer1.StopServer();
        PServer2.StopServer();

    }




}
