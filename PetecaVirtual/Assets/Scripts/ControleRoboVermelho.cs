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
        if (a.target == 0) //target == RedBot
        {
            if (a.type == "move") //type == Movement
            {
                vTranslacao = Mathf.Sign(a.value);
                DesiredDisplacement = a.value;
            }
            else if (a.type == "rot") //type == rotation
            {
                ang = -2;
                vRotacao = Mathf.Sign(a.value);
                float d = a.value % 360; d = (d > 0) ? d : d + 360;
                desiredDisplacement = d;
            }


        }
    }

    void Update()
    {

    }

    float ang = -1;
    //bool holdAngle = false;

    private void FixedUpdate()
    {

        if (ang == -2)
        {
            ang = (rigidbodyRobo.rotation.eulerAngles.y + DesiredDisplacement) % 360;
            //Debug.Log($"<color=#00ff00>(T:{rigidbodyRobo.rotation.eulerAngles.y:F2} + D:{DesiredDisplacement:F2})</color>" +
            //    $"\tParar em {ang:F2}");
        }

        float Translacao = Input.GetAxis("Vertical");
        float Rotacao = Input.GetAxis("Horizontal");

        if (Translacao == 0) Translacao = vTranslacao;
        if (Rotacao == 0) Rotacao = vRotacao;

        direcao = Vector3.zero;
        rotacaoRobo = Vector3.zero;

        if (Translacao != 0)
        {
            direcao = transform.right * Translacao;
        }
        if (Rotacao != 0)
        {
            rotacaoRobo = new Vector3(0, 0, Rotacao);
        }


        Vector3 displacement = (direcao * VelocidadeTranslacao * Time.fixedDeltaTime);
        rigidbodyRobo.MovePosition(rigidbodyRobo.position + displacement);


        //rotação
        Vector3 deltaRotation = rotacaoRobo * VelocidadeRotacao * Time.fixedDeltaTime;
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

        if (vRotacao != 0 && ang > 0)
        {
            var angN = rigidbodyRobo.rotation.eulerAngles.y;
            var diff = Mathf.Abs(ang - angN);
            //var dang = (diff < 180) ? diff : 360 - diff;
            var dang = Mathf.Min(diff, 360 - diff);

            if (dang < 0.1f)
            {
                //print($"{dang:F2}");
                //print($"O:{ang:F2}° D:{desiredDisplacement:F2}° N:{angN:F2}°");
                DesiredDisplacement = 0;
                ang = -1;
            }
        }

    }

    private void OnGUI()
    {

        var angN = rigidbodyRobo.rotation.eulerAngles.y;
        var diff = Mathf.Abs(ang - angN);
        var dang = Mathf.Min(diff, 360 - diff);
        if (DesiredDisplacement != 0)
        {

            GUI.Label(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100),
                            $"<color=#06357a><size=25><b>" +
                            $"Deslocando " +
                            $"{DesiredDisplacement:F2} u \n" +
                            ((vRotacao != 0) ? (
                            $"no sentido {((vRotacao == -1) ? "anti" : "")}horário \n " +
                            $"faltando {dang:F2}° " +
                            $"para {ang:F2}°") : "") +
                            $"</b></size></color>");
        }
    }

}



