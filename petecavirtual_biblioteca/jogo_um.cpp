#include "petecavirtual.h"

using namespace std;

Robo roboVermelho;

int main() {
	bool tentativa = roboVermelho.TipoDeRobo(0);
	roboVermelho.Inicializa();
	roboVermelho.PausaOuDespausa();
	roboVermelho.SelecionaCamera(2);
	
	for (int i = 0; i < 10; i++) {
		roboVermelho.VaiParaFrente();
		roboVermelho.SobeGarra();
	}
	
	system("pause");
}
