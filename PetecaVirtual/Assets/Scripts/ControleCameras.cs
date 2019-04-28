using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleCameras : MonoBehaviour {

    public Camera[] Cameras;
    public Camera cameraRoboVermelho;
    public Camera cameraRoboAzul;
    public GameObject RoboVermelho;
    public GameObject RoboAzul;
    private int quantidadeCameras;

    // Use this for initialization
    void Start () {
        Cameras = gameObject.GetComponentsInChildren<Camera>();
        cameraRoboVermelho = (RoboVermelho == null) ? null:RoboVermelho.GetComponentInChildren<Camera>();
        cameraRoboAzul = (RoboAzul == null) ? null: RoboAzul.GetComponentInChildren<Camera>();
        quantidadeCameras = Cameras.Length;
    }

	// Update is called once per frame
	void Update () {
        int numeroCamera;
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            numeroCamera = 12;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            numeroCamera = 0;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            numeroCamera = 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            numeroCamera = 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha5)) {
            numeroCamera = 3;
        } else if (Input.GetKeyDown(KeyCode.Alpha6)) {
            numeroCamera = 4;
        } else if (Input.GetKeyDown(KeyCode.Alpha7)) {
            numeroCamera = 5;
        } else if (Input.GetKeyDown(KeyCode.Alpha8)) {
            numeroCamera = 6;
        } else if (Input.GetKeyDown(KeyCode.Alpha9)) {
            numeroCamera = 7;
        } else if (Input.GetKeyDown(KeyCode.Alpha0)) {
            numeroCamera = 13;
        } else {
            numeroCamera = -1;
        }

        TrocarCamera(numeroCamera);
    }

    private void TrocarCamera(int numeroCamera) {
        if ((numeroCamera < quantidadeCameras) && (numeroCamera >= 0)) {
            for (int contagem = 0; contagem < quantidadeCameras; contagem++) {
                Cameras[contagem].enabled = (contagem == numeroCamera) ? true : false;
            }
            if (cameraRoboVermelho != null) {
                cameraRoboVermelho.enabled = false;
            }
            if (cameraRoboAzul != null) {
              cameraRoboAzul.enabled = false;
            }
            Cameras[numeroCamera].enabled = true;
        } else if (numeroCamera == 12 && (RoboVermelho!=null)) {
            for (int contagem = 0; contagem < quantidadeCameras; contagem++) {
                Cameras[contagem].enabled = false;
            }
            if (cameraRoboAzul != null) {
                cameraRoboAzul.enabled = false;
            }
            cameraRoboVermelho.enabled = true;
        } else if (numeroCamera == 13 && (RoboAzul!=null)) {
            for (int contagem = 0; contagem < quantidadeCameras; contagem++) {
                Cameras[contagem].enabled = false;
            }
            if (cameraRoboVermelho != null) {
                cameraRoboVermelho.enabled = false;
            }
            cameraRoboAzul.enabled = true;
        }
    }
}
