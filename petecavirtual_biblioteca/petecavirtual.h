#ifndef petecavirtual_h
#define petecavirtual_h

class Robo () {
    INPUT key;
    // Movimentações
    void VaiParaFrente(void);
    void VaiParaTras(void);
    void RotacionaEsquerda(void);
    void RotacionaDireita(void);

    // Configurações de jogo
    void Inicializa(void);
    void ReiniciaJogo(void);
    void PausaOuDespausa(void);
    void TipoDeRobo(uint_8 robo);

    // Configuracoes de Camera
    void SelecionaCamera(uint_8 camera);
}

#endif
