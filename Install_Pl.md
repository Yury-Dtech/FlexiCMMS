### Instrukcja wdrożenia FlexiCMMS na serwerze Linux

Niniejsza instrukcja opisuje wdrożenie aplikacji FlexiCMMS na serwerze Linux. Możesz wybrać wdrożenie z Nginx jako odwrotnym proxy lub bezpośrednio z Kestrel. Proces wdrożenia jest zautomatyzowany za pomocą dedykowanych skryptów.

**Wymagania wstępne:**

*   Serwer Linux (np. Ubuntu 20.04+ lub Debian 10+).
*   Dostęp SSH do serwera.
*   Opublikowane archiwum ZIP aplikacji (`FlexiCMMS.zip`).
*   W przypadku wdrożenia *bez Nginx*, wymagany jest plik certyfikatu `.pfx` (`blazortool.pfx`) dla HTTPS.

---

**Krok 0: Przygotowanie do wdrożenia**

1.  **Przenieś niezbędne pliki na serwer:**
    Skopiuj następujące pliki do katalogu domowego użytkownika na serwerze (np. `/home/youruser/`):
    *   Archiwum ZIP aplikacji: `FlexiCMMS.zip`
    *   **Wybierz jeden skrypt wdrożeniowy:**
        *   Do wdrożenia *z Nginx*: `deploy_flexicmms_nginx.sh`
        *   Do wdrożenia *bez Nginx*: `deploy_flexicmms_NoNginx.sh`
    *   W przypadku wdrożenia *bez Nginx*, skopiuj również plik certyfikatu PFX: `blazortool.pfx` (upewnij się, że ma nazwę `blazortool.pfx` i hasło `dtech`, zgodnie ze skryptem i konfiguracją Kestrel).

    Przykład użycia `scp` (dostosuj w zależności od wybranej metody wdrożenia):
    ```bash
    # Do wdrożenia z Nginx
    scp /path/to/local/deploy_flexicmms_nginx.sh user@your_server_ip:/home/user/
    scp /path/to/local/FlexiCMMS.zip user@your_server_ip:/home/user/

    # Do wdrożenia bez Nginx
    scp /path/to/local/deploy_flexicmms_NoNginx.sh user@your_server_ip:/home/user/
    scp /path/to/local/FlexiCMMS.zip user@your_server_ip:/home/user/
    scp /path/to/local/blazortool.pfx user@your_server_ip:/home/user/
    ```

2.  **Połącz się z serwerem przez SSH:**
    ```bash
    ssh user@your_server_ip
    ```

3.  **Ustaw wybrany skrypt jako wykonywalny:**
    ```bash
    # Do wdrożenia z Nginx
    chmod +x deploy_flexicmms_nginx.sh

    # Do wdrożenia bez Nginx
    chmod +x deploy_flexicmms_NoNginx.sh
    ```

---

**Krok 1: Uruchomienie skryptu wdrożeniowego**

Uruchom wybrany skrypt z uprawnieniami superużytkownika. Skrypt poprosi o adres serwera API i inne niezbędne informacje, a następnie wykona wszystkie wymagane kroki w celu zainstalowania zależności, rozpakowania aplikacji, skonfigurowania `appsettings.json` i skonfigurowania usługi Systemd.

*   **Do wdrożenia z Nginx:**
    ```bash
    sudo ./deploy_flexicmms_nginx.sh
    ```
    Podczas wykonywania skrypt poprosi o podanie:
    *   Rzeczywistego adresu serwera API (np. `http://10.1.40.128:1122/api/v1/`). Skrypt automatycznie doda ukośnik końcowy, jeśli go brakuje.
    *   Nazwy domeny lub adresu IP dla Nginx (np. `example.com` lub `192.168.1.100`).

*   **Do wdrożenia bez Nginx:**
    ```bash
    sudo ./deploy_flexicmms_NoNginx.sh
    ```
    Podczas wykonywania skrypt poprosi o podanie rzeczywistego adresu serwera API. Upewnij się, że wprowadzasz go w prawidłowym formacie (np. `http://10.1.40.128:1122/api/v1/`). Skrypt automatycznie doda ukośnik końcowy, jeśli go brakuje.

---

**Krok 2: Weryfikacja statusu i dostępności aplikacji**

Po zakończeniu działania skryptu:

1.  **Sprawdź status usługi FlexiCMMS:**
    ```bash
    sudo systemctl status flexicmms
    ```
    Powinieneś zobaczyć, że usługa jest aktywna (`active (running)`).

2.  **Skonfiguruj zaporę sieciową i uzyskaj dostęp do aplikacji:**

    *   **W przypadku wdrożenia z Nginx:**
        Upewnij się, że port 80 (HTTP) jest otwarty w zaporze sieciowej serwera. Jeśli używasz `ufw`, możesz go otworzyć w ten sposób:
        ```bash
        sudo ufw allow 80/tcp
        sudo ufw enable # Jeśli zapora nie jest aktywna
        ```
        Twoja aplikacja FlexiCMMS powinna być teraz dostępna pod nazwą domeny lub adresem IP serwera za pośrednictwem Nginx.
        **Ważna uwaga:** Jeśli planujesz używać HTTPS, będziesz musiał ręcznie skonfigurować certyfikat SSL w Nginx (np. za pomocą Certbot) po uruchomieniu tego skryptu i otworzyć port 443 w zaporze sieciowej.

    *   **W przypadku wdrożenia bez Nginx:**
        Upewnij się, że porty 80 (HTTP) i 443 (HTTPS) są otwarte w zaporze sieciowej serwera. Jeśli używasz `ufw`, możesz je otworzyć w ten sposób:
        ```bash
        sudo ufw allow 80/tcp
        sudo ufw allow 443/tcp
        sudo ufw enable # Jeśli zapora nie jest aktywna
        ```
        Twoja aplikacja FlexiCMMS powinna być teraz dostępna pod adresem IP serwera lub nazwą domeny przez HTTP (port 80) i HTTPS (port 443).
        **Ważna uwaga:** Jeśli używasz certyfikatu z podpisem własnym (`blazortool.pfx`), przeglądarki będą wyświetlać ostrzeżenia o bezpieczeństwie. W środowisku produkcyjnym zaleca się użycie certyfikatu wydanego przez zaufany Urząd Certyfikacji (np. Let's Encrypt).
