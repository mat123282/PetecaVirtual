using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleRoboAzul : MonoBehaviour {

    public float VelocidadeTranslacao = 2;
    public float VelocidadeRotacao = 50;
    public float VelocidadeGarra = 50;
    public GameObject SistemaBraco;
    private Vector3 direcao;
    private Rigidbody rigidbodyRobo;
    private Vector3 rotacaoRobo;
    private bool sobeGarra;
    private bool desceGarra;

    void Start () {
        rigidbodyRobo = GetComponent<Rigidbody>();
        rotacaoRobo = Vector3.zero;
    }
	
	void Update () {
        float Translacao = Input.GetAxis("Vertical2");
        float Rotacao = Input.GetAxis("Horizontal2");

        direcao = Vector3.zero;
        rotacaoRobo = Vector3.zero;
        if (Translacao != 0) {
            direcao = transform.right * Translacao;
        } else if (Rotacao != 0) {
            rotacaoRobo = new Vector3(0, 0, Rotacao);
        }
    }

    private void FixedUpdate()
    {
        rigidbodyRobo.MovePosition(rigidbodyRobo.position + (direcao * VelocidadeTranslacao * Time.fixedDeltaTime));

        Quaternion deltaRotation = Quaternion.Euler(rotacaoRobo * VelocidadeRotacao * Time.fixedDeltaTime);
        rigidbodyRobo.MoveRotation(rigidbodyRobo.rotation * deltaRotation);

        bool sobeGarra = Input.GetKey(KeyCode.Y);
        bool desceGarra = Input.GetKey(KeyCode.I);
        float posicao = SistemaBraco.transform.localRotation.eulerAngles.y;

        float minRotation = -110;
        float maxRotation = 0;
        Vector3 currentRotation = SistemaBraco.transform.localRotation.eulerAngles-Vector3.right*20;
        currentRotation.y = Mathf.Clamp(currentRotation.y, minRotation, maxRotation);
        var desiredRotQ = Quaternion.Euler(currentRotation+Vector3.right*20);

        SistemaBraco.transform.localRotation = Quaternion.Lerp(SistemaBraco.transform.localRotation, desiredRotQ, Time.deltaTime * 0.2f);
        //SistemaBraco.transform.localRotation = Quaternion.Euler(currentRotation+Vector3.right*20);

        //var rot = posicao-20;


        //SistemaBraco.transform.Rotate((+((sobeGarra) ? 1 : 0) - ((desceGarra) ? 1 : 0)) * Vector3.right * Time.deltaTime * VelocidadeGarra);
        //if ((posicao <= 20 && posicao >= -1) || (posicao >= 270) && (posicao <= 361)) {
        //    SistemaBraco.transform.Rotate((+((sobeGarra) ? 1 : 0) - ((desceGarra) ? 1 : 0)) * Vector3.right * Time.deltaTime * VelocidadeGarra);
        //} else if ((posicao > 20) && (posicao < 40)) {
        //    SistemaBraco.transform.localRotation = Quaternion.Euler(0, 20, -90);
        //} else if ((posicao > 240) && (posicao < 270)) {
        //    SistemaBraco.transform.localRotation = Quaternion.Euler(0, 270, -90);
        //}
    }
}
