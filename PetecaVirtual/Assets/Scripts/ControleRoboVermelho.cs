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
        ExtLibControl.OnCommandCalled += MoveCommand;
    }

    float vTranslacao, vRotacao;
    float desiredDisplacement;

    public float DesiredDisplacement {
        get => desiredDisplacement;
        set
        {
            desiredDisplacement = Mathf.Abs(value);
            if (desiredDisplacement <= 0.1f)
            {
                desiredDisplacement = vRotacao = vTranslacao = 0;
                ExtLibControl.DeQueueAction();
            }
        }
    }

    private void MoveCommand(object sender, ExtLibControl.UserAction a)
    {
        if (a.target == 0 && a.type < 2) //target == BlueBot
        {
            string s = " 00 ";
            if (a.type == 0) //type == Movement
            {
                vTranslacao = Mathf.Sign(a.value);
                DesiredDisplacement = a.value;
                s = "Translação";
            }
            else if (a.type == 1) //type == rotation
            {
                vRotacao = Mathf.Sign(a.value);
                DesiredDisplacement = a.value;
                s = "Rotação";
            }

            Debug.Log($"RedBot deslocando {DesiredDisplacement}u.( {s} -{a.type})");

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


        Vector3 displacement = (direcao * VelocidadeTranslacao * Time.deltaTime);
        rigidbodyRobo.MovePosition(rigidbodyRobo.position + displacement);


        //rotação

        Vector3 deltaRotation = rotacaoRobo * VelocidadeRotacao * Time.deltaTime;
        rigidbodyRobo.MoveRotation(rigidbodyRobo.rotation * Quaternion.Euler(deltaRotation));

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

        if (vTranslacao != 0)
            DesiredDisplacement -= displacement.magnitude;

        if (vRotacao != 0)
        {
            DesiredDisplacement -= Mathf.Deg2Rad*deltaRotation.magnitude;
            Debug.LogWarning(Mathf.Deg2Rad*deltaRotation.magnitude);
        }

    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 50, 400, 100),
                        $"<color=#ff0000><size=30><b>{DesiredDisplacement}</b></size></color>");
    }

}



