namespace Proiect01
{
    public static class Functii
    {
        public static void AdaugaProdus()
        {
            while (true)
            {
                Console.Clear();

                // ✅ Always show the list first
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== Lista de cumparaturi ===\n");
                Console.ResetColor();

                if (Program.Lista.DeCumparat.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Lista este goala! Foloseste optiunea 1 pentru a adauga un produs de cumparat.");
                    Console.ResetColor();
                }
                else
                {
                    Program.Lista.AfiseazaLista();
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== Adauga un produs ===\n");
                Console.ResetColor();

                string numeProdus;
                do
                {
                    Console.Write("Scrie numele produsului sau '0' pentru a reveni: ");
                    numeProdus = Console.ReadLine()?.Trim();

                    if (numeProdus == "0") return; // Exit to main menu

                    if (string.IsNullOrWhiteSpace(numeProdus))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Numele produsului nu poate fi gol!");
                        Console.ResetColor();
                    }

                } while (string.IsNullOrWhiteSpace(numeProdus));

                bool produsExista = false;

                // ✅ Check if the product already exists in DeCumparat
                for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                {
                    if (Program.Lista.DeCumparat[i].Denumire.ToLower() == numeProdus.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Produsul '{numeProdus}' este deja in lista de cumparaturi la numarul {i + 1}: {Program.Lista.DeCumparat[i].GetData()}");
                        Console.ResetColor();
                        Console.WriteLine("Apasa orice tasta pentru a incerca din nou");
                        Console.ReadKey();
                        produsExista = true;
                        break;
                    }
                }

                // ✅ Check if the product already exists in Achizitionate
                for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                {
                    if (Program.Lista.Achizitionate[i].Denumire.ToLower() == numeProdus.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Produsul '{numeProdus}' este deja achizitionat si se afla la numarul {i + 1}: {Program.Lista.Achizitionate[i].GetData()}");
                        Console.ResetColor();
                        Console.WriteLine("Apasa orice tasta pentru a incerca din nou");
                        Console.ReadKey();
                        produsExista = true;
                        break;
                    }
                }

                if (produsExista)
                {
                    Console.WriteLine("\nTe rog sa introduci un alt produs.");
                    continue; // ✅ Go back to asking for a new product name without exiting
                }

                int cantitateProdus;
                bool cantitateProdusValid;
                do
                {
                    Console.Write("Adauga cantitatea dorita: ");
                    cantitateProdusValid = int.TryParse(Console.ReadLine(), out cantitateProdus);

                    if (!cantitateProdusValid || cantitateProdus < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Datele introduse nu sunt valide! Introdu un numar pozitiv.");
                        Console.ResetColor();
                    }
                } while (!cantitateProdusValid || cantitateProdus < 1);

                Program.Lista.DeCumparat.Add(new Produs(numeProdus, cantitateProdus));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nProdusul '{numeProdus}' ({cantitateProdus} buc.) a fost adaugat in lista!");
                Console.ResetColor();

                Console.WriteLine("\nApasa orice tasta pentru a continua adaugarea...");
                Console.ReadKey();
            }
        }

        public static void StergeProdus()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== Sterge un produs ===\n");
                Console.ResetColor();

                // Show the current list of items to buy
                if (Program.Lista.DeCumparat.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lista este goala! Nu ai produse de sters.");
                    Console.ResetColor();
                    Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Produse disponibile pentru stergere:\n");
                for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {Program.Lista.DeCumparat[i].GetData()}");
                }

                Console.WriteLine("\nCum doresti sa stergi produsul?");
                Console.WriteLine("1: Dupa numar (index)");
                Console.WriteLine("2: Dupa nume");
                Console.WriteLine("3: Sterge TOATE produsele");
                Console.WriteLine("0: Revenire la meniul anterior");

                int optiuneStergere;
                bool optiuneValid;
                do
                {
                    Console.Write("\nAlege optiunea (1, 2, 3 sau 0): ");
                    optiuneValid = int.TryParse(Console.ReadLine(), out optiuneStergere);

                    if (!optiuneValid || (optiuneStergere < 0 || optiuneStergere > 3))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege 1, 2, 3 sau 0.");
                        Console.ResetColor();
                    }
                } while (!optiuneValid || (optiuneStergere < 0 || optiuneStergere > 3));

                if (optiuneStergere == 0)
                {
                    return; // Exit back to the previous menu
                }

                if (optiuneStergere == 1) // Delete by index
                {
                    int produsStersIndex = -1;
                    bool produsStersIndexValid;
                    do
                    {
                        Console.Write("\nScrie numarul produsului pentru a fi sters (0 pentru a reveni): ");
                        string input = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Trebuie sa introduci un numar!");
                            Console.ResetColor();
                            continue; // ✅ Loop again for valid input
                        }

                        if (!int.TryParse(input, out produsStersIndex))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Input invalid! Introdu un numar valid.");
                            Console.ResetColor();
                            continue; // ✅ Loop again for valid input
                        }

                        if (produsStersIndex == 0) break; // ✅ Allow returning

                        if (produsStersIndex < 1 || produsStersIndex > Program.Lista.DeCumparat.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numar invalid! Introdu un index corect.");
                            Console.ResetColor();
                        }

                    } while (produsStersIndex < 1 || produsStersIndex > Program.Lista.DeCumparat.Count);
                    if (produsStersIndex == 0) continue; // Go back to the main delete options

                    string produsSters = Program.Lista.DeCumparat[produsStersIndex - 1].Denumire;

                    string confirm;
                    do
                    {
                        Console.Write($"\nEsti sigur ca vrei sa stergi produsul '{produsSters}'? (Y/N): ");
                        confirm = Console.ReadLine()?.Trim().ToUpper();
                    } while (confirm != "Y" && confirm != "N");

                    if (confirm == "N") continue; // ✅ Immediately return to delete menu

                    if (confirm == "Y")
                    {
                        Program.Lista.DeCumparat.RemoveAt(produsStersIndex - 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nProdusul '{produsSters}' a fost sters cu succes.");
                        Console.ResetColor();
                        Console.WriteLine("\nApasa orice tasta pentru a continua...");
                        Console.ReadKey();
                    }
                }
                else if (optiuneStergere == 2) // Delete by name
                {
                    string numeProdusStergere;
                    bool produsGasit = false;

                    do
                    {
                        Console.Write("\nScrie numele produsului pe care doresti sa-l stergi (0 pentru a reveni): ");
                        numeProdusStergere = Console.ReadLine()?.Trim().ToLower();

                        if (numeProdusStergere == "0") break; // User chose to go back

                        if (string.IsNullOrWhiteSpace(numeProdusStergere))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numele produsului nu poate fi gol! Introdu un nume valid.");
                            Console.ResetColor();
                            continue;
                        }

                        for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                        {
                            if (Program.Lista.DeCumparat[i].Denumire.ToLower() == numeProdusStergere)
                            {
                                string confirm;
                                do
                                {
                                    Console.Write($"\nEsti sigur ca vrei sa stergi produsul '{Program.Lista.DeCumparat[i].Denumire}'? (Y/N): ");
                                    confirm = Console.ReadLine()?.Trim().ToUpper();
                                } while (confirm != "Y" && confirm != "N");

                                if (confirm == "N")
                                {
                                    produsGasit = true; // ✅ Prevents "Product not found" message
                                    break;               // ✅ Immediately return to delete menu
                                }

                                if (confirm == "Y")
                                {
                                    Program.Lista.DeCumparat.RemoveAt(i);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"\nProdusul '{numeProdusStergere}' a fost sters cu succes.");
                                    Console.ResetColor();
                                    Console.WriteLine("\nApasa orice tasta pentru a continua...");
                                    Console.ReadKey();
                                    produsGasit = true;
                                    break;
                                }
                            }
                        }

                        if (produsGasit) break; // ✅ Exits name search after "N" or "Y" is selected

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nProdusul nu a fost gasit in lista. Verifica numele si incearca din nou.");
                        Console.ResetColor();

                    } while (true);
                }
                else if (optiuneStergere == 3) // Delete all items
                {
                    string confirm;
                    do
                    {
                        Console.Write("\nEsti sigur ca vrei sa stergi TOATE PRODUSELE din lista? (Y/N): ");
                        confirm = Console.ReadLine()?.Trim().ToUpper();
                    } while (confirm != "Y" && confirm != "N");

                    if (confirm == "Y")
                    {
                        Program.Lista.DeCumparat.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nToate produsele au fost sterse din lista.");
                        Console.ResetColor();
                        Console.WriteLine("\nApasa orice tasta pentru te intoarce la meniul principal...");
                        Console.ReadKey();
                        return; // ✅ Returns to the main menu after deleting all products
                    }
                }
            }
        }

        public static void EditareProdus()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=== Editare produs ===\n");
                Console.ResetColor();

                if (Program.Lista.DeCumparat.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lista este goala! Nu ai produse de editat.");
                    Console.ResetColor();
                    Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Produse disponibile pentru editare:\n");
                for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {Program.Lista.DeCumparat[i].GetData()}");
                }

                Console.WriteLine("\nCum doresti sa editezi produsul?");
                Console.WriteLine("1: Dupa numar (index)");
                Console.WriteLine("2: Dupa nume");
                Console.WriteLine("0: Revenire la meniul anterior");

                int optiune;
                bool optiuneValid;
                do
                {
                    Console.Write("\nAlege o optiune: ");
                    optiuneValid = int.TryParse(Console.ReadLine(), out optiune);

                    if (!optiuneValid || optiune < 0 || optiune > 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege 0, 1 sau 2.");
                        Console.ResetColor();
                    }
                } while (!optiuneValid || optiune < 0 || optiune > 2);

                if (optiune == 0)
                {
                    return;
                }

                Produs produsDeEditat = null;
                int index = -1;

                if (optiune == 1) // Edit by index
                {
                    int produsIndex = -1;
                    bool produsIndexValid;
                    do
                    {
                        Console.Write("\nScrie numarul produsului (indexul) sau 0 pentru a reveni: ");
                        string input = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Trebuie sa introduci un numar!");
                            Console.ResetColor();
                            continue;
                        }

                        if (!int.TryParse(input, out produsIndex))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Input invalid! Introdu un numar valid.");
                            Console.ResetColor();
                            continue;
                        }

                        if (produsIndex == 0) break;

                        if (produsIndex < 1 || produsIndex > Program.Lista.DeCumparat.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numar invalid! Introdu un index corect.");
                            Console.ResetColor();
                        }

                    } while (produsIndex < 1 || produsIndex > Program.Lista.DeCumparat.Count);

                    if (produsIndex == 0) continue;

                    index = produsIndex - 1;
                    produsDeEditat = Program.Lista.DeCumparat[index];
                }
                else if (optiune == 2) // Edit by name
                {
                    string numeProdus;
                    bool produsGasit = false;
                    do
                    {
                        Console.Write("\nScrie numele produsului sau 0 pentru a reveni: ");
                        numeProdus = Console.ReadLine()?.Trim();

                        if (numeProdus == "0") break;

                        if (string.IsNullOrWhiteSpace(numeProdus))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numele produsului nu poate fi gol!");
                            Console.ResetColor();
                            continue;
                        }

                        for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                        {
                            if (Program.Lista.DeCumparat[i].Denumire.ToLower() == numeProdus.ToLower())
                            {
                                produsDeEditat = Program.Lista.DeCumparat[i];
                                index = i;
                                produsGasit = true;
                                break;
                            }
                        }

                        if (!produsGasit)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Produsul nu a fost gasit in lista.");
                            Console.ResetColor();
                        }

                    } while (!produsGasit);

                    if (numeProdus == "0") continue;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nProdus gasit la index {index + 1}: {produsDeEditat.GetData()}");
                Console.ResetColor();

                // Confirmation before editing
                string confirm;
                do
                {
                    Console.Write($"\nEsti sigur ca vrei sa editezi produsul '{produsDeEditat.Denumire}'? (Y/N): ");
                    confirm = Console.ReadLine()?.Trim().ToUpper();
                } while (confirm != "Y" && confirm != "N");

                if (confirm == "N")
                {
                    continue;
                }
                Console.WriteLine("\nCe doresti sa editezi?");
                Console.WriteLine("1: Numele produsului");
                Console.WriteLine("2: Cantitatea produsului");
                Console.WriteLine("3: Ambele");
                Console.WriteLine("0: Revenire la selectia produsului");

                int optiuneEditare;
                bool optiuneEditareValid;
                do
                {
                    Console.Write("\nAlege optiunea: ");
                    optiuneEditareValid = int.TryParse(Console.ReadLine(), out optiuneEditare);

                    if (!optiuneEditareValid || optiuneEditare < 0 || optiuneEditare > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege 0, 1, 2 sau 3.");
                        Console.ResetColor();
                    }
                } while (!optiuneEditareValid || optiuneEditare < 0 || optiuneEditare > 3);

                if (optiuneEditare == 0) continue;

                if (optiuneEditare == 1 || optiuneEditare == 3) // Edit name
                {
                    string numeNouProdus;
                    bool numeExista;

                    do
                    {
                        Console.Write("Scrie noul nume al produsului: ");
                        numeNouProdus = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(numeNouProdus))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numele nu poate fi gol! Te rog sa introduci un nume valid.");
                            Console.ResetColor();
                            numeExista = true; // ✅ Forces loop to continue
                            continue;
                        }

                        numeExista = false; // ✅ Reset before checking duplicates

                        // ✅ Check in DeCumparat (excluding the current product being edited)
                        for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                        {
                            if (i != index && Program.Lista.DeCumparat[i].Denumire.ToLower() == numeNouProdus.ToLower())
                            {
                                numeExista = true;
                                break;
                            }
                        }

                        // ✅ Check in Achizitionate
                        for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                        {
                            if (Program.Lista.Achizitionate[i].Denumire.ToLower() == numeNouProdus.ToLower())
                            {
                                numeExista = true;
                                break;
                            }
                        }

                        if (numeExista)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Produsul '{numeNouProdus}' exista deja in alta lista! Te rog introdu un nume diferit.");
                            Console.ResetColor();
                        }

                    } while (numeExista || string.IsNullOrWhiteSpace(numeNouProdus)); // ✅ Now the loop also checks for blank names

                    produsDeEditat.Denumire = numeNouProdus;
                }

                if (optiuneEditare == 2 || optiuneEditare == 3)
                {
                    int cantitateNoua;
                    bool cantitateNouaValid;
                    do
                    {
                        Console.Write("Scrie noua cantitate: ");
                        cantitateNouaValid = int.TryParse(Console.ReadLine(), out cantitateNoua);

                        if (!cantitateNouaValid || cantitateNoua < 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Cantitate invalida! Introdu un numar pozitiv.");
                            Console.ResetColor();
                        }
                    } while (!cantitateNouaValid || cantitateNoua < 1);

                    produsDeEditat.Cantitate = cantitateNoua;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nProdusul editat cu succes: {produsDeEditat.GetData()}");
                Console.ResetColor();
                Console.WriteLine("\nApasa orice tasta pentru a continua...");
                Console.ReadKey();
            }
        }

        public static void BifeazaCumparat()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=== Marcheaza produs(e) ca achizitionat(e) ===\n");
                Console.ResetColor();

                if (Program.Lista.DeCumparat.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lista de cumparaturi este goala! Nu ai produse de marcat ca achizitionate.");
                    Console.ResetColor();
                    Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Lista de cumparaturi:\n");
                for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {Program.Lista.DeCumparat[i].GetData()}");
                }

                Console.WriteLine("\nCum doresti sa marchezi produsele ca achizitionate?");
                Console.WriteLine("1: Dupa numar (index)");
                Console.WriteLine("2: Dupa nume");
                Console.WriteLine("3: Marcheaza TOATE produsele ca achizitionate");
                Console.WriteLine("0: Inapoi la meniul principal");

                int optiuneMarcare;
                bool optiuneValid;
                do
                {
                    Console.Write("\nAlege optiunea (1, 2, 3 sau 0): ");
                    optiuneValid = int.TryParse(Console.ReadLine(), out optiuneMarcare);

                    if (!optiuneValid || (optiuneMarcare < 0 || optiuneMarcare > 3))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege 0, 1, 2 sau 3.");
                        Console.ResetColor();
                    }
                } while (!optiuneValid || (optiuneMarcare < 0 || optiuneMarcare > 3));

                if (optiuneMarcare == 0) return;

                if (optiuneMarcare == 1) // ✅ Mark by index
                {
                    int produsMarcatIndex = -1;
                    bool produsMarcatIndexValid = false;

                    do
                    {
                        Console.Write("\nScrie numarul produsului pentru a-l marca ca achizitionat (0 pentru a reveni): ");
                        string input = Console.ReadLine()?.Trim();

                        if (input == "0") break;

                        produsMarcatIndexValid = int.TryParse(input, out produsMarcatIndex) &&
                                                 produsMarcatIndex >= 1 &&
                                                 produsMarcatIndex <= Program.Lista.DeCumparat.Count;

                        if (!produsMarcatIndexValid)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numar invalid! Introdu un index corect.");
                            Console.ResetColor();
                        }
                    } while (!produsMarcatIndexValid);

                    if (!produsMarcatIndexValid || produsMarcatIndex == 0) continue;

                    string produsMarcat = Program.Lista.DeCumparat[produsMarcatIndex - 1].Denumire;

                    string confirm;
                    do
                    {
                        Console.Write($"\nEsti sigur ca vrei sa marchezi produsul '{produsMarcat}' ca achizitionat? (Y/N): ");
                        confirm = Console.ReadLine()?.Trim().ToUpper();
                    } while (confirm != "Y" && confirm != "N");

                    if (confirm == "N") continue;

                    if (confirm == "Y")
                    {
                        Produs produs = Program.Lista.DeCumparat[produsMarcatIndex - 1];
                        produs.EsteCumparat = true;
                        Program.Lista.DeCumparat.RemoveAt(produsMarcatIndex - 1);
                        Program.Lista.Achizitionate.Add(produs);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nProdusul '{produsMarcat}' a fost marcat ca achizitionat.");
                        Console.ResetColor();
                        Console.WriteLine("\nApasa orice tasta pentru a continua...");
                        Console.ReadKey();
                    }
                }
                else if (optiuneMarcare == 2) // ✅ Mark by name
                {
                    string numeProdusMarcare;
                    bool produsGasit = false;

                    do
                    {
                        Console.Write("\nScrie numele produsului pe care doresti sa-l marchezi ca achizitionat (sau 0 pentru a reveni): ");
                        numeProdusMarcare = Console.ReadLine()?.Trim().ToLower();

                        if (numeProdusMarcare == "0") break;

                        if (string.IsNullOrWhiteSpace(numeProdusMarcare))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numele produsului nu poate fi gol! Te rog sa introduci un nume valid.");
                            Console.ResetColor();
                            continue;
                        }

                        for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                        {
                            if (Program.Lista.DeCumparat[i].Denumire.ToLower() == numeProdusMarcare)
                            {
                                string confirm;
                                do
                                {
                                    Console.Write($"\nEsti sigur ca vrei sa marchezi produsul '{Program.Lista.DeCumparat[i].Denumire}' ca achizitionat? (Y/N): ");
                                    confirm = Console.ReadLine()?.Trim().ToUpper();
                                } while (confirm != "Y" && confirm != "N");

                                if (confirm == "N")
                                {
                                    produsGasit = true;
                                    break;
                                }

                                if (confirm == "Y")
                                {
                                    Produs produs = Program.Lista.DeCumparat[i];
                                    produs.EsteCumparat = true;
                                    Program.Lista.DeCumparat.RemoveAt(i);
                                    Program.Lista.Achizitionate.Add(produs);

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"\nProdusul '{numeProdusMarcare}' a fost marcat ca achizitionat.");
                                    Console.ResetColor();
                                    Console.WriteLine("\nApasa orice tasta pentru a continua...");
                                    Console.ReadKey();
                                    produsGasit = true;
                                    break;
                                }
                            }
                        }

                        if (produsGasit) break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nProdusul nu a fost gasit in lista de cumparaturi. Verifica numele si incearca din nou.");
                        Console.ResetColor();

                    } while (true);
                }
                else if (optiuneMarcare == 3) // ✅ Mark all products as bought
                {
                    string confirm;
                    do
                    {
                        Console.Write("\nEsti sigur ca vrei sa marchezi TOATE produsele ca achizitionate? (Y/N): ");
                        confirm = Console.ReadLine()?.Trim().ToUpper();
                    } while (confirm != "Y" && confirm != "N");

                    if (confirm == "Y")
                    {
                        while (Program.Lista.DeCumparat.Count > 0)
                        {
                            Produs produs = Program.Lista.DeCumparat[0];
                            produs.EsteCumparat = true;
                            Program.Lista.Achizitionate.Add(produs);
                            Program.Lista.DeCumparat.RemoveAt(0);
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nToate produsele au fost marcate ca achizitionate!");
                        Console.ResetColor();
                        Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                        Console.ReadKey();
                        return;
                    }
                }
            }
        }

        public static void CautaProdus()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== Cauta un produs ===\n");
                Console.ResetColor();

                if (Program.Lista.DeCumparat.Count == 0 && Program.Lista.Achizitionate.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ambele liste sunt goale! Nu ai produse de cautat.");
                    Console.ResetColor();
                    Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                    Console.ReadKey();
                    return;
                }

                while (true) // Loop inside the search screen
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n=== Cauta un produs ===\n");
                    Console.ResetColor();

                    Console.WriteLine("Lista de cumparaturi:\n");
                    if (Program.Lista.DeCumparat.Count > 0)
                    {
                        for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}: {Program.Lista.DeCumparat[i].GetData()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Momentan nu exista produse in lista de cumparaturi, dar poti incerca sa le gasesti in lista de achizitionate folosind optiunea 2 (cautare dupa nume).");
                    }

                    Console.WriteLine("\nCum doresti sa cauti un produs?");
                    Console.WriteLine("1: Dupa numar (index)");
                    Console.WriteLine("2: Dupa nume (sau numele partial)");
                    Console.WriteLine("0: Revenire la meniul principal");

                    int optiune;
                    bool optiuneValida;
                    do
                    {
                        Console.Write("\nAlege o optiune: ");
                        optiuneValida = int.TryParse(Console.ReadLine(), out optiune);

                        if (!optiuneValida || (optiune < 0 || optiune > 2))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Optiune invalida! Alege 0, 1 sau 2.");
                            Console.ResetColor();
                        }
                    } while (!optiuneValida || (optiune < 0 || optiune > 2));

                    if (optiune == 0)
                    {
                        return; // Go back to the main menu immediately
                    }

                    bool produsGasit = false;

                    if (optiune == 1) // Search by index
                    {
                        if (Program.Lista.DeCumparat.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nLista de cumparat este goala! Nu exista produse de cautat.");
                            Console.ResetColor();
                            Console.WriteLine("\nApasa orice tasta pentru a continua cautarea...");
                            Console.ReadKey();
                            continue;
                        }

                        while (true) // ✅ Keep searching by index until the user presses 0
                        {
                            Console.Write("\nScrie numarul produsului (indexul) sau 0 pentru a reveni: ");
                            string input = Console.ReadLine()?.Trim();

                            if (string.IsNullOrWhiteSpace(input) || !int.TryParse(input, out int produsIndex))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Numar invalid! Introdu un index corect.");
                                Console.ResetColor();
                                continue;
                            }

                            if (produsIndex == 0)
                            {
                                break; // ✅ Exit back to "Cum doresti sa cauti un produs?"
                            }

                            if (produsIndex < 1 || produsIndex > Program.Lista.DeCumparat.Count)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Numar invalid! Introdu un index corect.");
                                Console.ResetColor();
                                continue;
                            }

                            // ✅ Display the found product
                            Produs produs = Program.Lista.DeCumparat[produsIndex - 1];
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\nProdus gasit in **Lista DeCumparat** la index {produsIndex}: {produs.GetData()}");
                            Console.ResetColor();

                            // ✅ FIX: No "Press any key" message—user stays in the index search
                        }
                    }

                    else if (optiune == 2) // Partial search by name
                    {
                        while (true)
                        {
                            produsGasit = false; // ✅ Reset before each search

                            Console.Write("\nScrie numele produsului (sau parte din nume) sau 0 pentru a reveni: ");
                            string numeProdus = Console.ReadLine()?.Trim();

                            if (numeProdus == "0")
                            {
                                break; // ✅ Go back immediately without showing "Press any key"
                            }

                            if (string.IsNullOrWhiteSpace(numeProdus))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Numele produsului nu poate fi gol! Te rog sa introduci un nume valid.");
                                Console.ResetColor();
                                continue;
                            }

                            List<string> produseGasite = new List<string>();

                            for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                            {
                                if (Program.Lista.DeCumparat[i].Denumire.ToLower().Contains(numeProdus.ToLower()))
                                {
                                    produseGasite.Add(i + 1 + ": " + Program.Lista.DeCumparat[i].GetData());
                                    produsGasit = true;
                                }
                            }

                            for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                            {
                                if (Program.Lista.Achizitionate[i].Denumire.ToLower().Contains(numeProdus.ToLower()))
                                {
                                    produseGasite.Add(i + 1 + ": " + Program.Lista.Achizitionate[i].GetData());
                                    produsGasit = true;
                                }
                            }

                            if (produseGasite.Count > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nProduse gasite:");

                                for (int i = 0; i < produseGasite.Count; i++)
                                {
                                    Console.WriteLine(produseGasite[i]);
                                }

                                Console.ResetColor();

                                // ✅ FIX: Removed "Apasa orice tasta..." since searching continues anyway
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nNiciun produs nu a fost gasit in liste.");
                                Console.ResetColor();
                            }
                        }
                    }
                }
            }
        }

        public static void VeziListaCumparate()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("=== Lista produselor cumparate ===\n");
                Console.ResetColor();

                if (Program.Lista.Achizitionate.Count == 0)
                {
                    Console.WriteLine("Nu ai cumparat niciun produs.");
                }
                else
                {
                    for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Program.Lista.Achizitionate[i].GetData()}");
                    }
                }

                Console.WriteLine("\nOptiuni produse achizitionate:");
                Console.WriteLine("1: Adauga un produs achizitionat");
                Console.WriteLine("2: Sterge produse achizitionate");
                Console.WriteLine("3: Editeaza un produs achizitionat");
                Console.WriteLine("4: Retrimite produse pe lista de cumparaturi");
                Console.WriteLine("5: Sorteaza produsele achizitionate");
                Console.WriteLine("0: Revenire la meniul principal");

                int optiune;
                bool optiuneValida;
                do
                {
                    Console.Write("Alege o optiune: ");
                    optiuneValida = int.TryParse(Console.ReadLine(), out optiune);

                    if (!optiuneValida || optiune < 0 || optiune > 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege un numar corect.");
                        Console.ResetColor();
                    }
                } while (!optiuneValida || optiune < 0 || optiune > 4);

                switch (optiune)
                {
                    case 0:
                        return;
                    case 1:
                        Functii.AdaugaProdusCumparat();
                        break;
                    case 2:
                        Functii.StergeProdusCumparat();
                        break;
                    case 3:
                        Functii.EditareProdusCumparat();
                        break;
                    case 4:
                        Functii.RetrimiteProdus();
                        break;
                }
            }
        }

        public static void VeziToateProdusele()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== Vezi toate produsele ===\n");
            Console.ResetColor();

            if (Program.Lista.DeCumparat.Count == 0 && Program.Lista.Achizitionate.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ambele liste sunt goale! Nu ai produse de afisat.");
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Produse din lista de cumparat:");
                if (Program.Lista.DeCumparat.Count > 0)
                {
                    for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Program.Lista.DeCumparat[i].GetData()}");
                    }
                }
                else
                {
                    Console.WriteLine("Lista de cumparat este goala.");
                }

                Console.WriteLine("\nProduse din lista de achizitionate:");
                if (Program.Lista.Achizitionate.Count > 0)
                {
                    for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Program.Lista.Achizitionate[i].GetData()}");
                    }
                }
                else
                {
                    Console.WriteLine("Lista de achizitionate este goala.");
                }
            }

            Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
            Console.ReadKey();
        }

        public static void AdaugaProdusCumparat()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("=== Adauga un produs achizitionat ===\n");
                Console.ResetColor();

                // ✅ Always show the list of purchased items first
                Console.WriteLine("Lista de produse achizitionate:\n");
                if (Program.Lista.Achizitionate.Count > 0)
                {
                    for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}: {Program.Lista.Achizitionate[i].GetData()}");
                    }
                }
                else
                {
                    Console.WriteLine("Lista de achizitionate este goala.");
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== Introducere produs nou ===\n");
                Console.ResetColor();

                string numeProdus;
                do
                {
                    Console.Write("Scrie numele produsului sau '0' pentru a reveni: ");
                    numeProdus = Console.ReadLine()?.Trim();

                    if (numeProdus == "0") return; // Exit to main menu

                    if (string.IsNullOrWhiteSpace(numeProdus))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Numele produsului nu poate fi gol!");
                        Console.ResetColor();
                    }

                } while (string.IsNullOrWhiteSpace(numeProdus));

                bool produsExista = false;

                // ✅ Check if the product already exists in Achizitionate
                for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                {
                    if (Program.Lista.Achizitionate[i].Denumire.ToLower() == numeProdus.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Produsul '{numeProdus}' este deja in lista de achizitionate la numarul {i + 1}: {Program.Lista.Achizitionate[i].GetData()}");
                        Console.ResetColor();
                        Console.WriteLine("Apasa orice tasta pentru a incerca din nou");
                        Console.ReadKey();
                        produsExista = true;
                        break;
                    }
                }

                // ✅ Check if the product already exists in DeCumparat
                for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                {
                    if (Program.Lista.DeCumparat[i].Denumire.ToLower() == numeProdus.ToLower())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Produsul '{numeProdus}' este deja in lista de cumparaturi la numarul {i + 1}: {Program.Lista.DeCumparat[i].GetData()}");
                        Console.ResetColor();
                        Console.WriteLine("Apasa orice tasta pentru a incerca din nou");
                        Console.ReadKey();
                        produsExista = true;
                        break;
                    }
                }

                if (produsExista)
                {
                    Console.WriteLine("\nTe rog sa introduci un alt produs.");
                    continue; // ✅ Go back to asking for a new product name without exiting
                }

                int cantitateProdus;
                bool cantitateProdusValid;
                do
                {
                    Console.Write("Adauga cantitatea dorita: ");
                    cantitateProdusValid = int.TryParse(Console.ReadLine(), out cantitateProdus);

                    if (!cantitateProdusValid || cantitateProdus < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Datele introduse nu sunt valide! Introdu un numar pozitiv.");
                        Console.ResetColor();
                    }
                } while (!cantitateProdusValid || cantitateProdus < 1);

                // ✅ Create the product and set it as purchased
                Produs produsNou = new Produs(numeProdus, cantitateProdus);
                produsNou.EsteCumparat = true; // ✅ Mark as purchased
                Program.Lista.Achizitionate.Add(produsNou);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nProdusul '{numeProdus}' ({cantitateProdus} buc.) a fost adaugat in lista de achizitionate!");
                Console.ResetColor();

                Console.WriteLine("\nApasa orice tasta pentru a continua...");
                Console.ReadKey();
            }
        }

        public static void StergeProdusCumparat()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== Sterge un produs achizitionat ===\n");
                Console.ResetColor();

                // Show the current list of purchased items
                if (Program.Lista.Achizitionate.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lista achizitionata este goala! Nu ai produse de sters.");
                    Console.ResetColor();
                    Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Produse disponibile pentru stergere:\n");
                for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {Program.Lista.Achizitionate[i].GetData()}");
                }

                Console.WriteLine("\nCum doresti sa stergi produsul?");
                Console.WriteLine("1: Dupa numar (index)");
                Console.WriteLine("2: Dupa nume");
                Console.WriteLine("3: Sterge TOATE produsele achizitionate");
                Console.WriteLine("0: Revenire la meniul anterior");

                int optiuneStergere;
                bool optiuneValid;
                do
                {
                    Console.Write("\nAlege optiunea (1, 2, 3 sau 0): ");
                    optiuneValid = int.TryParse(Console.ReadLine(), out optiuneStergere);

                    if (!optiuneValid || (optiuneStergere < 0 || optiuneStergere > 3))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege 1, 2, 3 sau 0.");
                        Console.ResetColor();
                    }
                } while (!optiuneValid || (optiuneStergere < 0 || optiuneStergere > 3));

                if (optiuneStergere == 0)
                {
                    return; // Exit back to the previous menu
                }

                if (optiuneStergere == 1) // Delete by index
                {
                    int produsStersIndex = -1;
                    bool produsStersIndexValid;
                    do
                    {
                        Console.Write("\nScrie numarul produsului pentru a fi sters (0 pentru a reveni): ");
                        string input = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Trebuie sa introduci un numar!");
                            Console.ResetColor();
                            continue; // ✅ Loop again for valid input
                        }

                        if (!int.TryParse(input, out produsStersIndex))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Input invalid! Introdu un numar valid.");
                            Console.ResetColor();
                            continue; // ✅ Loop again for valid input
                        }

                        if (produsStersIndex == 0) break; // ✅ Allow returning

                        if (produsStersIndex < 1 || produsStersIndex > Program.Lista.Achizitionate.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numar invalid! Introdu un index corect.");
                            Console.ResetColor();
                        }

                    } while (produsStersIndex < 1 || produsStersIndex > Program.Lista.Achizitionate.Count);
                    if (produsStersIndex == 0) continue; // Go back to the main delete options

                    string produsSters = Program.Lista.Achizitionate[produsStersIndex - 1].Denumire;

                    string confirm;
                    do
                    {
                        Console.Write($"\nEsti sigur ca vrei sa stergi produsul '{produsSters}'? (Y/N): ");
                        confirm = Console.ReadLine()?.Trim().ToUpper();
                    } while (confirm != "Y" && confirm != "N");

                    if (confirm == "N") continue; // ✅ Immediately return to delete menu

                    if (confirm == "Y")
                    {
                        Program.Lista.Achizitionate.RemoveAt(produsStersIndex - 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nProdusul '{produsSters}' a fost sters cu succes.");
                        Console.ResetColor();
                        Console.WriteLine("\nApasa orice tasta pentru a continua...");
                        Console.ReadKey();
                    }
                }
                else if (optiuneStergere == 2) // Delete by name
                {
                    string numeProdusStergere;
                    bool produsGasit = false;

                    do
                    {
                        Console.Write("\nScrie numele produsului pe care doresti sa-l stergi (0 pentru a reveni): ");
                        numeProdusStergere = Console.ReadLine()?.Trim().ToLower();

                        if (numeProdusStergere == "0") break; // User chose to go back

                        if (string.IsNullOrWhiteSpace(numeProdusStergere))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numele produsului nu poate fi gol! Introdu un nume valid.");
                            Console.ResetColor();
                            continue;
                        }

                        for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                        {
                            if (Program.Lista.Achizitionate[i].Denumire.ToLower() == numeProdusStergere)
                            {
                                string confirm;
                                do
                                {
                                    Console.Write($"\nEsti sigur ca vrei sa stergi produsul '{Program.Lista.Achizitionate[i].Denumire}'? (Y/N): ");
                                    confirm = Console.ReadLine()?.Trim().ToUpper();
                                } while (confirm != "Y" && confirm != "N");

                                if (confirm == "N")
                                {
                                    produsGasit = true; // ✅ Prevents "Product not found" message
                                    break;               // ✅ Immediately return to delete menu
                                }

                                if (confirm == "Y")
                                {
                                    Program.Lista.Achizitionate.RemoveAt(i);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"\nProdusul '{numeProdusStergere}' a fost sters cu succes.");
                                    Console.ResetColor();
                                    Console.WriteLine("\nApasa orice tasta pentru a continua...");
                                    Console.ReadKey();
                                    produsGasit = true;
                                    break;
                                }
                            }
                        }

                        if (produsGasit) break; // ✅ Exits name search after "N" or "Y" is selected

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nProdusul nu a fost gasit in lista. Verifica numele si incearca din nou.");
                        Console.ResetColor();

                    } while (true);
                }
                else if (optiuneStergere == 3) // Delete all purchased items
                {
                    string confirm;
                    do
                    {
                        Console.Write("\nEsti sigur ca vrei sa stergi TOATE PRODUSELE achizitionate? (Y/N): ");
                        confirm = Console.ReadLine()?.Trim().ToUpper();
                    } while (confirm != "Y" && confirm != "N");

                    if (confirm == "Y")
                    {
                        Program.Lista.Achizitionate.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nToate produsele achizitionate au fost sterse din lista.");
                        Console.ResetColor();
                        Console.WriteLine("\nApasa orice tasta pentru te intoarce la meniul principal...");
                        Console.ReadKey();
                        return; // ✅ Returns to the main menu after deleting all products
                    }
                }
            }
        }

        public static void EditareProdusCumparat()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=== Editare produs achizitionat ===\n");
                Console.ResetColor();

                if (Program.Lista.Achizitionate.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lista este goala! Nu ai produse de editat.");
                    Console.ResetColor();
                    Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Produse disponibile pentru editare:\n");
                for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {Program.Lista.Achizitionate[i].GetData()}");
                }

                Console.WriteLine("\nCum doresti sa editezi produsul?");
                Console.WriteLine("1: Dupa numar (index)");
                Console.WriteLine("2: Dupa nume");
                Console.WriteLine("0: Revenire la meniul anterior");

                int optiune;
                bool optiuneValid;
                do
                {
                    Console.Write("\nAlege o optiune: ");
                    optiuneValid = int.TryParse(Console.ReadLine(), out optiune);

                    if (!optiuneValid || optiune < 0 || optiune > 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege 0, 1 sau 2.");
                        Console.ResetColor();
                    }
                } while (!optiuneValid || optiune < 0 || optiune > 2);

                if (optiune == 0)
                {
                    return;
                }

                Produs produsDeEditat = null;
                int index = -1;

                if (optiune == 1) // Edit by index
                {
                    int produsIndex = -1;
                    bool produsIndexValid;
                    do
                    {
                        Console.Write("\nScrie numarul produsului (indexul) sau 0 pentru a reveni: ");
                        string input = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(input))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Trebuie sa introduci un numar!");
                            Console.ResetColor();
                            continue;
                        }

                        if (!int.TryParse(input, out produsIndex))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Input invalid! Introdu un numar valid.");
                            Console.ResetColor();
                            continue;
                        }

                        if (produsIndex == 0) break;

                        if (produsIndex < 1 || produsIndex > Program.Lista.Achizitionate.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numar invalid! Introdu un index corect.");
                            Console.ResetColor();
                        }

                    } while (produsIndex < 1 || produsIndex > Program.Lista.Achizitionate.Count);

                    if (produsIndex == 0) continue;

                    index = produsIndex - 1;
                    produsDeEditat = Program.Lista.Achizitionate[index];
                }
                else if (optiune == 2) // Edit by name
                {
                    string numeProdus;
                    bool produsGasit = false;
                    do
                    {
                        Console.Write("\nScrie numele produsului sau 0 pentru a reveni: ");
                        numeProdus = Console.ReadLine()?.Trim();

                        if (numeProdus == "0") break;

                        if (string.IsNullOrWhiteSpace(numeProdus))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numele produsului nu poate fi gol!");
                            Console.ResetColor();
                            continue;
                        }

                        for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                        {
                            if (Program.Lista.Achizitionate[i].Denumire.ToLower() == numeProdus.ToLower())
                            {
                                produsDeEditat = Program.Lista.Achizitionate[i];
                                index = i;
                                produsGasit = true;
                                break;
                            }
                        }

                        if (!produsGasit)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Produsul nu a fost gasit in lista.");
                            Console.ResetColor();
                        }

                    } while (!produsGasit);

                    if (numeProdus == "0") continue;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nProdus gasit la index {index + 1}: {produsDeEditat.GetData()}");
                Console.ResetColor();

                // Confirmation before editing
                string confirm;
                do
                {
                    Console.Write($"\nEsti sigur ca vrei sa editezi produsul '{produsDeEditat.Denumire}'? (Y/N): ");
                    confirm = Console.ReadLine()?.Trim().ToUpper();
                } while (confirm != "Y" && confirm != "N");

                if (confirm == "N") continue;

                Console.WriteLine("\nCe doresti sa editezi?");
                Console.WriteLine("1: Numele produsului");
                Console.WriteLine("2: Cantitatea produsului");
                Console.WriteLine("3: Ambele");
                Console.WriteLine("0: Revenire la selectia produsului");

                int optiuneEditare;
                bool optiuneEditareValid;
                do
                {
                    Console.Write("\nAlege optiunea: ");
                    optiuneEditareValid = int.TryParse(Console.ReadLine(), out optiuneEditare);

                    if (!optiuneEditareValid || optiuneEditare < 0 || optiuneEditare > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege 0, 1, 2 sau 3.");
                        Console.ResetColor();
                    }
                } while (!optiuneEditareValid || optiuneEditare < 0 || optiuneEditare > 3);

                if (optiuneEditare == 0) continue;

                if (optiuneEditare == 1 || optiuneEditare == 3) // Edit name
                {
                    string numeNouProdus;
                    bool numeExista;

                    do
                    {
                        Console.Write("Scrie noul nume al produsului: ");
                        numeNouProdus = Console.ReadLine()?.Trim();

                        if (string.IsNullOrWhiteSpace(numeNouProdus))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numele nu poate fi gol! Te rog sa introduci un nume valid.");
                            Console.ResetColor();
                            numeExista = true;
                            continue;
                        }

                        numeExista = false;

                        for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                        {
                            if (i != index && Program.Lista.Achizitionate[i].Denumire.ToLower() == numeNouProdus.ToLower())
                            {
                                numeExista = true;
                                break;
                            }
                        }

                        for (int i = 0; i < Program.Lista.DeCumparat.Count; i++)
                        {
                            if (Program.Lista.DeCumparat[i].Denumire.ToLower() == numeNouProdus.ToLower())
                            {
                                numeExista = true;
                                break;
                            }
                        }

                        if (numeExista)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Produsul '{numeNouProdus}' exista deja in alta lista! Te rog introdu un nume diferit.");
                            Console.ResetColor();
                        }

                    } while (numeExista || string.IsNullOrWhiteSpace(numeNouProdus));

                    produsDeEditat.Denumire = numeNouProdus;
                }

                if (optiuneEditare == 2 || optiuneEditare == 3)
                {
                    int cantitateNoua;
                    bool cantitateNouaValid;
                    do
                    {
                        Console.Write("Scrie noua cantitate: ");
                        cantitateNouaValid = int.TryParse(Console.ReadLine(), out cantitateNoua);

                        if (!cantitateNouaValid || cantitateNoua < 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Cantitate invalida! Introdu un numar pozitiv.");
                            Console.ResetColor();
                        }
                    } while (!cantitateNouaValid || cantitateNoua < 1);

                    produsDeEditat.Cantitate = cantitateNoua;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nProdusul editat cu succes: {produsDeEditat.GetData()}");
                Console.ResetColor();
                Console.WriteLine("\nApasa orice tasta pentru a continua...");
                Console.ReadKey();
            }
        }

        public static void RetrimiteProdus()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=== Muta produs(e) inapoi in lista de cumparaturi ===\n");
                Console.ResetColor();

                if (Program.Lista.Achizitionate.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lista de produse achizitionate este goala! Nu ai produse de mutat inapoi.");
                    Console.ResetColor();
                    Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Lista produselor achizitionate:\n");
                for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {Program.Lista.Achizitionate[i].GetData()}");
                }

                Console.WriteLine("\nCum doresti sa muti produsele inapoi?");
                Console.WriteLine("1: Dupa numar (index)");
                Console.WriteLine("2: Dupa nume");
                Console.WriteLine("3: Muta TOATE produsele inapoi in lista de cumparaturi");
                Console.WriteLine("0: Inapoi la meniul principal");

                int optiune;
                bool optiuneValida;
                do
                {
                    Console.Write("\nAlege optiunea (1, 2, 3 sau 0): ");
                    optiuneValida = int.TryParse(Console.ReadLine(), out optiune);

                    if (!optiuneValida || (optiune < 0 || optiune > 3))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Optiune invalida! Alege 0, 1, 2 sau 3.");
                        Console.ResetColor();
                    }
                } while (!optiuneValida || (optiune < 0 || optiune > 3));

                if (optiune == 0) return;

                if (optiune == 1) // ✅ Move by index
                {
                    int produsIndex = -1;
                    bool indexValid = false;

                    do
                    {
                        Console.Write("\nScrie numarul produsului (indexul) sau 0 pentru a reveni: ");
                        string input = Console.ReadLine()?.Trim();

                        if (input == "0") break;

                        indexValid = int.TryParse(input, out produsIndex) &&
                                     produsIndex >= 1 &&
                                     produsIndex <= Program.Lista.Achizitionate.Count;

                        if (!indexValid)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numar invalid! Introdu un index corect.");
                            Console.ResetColor();
                        }
                    } while (!indexValid);

                    if (!indexValid || produsIndex == 0) continue;

                    string produsMutat = Program.Lista.Achizitionate[produsIndex - 1].Denumire;

                    string confirm;
                    do
                    {
                        Console.Write($"\nEsti sigur ca vrei sa muti produsul '{produsMutat}' inapoi in lista de cumparaturi? (Y/N): ");
                        confirm = Console.ReadLine()?.Trim().ToUpper();
                    } while (confirm != "Y" && confirm != "N");

                    if (confirm == "N") continue;

                    if (confirm == "Y")
                    {
                        Produs produs = Program.Lista.Achizitionate[produsIndex - 1];
                        produs.EsteCumparat = false;
                        Program.Lista.DeCumparat.Add(produs);
                        Program.Lista.Achizitionate.RemoveAt(produsIndex - 1);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nProdusul '{produsMutat}' a fost mutat inapoi in lista de cumparaturi.");
                        Console.ResetColor();
                        Console.WriteLine("\nApasa orice tasta pentru a continua...");
                        Console.ReadKey();
                    }
                }
                else if (optiune == 2) // ✅ Move by name
                {
                    string numeProdus;
                    bool produsGasit = false;

                    do
                    {
                        Console.Write("\nScrie numele produsului pe care doresti sa-l muti inapoi (sau 0 pentru a reveni): ");
                        numeProdus = Console.ReadLine()?.Trim().ToLower();

                        if (numeProdus == "0") break;

                        if (string.IsNullOrWhiteSpace(numeProdus))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Numele produsului nu poate fi gol! Te rog sa introduci un nume valid.");
                            Console.ResetColor();
                            continue;
                        }

                        for (int i = 0; i < Program.Lista.Achizitionate.Count; i++)
                        {
                            if (Program.Lista.Achizitionate[i].Denumire.ToLower() == numeProdus)
                            {
                                string confirm;
                                do
                                {
                                    Console.Write($"\nEsti sigur ca vrei sa muti produsul '{Program.Lista.Achizitionate[i].Denumire}' inapoi in lista de cumparaturi? (Y/N): ");
                                    confirm = Console.ReadLine()?.Trim().ToUpper();
                                } while (confirm != "Y" && confirm != "N");

                                if (confirm == "N")
                                {
                                    produsGasit = true;
                                    break;
                                }

                                if (confirm == "Y")
                                {
                                    Produs produs = Program.Lista.Achizitionate[i];
                                    produs.EsteCumparat = false;
                                    Program.Lista.DeCumparat.Add(produs);
                                    Program.Lista.Achizitionate.RemoveAt(i);

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"\nProdusul '{numeProdus}' a fost mutat inapoi in lista de cumparaturi.");
                                    Console.ResetColor();
                                    Console.WriteLine("\nApasa orice tasta pentru a continua...");
                                    Console.ReadKey();
                                    produsGasit = true;
                                    break;
                                }
                            }
                        }

                        if (produsGasit) break;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nProdusul nu a fost gasit in lista de achizitionate. Verifica numele si incearca din nou.");
                        Console.ResetColor();

                    } while (true);
                }
                else if (optiune == 3) // ✅ Move all products back
                {
                    string confirm;
                    do
                    {
                        Console.Write("\nEsti sigur ca vrei sa muti TOATE produsele inapoi in lista de cumparaturi? (Y/N): ");
                        confirm = Console.ReadLine()?.Trim().ToUpper();
                    } while (confirm != "Y" && confirm != "N");

                    if (confirm == "Y")
                    {
                        while (Program.Lista.Achizitionate.Count > 0)
                        {
                            Produs produs = Program.Lista.Achizitionate[0];
                            produs.EsteCumparat = false;
                            Program.Lista.DeCumparat.Add(produs);
                            Program.Lista.Achizitionate.RemoveAt(0);
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nToate produsele au fost mutate inapoi in lista de cumparaturi!");
                        Console.ResetColor();
                        Console.WriteLine("\nApasa orice tasta pentru a reveni la meniul principal...");
                        Console.ReadKey();
                        return;
                    }
                }
            }
        }
    }
}