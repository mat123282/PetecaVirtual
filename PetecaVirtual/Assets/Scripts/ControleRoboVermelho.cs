using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleRoboVermelho : MonoBehaviour
{

    public float VelocidadeTranslacao = 4;
    public float VelocidadeRotacao = 100;
    public float VelocidadeGarra = 100;
    public GameObject SistemaBraco;
    private Vector3 direcao;
    private Rigidbody rigidbodyRobo;
    private Vector3 rotacaoRobo;
    private bool SobeGarra;
    private bool DesceGarra;

    void Start()
    {
        rigidbodyRobo = GetComponent<Rigidbody>();
        rotacaoRobo = Vector3.zero;
        ModeTrackingScript.OnMoveCommand += MoveCommand;
    }

    float vTranslacao, vRotacao;
    private void MoveCommand(object sender, int e)
    {
        //Debug.Log("VermReps");
        if (e >> 2 == 0)
        {
            //var SV = (sender as NamedPipeServer);
            //SV.SendMessage("A mover vermelho", SV.clientse);

            Debug.Log("Mover Vermelho " + (e & 3));
            switch ((e & 3))
            {
                case 0: vTranslacao = 1; break;
                case 1: vTranslacao = -1; break;
                case 2: vRotacao = 1; break;
                case 3: vRotacao = -1; break;
            }
        }
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        float Translacao = Input.GetAxis("Vertical");
        float Rotacao = Input.GetAxis("Horizontal");

        if (Translacao == 0)
            Translacao = vTranslacao;
        if (Rotacao == 0)
            Rotacao = vRotacao;

        direcao = Vector3.zero;
        rotacaoRobo = Vector3.zero;

        if (Translacao != 0)
        {
            direcao = transform.right * Translacao;
        }
        else if (Rotacao != 0)
        {
            rotacaoRobo = new Vector3(0, 0, Rotacao);
        }


        rigidbodyRobo.MovePosition(rigidbodyRobo.position + (direcao * VelocidadeTranslacao * Time.deltaTime));

        Quaternion deltaRotation = Quaternion.Euler(rotacaoRobo * VelocidadeRotacao * Time.deltaTime);
        rigidbodyRobo.MoveRotation(rigidbodyRobo.rotation * deltaRotation);

        bool SobeGarra = Input.GetKey(KeyCode.Q);
        bool DesceGarra = Input.GetKey(KeyCode.E);
        float posicao = SistemaBraco.transform.localRotation.eulerAngles.y;

        if ((posicao <= 20 && posicao >= -1) || (posicao >= 270) && (posicao <= 361))
        {
            SistemaBraco.transform.Rotate((+((SobeGarra) ? 1 : 0) - ((DesceGarra) ? 1 : 0)) * Vector3.right * Time.deltaTime * VelocidadeGarra);
        }
        else if ((posicao > 20) && (posicao < 40))
        {
            SistemaBraco.transform.localRotation = Quaternion.Euler(0, 20, -90);
        }
        else if ((posicao > 240) && (posicao < 270))
        {
            SistemaBraco.transform.localRotation = Quaternion.Euler(0, 270, -90);
        }

        vRotacao = vTranslacao = 0;
    }
}

