#include <stdio.h>
#include <windows.h>

class Comunicacao {
    private:
        handle hPipe1, hPipe2;
        bool finished = false;

        char buff[100];
        LPTSTR lpszPipename1 = L"\\\\.\\pipe\\PetServerPipe0";
        LPTSTR lpszPipename2 = TEXT("\\\\.\\pipe\\PetServerPipe1");

        DWORD cbWritten;
        DWORD dwBytesToWrite = (DWORD)strlen(buf);
        DWORD threadId;

        HANDLE hThread = NULL;
        BOOL Write_St = TRUE;

    public:
        void IniciaComunicacao() {
            hPipe1 = CreateFile(lpszPipename1, GENERIC_WRITE, 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL);
            hPipe2 = CreateFile(lpszPipename2, GENERIC_READ, 0, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL);

            if ((hPipe1 == NULL || hPipe1 == INVALID_HANDLE_VALUE) || (hPipe2 == NULL || hPipe2 == INVALID_HANDLE_VALUE)) {
                cout << "Ocorreu erro ao realizar conexão com o servidor" << endl;
            } else {
                hThread = CreateThread(NULL, 0, &NET_RvThr, NULL, 0, NULL);
                do {
                    cout <<  "Digita sua mensagem:";
                    cin >> buf;
                    if (strcmp(buf, "quit") == 0) {
                        Write_St = false;
                    } else {
                        WriteFile(hPipe1, buf, dwBytesToWrite, &cbWritten, NULL);
                        memset(buf, 0xCC, 100);
                    }
                } while(Write_St);
                CloseHandle(hPipe1);
                CloseHandle(hPipe2);
                Finished = TRUE;
            }
        }

        void EnviaDados() {

        }

        void RecebeDados() {

        }

        void EncerraComunicacao() {

        }
};

unsigned long __stdcall NET_RvThr(void * pParam) {
	BOOL fSuccess;
	char chBuf[100];
	DWORD dwBytesToWrite = (DWORD)strlen(chBuf);
	DWORD cbRead;
	int i;

	while(1) {
		fSuccess =ReadFile( hPipe2,chBuf,dwBytesToWrite,&cbRead, NULL);
		if(fSuccess) {
			printf("C++ App: Received %d Bytes \n\t ",cbRead);
			for(i=0;i<cbRead;i++)
				printf("%c",chBuf[i]);
			printf("\n");
		}
		if (! fSuccess && GetLastError() != ERROR_MORE_DATA) {
			printf("Can't Read\n");
			if(Finished)
				break;
		}
	}
}
