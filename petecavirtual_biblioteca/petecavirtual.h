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
        void VaiParaFrente() {
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
                std::cout << "Ocorreu algum erro." << std::endl;
            }
        }
        void VaiParaTras() {
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
                std::cout << "Ocorreu algum erro." << std::endl;
            }
        }
        void RotacionaEsquerda() {
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
                std::cout << "Ocorreu algum erro." << std::endl;
            }
        }
        void RotacionaDireita() {
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
                std::cout << "Ocorreu algum erro." << std::endl;
            }
        }
        void DesceGarra() {
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
                std::cout << "Ocorreu algum erro." << std::endl;
            }
        }
        void SobeGarra() {
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
                std::cout << "Ocorreu algum erro." << std::endl;
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
        void Inicializa() {
            if(BoolRobo == true) {
                key.type = INPUT_KEYBOARD;
                key.ki.wScan = 0; // hardware scan code for key
                key.ki.time = 0;
                key.ki.dwExtraInfo = 0;

                std::cout << "Inicializando configuracoes para partida..." << std::endl;
                std::cout << "Mova para a tela do programa." << std::endl;
                for(int iteracao = 5; iteracao > 0; iteracao--) {
                    std::cout << iteracao << std::endl;
                    Sleep(1000);
                }
            } else {
                std::cout << "Erro! Nao foi encontrado o robo selecionado." << std::endl;
                system("exit");
            }
        }
        void ReiniciaJogo() {
            key.ki.wVk = 0x42;
            key.ki.dwFlags = 0;
            SendInput(1, &key, sizeof(INPUT));
            Sleep(tempo);
            key.ki.dwFlags = KEYEVENTF_KEYUP;
            SendInput(1, &key, sizeof(INPUT));
        }
        void PausaOuDespausa() {
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
                std::cout << "Camera inexistente. Erro de sintaxe!"<< std::endl;
                return false;
            }
        }
};
#endif
