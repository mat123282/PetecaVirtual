#ifndef petecavirtual_h
#define petecavirtual_h

#define WINVER 0x0500
#include "windows.h"
#include "stdlib.h"
#include "iostream"
#include "math.h"

#define tempo 12

class Robo {
	private:
    	INPUT key;
    	int tipoRobo;
    	bool BoolRobo;

	public:
    // Movimentacoes
        void VaiParaFrente(void) {
            if (tipoRobo == 0) {
                key.ki.wVk = 0x57;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else if(tipoRobo == 1) {
                key.ki.wVk = 0x55;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else {
                cout << "Ocorreu algum erro." << endl;
            }
        }
        void VaiParaTras(void) {
            if (tipoRobo == 0) {
                key.ki.wVk = 0x53;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else if(tipoRobo == 1) {
                key.ki.wVk = 0x4A;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else {
                cout << "Ocorreu algum erro." << endl;
            }
        }
        void RotacionaEsquerda(void) {
            if (tipoRobo == 0) {
                key.ki.wVk = 0x41;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else if(tipoRobo == 1) {
                key.ki.wVk = 0x48;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else {
                cout << "Ocorreu algum erro." << endl;
            }
        }
        void RotacionaDireita(void) {
            if (tipoRobo == 0) {
                key.ki.wVk = 0x44;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else if(tipoRobo == 1) {
                key.ki.wVk = 0x4B;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else {
                cout << "Ocorreu algum erro." << endl;
            }
        }

        void DesceGarra(void) {
            if (tipoRobo == 0) {
                key.ki.wVk = 0x45;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else if(tipoRobo == 1) {
                key.ki.wVk = 0x49;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else {
                cout << "Ocorreu algum erro." << endl;
            }
        }
        void SobeGarra(void) {
            if (tipoRobo == 0) {
                key.ki.wVk = 0x51;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else if(tipoRobo == 1) {
                key.ki.wVk = 0x59;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));
            } else {
                cout << "Ocorreu algum erro." << endl;
            }
        }

        // Configuracoes de jogo
        bool TipoDeRobo(int robo) {
            if ((robo == 0) || (robo == 1)) {
                tipoRobo = robo;
                BoolRobo = true;
                return true;
            } else {
                BoolRobo = false;
                return false;
            }
        }
        void Inicializa(void) {
            if(BoolRobo == true) {
                key.type = INPUT_KEYBOARD;
                key.ki.wScan = 0; // hardware scan code for key
                key.ki.time = 0;
                key.ki.dwExtraInfo = 0;

                cout << "Inicializando configuracoes para partida..." << endl;
                cout << "Mova para a tela do programa." << endl;
                for(int iteracao = 5; iteracao > 0; iteracao--) {
                    cout << (iteracao + 1) << endl;
                    Sleep(1000);
                }
            } else {
                cout << "Erro! Nao foi encontrado o robo selecionado." << endl;
                system("pause");
                system("exit");
            }
        }
        void ReiniciaJogo(void) {
            key.ki.wVk = 0x42;
            key.ki.dwFlags = 0;
            SendInput(1, &key, sizeof(INPUT));
            Sleep(tempo);
            key.ki.dwFlags = KEYEVENTF_KEYUP;
            SendInput(1, &key, sizeof(INPUT));
        }
        void PausaOuDespausa(void) {
                key.ki.wVk = 0x50;
            key.ki.dwFlags = 0;
            SendInput(1, &key, sizeof(INPUT));
            Sleep(tempo);
            key.ki.dwFlags = KEYEVENTF_KEYUP;
            SendInput(1, &key, sizeof(INPUT));
        }

        // Configuracoes de Camera
        bool SelecionaCamera(int camera) {
            if ((camera >= 0) && (camera <= 9)) {
                key.ki.wVk = 0x30 + camera;
                key.ki.dwFlags = 0;
                SendInput(1, &key, sizeof(INPUT));
                Sleep(tempo);
                key.ki.dwFlags = KEYEVENTF_KEYUP;
                SendInput(1, &key, sizeof(INPUT));

                return true;
            } else {
                cout << "Camera inexistente. Erro de sintaxe!"<< endl;
                return false;
            }
        }
};
#endif
