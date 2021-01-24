
# ServerTCP

### Wprowadzenie - opis systemu

 <p align="justify">Aplikacja serwera asynchronicznego TCP z zaimplementowanym wzorcem TAP umożliwiająca logowanie użytkownika do systemu oraz założenie konta.  Użytkownik za pomocą klienta łączy się z serwerem przy użyciu podanego adresu IPv4 oraz portu. Następnie klient loguje się na serwerze za pomocą loginu i hasła. Po zalogowaniu klient ma dostęp do podstawowej wersji chmury – obsługa plików txt (możliwość dodawania, edycja, usuwania oraz przeglądania danych). Aplikacja kliencka dodatkowo została wyposażona w obsługę profilu użytkownika oraz podgląd historii działań (obie te rzeczy przechowywane są po stronie serwera). Prosta, intuicyjna aplikacja, umożliwiająca dalszy rozwój projektu. Serwer został wyposażony w bazę danych (SQLite), dzięki czemu po wyłączeniu serwera pozostają dane zarejestrowanych użytkowników oraz ich plików. Obie aplikacje zostały wyposażone w interfejs graficzny ułatwiający pracę </p>

### Słownik
<ul list-style-type: disc >
<li>IPv4 (adres IP) – adres wykorzystywany do komunikacji (format xxx.xxx.xxx.xxx -> np. 168.154.202.2),</li>
<li> port  - jeden z parametrów gniazda, który umożliwia nawiązanie połączenia,</li>
<li>asynchroniczność – sposób przesyłania danych pozwalający na nieregularne wysyłanie danych. Pozwala obsługiwać więcej niż 1 klienta na raz,</li>
<li>TCP – niezawodny protokół komunikacyjny stosowany do przesyłania danych między procesami uruchomionymi na różnych maszynach,</li>
<li>baza danych – zbiór danych zapisanych zgodnie z określonymi regułami,</li>
<li>SQLite – system zarządzania bazą danych, obsługujący język SQL, </li>
<li>logi – historia aktywności.</li>
</ul>

### Wymagania funkcjonalne
<ol>
<li> serwer utrzymuje połączenie z wieloma klientami,</li>
<li> administrator kończy działanie serwera,</li>
<li> klient dodaje, edytuje lub usuwa pliki tekstowe,</li>
<li> klient loguje się,</li>
<li> klient rejestruje się,</li>
<li> klient zmienia hasło,</li>
<li> serwer sprawdza poprawność hasła,</li>
<li> serwer przechowuje pliki tekstowe</li>
<li> serwer przechowuje logi</li>
<li> serwer łączy się z bazą danych</li>
<li> serwer wysyła  logi do klienta</li>
<li> serwer wyświetla bieżącą aktywność użytkowników </li>
<li> serwer przechowuje profil użytkownika</li>
<li> serwer udostępnia profil użytkownika klientowi</li>
<li> serwer wyświetla listę aktywnych użytkowników</li>
<li> klient edytuje profil użytkownika</li>
<li> klient sprawdza poprawność wprowadzanych danych</li>
<li> klient przegląda indywidualna historię działań</li>
</ol>

### Wymagania niefunkcjonalne
<ol>
<li> język programowania: C# (.NET Framework),</li>
<li> środowisko programistyczne: Visual Studio 2019,</li>
<li> system operacyjny Windows 10,</li>
<li> język bazy danych: SQL,</li>
<li> baza danych SQLite,</li>
<li> interfejs: graficzny,</li>
<li> serwer utrzymuje połączenie z wieloma klientami</li>
<li> serwer po rozłączeniu z klientem jest w stanie nadal obsługiwać pozostałych klientów,</li>
<li> możliwość łatwej rozbudowy projektu,</li>
<li> serwer obsługuje wyjątki,</li>
<li> klient obsługuje wyjątki,</li>
<li> serwer zapobiega rejestracji dwóch kont o tej samej nazwie,</li>
<li> serwer nie wymaga żadnych dodatkowych działań, prócz uruchomienia,</li>
<li> klient korzysta z funkcji skrótu SHA256,</li>
<li> serwer korzysta z funkcji skrótu SHA256.</li>
</ol>

### Diagram przypadków użycia

<p align="center">
  <img src="/Documentation/Diagram_przypadkow_uzycia.jpg">
</p>

### Diagram klas - serwer

<p align="center">
  <img src="/Documentation/Diagram_klas.jpg">
</p>

### Diagram aktywności - Wyświetlanie logów użytkonwika

<p align="center">
  <img src="/Documentation/logi_uzytkownik.jpg">
</p>

### Diagram aktywności - Wyświetlanie logów w aplikacji serwera

<p align="center">
  <img src="/Documentation/logi_serwer.jpg">
</p>

### Diagram aktywności - Edycja profilu użytkownika 

<p align="center">
  <img src="/Documentation/edycja_profilu.jpg">
</p>

### Diagram aktywności - Zmiana hasła użytkownika

<p align="center">
  <img src="/Documentation/zmiana_hasla.jpg">
</p>

### Diagram aktywności - Wyświetlanie aktywnych użytkowników

<p align="center">
  <img src="/Documentation/wyswietlanie_aktywnych_uzytkownikow.jpg">
</p>

### Diagram sekwencji - Dodawanie pliku przez użytkownika

<p align="center">
  <img src="/Documentation/Diagram_sekwencji_dodawanie_pliku.jpg">
</p>

### Diagram sekwencji - Edycja profilu użytkownika

<p align="center">
  <img src="/Documentation/Diagram_sekwencji_edycja_profilu_uzytkownika.jpg">
</p>

### Diagram sekwencji -Logowanie użytkownika

<p align="center">
  <img src="/Documentation/Diagram_sekwencji_logowanie_uzytkownika.jpg">
</p>

### Aplikacja - widok startowy

<p align="center">
  <img src="/Documentation/app1.JPG">
</p>

### Aplikacja - logowanie

<p align="center">
  <img src="/Documentation/app2.JPG">
</p>

### Aplikacja - panel główny użytkownika

<p align="center">
  <img src="/Documentation/app3.JPG">
</p>

### Aplikacja - logi

<p align="center">
  <img src="/Documentation/app4.JPG">
</p>

### Aplikacja - panel użytkownika

<p align="center">
  <img src="/Documentation/app5_user.JPG">
</p>

### Aplikacja - usuwanie pliku

<p align="center">
  <img src="/Documentation/app6_delete.JPG">
</p>

### Aplikacja - zmiana hasła

<p align="center">
  <img src="/Documentation/app6_pwd.JPG">
</p>

## Zespół
- Poduct Owner: Jordan Kondracki
- Scrum Team:
	- Development Team:
		- Mateusz Kuźniak
		- Artur Jackowiak
		- Jordan Kondracki
- Scrum Master: Mateusz Kuźniak 

 

