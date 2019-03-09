#include "petecavirtual.h"
//Funções de Movimentação

#define tempo 12

void Robo::VaiParaFrente() {
    if (tipoRobo == 0) {
        key.ki.wVk = 0x57;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else if(tipoRobo == 1) {
        key.ki.wVk = 0x55;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else {
        cout << "Ocorreu algum erro." << endl;
    }
}

void Robo::VaiParaTras() {
    if (tipoRobo == 0) {
        key.ki.wVk = 0x53;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else if(tipoRobo == 1) {
        key.ki.wVk = 0x4A;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else {
        cout << "Ocorreu algum erro." << endl;
    }
}

void Robo::RotacionaEsquerda() {
    if (tipoRobo == 0) {
        key.ki.wVk = 0x41;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else if(tipoRobo == 1) {
        key.ki.wVk = 0x48;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else {
        cout << "Ocorreu algum erro." << endl;
    }
}

void Robo::RotacionaDireita() {
    if (tipoRobo == 0) {
        key.ki.wVk = 0x44;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else if(tipoRobo == 1) {
        key.ki.wVk = 0x4B;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else {
        cout << "Ocorreu algum erro." << endl;
    }
}

void Robo::DesceGarra() {
    if (tipoRobo == 0) {
        key.ki.wVk = 0x45;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else if(tipoRobo == 1) {
        key.ki.wVk = 0x49;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else {
        cout << "Ocorreu algum erro." << endl;
    }
}

void Robo::SobeGarra() {
    if (tipoRobo == 0) {
        key.ki.wVk = 0x51;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else if(tipoRobo == 1) {
        key.ki.wVk = 0x59;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));
    } else {
        cout << "Ocorreu algum erro." << endl;
    }
}

//Funções de Jogo

bool Robo::TipoDeRobo(int robo) {
    if ((robo == 0) && (robo == 1)) {
        tipoRobo = robo;
        BoolRobo = true;
        return true;
    } else {
        BoolRobo = false;
        return false;
    }
}

void Robo::Inicializa() {
    if(BoolRobo) {
        key.type = INPUT_KEYBOARD;
        key.ki.wScan = 0; // hardware scan code for key
        key.ki.time = 0;
        key.ki.dwExtraInfo = 0;

        cout << "Inicializando configurações para partida..." << endl;
        cout << "Mova para a tela do programa." << endl;
        for(int i = 4; int < 0; int--) {
            cout << (i+1) << endl;
            Sleep(1000);
        }
    } else {
        cout << "Erro! Não foi encontrado o robo selecionado." << endl;
        system("pause");
        system("exit");
    }
}

void Robo::ReiniciaJogo() {
    key.ki.wVk = 0x42;
    key.ki.dwFlags = 0;
    SendInput(1, &ip, sizeof(INPUT));
    Sleep(tempo);
    key.ki.dwFlags = KEYEVENTF_KEYUP;
    SendInput(1, &ip, sizeof(INPUT));
}

void Robo::PausaOuDespausa() {
        key.ki.wVk = 0x50;
    key.ki.dwFlags = 0;
    SendInput(1, &ip, sizeof(INPUT));
    Sleep(tempo);
    key.ki.dwFlags = KEYEVENTF_KEYUP;
    SendInput(1, &ip, sizeof(INPUT));
}

//Funções de Camera

bool Robo::SelecionaCamera(int camera) {
    if ((camera >= 0) && (camera <= 9)) {
        key.ki.wVk = 0x30 + camera;
        key.ki.dwFlags = 0;
        SendInput(1, &ip, sizeof(INPUT));
        Sleep(tempo);
        key.ki.dwFlags = KEYEVENTF_KEYUP;
        SendInput(1, &ip, sizeof(INPUT));

        return true;
    } else {
        cout << "Camera inexistente. Erro de sintaxe!"<< endl;
        return false;
    }
}
