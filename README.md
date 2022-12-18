# Zarządzanie bazą danych osób
Prosta aplikacja konsolowa napisana w języku C# do zarządzania bazą danych osób. Umożliwia tworzenie nowych osob, modyfikowanie ich danych, usuwanie ich z bazy, zapis listy osób do pliku w formacie JSON i/lub CSV.

Komunikaty są w języku polskim, nazwy metod i klas również.

Zamieszczony projekt został utworzony w Visual Studio 2022 dla platformy .NET 6.0

Aplikajca realizuje następujące zadanie:
> Spróbuj stworzyć aplikację symulującą bazę danych użytkowników
> 
> Aby rozwiązać to zadanie skorzystaj z wiedzy dotyczącej struktur, pętli, kolekcji danych (lista), funkcji oraz interakcji z użytkownikiem.
> Dobrze, żeby dane aplikacji były zapisywane do pliku i przy uruchomieniu - odczytywane z niego :-)
> Dane osobowe można reprezentować za pomocą następującej struktury :
> 
> Imię
> Nazwisko
> Wiek
> Kod Pocztowy
> Miasto
> Ulica
> Numer Domu
> 
> Dobierz odpowiednie typy danych.
> 
> Aplikacja może wyglądać tak:
> 
> *** MENU GŁÓWNE ***
> 
> 1. Dodaj dane osoby
> 2. Popraw dane osoby
> 3. Wyszukaj osobę
> 4. Koniec programu
> 
> Po wybraniu opcji nr 1 użytkownik zostanie poproszony o następujące informacje :
> 
> Podaj indeks: (numer w tablicy, pod którym przypisane zostaną dane)
> Podaj imię:
> Podaj nazwisko:
> Podaj wiek:
> Podaj kod pocztowy:
> Podaj miasto:
> Podaj ulice:
> Podaj numer domu:
> 
> UWAGA! Pamiętaj, żeby wprowadzanie danych było bezpieczne :-)
> 
> Po wybraniu opcji nr 2 użytkownik zostanie poproszony o następujące informacje :
> 
> Podaj indeks: (numer w tablicy, pod którym przypisane zostaną dane)
> 
> Po podaniu indeksu system wyświetli informację o wartości aktualnie wpisanej pod indeksem - po czym zaproponuje jej zmianę
> 
> Aktualne imię to :
> Podaj imię:
> Aktualne nazwisko to:
> Podaj nazwisko:
> Aktualny wiek to :
> Podaj wiek:
> Aktualny kod pocztowy to:
> Podaj kod pocztowy:
> Aktualne miasto to:
> Podaj miasto:
> Aktualna ulica to:
> Podaj ulice:
> Aktualny numer domu to:
> Podaj numer domu:
> 
> 
> Wybranie opcji nr 3 spowoduje wyświetlenie dodatkowego menu:
> 
> *** Menu wyszukiwania ***
> 1. Wyszukaj po imieniu
> 2. Wyszukaj po nazwisku
> 3. Wyszukaj po przedziale wiekowym
> 4. Wyszukaj po kodzie pocztowym
> 5. Wyszukaj po mieście
> 6. Wyszukaj po ulicy
> 7. Powrót do menu głównego
> 
> Wybranie każdej z opcji spowoduje, że użytkownik zostanie poproszony o podanie odpowiedniej wartości (1 - imię, 2 nazwisko, itd. ) - po tej wartości system przeszuka > kolekcję danych i wyświetli te, które pasują do wyszukiwanego wzorca.
