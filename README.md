### MvcRestauracja

## Autorzy: Aleksandra Ignacyk

## Opis Projektu
MvcRestauracja to aplikacja internetowa stworzona w technologii ASP.NET Core MVC, służąca do zarządzania rezerwacjami stolików w restauracji. Aplikacja umożliwia użytkownikom rezerwowanie stolików, zarządzanie własnymi danymi, a także procesy logowania i rejestracji. Administratorzy mają możliwość zarządzania użytkownikami oraz przeglądania wszystkich rezerwacji.
Funkcjonalności Aplikacji
- Rejestracja i Logowanie
Rejestracja użytkowników: Użytkownicy mogą się rejestrować, podając podstawowe informacje takie jak nazwa użytkownika, hasło, email i numer telefonu.
Logowanie: Użytkownicy mogą się logować przy użyciu swojej nazwy użytkownika i hasła. System przechowuje hasła w bezpieczny sposób przy użyciu technik hashowania.
- Zarządzanie Użytkownikami
Wyświetlanie listy użytkowników: Administratorzy mogą przeglądać listę zarejestrowanych użytkowników.
Szczegóły użytkownika: Administratorzy mogą przeglądać szczegóły wybranego użytkownika.
Tworzenie użytkowników: Możliwość tworzenia nowych użytkowników (dostępne dla administratorów).
Edycja użytkowników: Możliwość edytowania danych użytkowników.
Usuwanie użytkowników: Możliwość usuwania użytkowników.
- Zarządzanie Stolikiem
Wyświetlanie listy stolików: Użytkownicy mogą przeglądać dostępne stoliki w restauracji.
Szczegóły stolika: Użytkownicy mogą przeglądać szczegóły wybranego stolika.
Tworzenie stolików: Możliwość dodawania nowych stolików do systemu (dostępne dla administratorów).
Edycja stolików: Możliwość edytowania danych stolików (dostępne dla administratorów).
Usuwanie stolików: Możliwość usuwania stolików (dostępne dla administratorów).
- Główna Strona
Strona główna: Główna strona aplikacji, na której użytkownicy mogą się logować i uzyskać dostęp do funkcjonalności aplikacji.
Polityka prywatności: Strona zawierająca informacje o polityce prywatności.
Dashboard: Panel administracyjny dostępny dla zalogowanych administratorów.
- Wylogowanie
Wylogowanie: Możliwość wylogowania się z aplikacji, co kończy sesję użytkownika.


## Architektura Aplikacji
MvcRestauracja jest zbudowana w oparciu o wzorzec architektoniczny Model-View-Controller (MVC). Aplikacja składa się z kilku głównych komponentów:
- Kontrolery
Kontrolery obsługują logikę aplikacji i reagują na działania użytkownika, manipulując modelami i wybierając odpowiednie widoki do renderowania. W aplikacji znajdują się następujące kontrolery:
HomeController: Odpowiada za główną stronę aplikacji i podstawowe widoki, takie jak strona główna, polityka prywatności i dashboard.
UserController: Zarządza funkcjonalnością związaną z użytkownikami, taką jak wyświetlanie listy użytkowników, tworzenie, edycja i usuwanie użytkowników.
StolikController: Zarządza funkcjonalnością związaną ze stolikami, taką jak wyświetlanie listy stolików, tworzenie, edycja i usuwanie stolików.
LoginController: Obsługuje logowanie i wylogowywanie użytkowników oraz rejestrację nowych użytkowników.
- Modele
Modele reprezentują dane aplikacji i logikę biznesową. W aplikacji MvcRestauracja znajdują się następujące modele:
User: Reprezentuje użytkownika aplikacji.
Stolik: Reprezentuje stolik w restauracji.
Danie: Reprezentuje danie dostępne w restauracji.
Klient: Reprezentuje klienta restauracji.
Kelner: Reprezentuje kelnera pracującego w restauracji.
- Widoki
Widoki są odpowiedzialne za prezentację danych użytkownikowi. Są to pliki .cshtml, które zawierają kod HTML z wbudowanymi elementami C#. Widoki są renderowane przez kontrolery na podstawie modeli.
- Baza Danych
Aplikacja korzysta z Entity Framework Core jako ORM (Object-Relational Mapping), aby zarządzać bazą danych. Klasa MvcRestauracjaContext definiuje kontekst bazy danych, który jest używany do wykonywania operacji CRUD (Create, Read, Update, Delete) na danych.
-  Filtry
Filtry umożliwiają wykonywanie logiki przed lub po wykonaniu akcji w kontrolerze. W aplikacji znajduje się filtr UserSessionFilter, który sprawdza, czy użytkownik jest zalogowany.

## Sposób Użycia
- Rejestracja: Nowy użytkownik rejestruje się, wypełniając formularz rejestracyjny.
- Logowanie: Po rejestracji, użytkownik loguje się, podając swoją nazwę użytkownika i hasło.
- Rezerwacja stolików: Zalogowany użytkownik może przeglądać dostępne stoliki i dokonywać rezerwacji.
- Zarządzanie kontem: Użytkownik może edytować swoje dane lub usunąć konto.
- Panel administracyjny: Administrator ma dostęp do dodatkowych funkcji, takich jak zarządzanie użytkownikami i stolikami.
- Wylogowanie: Użytkownik może się wylogować, kończąc swoją sesję.
