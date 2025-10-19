using Lab_4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    class Program
    {
        private static Film[] films;
        private static int filmCount = 0;
        private static int maxFilms;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("-СИСТЕМА УПРАВЛІННЯ ФІЛЬМАМИ-");
            Console.Write("Введіть кількість об'єктів: ");
            maxFilms = ReadPositiveInt();
            films = new Film[maxFilms];
            int choice;
            do
            {
                ShowMenu();
                choice = ReadInt();
                switch (choice)
                {
                    case 1: AddFilm(); break;
                    case 2: ShowAllFilms(); break;
                    case 3: FindFilm(); break;
                    case 4: DemonstrateFilmBehavior(); break;
                    case 5: DeleteFilm(); break;
                    case 6: DemonstrateStaticMethods(); break;
                    case 0: Console.WriteLine("Вихід"); break;
                    default: Console.WriteLine("Невірний вибір! Введіть число від 0 до 6."); break;
                }
                if (choice != 0)
                    Console.WriteLine();
            } while (choice != 0);
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n-МЕНЮ-");
            Console.WriteLine("1 – Додати об'єкт");
            Console.WriteLine("2 – Переглянути всі об'єкти");
            Console.WriteLine("3 – Знайти об'єкт");
            Console.WriteLine("4 – Продемонструвати поведінку");
            Console.WriteLine("5 – Видалити об'єкт");
            Console.WriteLine("6 – Продемонструвати static-методи");
            Console.WriteLine("0 – Вийти з програми");
            Console.Write("Ваш вибір: ");
        }

        //Демонстрація роботи перевантажених конструкторів та методів
        static void DemonstrateConstructors()
        {
            Console.WriteLine("\n-ДЕМОНСТРАЦІЯ ПЕРЕВАНТАЖЕНИХ КОНСТРУКТОРІВ ТА МЕТОДІВ-");
            //1.Конструктор за замовчуванням
            Console.WriteLine("\n1. Конструктор за замовчуванням:");
            Film film1 = new Film();
            Console.WriteLine(film1.GetInfo(true));
            //2.Конструктор з базовими параметрами
            Console.WriteLine("\n2. Конструктор з базовими параметрами (назва, режисер, рік):");
            Film film2 = new Film("Inception", "Nolan", 2010);
            Console.WriteLine(film2.GetInfo(false));
            //3.Конструктор з усіма параметрами
            Console.WriteLine("\n3. Конструктор з усіма параметрами:");
            Film film3 = new Film("Interstellar", "Nolan", 2014, 8.6, FilmGenre.ACTION);
            Console.WriteLine(film3.GetInfo(1));
            //4.Копіюючий конструктор
            Console.WriteLine("\n4. Копіюючий конструктор (копія попереднього фільму):");
            Film film4 = new Film(film3);
            Console.WriteLine(film4.GetInfo());
            //Демонстрація перевантажених версій GetInfo
            Console.WriteLine("\n5. Демонстрація різних версій GetInfo():");
            Console.WriteLine("\nа) Вибіркове відображення (тільки назва та рейтинг):");
            Console.WriteLine(film3.GetInfo(true, false, false, true));
            Console.WriteLine("\nб) Детальний вивід:");
            Console.WriteLine(film3.GetInfo(true));
            Console.WriteLine("\nв) Короткий вивід:");
            Console.WriteLine(film3.GetInfo(false));
        }

        //Демонстрація static-методів
        static void DemonstrateStaticMethods()
        {
            Console.WriteLine("\n-ДЕМОНСТРАЦІЯ STATIC-МЕТОДІВ-");
            Console.WriteLine("\n1. Демонстрація методу Parse() та TryParse():");
            //Приклад 1: Успішний Parse
            Console.WriteLine("\nПриклад 1 - Коректний рядок:");
            string str1 = "Титанік,Cameron,1997,8.5,2";
            Console.WriteLine($"Рядок: {str1}");
            try
            {
                Film film1 = Film.Parse(str1);
                Console.WriteLine($"Результат Parse: {film1.GetInfo()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            //Приклад 2: Некоректний рядок (Parse викине виняток)
            Console.WriteLine("\nПриклад 2 - Некоректний рядок (недостатньо полів):");
            string str2 = "Аватар,Cameron";
            Console.WriteLine($"Рядок: {str2}");
            try
            {
                Film film2 = Film.Parse(str2);
                Console.WriteLine($"Результат Parse: {film2.GetInfo()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            //Приклад 3: TryParse з коректним рядком
            Console.WriteLine("\nПриклад 3 - TryParse з коректним рядком:");
            string str3 = "Початок,Nolan,2010,8.8,0";
            Console.WriteLine($"Рядок: {str3}");
            Film film3;
            if (Film.TryParse(str3, out film3))
            {
                Console.WriteLine($"TryParse успішний! {film3.GetInfo()}");
            }
            else
            {
                Console.WriteLine("TryParse не вдався!");
            }
            //Приклад 4: TryParse з некоректним рядком
            Console.WriteLine("\nПриклад 4 - TryParse з некоректним рядком:");
            string str4 = "Матриця,Wachowski,abc,9.0,0"; // abc замість року
            Console.WriteLine($"Рядок: {str4}");
            Film film4;
            if (Film.TryParse(str4, out film4))
            {
                Console.WriteLine($"TryParse успішний! {film4.GetInfo()}");
            }
            else
            {
                Console.WriteLine("TryParse не вдався! (Некоректний формат року)");
            }
            //Демонстрація статичного методу GetRatingRecommendation
            Console.WriteLine("\n\n2. Демонстрація статичного методу GetRatingRecommendation():");
            double[] ratings = { 9.5, 8.2, 7.0, 5.5, 3.0 };
            foreach (double rating in ratings)
            {
                Console.WriteLine($"Рейтинг {rating:F1}: {Film.GetRatingRecommendation(rating)}");
            }
            //Демонстрація статичного методу IsCultClassic
            Console.WriteLine("\n\n3. Демонстрація статичного методу IsCultClassic():");
            Console.WriteLine($"Фільм 1994 року з рейтингом 8.5: {(Film.IsCultClassic(1994, 8.5) ? "Культова класика!" : "Не культова класика")}");
            Console.WriteLine($"Фільм 2020 року з рейтингом 9.0: {(Film.IsCultClassic(2020, 9.0) ? "Культова класика!" : "Не культова класика")}");
            Console.WriteLine($"Фільм 1980 року з рейтингом 6.0: {(Film.IsCultClassic(1980, 6.0) ? "Культова класика!" : "Не культова класика")}");
            //Виведення статистики
            Console.WriteLine("\n\n4. Статистика об'єктів Film:");
            Console.WriteLine($"Кількість створених об'єктів: {Film.FilmCounter}");
            Console.WriteLine($"Середній рейтинг усіх фільмів: {Film.AverageRating:F2}");
        }

        static void AddFilm()//додавання нового фільму з обробкою винятків
        {
            if (filmCount >= maxFilms)
            {
                Console.WriteLine("Досягнуто максимум об'єктів!");
                return;
            }
            Console.WriteLine("\n-ДОДАВАННЯ ФІЛЬМУ-");
            Console.WriteLine("Оберіть спосіб додавання:");
            Console.WriteLine("1 - Ввести дані вручну");
            Console.WriteLine("2 - Ввести через рядок (Parse)");
            Console.Write("Ваш вибір: ");
            int addChoice = ReadAddChoice();
            if (addChoice == 2)
            {
                // Додавання через рядок з використанням TryParse
                Console.WriteLine("\nФормат рядка: Назва,Режисер,Рік,Рейтинг,Жанр");
                Console.WriteLine("Де Жанр: 0-Бойовик, 1-Комедія, 2-Драма, 3-Жахи, 4-Романтика");
                Console.WriteLine("Приклад: Титанік,Cameron,1997,8.5,2");
                Console.Write("\nВведіть рядок: ");
                string inputStr = Console.ReadLine();
                Film newFilm;
                if (Film.TryParse(inputStr, out newFilm))
                {
                    Console.WriteLine($"Років з прем'єри: {newFilm.YearsFromPremiere}");
                    films[filmCount] = newFilm;
                    filmCount++;
                    Console.WriteLine("Фільм успішно додано через Parse!");
                }
                else
                {
                    Console.WriteLine("Помилка! Не вдалося створити фільм з введеного рядка.");
                    Console.WriteLine("Перевірте формат і спробуйте ще раз.");
                }
                return;
            }
            Random random = new Random();//Випадковий вибір конструктора
            int constructorChoice = random.Next(1, 4); //Випадкове число від 1 до 3
            Film newFilmManual = null;
            //Конструктор 1: За замовчуванням
            if (constructorChoice == 1)
            {
                Console.WriteLine(">> Використовується конструктор за замовчуванням (без параметрів)");
                newFilmManual = new Film();
                //користувач вводить дані для зміни значень за замовчуванням
                bool isValid = false;
                while (!isValid)
                {
                    Console.Write("Назва: ");
                    string input = Console.ReadLine();
                    try
                    {
                        newFilmManual.Title = input;
                        isValid = true;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Режисер: ");
                    string input = Console.ReadLine();
                    try
                    {
                        newFilmManual.Director = input;
                        isValid = true;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Рік: ");
                    int year = ReadInt();
                    try
                    {
                        newFilmManual.ReleaseYear = year;
                        isValid = true;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Рейтинг: ");
                    double rate = ReadDouble();
                    try
                    {
                        newFilmManual.SetRating(rate);
                        isValid = true;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Жанр (1-Бойовик, 2-Комедія, 3-Драма, 4-Жахи, 5-Романтика): ");
                    int genreChoice = ReadGenreChoice();
                    try
                    {
                        newFilmManual.Genre = (FilmGenre)(genreChoice - 1);
                        isValid = true;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.Write(ex.Message);
                    }
                }
            }
            //Конструктор 2: З базовими параметрами (назва, режисер, рік)
            else if (constructorChoice == 2)
            {
                Console.WriteLine(">> Використовується конструктор з базовими параметрами (назва, режисер, рік)");
                string title = "";
                string director = "";
                int year = 0;
                bool isValid = false;
                while (!isValid)
                {
                    Console.Write("Назва: ");
                    title = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(title) && title.Length >= 3 && title.Length <= 20)
                        isValid = true;
                    else
                        Console.Write("Помилка! Назва від 3 до 20 символів. Введіть ще раз: ");
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Режисер: ");
                    director = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(director) && director.Length >= 3 && director.Length <= 20)
                        isValid = true;
                    else
                        Console.Write("Помилка! Режисер від 3 до 20 букв. Введіть ще раз: ");
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Рік: ");
                    year = ReadInt();
                    if (year >= 1960 && year <= 2025)
                        isValid = true;
                    else
                        Console.Write("Помилка! Рік від 1960 до 2025. Введіть ще раз: ");
                }
                try
                {
                    newFilmManual = new Film(title, director, year);
                    Console.WriteLine("(Рейтинг та жанр встановлені за замовчуванням: 5.0, Драма)");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка створення об'єкта: {ex.Message}");
                    return;
                }
            }
            // Конструктор3: З усіма параметрами
            else if (constructorChoice == 3)
            {
                Console.WriteLine(">> Використовується конструктор з усіма параметрами (назва, режисер, рік, рейтинг, жанр)");
                string title = "";
                string director = "";
                int year = 0;
                double rating = 0;
                FilmGenre genre = FilmGenre.DRAMA;
                bool isValid = false;
                while (!isValid)
                {
                    Console.Write("Назва: ");
                    title = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(title) && title.Length >= 3 && title.Length <= 20)
                        isValid = true;
                    else
                        Console.Write("Помилка! Назва від 3 до 20 символів. Введіть ще раз: ");
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Режисер: ");
                    director = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(director) && director.Length >= 3 && director.Length <= 20)
                        isValid = true;
                    else
                        Console.Write("Помилка! Режисер від 3 до 20 букв. Введіть ще раз: ");
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Рік: ");
                    year = ReadInt();
                    if (year >= 1960 && year <= 2025)
                        isValid = true;
                    else
                        Console.Write("Помилка! Рік від 1960 до 2025. Введіть ще раз: ");
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Рейтинг: ");
                    rating = ReadDouble();
                    if (rating >= 0.0 && rating <= 10.0)
                        isValid = true;
                    else
                        Console.Write("Помилка! Рейтинг від 0.0 до 10.0. Введіть ще раз: ");
                }
                isValid = false;
                while (!isValid)
                {
                    Console.Write("Жанр (1-Бойовик, 2-Комедія, 3-Драма, 4-Жахи, 5-Романтика): ");
                    int genreChoice = ReadGenreChoice();
                    genre = (FilmGenre)(genreChoice - 1);
                    isValid = true;
                }
                try
                {
                    newFilmManual = new Film(title, director, year, rating, genre);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Помилка створення об'єкта: {ex.Message}");
                    return;
                }
            }
            Console.WriteLine($"Років з прем'єри: {newFilmManual.YearsFromPremiere}");
            films[filmCount] = newFilmManual;
            filmCount++;
            Console.WriteLine("Фільм додано!");
        }

        //вивід усіх фільмів з використанням перевантаженого методу
        static void ShowAllFilms()
        {
            if (filmCount == 0)
            {
                Console.WriteLine("Немає фільмів!");
                return;
            }
            Console.WriteLine("\n-ВСІ ФІЛЬМИ-");
            Console.WriteLine($"Кількість створених об'єктів Film: {Film.FilmCounter}");
            Console.WriteLine($"Середній рейтинг усіх фільмів: {Film.AverageRating:F2}");
            Console.WriteLine();
            for (int i = 0; i < filmCount; i++)
            {
                Console.WriteLine(films[i].GetInfo(i + 1));
            }
        }

        //пошук фільмів за назвою або режисером
        static void FindFilm()
        {
            if (filmCount == 0)
            {
                Console.WriteLine("Немає фільмів!");
                return;
            }
            Console.WriteLine("\n1 - Пошук за назвою");
            Console.WriteLine("2 - Пошук за режисером");
            Console.Write("Ваш вибір: ");
            int choice = ReadSearchChoice();
            bool found = false;
            if (choice == 1)
            {
                Console.Write("Введіть назву: ");
                string searchTitle = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(searchTitle))
                {
                    for (int i = 0; i < filmCount; i++)
                    {
                        if (films[i].Title.ToLower().Contains(searchTitle.ToLower()))
                        {
                            Console.WriteLine(films[i].GetInfo(true));
                            found = true;
                        }
                    }
                }
            }
            else if (choice == 2)
            {
                Console.Write("Введіть режисера: ");
                string searchDirector = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(searchDirector))
                {
                    for (int i = 0; i < filmCount; i++)
                    {
                        if (films[i].Director.ToLower().Contains(searchDirector.ToLower()))
                        {
                            Console.WriteLine(films[i].GetInfo(true));
                            found = true;
                        }
                    }
                }
            }
            if (!found)
                Console.WriteLine("Нічого не знайдено!");
        }

        //демонстрація поведінки фільмів за різними критеріями
        static void DemonstrateFilmBehavior()
        {
            if (filmCount == 0)
            {
                Console.WriteLine("Немає фільмів!");
                return;
            }
            Console.WriteLine("\n-ДЕМОНСТРАЦІЯ ПОВЕДІНКИ-");
            Console.WriteLine("1 - Високорейтингові фільми");
            Console.WriteLine("2 - Фільми за жанром");
            Console.WriteLine("3 - Класичні фільми");
            Console.WriteLine("4 - Сучасні фільми");
            Console.WriteLine("5 - Перевантажені конструктори та методи");
            Console.Write("Ваш вибір: ");
            int choice = ReadDemoChoice();
            if (choice == 1)
            {
                Console.WriteLine("Високорейтингові фільми (>= 8,0):");
                bool foundHighRated = false;
                for (int i = 0; i < filmCount; i++)
                {
                    if (films[i].IsHighRated())
                    {
                        Console.WriteLine(films[i].GetInfo());
                        foundHighRated = true;
                    }
                }
                if (!foundHighRated)
                {
                    Console.WriteLine("Немає високорейтингових фільмів!");
                }
            }
            else if (choice == 2)
            {
                Console.Write("Жанр (1-Бойовик, 2-Комедія, 3-Драма, 4-Жахи, 5-Романтика): ");
                int genreChoice = ReadGenreChoice();
                FilmGenre selectedGenre = (FilmGenre)(genreChoice - 1);
                bool foundByGenre = false;
                for (int i = 0; i < filmCount; i++)
                {
                    if (films[i].Genre == selectedGenre)
                    {
                        Console.WriteLine(films[i].GetInfo());
                        foundByGenre = true;
                    }
                }
                if (!foundByGenre)
                {
                    Console.WriteLine($"Немає фільмів обраного жанру!");
                }
            }
            else if (choice == 3)
            {
                Console.WriteLine("Класичні фільми (старше 20 років):");
                bool foundClassic = false;
                for (int i = 0; i < filmCount; i++)
                {
                    if (films[i].IsClassic())
                    {
                        Console.WriteLine(films[i].GetInfo());
                        Console.WriteLine($"Категорія: {films[i].GetAgeCategory()} ({films[i].YearsFromPremiere} років з прем'єри)");
                        foundClassic = true;
                    }
                }
                if (!foundClassic)
                {
                    Console.WriteLine("Немає класичних фільмів!");
                }
            }
            else if (choice == 4)
            {
                Console.WriteLine("Сучасні фільми (молодше 5 років):");
                bool foundModern = false;
                for (int i = 0; i < filmCount; i++)
                {
                    if (films[i].IsModern())
                    {
                        Console.WriteLine(films[i].GetInfo());
                        Console.WriteLine($"Категорія: {films[i].GetAgeCategory()} ({films[i].YearsFromPremiere} років з прем'єри)");
                        foundModern = true;
                    }
                }
                if (!foundModern)
                {
                    Console.WriteLine("Немає сучасних фільмів!");
                }
            }
            else if (choice == 5)
            {
                Console.WriteLine("\n-ДЕМОНСТРАЦІЯ ПЕРЕВАНТАЖЕНИХ КОНСТРУКТОРІВ ТА МЕТОДІВ-");
                Console.WriteLine("1 - Демонстрація на прикладах (тестові об'єкти)");
                Console.WriteLine("2 - Демонстрація на реальних об'єктах (з ваших доданих)");
                Console.Write("Ваш вибір: ");
                int subChoice = ReadSubDemoChoice();
                if (subChoice == 1)
                {
                    DemonstrateConstructors();
                }
                else if (subChoice == 2)
                {
                    Console.WriteLine("\n-ДЕМОНСТРАЦІЯ ПЕРЕВАНТАЖЕНОЇ ПОВЕДІНКИ ОБРАНОГО ОБ'ЄКТА-");
                    ShowAllFilms();
                    Console.Write("\nОберіть номер фільму для демонстрації (1-{0}): ", filmCount);
                    int selectedIndex = ReadDeleteIndex() - 1;
                    Film selectedFilm = films[selectedIndex];
                    Console.WriteLine("\n-ДЕМОНСТРАЦІЯ ПЕРЕВАНТАЖЕНИХ МЕТОДІВ GetInfo()-");
                    Console.WriteLine("\n1. Базовий виклик GetInfo():");
                    Console.WriteLine(selectedFilm.GetInfo());
                    Console.WriteLine("\n2. Детальний вивід GetInfo(true):");
                    Console.WriteLine(selectedFilm.GetInfo(true));
                    Console.WriteLine("\n3. Короткий вивід GetInfo(false):");
                    Console.WriteLine(selectedFilm.GetInfo(false));
                    Console.WriteLine("\n4. Вивід з номером GetInfo(99):");
                    Console.WriteLine(selectedFilm.GetInfo(99));
                    Console.WriteLine("\n5. Вибіркове відображення - тільки назва та рейтинг:");
                    Console.WriteLine(selectedFilm.GetInfo(true, false, false, true));
                    Console.WriteLine("\n6. Вибіркове відображення - тільки режисер та рік:");
                    Console.WriteLine(selectedFilm.GetInfo(false, true, true, false));
                    Console.WriteLine("\n-ДОДАТКОВА ІНФОРМАЦІЯ ПРО ОБ'ЄКТ-");
                    Console.WriteLine(selectedFilm.GetAdditionalInfo());
                }
            }
        }

        //видалення фільму за індексом або жанром
        private static int ReadSubDemoChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 2)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 2: ");
            }
        }

        //видалення фільму за індексом або жанром
        static void DeleteFilm()
        {
            if (filmCount == 0)
            {
                Console.WriteLine("Немає фільмів!");
                return;
            }
            Console.WriteLine("\n-ВИДАЛЕННЯ ФІЛЬМУ-");
            Console.WriteLine("1 - Видалити за номером");
            Console.WriteLine("2 - Видалити за жанром");
            Console.Write("Ваш вибір: ");
            int choice = ReadDeleteMethodChoice();
            if (choice == 1)
            {
                DeleteFilmByIndex();
            }
            else if (choice == 2)
            {
                DeleteFilmByGenre();
            }
        }

        //видалення фільму за індексом
        static void DeleteFilmByIndex()
        {
            ShowAllFilms();
            Console.Write("Номер для видалення: ");
            int index = ReadDeleteIndex() - 1;
            if (index >= 0 && index < filmCount)
            {
                for (int i = index; i < filmCount - 1; i++)
                {
                    films[i] = films[i + 1];
                }
                filmCount--;
                Console.WriteLine("Видалено!");
            }
            else
            {
                Console.WriteLine("Невірний номер!");
            }
        }

        //видалення фільмів за жанром
        static void DeleteFilmByGenre()
        {
            Console.Write("Жанр для видалення (1-Бойовик, 2-Комедія, 3-Драма, 4-Жахи, 5-Романтика): ");
            int genreChoice = ReadGenreChoice();
            FilmGenre genreToDelete = (FilmGenre)(genreChoice - 1);
            int deletedCount = 0;
            for (int i = filmCount - 1; i >= 0; i--)
            {
                if (films[i].Genre == genreToDelete)
                {
                    for (int j = i; j < filmCount - 1; j++)
                    {
                        films[j] = films[j + 1];
                    }
                    filmCount--;
                    deletedCount++;
                }
            }
            if (deletedCount > 0)
            {
                Console.WriteLine($"Видалено {deletedCount} фільм(ів) жанру {GetGenreName(genreToDelete)}!");
            }
            else
            {
                Console.WriteLine($"Фільми жанру {GetGenreName(genreToDelete)} не знайдені!");
            }
        }

        //отримання назви жанру за його значенням
        private static string GetGenreName(FilmGenre genre)
        {
            switch (genre)
            {
                case FilmGenre.ACTION: return "Бойовик";
                case FilmGenre.COMEDY: return "Комедія";
                case FilmGenre.DRAMA: return "Драма";
                case FilmGenre.HORROR: return "Жахи";
                case FilmGenre.ROMANCE: return "Романтика";
                default: return "Невідомий";
            }
        }

        //видалення фільму за індексом або жанром
        private static int ReadDeleteMethodChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 2)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 2: ");
            }
        }

        //читання цілого числа з обробкою помилок
        public static int ReadInt()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int result))
                    return result;
                Console.Write("Помилка! Введіть ціле число: ");
            }
        }

        //читання додатного цілого числа з обробкою помилок
        public static int ReadPositiveInt()
        {
            while (true)
            {
                int result = ReadInt();
                if (result > 0)
                    return result;
                Console.Write("Помилка! Введіть додатне число: ");
            }
        }

        //читання числа з плаваючою комою з обробкою помилок
        public static double ReadDouble()
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, out double result))
                    return result;
                Console.Write("Помилка! Введіть число (наприклад 7,5): ");
            }
        }

        //читання вибору жанру з обробкою помилок
        private static int ReadGenreChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 5)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 5: ");
            }
        }

        //читання вибору пошуку з обробкою помилок
        private static int ReadSearchChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 2)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 2: ");
            }
        }

        //читання вибору демонстрації з обробкою помилок
        private static int ReadDemoChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 5)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 5: ");
            }
        }

        //читання індексу для видалення з обробкою помилок
        private static int ReadDeleteIndex()
        {
            while (true)
            {
                int index = ReadInt();
                if (index >= 1 && index <= filmCount)
                    return index;
                Console.Write($"Помилка! Введіть число від 1 до {filmCount}: ");
            }
        }

        //читання вибору додавання з обробкою помилок
        private static int ReadAddChoice()
        {
            while (true)
            {
                int choice = ReadInt();
                if (choice >= 1 && choice <= 2)
                    return choice;
                Console.Write("Помилка! Введіть число від 1 до 2: ");
            }
        }
    }
}