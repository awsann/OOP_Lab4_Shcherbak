using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4
{
    public class Film
    {
        private string title;
        private string director;
        private int releaseYear;
        private FilmGenre genre;

        //статичні поля та властивості
        private static int filmCounter = 0; //лічильник коректно створених об'єктів
        private static double averageRating = 0.0; //середній рейтинг усіх фільмів
        private static double totalRating = 0.0; //сума всіх рейтингів

        public static int FilmCounter //властивість для отримання лічильника фільмів
        {
            get { return filmCounter; }
        }

        public static double AverageRating //властивість для отримання середнього рейтингу
        {
            get { return averageRating; }
        }

        public int YearsFromPremiere //властивість для обчислення років з прем'єри
        {
            get { return 2025 - releaseYear; }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 20)
                {
                    throw new ArgumentException("Помилка! Назва від 3 до 20 символів. Введіть ще раз: ");
                }
                title = value.Trim();
            }
        }

        public string Director
        {
            get { return director; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3 || value.Length > 20 || !IsOnlyLetters(value))
                {
                    throw new ArgumentException("Помилка! Режисер від 3 до 20 букв (тільки літери). Введіть ще раз: ");
                }
                director = value.Trim();
            }
        }

        private bool IsOnlyLetters(string text)//перевірка чи містить тільки букви та пробіли
        {
            foreach (char c in text)
            {
                if (!char.IsLetter(c) && c != ' ')
                    return false;
            }
            return true;
        }

        public int ReleaseYear
        {
            get { return releaseYear; }
            set
            {
                if (value < 1960 || value > 2025)
                {
                    throw new ArgumentException("Помилка! Рік від 1960 до 2025. Введіть ще раз: ");
                }
                releaseYear = value;
            }
        }

        public double Rating { get; private set; }

        public void SetRating(double value)//метод для встановлення рейтингу з валідацією
        {
            if (value < 0.0 || value > 10.0)
            {
                throw new ArgumentException("Помилка! Рейтинг від 0.0 до 10.0. Введіть ще раз: ");
            }
            //оновлення статичних даних про рейтинги
            totalRating = totalRating - Rating + value;
            Rating = value;
            if (filmCounter > 0)
                averageRating = totalRating / filmCounter;
        }

        public FilmGenre Genre
        {
            get { return genre; }
            set
            {
                if (!Enum.IsDefined(typeof(FilmGenre), value))
                {
                    throw new ArgumentException("Помилка! Невірне значення жанру.");
                }
                genre = value;
            }
        }

        //КОНСТРУКТОРИ
        //1.Конструктор за замовчуванням (без параметрів)
        public Film()
        {
            title = "Без назви";
            director = "Невідомий";
            releaseYear = 2024;
            Rating = 0.0;
            genre = FilmGenre.DRAMA;
            filmCounter++;
            totalRating += Rating;
            if (filmCounter > 0)
                averageRating = totalRating / filmCounter;
        }

        //2.Конструктор з базовими параметрами (назва, режисер, рік)
        public Film(string title, string director, int releaseYear)
            : this(title, director, releaseYear, 5.0, FilmGenre.DRAMA)
        {
        }

        //3.Конструктор з усіма основними параметрами
        public Film(string title, string director, int releaseYear, double rating, FilmGenre genre)
        {
            this.Title = title;
            this.Director = director;
            this.ReleaseYear = releaseYear;
            this.Rating = rating;
            this.Genre = genre;
            filmCounter++;
            totalRating += rating;
            if (filmCounter > 0)
                averageRating = totalRating / filmCounter;
        }

        //4.Копіюючий конструктор (створює копію іншого фільму)
        public Film(Film other)
        {
            if (other == null)
                throw new ArgumentNullException("Неможливо скопіювати null об'єкт!");
            this.title = other.title;
            this.director = other.director;
            this.releaseYear = other.releaseYear;
            this.Rating = other.Rating;
            this.genre = other.genre;
            filmCounter++;
            totalRating += Rating;
            if (filmCounter > 0)
                averageRating = totalRating / filmCounter;
        }

        // СТАТИЧНІ МЕТОДИ
        //1. Статичний метод для отримання рекомендації за рейтингом
        public static string GetRatingRecommendation(double rating)
        {
            if (rating >= 9.0) return "Шедевр! Обов'язково до перегляду!";
            else if (rating >= 8.0) return "Відмінний фільм, рекомендується!";
            else if (rating >= 7.0) return "Хороший фільм, варто подивитись.";
            else if (rating >= 5.0) return "Середній фільм, на любителя.";
            else return "Низький рейтинг, дивитись не рекомендується.";
        }

        //2. Статичний метод для перевірки чи є фільм культовим
        public static bool IsCultClassic(int year, double rating)
        {
            int age = 2025 - year;
            return (age >= 20 && rating >= 7.5);
        }

        //3. Статичний метод для скидання лічильника (для тестування)
        public static void ResetCounter()
        {
            filmCounter = 0;
            totalRating = 0.0;
            averageRating = 0.0;
        }

        public override string ToString()//ToString() - перетворення об'єкта Film на рядок
        {
            return $"{title},{director},{releaseYear},{Rating},{(int)genre}";
        }

        public static Film Parse(string s)//Parse - перетворення рядка на об'єкт Film
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException("Рядок не може бути null або пустим!");
            string[] parts = s.Split(',');
            if (parts.Length != 5)
                throw new FormatException("Рядок не у правильному форматі! Очікується: Назва,Режисер,Рік,Рейтинг,Жанр");
            try
            {
                string title = parts[0].Trim();
                string director = parts[1].Trim();
                int year = int.Parse(parts[2].Trim());
                double rating = double.Parse(parts[3].Trim(), System.Globalization.CultureInfo.InvariantCulture);
                int genreInt = int.Parse(parts[4].Trim());
                if (genreInt < 0 || genreInt > 4)
                    throw new ArgumentException("Жанр має бути від 0 до 4!");
                FilmGenre genre = (FilmGenre)genreInt;
                return new Film(title, director, year, rating, genre);
            }
            catch (FormatException)
            {
                throw new FormatException("Помилка формату! Перевірте правильність введених даних.");
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Помилка валідації: {ex.Message}");
            }
        }

        //TryParse - безпечне перетворення рядка на об'єкт Film
        public static bool TryParse(string s, out Film film)
        {
            film = null;
            try
            {
                film = Parse(s);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //ПЕРЕВАНТАЖЕНІ МЕТОДИ GetInfo()
        //1.Базова версія (повертає повну інформацію)
        public string GetInfo()
        {
            return $"Назва: {title}, Режисер: {director}, Рік: {releaseYear}, Рейтинг: {Rating:F1}, Жанр: {GetGenreName()}";
        }

        //2.Версія з параметром детальності (bool detailed)
        public string GetInfo(bool detailed)
        {
            if (detailed)
            {
                return $"Назва: {title}\n" +
                       $"Режисер: {director}\n" +
                       $"Рік випуску: {releaseYear}\n" +
                       $"Рейтинг: {Rating:F1}/10.0\n" +
                       $"Жанр: {GetGenreName()}\n" +
                       $"Років з прем'єри: {YearsFromPremiere}\n" +
                       $"Категорія за віком: {GetAgeCategory()}\n" +
                       $"Високорейтинговий: {(IsHighRated() ? "Так" : "Ні")}";
            }
            else
            {
                return $"{title} ({releaseYear}) - {Rating:F1}";
            }
        }

        //3.Версія з форматуванням (повертає з певним префіксом/номером)
        public string GetInfo(int number)
        {
            return $"{number}. {GetInfo()}";
        }

        //4.Версія яка повертає тільки вибрані поля
        public string GetInfo(bool showTitle, bool showDirector, bool showYear, bool showRating)
        {
            List<string> parts = new List<string>();
            if (showTitle) parts.Add($"Назва: {title}");
            if (showDirector) parts.Add($"Режисер: {director}");
            if (showYear) parts.Add($"Рік: {releaseYear}");
            if (showRating) parts.Add($"Рейтинг: {Rating:F1}");
            return string.Join(", ", parts);
        }

        public bool IsClassic()//визначення чи є фільм класичним (20 і більше років з прем'єри)
        {
            return YearsFromPremiere >= 20;
        }

        public bool IsModern()//визначення чи є фільм сучасним (до 5 років з прем'єри)
        {
            return YearsFromPremiere <= 5;
        }

        public string GetAgeCategory()//визначення категорії за віком
        {
            if (YearsFromPremiere >= 20) return "Класичний";
            else if (YearsFromPremiere >= 5) return "Недавній";
            else return "Сучасний";
        }

        //отримання назви жанру
        public string GetGenreName()
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

        //перевірка високого рейтингу
        public bool IsHighRated()
        {
            return Rating >= 8.0;
        }

        //НОВЕ: Метод для отримання додаткової інформації
        public string GetAdditionalInfo()
        {
            return $"Років з прем'єри: {YearsFromPremiere}\n" +
                   $"Категорія за віком: {GetAgeCategory()}\n" +
                   $"Високорейтинговий: {(IsHighRated() ? "Так" : "Ні")}\n" +
                   $"Класичний: {(IsClassic() ? "Так" : "Ні")}\n" +
                   $"Сучасний: {(IsModern() ? "Так" : "Ні")}";
        }
    }
}