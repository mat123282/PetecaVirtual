using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleRoboAzul : MonoBehaviour {

    public float VelocidadeTranslacao = 4;
    public float VelocidadeRotacao = 100;
    public float VelocidadeGarra = 100;
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

    }

    private void FixedUpdate() {
        float Translacao = Input.GetAxis("Vertical2");
        float Rotacao = Input.GetAxis("Horizontal2");

        direcao = Vector3.zero;
        rotacaoRobo = Vector3.zero;
        if (Translacao != 0) {
            direcao = transform.right * Translacao;
        } else if (Rotacao != 0) {
            rotacaoRobo = new Vector3(0, 0, Rotacao);
        }


        rigidbodyRobo.MovePosition(rigidbodyRobo.position + (direcao * VelocidadeTranslacao * Time.deltaTime));

        Quaternion deltaRotation = Quaternion.Euler(rotacaoRobo * VelocidadeRotacao * Time.deltaTime);
        rigidbodyRobo.MoveRotation(rigidbodyRobo.rotation * deltaRotation);

        bool SobeGarra = Input.GetKey(KeyCode.Y);
        bool DesceGarra = Input.GetKey(KeyCode.I);
        float posicao = SistemaBraco.transform.localRotation.eulerAngles.y;

        if ((posicao <= 20 && posicao >= -1) || (posicao >= 270) && (posicao <= 361)) {
            SistemaBraco.transform.Rotate((+((SobeGarra) ? 1 : 0) - ((DesceGarra) ? 1 : 0)) * Vector3.right * Time.deltaTime * VelocidadeGarra);
        } else if ((posicao > 20) && (posicao < 40)) {
            SistemaBraco.transform.localRotation = Quaternion.Euler(0, 20, -90);
        } else if ((posicao > 240) && (posicao < 270)) {
            SistemaBraco.transform.localRotation = Quaternion.Euler(0, 270, -90);
        }
    }
}
