using static Toolbox;

namespace baza_danych_osob
{
    class Osoby
    {
        private List<Osoba> internal_list;

        public struct Osoba
        {
            public string imie, nazwisko, ulica, nr_domu, miasto, kod_pocztowy;
            public int wiek, id;

            public string DaneSzczegolowo() => $"Numer identyfikacyjny: {id}\nImię: {imie}\nNazwisko: {nazwisko}\nWiek: {wiek}\nNazwa ulicy: {ulica}\nNumer domu/mieszkania: {nr_domu}\nKod pocztowy: {kod_pocztowy}\nMiasto: {miasto}\n";
            public string Dane() => $"{id}.\t{imie} {nazwisko}, {wiek} lat, ul. {ulica} {nr_domu}, {kod_pocztowy} {miasto}\n";
            public string ToCSV() => $"id={id};imie={imie};nazwisko={nazwisko};wiek={wiek};ulica={ulica};nr_domu={nr_domu};kod_pocztowy={kod_pocztowy};miasto={miasto}";
            public string ToJSON() => string.Concat("{\n\t\"id\": ", id, ",\n\t\"imie\": \"", imie, "\",\n\t\"nazwisko\": \"", nazwisko, "\",\n\t\"wiek\": ", wiek, ",\n\t\"ulica\": \"", ulica, "\",\n\t\"nr_domu\": \"", nr_domu, "\",\n\t\"kod_pocztowy\": \"", kod_pocztowy, "\",\n\t\"miasto\": \"", miasto, "\"\n}");
            public void OdczytajCSV(string input)
            {
                string[] splitted_input = input.Split(';');
                foreach(string s in splitted_input)
                {
                    string[] splitted_record = s.Split('=');
                    switch(splitted_record[0])
                    {
                        case "id":
                            if (!StringToInteger(splitted_record[1], ref id)) Print("Wystąpił problem z odczytem ID!");
                            break;
                        case "imie":
                            imie = splitted_record[1];
                            break;
                        case "nazwisko":
                            nazwisko = splitted_record[1];
                            break;
                        case "wiek":
                            if (!StringToInteger(splitted_record[1], ref wiek)) Print("Wystąpił problem z odczytem wieku!");
                            break;
                        case "ulica":
                            ulica = splitted_record[1];
                            break;
                        case "nr_domu":
                            nr_domu = splitted_record[1];
                            break;
                        case "kod_pocztowy":
                            kod_pocztowy= splitted_record[1];
                            break;
                        case "miasto":
                            miasto = splitted_record[1];
                            break;
                    }
                }
            }
            public void OdczytajJSON(string input)
            {
                input = input.Replace("\"", string.Empty).Replace("{", string.Empty).Replace("}", string.Empty).Replace("\t", string.Empty).Replace("\n", string.Empty).Replace(",", ";").Replace(": ", "=");
                OdczytajCSV(input);
            }
            public void WprowadzOsobe(int id)
            {
                Print("ID: " + id + "\n");
                this.id = id;
                Print("Imię: ");
                imie = Read();
                Print("Nazwisko: ");
                nazwisko = Read();
                wiek = ReadInt(true, "Wiek: ", "Wiek musi być liczbą większą od 0!", 0);
                Print("Nazwa ulicy: ");
                ulica = Read();
                Print("Numer domu/mieszkania: ");
                nr_domu = Read();
                Print("Kod pocztowy: ");
                kod_pocztowy = Read();
                Print("Miasto: ");
                miasto = Read();
            }
        }
        public Osoby()
        {
            internal_list = new();
        }
        public Osoby(List<Osoba> lista_osob)
        {
            this.internal_list = lista_osob;
        }

        public void Init(Menu.Theme styl_menu)
        {
            Dictionary<int, string> dic = new()
            {
                { 1, "Tak" },
                { 2, "Nie" }
            };
            Menu menu = new(styl_menu, dic, "Czy chcesz wprowadzić dane nowej osoby?");
            int choice;

            internal_list.Clear();
            Osoba temp = new();
            while((choice = menu.ReadChoice($"Tworzenie bazy danych - liczba osób w bazie: {internal_list.Count}\n")) > 0)
            {
                if (choice == 1)
                {
                    Console.Clear();
                    Print("Wprowadzanie nowej osoby\n\n");
                    temp.WprowadzOsobe(DostepneID());
                    internal_list.Add(temp);
                }
                else
                {
                    if (internal_list.Count > 0) break;
                    else
                    {
                        Console.Clear();
                        Print("Dodaj co najmniej 1 osobę!");
                        Czekaj();
                    }
                }
            }
        }

        public string ToJSON()
        {
            string output = "{";
            foreach (Osoba os in internal_list)
            {
                output = string.Concat(output, "\n", os.ToJSON().Replace("\t", "\t\t").Replace("{", "\t{").Replace("}", "\t}"), ",");
            }
            return string.Concat(output.Remove(output.Length - 1), "\n}");
        }
        public string ToCSV()
        {
            string output = "";
            foreach (Osoba os in internal_list)
            {
                output = string.Concat(output, os.ToCSV(), "\n");
            }
            return output;
        }
        private int DostepneID()
        {
            List<int> list = new();
            foreach(Osoba o in internal_list)
            {
                list.Add(o.id);
            }
            int iter = 1;
            while(true)
            {
                if (list.Contains(iter)) iter++;
                else return iter;
            }
        }
        public bool CzyListaJestPusta() => internal_list.Count < 1;

        public void WczytajOsobyCSV(string plik_csv)
        {
            string[]? input = WczytajPlik(plik_csv);
            if (input == null) return;

            Osoba temp_osoba = new();

            foreach (string s in input)
            {
                temp_osoba.OdczytajCSV(s);
                internal_list.Add(temp_osoba);
            }
        }
        public void WczytajOsobyJSON(string plik_json)
        {
            string[]? input = WczytajPlik(plik_json);
            if (input == null) return;

            string input_s = string.Concat(input).Replace("\t\t", "\t").Replace("{\n\t{", "{").Replace("\t}\n}", "}").Replace("\t{\n", "{\n").Replace("\t},\n", "},\n").Replace("},", "}%");

            Osoba temp_osoba = new();

            foreach (string s in input_s.Split('%'))
            {
                temp_osoba.OdczytajJSON(s);
                DodajOsobe(temp_osoba);
            }
        }

        public bool ZapiszJakoCSV(string plik_csv) => ZapiszPlik(plik_csv, ToCSV());
        public bool ZapiszJakoJSON(string plik_json) => ZapiszPlik(plik_json, ToJSON());

        public void DodajOsobe(Osoba nowa_osoba)
        { 
            foreach(Osoba os in internal_list)
            {
                if (os.id == nowa_osoba.id) return;
            }
            internal_list.Add(nowa_osoba); 
        }
        public void WprowadzOsobe()
        {
            Console.Clear();
            Osoba nowa_osoba = new();
            nowa_osoba.WprowadzOsobe(DostepneID());
            DodajOsobe(nowa_osoba);
            Print("\nZakończono wprowadzanie nowej osoby!\n");
            Czekaj();
        }
        public Osoba WybierzOsobe(Menu.Theme styl_menu)
        {
            Dictionary<int, string> dic = new();
            foreach (Osoba os in internal_list)
            {
                dic.Add(os.id, "\t" + os.imie + " " + os.nazwisko + " " + os.wiek + " lat");
            }
            dic.Add(0, "Powrót do menu głównego");
            Menu menu = new(styl_menu, dic, "Wybierz osobę z listy:\n\n  ID\tDane osobowe");
            int selected = menu.ReadChoice();

            if (selected == 0) return new() { id = 0 };

            foreach(Osoba os in internal_list)
            {
                if (os.id == selected) return os;
            }

            return new() { id = 0 };
        }
        public void PoprawDane(Menu.Theme styl_menu)
        {
            Console.Clear();
            Osoba wybrana = WybierzOsobe(styl_menu);
            if (wybrana.id == 0) return;
            Dictionary<int, string> dict = new()
            {
                { 1, "Imię" },
                { 2, "Nazwisko" },
                { 3, "Wiek" },
                { 4, "Nazwa ulicy" },
                { 5, "Numer domu" },
                { 6, "Kod pocztowy" },
                { 7, "Miasto" },
                { 0, "Zakończ poprawianie danych"}
            };
            Menu dane_do_poprawy = new(styl_menu, dict, "Co chcesz poprawić?");
            internal_list.Remove(wybrana);
            int selected;
            while ((selected = dane_do_poprawy.ReadChoice(wybrana.Dane())) != 0)
            {
                if (selected == 1)
                {
                    Print("Imię: ");
                    wybrana.imie = Read();
                }
                else if (selected == 2)
                {
                    Print("Nazwisko: ");
                    wybrana.nazwisko = Read();
                }
                else if (selected == 3)
                {
                    wybrana.wiek = ReadInt(true, "Wiek: ", "Wiek musi być liczbą większą od 0!", 0);
                }
                else if (selected == 4)
                {
                    Print("Nazwa ulicy: ");
                    wybrana.ulica = Read();
                }
                else if (selected == 5)
                {
                    Print("Numer domu/mieszkania: ");
                    wybrana.nr_domu = Read();
                }
                else if (selected == 6)
                {
                    Print("Kod pocztowy: ");
                    wybrana.kod_pocztowy = Read();
                }
                else if (selected == 7)
                {
                    Print("Miasto: ");
                    wybrana.miasto = Read();
                }
            }
            internal_list.Add(wybrana);
            Print("\nZakończono poprawianie danych!");
            Czekaj();
        }
        public void ZnajdzPasujaceOsoby(Menu.Theme styl_menu)
        {
            Console.Clear();
            Dictionary<int, string> dic = new() 
            {
                { 1, "Imię" },
                { 2, "Nazwisko" },
                { 3, "Wiek" },
                { 4, "Nazwa ulicy" },
                { 5, "Numer domu/mieszkania" },
                { 6, "Kod pocztowy" },
                { 7, "Miasto" },
                { 0, "Powrót do menu głównego" }
            };
            Menu menu = new(styl_menu, dic, "Jakiego kryterium chcesz użyć?");
            int wybor = menu.ReadChoice();
            string kryterium = string.Empty;
            if (wybor == 1) kryterium = Read("Imię: ");
            else if (wybor == 2) kryterium = Read("Nazwisko: ");
            else if (wybor == 3) kryterium = Read("Wiek: ");
            else if (wybor == 4) kryterium = Read("Nazwa ulicy: ");
            else if (wybor == 5) kryterium = Read("Numer domu/mieszkania: ");
            else if (wybor == 6) kryterium = Read("Kod pocztowy: ");
            else if (wybor == 7) kryterium = Read("Miasto: ");
            else if (wybor == 0) return;

            List<Osoba> list = new();

            foreach(Osoba o in internal_list)
            {
                if (wybor == 1) if (o.imie == kryterium) list.Add(o);
                else if (wybor == 2) if (o.nazwisko == kryterium) list.Add(o);
                else if (wybor == 3) if (o.wiek.ToString() == kryterium) list.Add(o);
                else if (wybor == 4) if (o.ulica == kryterium) list.Add(o);
                else if (wybor == 5) if (o.nr_domu == kryterium) list.Add(o);
                else if (wybor == 6) if (o.kod_pocztowy == kryterium) list.Add(o);
                else if (wybor == 7) if (o.miasto == kryterium) list.Add(o);
            }

            Console.Clear();
            if(list.Count > 0)
            {
                Print("Znalezione osoby:\n\n");
                foreach(Osoba o in list)
                {
                    Print(o.Dane());
                }
                Czekaj();
            } else
            {
                Print("Nie znaleziono żadnych osób pasujących do wybranego kryterium!");
                Czekaj();
            }

        }
        public void WyswietlListeOsob()
        {
            Console.Clear();
            Print("ID\tDane osobowe\n\n");
            foreach (Osoba os in internal_list)
            {
                Print(os.Dane());
            }
            Czekaj();
        }
    }
    internal class Program
    {
        public static void Main()
        {
            Menu.Theme styl_menu = Menu.Theme.RedArrow;
            Dictionary<int, string> opcje_wyboru = new()
            {
                { 1, "Użyj istniejącej bazy danych w formacie CSV" },
                { 2, "Użyj istniejącej bazy danych w formacie JSON" },
                { 3, "Utwórz nową bazę danych" }
            };
            Menu menu = new(styl_menu, opcje_wyboru, "Wybierz co chcesz zrobić:");

            Osoby lista_osob = new();

            int wybor = menu.ReadChoice();
            if (wybor == 1)
            {
                Print("\nPodaj ścieżkę do pliku CSV: ");
                lista_osob.WczytajOsobyCSV(Read());

                if (lista_osob.CzyListaJestPusta())
                {
                    PrintError("Istniejąca baza danych jest pusta! Utwórz nową bazę danych");
                    Czekaj();
                    lista_osob.Init(styl_menu);
                }

            }
            else if (wybor == 2)
            {
                Print("\nPodaj ścieżkę do pliku JSON: ");
                lista_osob.WczytajOsobyJSON(Read());

                if (lista_osob.CzyListaJestPusta())
                {
                    PrintError("Istniejąca baza danych jest pusta! Utwórz nową bazę danych");
                    Czekaj();
                    lista_osob.Init(styl_menu);
                }
            }
            else
            {
                lista_osob.Init(styl_menu);
            }

            opcje_wyboru = new()
            {
                { 1, "Dodaj nową osobę" },
                { 2, "Popraw dane osoby" },
                { 3, "Wyszukaj osoby" },
                { 4, "Wyświetl wszystkie osoby" },
                { 5, "Zapisz bazę danych do pliku CSV" },
                { 6, "Zapisz bazę danych do pliku JSON\n" },
                { 0, "Zakończ działanie programu" }
            };

            menu = new(styl_menu, opcje_wyboru, "Wybierz co chcesz zrobić:");

            while ((wybor = menu.ReadChoice()) > 0)
            {
                Console.Clear();
                if (wybor == 1) lista_osob.WprowadzOsobe();
                else if (wybor == 2) lista_osob.PoprawDane(styl_menu);
                else if (wybor == 3) lista_osob.ZnajdzPasujaceOsoby(styl_menu);
                else if (wybor == 4) lista_osob.WyswietlListeOsob();
                else if (wybor == 5)
                {
                    Print("Podaj ścieżkę do pliku CSV: ");
                    if (lista_osob.ZapiszJakoCSV(Read()))
                    {
                        Print("\nZapisano bazę danych!");
                        Czekaj();
                    }
                }
                else if (wybor == 6)
                {
                    Print("Podaj ścieżkę do pliku JSON: ");
                    if (lista_osob.ZapiszJakoJSON(Read()))
                    {
                        Print("\nZapisano bazę danych!");
                        Czekaj();
                    }
                }
                else if (wybor == 9) return;
            }
        }
    }
}