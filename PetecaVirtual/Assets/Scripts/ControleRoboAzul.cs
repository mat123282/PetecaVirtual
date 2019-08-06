using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleRoboAzul : MonoBehaviour
{

    public float VelocidadeTranslacao = 4;
    public float VelocidadeRotacao = 100;
    public float VelocidadeGarra = 100;
    public GameObject SistemaBraco;
    private Vector3 direcao;
    private Rigidbody rigidbodyRobo;
    private Vector3 rotacaoRobo;
    private bool sobeGarra;
    private bool desceGarra;

    void Start()
    {
        rigidbodyRobo = GetComponent<Rigidbody>();
        rotacaoRobo = Vector3.zero;
        ExtLibControl.OnCommandCalled += MoveCommand;
    }

    float vTranslacao, vRotacao;
    float desiredDisplacement;

    float DesiredDisplacement {
        get => desiredDisplacement;
        set
        {
            desiredDisplacement = Mathf.Abs(value);
            if (desiredDisplacement <= 0.01f)
            {
                desiredDisplacement = vRotacao = vTranslacao = 0;
                ExtLibControl.DeQueueAction();
            }
        }
    }

    private void MoveCommand(object sender, ExtLibControl.UserAction a)
    {
        //Debug.Log("AzulResp");
        if (a.target == 1) //target == BlueBot
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

    private void FixedUpdate()
    {

        if (ang == -2)
        {
            ang = (rigidbodyRobo.rotation.eulerAngles.y + DesiredDisplacement) % 360;
        }


        float Translacao = Input.GetAxis("Vertical2");
        float Rotacao = Input.GetAxis("Horizontal2");

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

        Vector3 displacement = (direcao * VelocidadeTranslacao * Time.deltaTime);
        rigidbodyRobo.MovePosition(rigidbodyRobo.position + displacement);

        //rotação
        Vector3 deltaRotation = rotacaoRobo * VelocidadeRotacao * Time.fixedDeltaTime;
        rigidbodyRobo.MoveRotation(rigidbodyRobo.rotation * Quaternion.Euler(deltaRotation));

        bool SobeGarra = Input.GetKey(KeyCode.Y);
        bool DesceGarra = Input.GetKey(KeyCode.I);
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
                DesiredDisplacement = 0;
                ang = -1;
            }
        }

    }
}
