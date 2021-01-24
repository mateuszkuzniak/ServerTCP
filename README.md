
# ServerTCP

### Wprowadzenie - opis systemu

 <p align="justify">Aplikacja serwera asynchronicznego TCP z zaimplementowanym wzorcem TAP umożliwiająca logowanie użytkownika do systemu oraz założenie konta.  Użytkownik za pomocą klienta łączy się z serwerem przy użyciu podanego adresu IPv4 oraz portu. Następnie klient loguje się na serwerze za pomocą loginu i hasła. Po zalogowaniu klient ma dostęp do podstawowej wersji chmury – obsługa plików txt (możliwość dodawania, edycja, usuwania oraz przeglądania danych). Aplikacja kliencka dodatkowo została wyposażona w obsługę profilu użytkownika oraz podgląd historii działań (obie te rzeczy przechowywane są po stronie serwera). Prosta, intuicyjna aplikacja, umożliwiająca dalszy rozwój projektu. Serwer został wyposażony w bazę danych (SQLite), dzięki czemu po wyłączeniu serwera pozostają dane zarejestrowanych użytkowników oraz ich plików. Obie aplikacje zostały wyposażone w interfejs graficzny ułatwiający pracę </p>

### Słownik
<ul list-style-type: disc >
<li>```IPv4``` (adres IP) – adres wykorzystywany do komunikacji (format xxx.xxx.xxx.xxx -> np. 168.154.202.2),</li>
<li> port  - jeden z parametrów gniazda, który umożliwia nawiązanie połączenia,</li>
<li>asynchroniczność – sposób przesyłania danych pozwalający na nieregularne wysyłanie danych. Pozwala obsługiwać więcej niż 1 klienta na raz,</li>
<li>TCP – niezawodny protokół komunikacyjny stosowany do przesyłania danych między procesami uruchomionymi na różnych maszynach,</li>
<li>baza danych – zbiór danych zapisanych zgodnie z określonymi regułami,</li>
<li>```SQLite``` – system zarządzania bazą danych, obsługujący język SQL, </li>
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
<li> język programowania: ```C# (.NET Framework)```,</li>
<li> środowisko programistyczne: Visual Studio 2019,</li>
<li> system operacyjny Windows 10,</li>
<li> język bazy danych: ```SQL```,</li>
<li> baza danych ```SQLite```,</li>
<li> interfejs: graficzny,</li>
<li> serwer utrzymuje połączenie z wieloma klientami</li>
<li> serwer po rozłączeniu z klientem jest w stanie nadal obsługiwać pozostałych klientów,</li>
<li> możliwość łatwej rozbudowy projektu,</li>
<li> serwer obsługuje wyjątki,</li>
<li> klient obsługuje wyjątki,</li>
<li> serwer zapobiega rejestracji dwóch kont o tej samej nazwie,</li>
<li> serwer nie wymaga żadnych dodatkowych działań, prócz uruchomienia,</li>
<li> klient korzysta z ```funkcji skrótu SHA256```,</li>
<li> serwer korzysta z ```funkcji skrótu SHA256```.</li>
</ol>

### Diagram przypadków użycia

<p align="center">
  <img src="/Documentation/Diagram_przypadkow_uzycia.jpg">
</p>

### Diagram klas - serwer

<p align="center">
  <img src="/Documentation/Diagram_klas.jpg">
</p>

### Diagram klas - klient

<p align="center">
  <img src="/Documentation/Diagram_klas_klient.jpg">
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

### Diagram wdrożenia

<p align="center">
  <img src="/Documentation/Diagram_wdrozenia.jpg">
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

###Wybrane fragmenty kodu 
<ul list-style-type: disc >
<li>Międzywątkowa synchronizacja logera - zapis logów</li>
```cs
        private delegate void SafeCallDelegate(string text);
		
		private void cw(string mess)
        {
            if (Logs.InvokeRequired)
            {
                var d = new SafeCallDelegate(cw);
                Logs.Invoke(d, new object[] { mess });
            }
            else
            {
                Logs.AppendText(mess);
                Logs.AppendText(Environment.NewLine);
            }
        }
		
		private void saveLogs(string mess, string userName = null)
        {
            checkDirectory(directoryLogsName);
            var path = $"{directoryLogsPath}{((userName != null) ? $"{userName}.txt" : serverLogsFileName)}";
            mess = $"{Date()} {mess}";

            cw(mess);

            checkFileExist(path, mess);
            if (!path.Contains(serverLogsFileName))
                checkFileExist($"{directoryLogsPath}{serverLogsFileName}", mess);
        }
		
```
<li>Dzielenie wiadomości na pakiery</li>
```cs
        int HowManyParts(int size)
        {
            int parts = size / bufferSize;
            if (parts * bufferSize < size)
                return parts + 1;
            else
                return parts;
        }


        List<byte[]> DivideIntoParts(string response)
        {
            var responseByte = ASCIIEncoding.UTF8.GetBytes(response);
            int parts = HowManyParts(response.Length);
            List<byte[]> newBuffer = new List<byte[]>();

            byte[] tempBuffer = new byte[bufferSize];
            int x = 0;
            for (int i = 0; i < parts; i++)
            {
                tempBuffer = new byte[bufferSize];
                for (int j = 0; j < bufferSize; j++)
                {
                    if (x < responseByte.Length)
                    {
                        tempBuffer[j] = responseByte[x];
                        x++;
                    }
                    else
                    {
                        x = 0;
                        break;
                    }

                }
                newBuffer.Add(tempBuffer);
            }

            return newBuffer;

        }
```
<li>Sprawdzenie poprawności danych - panel użytkownika</li>
```cs
        bool ValidUserData(string[] data)
        {
            if (data[0].Length > 0 && (!Regex.IsMatch(data[0], @"[@]") || !Regex.IsMatch(data[0], @"[.]")))
                user.Status = Account.StatusCode.inv_mail;
            else if (data[1].Length > 0 && !Regex.IsMatch(data[1], @"^[a-zA-Z]+$"))
                user.Status = Account.StatusCode.inv_first_name;
            else if (data[2].Length > 0 && !Regex.IsMatch(data[2], @"^[a-zA-Z]+(\-[a-zA-Z]+)?$"))
                user.Status = Account.StatusCode.inv_second_name;
            else if (data[3].Length > 0 && (!Regex.IsMatch(data[3], @"^[0-9]+$") || data[3].Length < 9 || data[3].Length > 9))
                user.Status = Account.StatusCode.inv_phone_number;
            else
            {
                user.Status = Account.StatusCode.user_valid_data;
                return true;
            }
            return false;
        }
```
<li>Komunikacja serwer -> klienta</li>
```cs
 string GetLogStatus()
        {
            if (user.Status == Account.StatusCode.inv_user)
                return ServerMessage.invUser;
            else if (user.Status == Account.StatusCode.user_is_logged)
                return ServerMessage.currentlyLogged;
            else if (user.Status == Account.StatusCode.must_be_logged)
                return ServerMessage.mustBelogged;
            else if (user.Status == Account.StatusCode.inv_pass)
                return ServerMessage.invPwd;
            else if (user.Status == Account.StatusCode.logged)
                return ServerMessage.logged;
            else if (user.Status == Account.StatusCode.already_logged)
                return ServerMessage.alreadyLogged;
            else if (user.Status == Account.StatusCode.user_exists)
                return ServerMessage.userExists;
            else if (user.Status == Account.StatusCode.user_does_not_exist)
                return ServerMessage.userDoesNotExists;
            else if (user.Status == Account.StatusCode.successful_registration)
                return ServerMessage.regOk;
            else if (user.Status == Account.StatusCode.change_pwd)
                return ServerMessage.changePwd;
            else if (user.Status == Account.StatusCode.change_pwd_error)
                return ServerMessage.changePwdError;
            else if (user.Status == Account.StatusCode.get_all_user_data)
                return UsersDatabase.GetListData((int)user.Id, DatabaseAbstract.DatabaseType.User);
            else if (user.Status == Account.StatusCode.inv_mail)
                return ServerMessage.invMail;
            else if (user.Status == Account.StatusCode.inv_first_name)
                return ServerMessage.invFirstName;
            else if (user.Status == Account.StatusCode.inv_second_name)
                return ServerMessage.invSecondName;
            else if (user.Status == Account.StatusCode.inv_phone_number)
                return ServerMessage.invPhoneNumber;
            else if (user.Status == Account.StatusCode.user_valid_data)
                return ServerMessage.userValidData;
            else if (user.Status == Account.StatusCode.user_inv_data)
                return ServerMessage.userInvData;
            return ServerMessage.unk;
        }

        string GetFileStatus()
        {
            if (user.FileStatus == Account.FileCode.file_added)
                return ServerMessage.fileAdd;
            else if (user.FileStatus == Account.FileCode.file_exists)
                return ServerMessage.fileExists;
            else if (user.FileStatus == Account.FileCode.must_be_logged)
                return ServerMessage.mustBelogged;
            else if (user.FileStatus == Account.FileCode.inv_file_name)
                return ServerMessage.invFileName;
            else if (user.FileStatus == Account.FileCode.get_all)
                return FileDatabase.GetListData((int)user.Id, DatabaseAbstract.DatabaseType.File);
            else if (user.FileStatus == Account.FileCode.file_deleted)
                return ServerMessage.fileDeleted;
            else if (user.FileStatus == Account.FileCode.file_deleted_error)
                return ServerMessage.fileDeletedError;
            else if (user.FileStatus == Account.FileCode.file_update)
                return ServerMessage.fileUpdate;
            else if (user.FileStatus == Account.FileCode.get_logs)
                return getLogs(user.Login);
            return ServerMessage.unk;

```

<li> Sprawdzenei poprawności hasła </li>
```cs
        public static bool isValid(string password)
        {
            if (password.Length < 7)
                return false;
            if (!password.Any(char.IsUpper))
                return false;
            if (!password.Any(char.IsLower))
                return false;
            if (!password.Any(char.IsDigit))
                return false;
            if (!(password.IndexOfAny(new char[] { '*', '&', '#', '!', '@', '%' }) != -1))
                return false;
            if (password.Any(char.IsWhiteSpace))
                return false;
            
            return true;
        }
```
<li> Pobranie listy plików/użytkowników </li>
```cs
		public string GetListData(int id, DatabaseType type)
        {
            OpenConnection();
            string tableName;
            string data = "";

            if (type == DatabaseType.File)
                tableName = _tableFiles;
            else
                tableName = _tableUsers;


            if (checkForTableExist(tableName))
            {
                lock (keyLock)
                {
                    if (type == DatabaseType.File)
                        _command.CommandText = $"SELECT * FROM {tableName} WHERE userId = '{id}'";
                    else
                        _command.CommandText = $"SELECT * FROM {tableName} WHERE id = '{id}'";

                    SQLiteDataReader reader = _command.ExecuteReader();

                    while (reader.Read())
                    {
                        if(type==DatabaseType.File)
                        {
                            data += reader.GetString(2);
                            data = AddSeparator(data, ";");
                        }
                        if(type == DatabaseType.User)
                        {
                            if (!reader.IsDBNull(4))
                                data += reader.GetString(4);
                            data = AddSeparator(data, ";");
                            if (!reader.IsDBNull(5))
                                data += reader.GetString(5);
                            data = AddSeparator(data, ";");
                            if (!reader.IsDBNull(6))
                                data += reader.GetString(6);
                            data = AddSeparator(data, ";");
                            if (!reader.IsDBNull(7))
                                data += reader.GetInt32(7).ToString();
                            data = AddSeparator(data, ";");

                        }
                    }

                    reader.Close();
                }
            }

            if (data.Length > 0)
                return data;
            else
                return DbMessage.invFileListERROR;
        }
```
## Zespół
- Poduct Owner: Jordan Kondracki
- Scrum Team:
	- Development Team:
		- Mateusz Kuźniak
		- Artur Jackowiak
		- Jordan Kondracki
- Scrum Master: Mateusz Kuźniak 

 

