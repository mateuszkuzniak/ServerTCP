# ServerTCP

### Wprowadzenie - opis systemu

<p text-indent: 1.5em >Aplikacja serwera asynchronicznego TCP z zaimplementowanym wzorcem TAP umożliwiająca logowanie użytkownika do systemu oraz założenie konta.  Użytkownik za pomocą klienta łączy się z serwerem przy użyciu podanego adresu IPv4 oraz portu. Następnie klient loguje się na serwerze za pomocą loginu i hasła. Po zalogowaniu klient ma dostęp do podstawowej wersji chmury – obsługa plików txt (możliwość dodawania, edycja, usuwania oraz przeglądania danych). Aplikacja kliencka dodatkowo została wyposażona w obsługę profilu użytkownika oraz podgląd historii działań (obie te rzeczy przechowywane są po stronie serwera). Prosta, intuicyjna aplikacja, umożliwiająca dalszy rozwój projektu. Serwer został wyposażony w bazę danych (SQLite), dzięki czemu po wyłączeniu serwera pozostają dane zarejestrowanych użytkowników oraz ich plików. Obie aplikacje zostały wyposażone w interfejs graficzny ułatwiający pracę </p>

### Class diagram

<p align="center">
  <img src="/Documentation/class_diagram.jpg">
</p>

### Use case diagram

<p align="center">
  <img src="/Documentation/use_case_diagram.png">
</p>

### Activity diagram

<p align="center">
  <img src="/Documentation/diagram_aktywności.jpg">
</p>
