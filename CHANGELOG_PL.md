## 2025-09-19

### Nowe Funkcje
- **Automatyczne Odświeżanie Danych**: Zaimplementowano automatyczne odświeżanie danych z konfigurowalnymi interwałami na stronach Główna, Aktywności i Harmonogram. Użytkownicy otrzymują powiadomienia o nowych zleceniach na stronach Główna i Aktywności.
- **Informacje o Urządzeniu w Zleceniu Pracy**: Komponent Zlecenia Pracy wyświetla teraz szczegółowe informacje o urządzeniu (Lokalizacja, Miejsce, Stan Urządzenia).

### Naprawiono / Ulepszenia
- **Obsługa Stanu Urządzenia i Kolorów**: Ulepszono wyświetlanie stanu urządzenia w komponencie WorkOrderComponent z miękkimi kolorami tła i spójną reprezentacją kolorów w siatce DevicesPage.
- **Stabilność Komponentu Timera**: Zrefaktoryzowano `Timer.razor` pod kątem kompatybilności AOT na serwerach Linux, zastępując `PeriodicTimer` przez `System.Threading.Timer` i `Random` niestandardowym LCG.
- **Uprawnienia Menu Nawigacyjnego**: Linki "Zlecenia" i "Urządzenia" w menu nawigacyjnym są teraz widoczne tylko dla administratorów.
- **Wprowadzanie Opisu Aktywności**: Pole "Opis" do dodawania nowych aktywności używa teraz prostszego pola tekstowego (`TelerikTextArea`) zamiast edytora tekstu sformatowanego.

## 2025-09-17

### Nowe Funkcje
- **Widget Pogody**: Dodano nowy widget pogodowy do głównego layoutu. Automatycznie wykrywa on lokalizację użytkownika, aby wyświetlić aktualne warunki pogodowe, temperaturę oraz godziny wschodu i zachodu słońca. Dane są buforowane w celu zminimalizowania liczby wywołań API.
- **Ulepszenia Interfejsu Użytkownika**:
    - Link nawigacyjny "Main" został zmieniony na "Moja strona" dla większej przejrzystości.
    - Na stronie głównej ukryto filtr kolumny "Przypisana osoba" dla listy osobistych zleceń pracy użytkownika, ponieważ był on zbędny.
    - Okno "Dodaj aktywność" jest teraz szersze, co zapewnia więcej miejsca na zawartość formularza.

### Naprawiono / Ulepszenia
- **Stabilność Harmonogramu**: Zrefaktoryzowano logikę aktualizacji terminów na stronie Aktywności. Bezpośrednie tworzenie i usuwanie z harmonogramu jest teraz wyłączone na rzecz okien modalnych.
- **Jakość Kodu**: Wprowadzono liczne drobne ulepszenia w całej aplikacji.
- **CI/CD**: Zaktualizowano i usprawniono przepływ pracy GitHub Actions do budowania i publikowania aplikacji w celu zapewnienia większej niezawodności.
- **Dokumentacja**: Zaktualizowano przewodniki instalacyjne o nowe informacje i dodano wersje PDF do dostępu offline.

## 2025-09-15..16

### Wydajność i Stabilność
- **Zoptymalizowane Ładowanie Danych**: Zrefaktoryzowano pobieranie danych dla słowników (Kategorie, Powody, Działy itp.), aby było asynchroniczne i buforowane. Znacząco poprawia to czas uruchamiania aplikacji i responsywność na stronach takich jak Strona Główna, Zlecenia i Harmonogram.
- **Ulepszona Obsługa Sesji**: Przebudowano walidację sesji podczas uruchamiania aplikacji. Aplikacja sprawdza teraz sesję użytkownika z serwerem przed renderowaniem interfejsu, przekierowując na stronę logowania, jeśli sesja jest nieprawidłowa. Zapobiega to dostępowi użytkowników do niefunkcjonalnego stanu aplikacji.
- **Poprawki Błędów**: Usunięto kilka potencjalnych wyjątków `NullReferenceException` w całej aplikacji, w szczególności na stronach Aktywności i Urządzeń, co czyni interfejs bardziej stabilnym.

### UI/UX
- **Drobne Poprawki Interfejsu**: Wprowadzono niewielkie ulepszenia wizualne na stronie Urządzeń, w tym dostosowanie rozmiaru strony siatki i jej wysokości dla lepszej użyteczności.
- **Czyszczenie Kodu**: Usunięto nieużywane zmienne i komentarze w kodzie, aby poprawić jego utrzymywalność.

## 2025-09-11 .. 2025-09-12

### Podsumowanie
- Harmonogram i Aktywności: zrefaktoryzowano pasek narzędzi harmonogramu i sposób renderowania terminów; zaktualizowano zasoby i napisy lokalizacyjne (Telerik) dla czytelniejszych etykiet.
- Formularz Dodawania Aktywności: dodano pole "End Date" oraz zmieniono domyślny `EndTime` nowej aktywności tak, aby podążał za `WorkOrder.EndTime` — ułatwia planowanie przy końcu zlecenia.
- Ustawienia i style: drobne poprawki układu i CSS na stronach Aktywności i Ustawień dla lepszej czytelności i odstępów.
- Wdrażanie i dokumentacja: dodano workflowy GitHub i skrypty wdrożeniowe oraz krótkie przewodniki instalacyjne (EN/PL/RU) usprawniające wydania i instalację.

## 2025-09-10

### Strona aktywności: 
- dodano pole zmiany wysokości wiersza
- ulepszono style wyświetlania tekstu wpisów
- Zaimplementowano lokalizację komponentów Telerik Blazor przy użyciu niestandardowych plików zasobów (`TelerikMessages.resx`, `TelerikMessages.pl-PL.resx`) i niestandardowej usługi `ITelerikStringLocalizer`.

## 2025-09-09

### Nowe funkcje
- Zaimplementowano funkcję edycji aktywności, umożliwiającą użytkownikom modyfikowanie istniejących aktywności.
- Dodano widok MultiDay do harmonogramu i strony Aktywności, z konfigurowalnymi podziałami slotów i czasem trwania.
- Ulepszono filtrowanie użytkowników na stronie Aktywności według Działów.
- Wprowadzono opcję "Edytuj aktywność" w menu kontekstowym harmonogramu dla płynnej edycji.
- Dodano model `UserInfo` i powiązane metody API do pobierania informacji o użytkowniku i filtrowania opartego na działach.
- Zaimplementowano `UserController` z punktem końcowym `GetUsersList` do pobierania danych użytkowników.

### Naprawiono / Ulepszenia
- Zrefaktoryzowano `NewActivityFormComponent` w celu obsługi edycji istniejących aktywności i poprawiono logikę jego inicjalizacji.
- Zaktualizowano `ActivitiesPage.razor` w celu integracji filtrowania użytkowników, ustawień widoku MultiDay i funkcji edycji aktywności.
- Ulepszono `ApiServiceClient` o metodę `UpdateActivity`, pobieranie informacji o użytkowniku oraz ulepszone logowanie/buforowanie.

## 2025-09-08

### Funkcje
- Dodano tryb MultiDay na stronie aktywności.
- Zaimplementowano metody i kontrolery do edycji aktywności.

### Naprawiono
- Naprawiono funkcjonalność przeciągania i upuszczania wpisów w kalendarzu poprzez refaktoryzację klasy `ActivityAppointment`.

## 2025-09-05

### Funkcje
*   Dodano filtry na stronie Aktywności: filtrowanie po użytkownikach oraz po urządzeniach (zasobach). Kontrolka filtrowania po dziale istnieje, ale jest obecnie nieaktywna (oczekuje na aktualizację API).
*   Zaktualizowano modele związane z aktywnościami i klucze lokalizacyjne, aby wesprzeć nowe zachowania interfejsu.

### Naprawiono / Ulepszenia
*   Naprawiono trwałość stanu użytkownika — `MatrixID/RightMatrixID` oraz `DepartmentID` są prawidłowo zapisywane i odczytywane z local storage.
*   Zapisano i przywrócono stan filtrów i ustawień widoku: wybrany użytkownik, wybrane urządzenie, wybrany dział (kontrolka filtrowania po dziale jest zapisywana i przywracana; rzeczywiste filtrowanie po działach aktualnie nieaktywne) oraz ustawienia widoku harmonogramu.

## 2025-09-04

### Funkcje
*   Dodano możliwość otwierania szczegółów aktywności w oknie modalnym po kliknięciu w wiersz
*   Ulepszono proces dodawania aktywności: formularz obsługuje sugerowaną datę rozpoczęcia (`RecommendedStart`) i zwraca utworzoną aktywność po zapisaniu, co daje natychmiastowe potwierdzenie.
*   Wybór urządzenia teraz filtruje powiązane zlecenia i kategorie aktywności w formularzu nowej aktywności, ułatwiając wybór.

## 2025-09-03
### Nowe Funkcje
*   **Interfejs Aktywności / Komponenty**:
    *   Dodano komponent `ActivityCard` z obsługą lokalizacji oraz zmiennymi CSS dla lepszego themingu.
    *   Zaktualizowano `ActivitySchedulerItem` oraz style `ActivityDisplay`, aby poprawić renderowanie terminów i wspierać różne widoki harmonogramu.
*   **Strona Aktywności**:
    *   Dodano okno/modal do dodawania aktywności i dopracowano obsługę terminów na stronie.
    *   Zmieniono wywołanie ładowania danych z `LoadData()` na `HardLoadData()` w celu bardziej niezawodnego odświeżania.
*   **Modele i API**:
    *   Ulepszono model `ActivityAppointment`: właściwość `Title` teraz zawiera informacje o zleceniu, dodano dodatkowe konstruktory.
    *   Uproszczono logikę pobierania zleceń po stronie klienta API (usunięto przestarzałe cache), co poprawia spójność danych.

### Naprawiono / Ulepszenia
*   **Formularz Nowej Aktywności**: Zaktualizowano etykiety w `NewActivityFormComponent`, tak aby zawierały identyfikatory Urządzenia i Zlecenia Pracy; zwiększono minimalną szerokość etykiet oraz poprawiono walidację i UX formularza.
*   **Style**: Wprowadzono poprawki CSS (komponentowe i globalne `app.css`) oraz zmienne CSS dla lepszego wyglądu kart aktywności i terminów.
*   **Wyświetlanie Urządzenia**: Drobne aktualizacje integracji, aby lepiej wyświetlać informacje związane z aktywnościami.

## 2025-09-02

### Nowe Funkcje
*   **Moduł Aktywności (Activities)**:
    *   Dodano stronę `Activities` z harmonogramem oraz nowe komponenty: `ActivitySchedulerItem`, `NewActivityFormComponent`.
    *   Nowe modele: `ActivityAppointment`, `NewActivityFormModel`.
    *   Dodano pliki CSS i układ dla strony aktywności.
*   **Refaktoryzacja klienta API**:
    *   Podzielono `ApiServiceClient.cs` na części (`ApiServiceClient.Activities.cs`, `...Devices.cs`, `...Dictionaries.cs`, `...Settings.cs`, `...Users.cs`, `...WorkOrders.cs`) w celu lepszej organizacji.
*   **Strona główna / Nawigacja**:
    *   Dodano przycisk odświeżania danych na stronie głównej.
    *   Zmieniono `NavMenu.razor` i `Home.razor`, aby główna zawartość i menu były widoczne tylko dla uwierzytelnionych użytkowników.
*   **Lokalizacja**:
    *   Zaktualizowano `UIStrings.resx` i `UIStrings.pl-PL.resx` o klucze dla nowego interfejsu aktywności.

## 2025-09-01
### Nowe Funkcje
*   **Dostęp do plików SMB i poświadczenia**:
    *   Dodano obsługę dostępu do plików w udziałach sieciowych (SMB) z poświadczeniami dostarczonymi przez użytkownika.
    *   Zaimplementowano nowy interfejs użytkownika w ustawieniach do konfigurowania serwera SMB, nazwy użytkownika i hasła.
    *   Dodano funkcję sprawdzania poprawności poświadczeń SMB.
*   **Ulepszony system powiadomień**:
    *   Wprowadzono nowy system powiadomień wykorzystujący komponent Telerik Notification do wyświetlania użytkownikowi różnych typów komunikatów (informacyjnych, sukcesu, ostrzeżeń, błędów).
*   **Ulepszony formularz aktywności**:
    *   Ulepszono obsługę dat w formularzach aktywności, umożliwiając bardziej elastyczne minimalne i maksymalne daty aktywności w oparciu o właściwości zlecenia pracy.
    *   Wprowadzono walidację dla pola obciążenia (workload) w zakresie od 0 do 24 godzin.
    *   Dodano nowe ciągi lokalizacyjne dla ustawień udziału sieciowego i walidacji obciążenia.
*   **Profil użytkownika i ustawienia**:
    *   Wyświetlanie profilu użytkownika obejmuje teraz `RightMatrixID` i `DepartmentID`.
    *   Ulepszono ładowanie ustawień użytkownika po zalogowaniu, w tym danych uwierzytelniających udziału sieciowego i preferencji schematu kolorów.

### Naprawiono / Ulepszenia
*   **Formularz Aktywności**: Warunkowo wyświetla minimalne i maksymalne daty aktywności dla lepszej przejrzystości dla użytkownika.
*   **Proces Logowania**: Zoptymalizowano proces logowania poprzez scentralizowanie ładowania ustawień użytkownika w dedykowanej metodzie klienta API, zapewniając spójne zarządzanie stanem.
*   **Pobieranie plików**: Zmieniono linki do pobierania plików, aby otwierały się w nowej karcie (`target="_blank"`).
*   **Menu nawigacyjne**: Menu nawigacyjne jest teraz widoczne tylko wtedy, gdy użytkownik jest uwierzytelniony.
*   **Strona główna**: Główna zawartość strony głównej jest teraz widoczna tylko wtedy, gdy użytkownik jest uwierzytelniony.
*   **Uprawnienia administratora**: Sprawdzanie administratora obejmuje teraz `RightMatrixID == 1`.
*   **Zależności**: Zastąpiono `SharpCifs.Std` przez `SMBLibrary` dla funkcjonalności SMB.

## 2025-08-28
### Naprawiono / Ulepszenia
*   **Komponent Wyświetlania Urządzeń (Device Display Component)**:
    *   Zaktualizowano pola ogólnych informacji, aby używały `TelerikTextBox` dla spójnego wyświetlania.
    *   Ulepszono placeholder obrazu, gdy brak dostępnych obrazów urządzenia.
    *   Zrefaktoryzowano listę plików dokumentacji, aby używała niestandardowego HTML/CSS dla lepszego stylu w oknie Telerik.
    *   Ulepszono ładowanie obrazów za pomocą wskaźników ładowania i zoptymalizowano ładowanie obrazów w pełnym rozmiarze w oknie modalnym.
    *   Dodano funkcjonalność otwierania plików z listy dokumentacji.
*   **Strona Urządzeń (Devices Page)**:
    *   Dostosowano minimalną szerokość okna szczegółów urządzenia dla lepszej responsywności.
    *   Zaimplementowano zapisywanie i ładowanie stanu Telerik Grid w celu zachowania preferencji użytkownika między sesjami.
    *   Ulepszono wyświetlanie listy urządzeń z dynamicznym kolorowaniem wierszy na podstawie statusu urządzenia.
    *   Dodano nowe opcje filtrowania dla Kategorii Urządzenia, Nazwy Stanu, Typu, Lokalizacji, Nazwy Lokalizacji, Nazwy Zestawu i Właściciela.
    *   Wprowadzono kolumnę `AssetNoShort`.
*   **Klient API (API Client)**:
    *   Zrefaktoryzowano `GetDeviceImageAsync` do używania `SingleResponse<DeviceImage>`.
    *   Dodano `GetDeviceInfoForGridAsync` i `GetListDeviceInfoForGrid` dla zoptymalizowanego pobierania informacji o urządzeniach do wyświetlania w siatce.
    *   Dodano metody `GetWorkOrderDirectoryFiles` i `GetWorkOrderFile` do obsługi plików dokumentacji.
    *   Dodano `GetFileDownloadUrl` do bezpośredniego pobierania plików.
    *   Usunięto `AddNewWODict` (przestarzałe).
*   **Serwer (Server)**:
    *   Dodano punkty końcowe `DeviceController` dla `GetDirectoryFiles`, `GetFile` i `DownloadFile` do serwowania plików dokumentacji.
*   **Lokalizacja (Localization)**:
    *   Dodano liczne nowe klucze lokalizacyjne dla elementów interfejsu użytkownika związanych z urządzeniami, w tym `AssetNoShort` i różne prefiksy `DeviceDisplay_`.
*   **Menu Nawigacyjne (Navigation Menu)**:
    *   Zaktualizowano linki nawigacyjne, aby używały `TelerikSvgIcon` dla lepszej spójności wizualnej.
*   **Komponent Zlecenia Pracy (Work Order Component)**:
    *   Dostosowano edytowalność pola opisu, aby umożliwić edycję nowych zleceń pracy niezależnie od konkretnych uprawnień.
*   **Wewnętrzne (Internal)**:
    *   Dodano fallback dla nagłówka `Authorization` w `ServerAuthTokenService.cs` dla lokalnego środowiska deweloperskiego.
    *   Wprowadzono klasę narzędziową `ColorHelper` do generowania stonowanych kolorów.
    *   Zaktualizowano model `FullDeviceInfo` o właściwości `LastState`, `StateHistory`, `LastStatus`, `HaveFiles`, `StateName` i zainicjalizowano `Images` oraz `DirectoryFiles`.
    *   Dodano model `WorkOrderFileItem`.
    *   Zaktualizowano `ViewSettingsService` o klucz `DevicesPage`.

## 2025-08-26
### Nowe Funkcje
*   **Pliki Zleceń Pracy**: Zaimplementowano listowanie i przeglądanie plików dokumentacji zleceń pracy. Obejmuje to nowe metody klienta API (`GetWorkOrderDirectoryFiles`, `GetWorkOrderFile`), rozszerzony model `FullDeviceInfo` o listy plików oraz zaktualizowany komponent `DeviceDisplay` do wyświetlania tych plików z odpowiednimi ikonami i funkcjonalnością otwierania.
*   **Model plików**: Dodano model `WorkOrderFileItem` do reprezentacji plików dokumentacji po stronie klienta.
*   **Serwer**: Dodano endpoint `DeviceController.GetFile` do serwowania pojedynczych plików zleceń pracy z normalizacją ścieżek.
*   **Klient API**: Zrefaktoryzowano `GetAllDevicesCachedAsync` na `GetAllDevicesAsync` i zaktualizowano jego użycie w `OrdersGrid`, `WorkOrderComponent` i `DevicesPage`.
*   **Strona Zleceń**: Dodano przycisk "Przeładuj wszystkie dane" i metodę `HardLoadData` do jawnego odświeżania danych.

### Naprawiono / Ulepszenia
*   **Strona Urządzeń**: Zaimplementowano zapisywanie i ładowanie stanu Telerik Grid na `DevicesPage` w celu zachowania preferencji użytkownika między sesjami.
*   **Komponent Wyświetlania Urządzeń**:
    *   Dodano lokalizację dla różnych tekstów i formatowania czasu trwania.
    *   Ulepszono ładowanie obrazów za pomocą wskaźników ładowania i zoptymalizowano ładowanie obrazów w pełnym rozmiarze w oknie modalnym.
    *   Ulepszono okno szczegółów urządzenia dzięki ulepszonemu stylowi, `MaxHeight`, `ThemeColor` i funkcjonalności `CloseOnOverlayClick`.
*   **Komponent Zlecenia Pracy**: Dostosowano edytowalność pola opisu, aby umożliwić edycję nowych zleceń pracy niezależnie od konkretnych uprawnień.
*   **Klient API**: Zrefaktoryzowano `GetDeviceImageAsync` i `GetFullDeviceInfoAsync` w celu użycia `SingleResponse<DeviceImage>` dla lepszej spójności i dodano opcję `skipImageLoad` dla `GetFullDeviceInfoAsync`.
*   **Lokalizacja**: Dodano liczne nowe klucze lokalizacyjne dla elementów interfejsu użytkownika związanych z urządzeniami.
*   **Poprawka Błędu**: Poprawiono logikę filtrowania kategorii zleceń pracy według kategorii urządzenia w `ApiServiceClient`.

## 2025-08-25
### Nowe Funkcje
* Zaimplementowano kompleksowe funkcje zarządzania urządzeniami, w tym nowe metody klienta API (`GetDeviceDetailAsync`, `GetDeviceStateAsync`, `GetDeviceStatusAsync`, `GetSingleDeviceAsync`, `GetDeviceImageAsync`, `GetDeviceDictionariesAsync`, `GetFullDeviceInfoAsync`), nowe kontrolery po stronie serwera (`GetDetail`, `GetState`, `GetStatus`, `Get`, `GetImage`, `GetDict`) oraz nowe modele danych (`DeviceDetailProperty.cs`, `DeviceDict.cs`, `DeviceImage.cs`, `DeviceState.cs`, `DeviceStatus.cs`, `FullDeviceInfo.cs`).
* Ulepszono stronę Urządzeń, dodając domyślny `PageSize` równy 30 dla Telerik Grid.
* Dodano wpisy lokalizacyjne "NavMenu_Devices" dla języka angielskiego i polskiego.
* Wprowadzono nowy komponent `DeviceDisplay` i powiązany z nim CSS dla ulepszonej prezentacji informacji o urządzeniach.
* Zaktualizowano `DevicesPage.razor` w celu integracji nowego komponentu `DeviceDisplay` i ulepszenia interfejsu użytkownika związanego z urządzeniami.

## 2025-08-22
### Nowe Funkcje
* Dodano buforowanie dla plików zleceń pracy (`ApiServiceClient.GetWorkOrderFilesAsync`, `ApiServiceClient.GetWorkOrderFileAsync`) oraz publiczną metodę do unieważniania tych buforów (`ApiServiceClient.InvalidateWorkOrderFilesCache`).

### Naprawiono / Ulepszenia
* Dodano metodę pobierania plików zlecenia pracy (model `WorkOrderFile`, `ApiServiceClient.GetWorkOrderFilesAsync`, `WoController.GetFiles`).
* Dodano wyświetlanie informacji o użytkowniku na stronie Ustawień.
* Ulepszono wyświetlanie aktywności i funkcje interfejsu użytkownika.
* Zaktualizowano `ApiServiceClient` i `WoController` do używania `ApiResponse<WorkOrderFile>`.
* Zmieniono sortowanie listy aktywności na malejące.
* Naprawiono automatyczne zapisywanie rozmiarów paneli na stronie głównej.

## 2025-08-21
### Nowe Funkcje
* Zaimplementowano kompleksowe sprawdzanie uprawnień użytkownika w komponentach, aby kontrolować widoczność elementów interfejsu użytkownika i funkcjonalność w oparciu o role użytkowników.

### Naprawiono / Ulepszenia
* Naprawiono błąd, w którym pole `person_Take_Date` było zawsze puste podczas przyjmowania zlecenia.
* Dodano funkcję pobierania listy kategorii i przyczyn WorkOrder na podstawie ID urządzenia oraz użyto jej przy tworzeniu/edycji zleceń i kategorii aktywności.
* Ulepszono komponenty `TelerikComboBox`/wybory z listą o możliwość filtrowania wpisem (lepsze wyszukiwanie elementów).

## 2025-08-20
### Ulepszenia Wydajności
*   Poprawiono responsywność na stronie głównej podczas zwijania/rozwijania sekcji. Aplikacja działa teraz płynniej, ponieważ zapisywanie ustawień do lokalnej pamięci przeglądarki jest zoptymalizowane i odbywa się rzadziej.

## 2025-08-19
### Ulepszenia UI/UX
* Dodano przewijanie wirtualne w `OrdersGrid` dla płynniejszej nawigacji.
* Przebudowano stronę główną z nowymi kontenerami siatek i responsywnymi styliami CSS.
* Umożliwiono wyszukiwanie/filtrowanie w `TelerikMultiSelect` na stronie Harmonogramu.
* Zapisano pozycje splitterów i stan zwijalnych paneli między sesjami.

### Lokalizacja
* Dodano nowe ciągi lokalizacyjne dla ulepszeń interfejsu.

## 2025-08-18
### Ulepszenia UI/UX
* Dodano możliwość zwijania sekcji siatek na stronie głównej (Assigned, Taken, Department)
 * Dodano przyciski "+"/ "-" w nagłówkach siatek
 * Dodano filtrowanie wielokrotnego wyboru na stronach Orders i Scheduler

### Lokalizacja
* Poprawiono tłumaczenie komunikatów błędów w formularzu dodawania aktywności

### Zmiany w kodzie
* Zaktualizowano `Home.razor`, aby owinąć zawartość siatek w zwijalne kontenery z dynamicznymi klasami
* Zaktualizowano `Home.razor.css` do resetowania wysokości kontenera po zwinięciu
* Rozszerzono `ActivityController` o metody ActDict do dodawania nowych aktywności

## 2025-08-12 - 2025-08-13
### Ulepszenia UI/UX
*   **Menu Kontekstowe dla Siatek:** Zaimplementowano menu kontekstowe (prawy przycisk myszy) dla siatek na stronie głównej, co poprawia interakcję użytkownika.
*   **Przycisk Nowego Zlecenia:** Dodano przycisk do tworzenia nowych zleceń na stronie głównej.
*   **Lokalizacja:** Dodano tłumaczenia dla nowo wprowadzonego menu kontekstowego.

### Zmiany w kodzie i wydajności
*   **Obsługa Zdarzeń:** Zaimplementowano zdarzenie `OnRowContextMenu` dla odpowiednich komponentów w celu obsługi nowej funkcjonalności menu kontekstowego.

## 2025-08-11
### Ulepszenia UI/UX
*   **Utrwalanie stanu widoku:** Aplikacja zapamiętuje teraz stan siatek i filtrów na stronach "Główna", "Zlecenia" i "Harmonogram". Po ponownym załadowaniu strony lub nowym zalogowaniu, wszystkie ustawienia sortowania, filtrowania i paginacji zostaną przywrócone.
*   **Strona główna:** Dodano wskaźnik informujący, czy użytkownik może przyjmować wiele zleceń jednocześnie.
*   **Harmonogram:** Dodano przycisk "Dodaj aktywność" bezpośrednio w oknie edycji spotkania, co upraszcza dodawanie nowych aktywności do zlecenia.
*   **Formularz dodawania aktywności:** Ulepszono logikę obliczania czasu pracy.

### Zmiany w kodzie i wydajności
*   **Wdrożono `ViewSettingsService`:** Utworzono nową usługę do zarządzania ustawieniami widoku specyficznymi dla użytkownika. Odpowiada za zapisywanie i ładowanie stanów komponentów (siatek, filtrów) z lokalnej pamięci przeglądarki oraz synchronizację ich z serwerem.
*   **Rozszerzono API:** Dodano nowe punkty końcowe w `SettingsController` do zapisywania i pobierania ustawień widoku dla konkretnego użytkownika.
*   **Refaktoryzacja komponentów:** Komponent `OrdersGrid` został zrefaktoryzowany w celu lepszego zarządzania stanem. Poprawiono logikę ładowania danych i stosowania filtrów na stronach `HomePage`, `OrdersPage` i `SchedulerPage`, aby zapewnić płynniejsze działanie.
*   **Optymalizacja zarządzania stanem:** Scentralizowano zarządzanie stanem widoków za pomocą `ViewSettingsService`, co zmniejsza duplikację kodu i upraszcza konserwację.

## 2025-08-07 - 2025-08-08
### Naprawiono
- Poprawiono drobny błąd logiczny w komponencie `WorkOrderComponent`, który wpływał na powiadomienia o zmianie stanu.
- Rozwiązano problem `AntiforgeryValidationException` przy wdrożeniu na serwerze poprzez skonfigurowanie trwałych kluczy ochrony danych, co zapewnia stabilność sesji po restarcie aplikacji.

### Zmieniono
- Zrefaktoryzowano formularze (`AddActivityForm`, `AppointmentEditor`) w celu uzyskania lepszego układu, walidacji i ulepszonej logiki interakcji z użytkownikiem.
- Ulepszono siatkę zleceń (`OrdersGrid`) oraz stronę główną (`Home`), aby zapewnić lepsze aktualizacje danych w czasie rzeczywistym i bardziej responsywne doświadczenie użytkownika.
- Zaktualizowano elementy interfejsu użytkownika, w tym etykiety przycisków i ikony nawigacyjne, dla większej przejrzystości i nowocześniejszego wyglądu.
- Zmodyfikowano `WorkOrderComponent`, aby poprawnie wyświetlał przypisanych użytkowników i wydajniej obsługiwał odświeżanie danych.

### Dodano
- Wprowadzono funkcję "Podejmij zlecenie", umożliwiającą użytkownikom przypisywanie się do zlecenia bezpośrednio z interfejsu.
- Dodano nowe punkty końcowe API i modele żądań do obsługi funkcjonalności "Podejmij zlecenie".
- Wprowadzono w `AppointmentService` metodę do odświeżania pojedynczych terminów, co poprawia spójność danych.

## 2025-08-06
### Dodano
- **Strona Zleceń**: Utworzono nowy, oddzielny komponent `OrdersGrid` do wyświetlania listy Zleceń, co poprawia modułowość.
- **Strona Zleceń**: Zlecenia pracy są teraz edytowalne bezpośrednio z listy Zleceń.
- **Timeline**: Dodano przyciski szybkiego wyboru, aby ustawić widok osi czasu na 3, 5, 7 lub 14 dni.
- **Strona Główna**: Wprowadzono sprawdzanie sesji użytkownika.

### Zmieniono
- **Timeline**: Widok osi czasu wyświetla teraz tylko osoby wybrane w filtrze.
- **Lokalizacja**: Dodano nowe tłumaczenia dla elementów interfejsu użytkownika w języku angielskim i polskim.
- **Zmiana Nazwy Modelu**: Model danych `Dict` został przemianowany na `WODict` dla większej przejrzystości.

### Naprawiono
- **Strona Zleceń**: Poprawiono sortowanie Zleceń, aby było malejące według ID.
- **Strona Zleceń**: Dane w siatce odświeżają się teraz automatycznie po edycji zlecenia pracy.

## 2025-08-05
### Dodano
- Dodano przycisk "Wyczyść filtry" do list aktywności i zleceń pracy.
- Zaimplementowano filtrowanie wielokrotnego wyboru dla kolumn siatki (np. AssetNo w Harmonogramie).
- Dodano filtrowanie zakresu dat (Ostatni rok, Kwartał, Miesiąc) do kolumny "Data dodania" w siatce nieprzypisanych zleceń Harmonogramu.
- Dodano kolumnę "Opis" do siatki na stronie Zleceń.
- Dodano model `ListTypeEnum` (wewnętrzna zmiana wspierająca przyszłe skategoryzowane listy).

### Zmieniono
- Ulepszono UI/UX w komponentach ActivityDisplay, ActivityList, WorkOrderComponent, OrdersPage i SchedulerPage poprzez różne aktualizacje stylów dla lepszej czytelności i atrakcyjności wizualnej.
- Standaryzowano odwołania do kluczy lokalizacji w komponentach UI dla lepszej utrzymywalności.
- Usunięto przycisk "Wybierz wszystkie urządzenia" ze strony Zleceń.
- Ulepszono okno szczegółów zlecenia pracy na stronie Zleceń, dodając ciemny motyw i niestandardową akcję zamknięcia.
- Poprawiono czytelność ID zlecenia pracy w tytule okna szczegółów.
- Standaryzowano tytuły kolumn w siatce nieprzypisanych zleceń Harmonogramu, używając zlokalizowanych ciągów znaków.
- Zaktualizowano style wierszy na stronie Zleceń dla lepszego wizualnego rozdzielenia i kompaktowości.

### Naprawiono
- Poprawiono logikę filtrowania w Harmonogramie, aby prawidłowo uwzględniać osoby "Nieprzypisane" po ich wybraniu.

## 2025-08-04
### Dodano
- **Dołączanie do Istniejącej Aktywności**: Użytkownicy mogą być teraz dodawani do istniejącej aktywności za pomocą nowego formularza w widoku zlecenia pracy.
- **Dodawanie Aktywności**: Zaimplementowano funkcję dodawania nowych aktywności bezpośrednio z widoku zlecenia pracy, włączając w to dedykowany formularz i punkt końcowy API.

### Naprawiono
- **Błąd TimeSlot w Harmonogramie**: Poprawiono błąd związany z obliczeniami TimeSlot na stronie harmonogramu.
- **Odświeżanie Listy Aktywności**: Lista aktywności jest teraz automatycznie aktualizowana po dodaniu nowej aktywności.
- **Trwałość Danych Zlecenia Pracy**: Naprawiono błąd, przez który szczegóły zlecenia (np. liczba aktywności) wracały do poprzedniego stanu po interakcjach w interfejsie użytkownika.

### Zmieniono
- **UI/UX**: Poprawiono układ i style komponentu `WorkOrderComponent` w celu zwiększenia czytelności i użyteczności.

## 2025-08-01
### Dodano
- **Edycja Powodu Zlecenia Pracy**: Pole `WOReason` jest teraz edytowalne w edytorze zlecenia pracy.
- **Widok Przypisanych Osób**: Dodano możliwość przeglądania przypisanych osób w szczegółach zlecenia pracy.
- **Wsparcie dla Języka Polskiego**: Wprowadzono wsparcie dla języka polskiego. Język można przełączać po zalogowaniu oraz w Ustawieniach.
### Zmieniono
- **Walidacja Zlecenia Pracy**: Zmodyfikowano walidację danych zlecenia pracy w oknie edytora.
- **Daty Zlecenia Pracy**: Pola `StartDate` i `EndDate` w edytorze zlecenia pracy są teraz edytowalne.
### Naprawiono
- **Błąd Listy Działów**: Naprawiono błąd związany z ładowaniem listy działów.
- **Błąd Nowego Zlecenia Pracy**: Naprawiono błąd związany z tworzeniem nowego zlecenia pracy (spotkania).

## 2025-07-30
### Dodano
- **Zaawansowane Filtry Grid**: Wprowadzono zaawansowane możliwości filtrowania w `TelerikGrid` na stronie `OrdersPage`, w tym filtry dla Department, Reason, Modified Person, Assigned Person, Work Order State, Work Order Level, Add Date i Start Date.
- **Widok Osób w Aktywnościach**: Dodano możliwość przeglądania przypisanych osób po nazwisku w komponentach `ActivityDisplay` i `ActivityList`.
- **Cache Danych Osób**: Zaimplementowano cache dla danych osób w `ApiServiceClient` w celu poprawy wydajności.

### Zmieniono
- **UI/UX - Orders Page**: Usunięto stare filtry dla Device Type i Order State.
- **UI/UX - Kolory Statusu Zlecenia**: Zaktualizowano logikę wyświetlania kolorów statusu zlecenia, aby opierała się na `StateID` dla spójności. Wprowadzono nowe, jaśniejsze kolory tła dla lepszej czytelności.

### Naprawiono
- **Błąd Uwierzytelniania**: Zmodyfikowano obsługę nagłówków uwierzytelniania, aby czyścić stan użytkownika po żądaniach logowania.

## 2025-07-29
### Dodano
- **Menu Kontekstowe Harmonogramu**: Wprowadzono menu kontekstowe (prawy przycisk myszy) w Harmonogramie. Użytkownicy mogą teraz:
    - Tworzyć nowe spotkania, klikając prawym przyciskiem myszy na pustym slocie czasowym.
    - Otwierać, usuwać lub duplikować istniejące spotkania, klikając na nie prawym przyciskiem myszy.
### Zmieniono
- **Ustawienia**: Zmiana adresu API na stronie Ustawień teraz automatycznie przeładowuje aplikację, aby zapewnić poprawne zastosowanie nowych ustawień.
- **Schemat Kolorów**: Zrefaktoryzowano logikę kolorów statusu, aby opierała się na identyfikatorach stanu (State ID), zapewniając spójną reprezentację kolorów w całej aplikacji (z uwzględnieniem ustawienia "Użyj oryginalnych kolorów"). Wprowadzono nowe, jaśniejsze kolory tła dla lepszej czytelności.

### Naprawiono
- **Uwierzytelnianie**: Naprawiono błąd, który mógł powodować nieprawidłowe uruchomienie sprawdzania sesji na stronie logowania, prowadząc do pętli przekierowań.
- **Edytor Spotkań**: Anulowanie tworzenia nowego spotkania teraz poprawnie zamyka okno edytora.

## 2025-07-28
### Dodano
- **Przełącznik Schematów Kolorów**: Dodano pole wyboru "Użyj oryginalnych kolorów" w menu nawigacyjnym. Pozwala to użytkownikom przełączać się między oryginalnymi kolorami dostarczanymi przez API a standaryzowaną paletą kolorów.

### Zmieniono
- **Interfejs Użytkownika (UI/UX)**:
    - Zastąpiono podstawowe wskaźniki ładowania komponentem `TelerikLoaderContainer` na stronach `OrdersPage` i `SchedulerPage`.
    - Całkowita liczba zleceń jest teraz wyświetlana w nagłówku strony `OrdersPage`.
- **Refaktoryzacja**:
    - Scentralizowano zarządzanie kolorami w `AppStateService`, aby zapewnić spójny schemat kolorów we wszystkich komponentach (`SchedulerSummary`, `OrdersPage`, `SchedulerPage`).
    - Komponenty subskrybują teraz `AppStateService.OnChange`, aby dynamicznie aktualizować swój wygląd po zmianie schematu kolorów.
- **Zmiana Adresu API**: Wprowadzono mechanizm restartu aplikacji po zmianie adresu zewnętrznego API na stronie Ustawień. Zapewnia to poprawne zastosowanie nowego adresu.
- **Potwierdzenie Zmiany API**: Dodano okno dialogowe Telerik na stronie Ustawień, które pojawia się, gdy użytkownik próbuje zmienić adres API. Dialog wyświetla stary i nowy adres oraz ostrzega, że aplikacja zostanie ponownie uruchomiona.
- **Refaktoryzacja**: 
    - Zastąpiono przestarzały `ListTypeEnum` poprawnym `WOListTypeEnum` w całej aplikacji klienckiej.
    - Ulepszono logikę ładowania danych i filtrowania na stronie `SchedulerPage` dla działów i kategorii urządzeń.
    - Zrefaktoryzowano `ApiServiceClient` do poprawnego obsługiwania `personID` (który może być null) w metodach związanych ze słownikami.

### Naprawiono
- **Obsługa Błędów**: Ulepszono obsługę wyjątków w `UserSettings.cs`, aby zapewnić, że błędy są prawidłowo zgłaszane i logowane.

## 2025-07-24
### Zmieniono
- **Konfiguracja Adresu API**: Adres zewnętrznego API może być teraz konfigurowany na stronie Ustawień przez użytkownika "MES". Adres jest przechowywany globalnie dla aplikacji w pliku `appsettings.json` na serwerze.
- **Obsługa Adresu API**: Wywołania po stronie serwera zostały ujednolicone, aby poprawnie obsługiwać adresy serwerów API z przyrostkiem `/api/v1` lub bez niego, korzystając ze scentralizowanego adresu z konfiguracji.

### Naprawiono
- **Przeciąganie i Upuszczanie w Harmonogramie**: Naprawiono krytyczny błąd, który powodował, że przeciągnięcie zlecenia z siatki do Harmonogramu kończyło się niepowodzeniem, jeśli Harmonogram nie był w widoku "Osi czasu" (Timeline). Logika przypisywania jest teraz poprawnie stosowana tylko w widoku Osi czasu, co zapobiega błędom w innych widokach.

## 2025-07-23
### Dodano
- **Wyświetlanie Daty Kompilacji**: Wprowadzono mechanizm wyświetlania daty kompilacji aplikacji w głównym layoucie, co dostarcza jasnej informacji o wersji.
- **Tabela Podsumowująca w Harmonogramie**: Na stronie Harmonogramu (w widoku osi czasu) dodano tabelę podsumowującą, która pokazuje liczbę zleceń pracy dla każdej osoby z podziałem na statusy. Umożliwia to szybki przegląd obciążenia każdego członka zespołu.
- **Filtrowanie na Stronie Zleceń**: Wprowadzono nowy filtr "Przypisane osoby" na stronie Zleceń, umożliwiając użytkownikom zawężenie listy zleceń do konkretnych członków zespołu.

### Naprawiono
- **Przeciąganie i Upuszczanie w Harmonogramie**: Naprawiono błąd, który powodował, że przeciągnięcie zlecenia z siatki na oś czasu w Harmonogramie nie przypisywało poprawnie zlecenia do docelowej osoby (zasobu).
- **Zachowanie Filtra w Interfejsie Użytkownika**: Filtr "Przypisane osoby" na stronie Zleceń teraz natychmiast aktualizuje listę po jego wyczyszczeniu za pomocą przycisku "Wyczyść".
- **Wyrównanie Etykiety Filtra**: Poprawiono wyrównanie etykiety filtra "Przypisane osoby" na stronie Zleceń, aby zapewnić jej prawidłowe umiejscowienie nad komponentem multiselect, zgodnie z pozostałymi filtrami.

## 2025-07-22
## Dodano
- **Widok osi czasu harmonogramu (Timeline View):** Wprowadzono nowy widok osi czasu dla harmonogramu, umożliwiający poziome wyświetlanie spotkań.
- **Dynamiczne ustawienia widoku osi czasu:** Dodano elementy sterujące interfejsu użytkownika do dynamicznej regulacji `Szerokości kolumn` (Column Width), `Czasu trwania slotu` (Slot Duration) i `Podziałów slotów` (Slot Divisions) dla widoku osi czasu, zapewniając większą elastyczność w wyświetlaniu interwałów czasowych.
- **Komponent elementu harmonogramu:** Wprowadzono komponent `SchedulerItem` do ponownego użycia w celu standaryzacji renderowania spotkań w różnych widokach harmonogramu.
- **Wskaźnik ładowania harmonogramu:** Dodano wskaźnik ładowania do harmonogramu, aby zapewnić wizualną informację zwrotną podczas pobierania danych i procesów autoryzacji.

## Zmieniono
- **Logika grupowania harmonogramu:** Rozwiązano problem `KeyNotFoundException` w grupowaniu widoku osi czasu, zapewniając prawidłowe odwołanie do pola zasobu (`AssignedPerson`) w `SchedulerGroupSettings`.
- **Ulepszenia interfejsu użytkownika harmonogramu:** Zastosowano niestandardowe style do elementów harmonogramu i komórek dnia w widokach Miesiąc, Dzień i Tydzień w celu poprawy wyglądu wizualnego.
- **Refaktoryzacja zarządzania pamięcią podręczną:** Zrefaktoryzowano wewnętrzne zarządzanie pamięcią podręczną dla spotkań, co prowadzi do bardziej wydajnej obsługi danych i płynniejszych aktualizacji interfejsu użytkownika w harmonogramie.
- **Zmiana nazwy metody usługi spotkań:** Zmieniono nazwę metody `CloseAppointment` na `RemoveAppointment` w `AppointmentService` dla jaśniejszej semantyki.

## Naprawiono
- `KeyNotFoundException`: Rozwiązano problem `KeyNotFoundException` występujący podczas przełączania na widok osi czasu Telerik Scheduler, w szczególności podczas grupowania według przypisanych osób.


## 2025-07-21
- **Refaktoryzacja i Poprawki Błędów:**
    - Zrefaktoryzowano zarządzanie pamięcią podręczną dla obiektów `WorkOrder` w `ApiServiceClient`, aby zapewnić spójność danych. Wprowadzono scentralizowane metody do aktualizacji, unieważniania i odświeżania pamięci podręcznej (`UpdateWorkOrderInCache`, `InvalidateWorkOrdersCache`, `RefreshWorkOrderInCacheAsync`).
    - Wszystkie metody manipulacji `WorkOrder` (`Update`, `Save`, `Close`) korzystają teraz z nowej, scentralizowanej logiki pamięci podręcznej.
    - Poprawiono logikę aktualizacji interfejsu użytkownika w `SchedulerPage.razor`. Lokalna kolekcja spotkań (`_allAppointments`) jest teraz poprawnie modyfikowana po zmianie lub zamknięciu spotkania, co zapewnia, że interfejs użytkownika odzwierciedla aktualny stan.
    - Ulepszono `AppointmentEditor.razor` do obsługi zamykania zleceń pracy i poprawnego propagowania aktualizacji interfejsu użytkownika.
    - Dodano model `WorkOrderInfo.cs` i zaktualizowano `WoController` do obsługi pobierania szczegółowych informacji o zleceniach pracy.

## 2025-07-09
- **Funkcjonalność i Interfejs Użytkownika:**
    - Wdrożono pełny cykl życia dla Zleceń Pracy (Work Orders), umożliwiając ich tworzenie, aktualizację i zamykanie przez API.
    - Dodano solidną walidację dla wymaganych pól (takich jak Opis, Poziom) w edytorze Zleceń Pracy, z wizualnym podświetleniem brakujących danych.
    - Zamykanie Zlecenia Pracy wymaga teraz co najmniej jednej powiązanej czynności (activity).
    - Wyskakujące powiadomienia wyświetlają teraz konkretne komunikaty o błędach z API, zapewniając użytkownikowi jaśniejszą informację zwrotną.
- **Techniczne i Refaktoryzacja:**
    - Wprowadzono sprawdzanie sesji po stronie serwera przy ładowaniu aplikacji. Użytkownicy są teraz automatycznie wylogowywani, jeśli ich sesja na serwerze jest nieprawidłowa, co zapobiega błędom API.
    - Zrefaktoryzowano metody `ApiServiceClient` (`Save`, `Update`, `Close`), aby zwracały obiekt `SingleResponse<T>`, dostarczając szczegółowych informacji o błędach.
    - Dodano dedykowane punkty końcowe API i modele żądań (`WorkOrderCreateRequest`, `UpdateWorkOrderRequest`, `CloseWorkOrderRequest`) dla wszystkich operacji na Zleceniach Pracy.
    - Przełączono się z używania nazw w postaci ciągów znaków na identyfikatory (ID) dla encji takich jak Kategorie i Poziomy, co poprawia integralność danych.

## 2025-07-08
- **Ulepszenie zarządzania zleceniami pracy**: Zaimplementowano walidację i mapowanie `WorkOrder` na `WorkOrderCreateRequest` po stronie klienta w `ApiServiceClient.SaveWorkOrderAsync`. Punkt końcowy `WoController.Create` teraz bezpośrednio akceptuje `WorkOrderCreateRequest`. `ApiServiceClient.UpdateWorkOrderAsync` wykorzystuje teraz `SaveWorkOrderAsync` dla nowych zleceń pracy, zapewniając spójne zapisywanie do API. Dodano pola `ReasonID`, `CategoryID`, `DepartmentID` i `AssignedPersonID` do `WorkOrderCreateRequest`.
- **Synchronizacja tokenów między klientem a serwerem**: Zaimplementowano solidny mechanizm, w którym serwer Blazor pobiera tokeny zewnętrznego API ze swojej pamięci podręcznej, używając `PersonID` dostarczonego przez klienta w niestandardowym nagłówku HTTP `X-Person-ID`. Eliminuje to potrzebę bezpośredniego wysyłania tokena zewnętrznego API z klienta do serwera Blazor.
- **Ulepszenie inicjalizacji po stronie klienta**: Upewniono się, że dane `UserState` są w pełni załadowane z `localStorage` przed wykonaniem jakichkolwiek wywołań API po stronie klienta. Osiągnięto to poprzez oczekiwanie na `UserState.InitializationTask` w `AuthHeaderHandler` i odpowiednich stronach Razor (`SchedulerPage`, `OrdersPage`, `Settings`).
- **Zarządzanie tokenami po stronie serwera**: `IdentityController` teraz buforuje `IdentityData` (w tym token zewnętrznego API) po pomyślnym zalogowaniu użytkownika. `ServerAuthTokenService` został zrefaktoryzowany w celu pobierania tych tokenów z pamięci podręcznej serwera (`IMemoryCache`) na podstawie `PersonID` wyodrębnionego z nagłówka `X-Person-ID`.
- **Obsługa błędów API**: Dodano bloki `try-catch` do wszystkich metod żądań HTTP w `ApiServiceClient`, aby elegancko obsługiwać błędy sieciowe i API, logując je do konsoli i zwracając puste/domyślne wartości.
- **Udoskonalenie i czyszczenie zależności**: Usunięto przestarzałe konfiguracje uwierzytelniań JWT Bearer i zakodowane na stałe dane logowania z pliku `Program.cs` po stronie serwera, usprawniając konfigurację uwierzytelniania i wstrzykiwania zależności.
- **Dokumentacja API**: Dodano `API-info.md` w celu zapewnienia jasnej dokumentacji punktów końcowych API i ich typów autoryzacji.

## 2025-07-07
- **Interfejs użytkownika i funkcjonalność:**
    - Ulepszone logowanie z rozwijaną listą wyszukiwania nazwy użytkownika i podstawową walidacją danych wejściowych.
    - Ulepszony edytor zleceń pracy z dynamicznym wyborem kategorii i poziomów oraz ulepszonym wyświetlaniem urządzeń/kategorii.
    - Automatyczne buforowanie nowych wpisów słownikowych (kategorie, poziomy, działy).
    - Udoskonalono logikę filtrowania zleceń pracy na stronie harmonogramu.
    - Ulepszona obsługa błędów klienta API.
- **Zmiany w kodzie:**
    - Usunięto parametr `ListDicts` z `AppointmentEditor.razor`.
    - Zaktualizowano `WorkOrderComponent.razor` do używania `ListCategories` i `ListLevels` dla TelerikComboBox, usunięto logikę inicjalizacji `ListDicts` i dodano logikę aktualizacji `selectedDevice`.
    - Zastąpiono bezpośrednie manipulacje `ListDicts` wywołaniami `apiService.AddNewWODict` dla kategorii, działów i poziomów.
    - Zastąpiono pole wprowadzania nazwy użytkownika komponentem `TelerikComboBox` w `Login.razor`.
    - Dodano wskaźnik ładowania i usunięto parametr `ListDicts` ze `SchedulerPage.razor`.
    - Zmodyfikowano logikę filtrowania zleceń pracy w `SchedulerPage.razor`.
    - Dodano `_dictCache` i nowe metody związane ze słownikami (`GetWODictionaries`, `GetWODictionariesCached`, `GetWOCategories`, `GetWOStates`, `GetWOLevels`, `GetWOReasons`, `GetWODepartments`, `AddNewWODict`) do `ApiServiceClient.cs`.
    - Ulepszono obsługę błędów w `PostSingleAsync` w `ApiServiceClient.cs`.
    - Dodano konstruktor bez parametrów do `UserState.cs`.
    - Zmieniono punkt końcowy logowania na `api/v1/identity/loginpass` i metodę na GET w `IdentityController.cs`.
    - Zarejestrowano `UserState` jako usługę Scoped w `Program.cs`.

## 2025-07-04
- **Interfejs użytkownika i funkcjonalność:**
    - Zaimplementowano autoryzację użytkownika za pośrednictwem strony `Login.razor`.
    - Zintegrowano `UserState` do zarządzania danymi logowania i tokenem bieżącego użytkownika.
- **Zmiany w kodzie:**
	- Dodano właściwość `Password` do `UserState.cs`.
    - Zrefaktoryzowano `AuthHeaderHandler.cs` w celu pobierania danych uwierzytelniających użytkownika z `UserState` do odświeżania tokena.
    - Zmodyfikowano `BlazorTool.Client/Program.cs` w celu prawidłowego wstrzykiwania `UserState` do `AuthHeaderHandler`.
    - Dodano metodę `PostSingleAsync` do `ApiServiceClient.cs` do obsługi odpowiedzi API zawierających pojedynczy obiekt.
    - Zaktualizowano `Login.razor` w celu użycia `ApiServiceClient.PostSingleAsync` do uwierzytelnia i aktualizacji `UserState` po pomyślnym zalogowaniu.
    - Uzupełniono model `RightMatrix` w celu prawidłowej deserializacji uprawnień użytkownika.
    - Dodano właściwość `Expires` do modelu `IdentityData`.
    - Zrefaktoryzowano `AuthHeaderHandler` w celu użycia `IdentityData` i `ApiResponse<IdentityData>` do zarządzania tokenami.
    - Ulepszono `UserState` w celu utrwalania `IdentityData` w `localStorage` i ładowania jej przy uruchomieniu.
    - Zmodyfikowano `Login.razor` w celu jawnego zapisywania `IdentityData` w `UserState` (a tym samym w `localStorage`) po pomyślnym zalogowaniu.

## 2025-07-03
- **Interfejs użytkownika i funkcjonalność:**
    - Zaimplementowano tworzenie nowych spotkań w Harmonogramie.
    - Dodano pole kombi do wyboru urządzenia podczas tworzenia nowego spotkania.
    - Zaimplementowano walidację wymaganych pól w Edytorze Spotkań.
    - Dodano wyskakujące powiadomienie informujące użytkowników o niewypełnionych wymaganych polach.
    - Zmieniono nazwę pola "Title" na "AssetNo" w Edytorze Spotkań i podświetlono je, jeśli jest puste.
    - Ulepszono stronę `ChangelogPage` w celu renderowania Markdown dla lepszej czytelności.
- **Zmiany w kodzie:**
    - Dodano logikę do `AppointmentService` do obsługi tworzenia nowych spotkań.
    - Zaimplementowano zapisywanie nowych spotkań za pośrednictwem `ApiServiceClient`.
    - Dodano rozwijaną listę do wyboru `Device` w `WorkOrderComponent` dla nowych spotkań.
    - Dodano walidację w `AppointmentEditor.razor`.
    - Zaimplementowano wyskakujące okienko Telerik do powiadomień w `AppointmentEditor.razor`.
    - Zaktualizowano style w `AppointmentEditor.razor.css`.
    - Dodano pakiet `Markdig` do renderowania Markdown na stronie `ChangelogPage`.

## 2025-07-02
- **Interfejs użytkownika i funkcjonalność:**
    - Dodano filtr statusu na stronie `OrdersPage`.
    - Poprawiono logikę filtrowania na stronie `OrdersPage`.
    - Dodano opisowe etykiety dla filtrów.
    - Dodano sumaryczną pracochłonność do nagłówka listy działań.
    - Komponent `WorkOrderComponent` teraz rozszerza się, aby pokazać pełną listę działań bez przewijania.
    - Wyrównano rozmiary kolumn między nagłówkiem w `ActivityList` a elementami w `ActivityDisplay` dla spójnego wyglądu.
    - Użytkownicy mogą teraz klikać na statystykę "Akcje" w widoku zlecenia pracy, aby zobaczyć szczegółową listę działań.
    - Lista działań jest wyświetlana w kompaktowym, czytelnym formacie tabeli.
- **Zmiany w kodzie:**
    - Dodano obliczanie `totalWorkload` w `ActivityList.razor`.
    - Użyto warunkowej klasy CSS w `WorkOrderComponent.razor` do dostosowania `max-height`, gdy działania są widoczne.
    - Poprawiono deserializację JSON dla modelu `Activity`, dodając `[JsonConstructor]` do domyślnego konstruktora.
    - Utworzono model `Activity.cs`.
    - Dodano `ActivityController` do pobierania danych o działaniach z zewnętrznego API.
    - Zaimplementowano `GetActivityByWO` w `ApiServiceClient` do pobierania działań.
    - Opracowano komponenty Blazor `ActivityList` i `ActivityDisplay` do wyświetlania działań.
    - Zmodyfikowano `WorkOrderComponent` do wyświetlania listy działań po interakcji użytkownika.
    - Porządek na ChangelogPage

## 2025-07-01
- **Interfejs użytkownika i funkcjonalność:**
    - Na stronie `SchedulerPage` dodano filtr `Device` po `AssetNo` za filtrem `Department`.
    - Umożliwiono edycję `WorkOrderComponent`, dodając Telerik ComboBox dla pola `WOCategory`, `WOLevel`.
    - W komponencie `WorkOrderComponent` wyróżniono puste pola `Department` i `StartDate`.
    - Na stronie `SchedulerPage` zmieniono logikę wyświetlania zajętych/niezajętych zleceń.
    - Na stronie `SchedulerPage` dostosowano tekst i kolor elementu harmonogramu w zależności od jego stanu.
- **Zmiany w kodzie:**
    - Dodano nowy punkt końcowy `api/v1/wo/getdict` do `WoController` w celu pobierania kategorii zleceń pracy.
    - Zaktualizowano `ApiServiceClient` do pobierania obiektów `Dict`.
    - Dodano model `Dict.cs`.
- **Poprawki błędów:**
    - Naprawiono problem w `WorkOrderComponent`, gdzie `Department` i `assignedPersons` nie wyświetlały się poprawnie.

## 2025-06-30
- **Interfejs użytkownika i funkcjonalność:**
    - Umożliwiono edycję komponentu `WorkOrderComponent`, dodając Telerik components dla pola `assignedPersons`, `Department` i `Description`.
    - Na stronie `OrderPage` dodano kolumnę 'Device'.
    - Na stronie `SchedulerPage` dodano funkcjonalność `OnClick` dla zleceń (aby przeglądać WorkOrder na liście zleceń) oraz zaimplementowano filtry nagłówków dla zleceń.
- **Zmiany w kodzie:**
    - Utwożono i dodano plik `telerik_manual.md` z instrukcjami użycia komponentów Telerik.

## 2025-06-27
- Dodano stronę Dziennika zmian i zaktualizowano mechanizm logowania.
- Wprowadzono pliki `ChangelotPage.razor` i `CHANGELOG.md` do obsługi informacji o wydaniu.
- Zastąpiono `Debug.Print` przez `Console.WriteLine` w celu ujednolicenia logowania po stronie klienta i serwera.
- Drobne poprawki w interfejsie użytkownika (UI) w plikach `MainLayout.razor` i `OrdersPage.razor`.
- Skonfigurowano kopiowanie pliku `CHANGELOG.md` do katalogu `wwwroot` w celu zapewnienia dostępu po stronie klienta.
- Zaimplementowano wyświetlanie zawartości pliku `CHANGELOG.md` na stronie `ChangelogPage.razor`.
- Skonfigurowano domyślny `HttpClient` w `Program.cs` z `BaseAddress` dla dostępu do plików statycznych.
- Poprawiono atrybut `Link` w pliku `BlazorTool.Client.csproj` dla `CHANGELOG.md`, aby zapewnić jego prawidłowe umieszczenie w `wwwroot`.
- Wycofano zmianę `app.UseBlazorFrameworkFiles()` w `BlazorTool/Program.cs` ponieważ powodowała problemy z ładowaniem aplikacji.
- Ponownie dodano rejestrację `HttpClient` z `BaseAddress` i sprawdzaniem `serverBaseUrl` w `BlazorTool.Client/Program.cs`.
- **SchedulerPage.razor:**
    - Dodano filtr kategorii urządzeń do siatki nieprzypisanych zleceń.
    - Zaimplementowano pole wyboru (checkbox) do pokazywania/ukrywania zrealizowanych zleceń w siatce nieprzypisanych zleceń.
    - Dodano zdarzenie `OnChange` dla pola wielokrotnego wyboru (multiselect) z przypisanymi osobami, aby wyzwalać natychmiastową aktualizację widoku.
    - Dostosowano układ kontrolek filtrowania i widoku zleceń.

## 2025-06-26
- Ulepszono interfejs użytkownika (UI) Schedulera i zaktualizowano tryb renderowania Blazor.
- Skonfigurowano klienta HttpClient po stronie klienckiej, aby obsługiwał dynamiczny bazowy adres URL serwera.
- Zaktualizowano plik .gitignore.
- Zaimplementowano `PersonController` i zrefaktoryzowano klienta API do obsługi zewnętrznych wywołań.

## 2025-06-25
- W trakcie implementacji: dodawanie filtrów do Schedulera.
- Komponent `WorkOrderComponent`: dodano przewijanie, bardziej kompaktowy widok danych, dynamiczne kolory statusu. Strona `OrdersPage`: wyrównano elementy filtra w jednej linii.

## 2025-06-24
- Dodano filtry na stronie Zleceń, w tym pole wielokrotnego wyboru (multiSelect).
- Naprawiono problemy z autoryzacją (AUTH), dodano różne klienty HTTP.
- Refaktoryzacja pliku `Program.cs`: wstrzykiwanie zależności (DI), dodano `AuthHeaderHandler` do pobierania tokenu, usunięto pobieranie tokenu dla `ApiServiceClient`, dodano `HostAddress` w `appSettings` dla serwera.
- Wyrównano przyciski w edytorze Schedulera.

## 2025-06-23
- Zaimplementowano metody `Save/Delete` w `AppointmentService` oraz `Save/DeleteAppointment` w `ApiServiceClient`..
- Dodano widok terminów w Schedulerze oraz funkcjonalność przesuwania/aktualizowania elementów.
- Zaktualizowano klasę `SchedulerAppointment`, aby dziedziczyła po `WorkOrder`.
- Ustawiono komponent `WorkOrderComponent` jako tylko do odczytu.

## 2025-06-20
- Skonfigurowano układ 3-kolumnowy na stronie Zleceń.
- Włączono otwieranie zleceń po kliknięciu.
- Zaimplementowano sprawdzanie połączenia z API serwera oraz zapis i odczyt ustawień.
- Dodano kontroler `SettingsController`.
- Dodano stronę Ustawień (`Settings.razor`) z opcją `TestApiAddress` i metodą `CheckApiAddress` w `ApiServiceClient`.
- Dodano stronę Zamówienia (`OrderPage`).