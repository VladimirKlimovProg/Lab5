using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class Dialog
    {
        public static void PrintMenu1stLevel()
        {
            Console.WriteLine("1. Работа с одномерными массивами");
            Console.WriteLine("2. Работа с двумерными массивами");
            Console.WriteLine("3. Работа с рваными массивами");
            Console.WriteLine("4. Выход");
        }

        public static void PrintMenu2ndLevelArray1D()
        {
            Console.WriteLine("1. Сформировать массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Добавить элемент в начало массива");
            Console.WriteLine("4. Назад");
        }

        public static void PrintMenu2ndLevelArray2D()
        {
            Console.WriteLine("1. Сформировать массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Удалить строку с номером К");
            Console.WriteLine("4. Назад");
        }

        public static void PrintMenu2ndLevelJaggedArray()
        {
            Console.WriteLine("1. Сформировать массив");
            Console.WriteLine("2. Напечатать массив");
            Console.WriteLine("3. Добавить К строк, начиная с номера N");
            Console.WriteLine("4. Назад");
        }

        public static void PrintMenu3dLevel()
        {
            Console.WriteLine("1. Создать массив вручную");
            Console.WriteLine("2. Создать массив с помощью ДСЧ");
            Console.WriteLine("3. Назад");
        }

        public static int InputNumber(string Text, params int[] sizes)
        {

            int number = 0;
            bool ok = false;
            do
            {
                try
                {
                    Console.WriteLine(Text);
                    number = Convert.ToInt32(Console.ReadLine());
                    if (sizes.Length == 0)
                    {
                        return number;
                    }
                    if (number >= sizes[0] && number <= sizes[1]) ok = true;
                    else ok = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка при вводе числа");
                    ok = false;
                }
            } while (!ok);
            return number;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            MakeMenu(exit);
        }

        //Главное меню
        private static void MakeMenu(bool exit)
        {
            do
            {
                Dialog.PrintMenu1stLevel();
                int menuItemLevel1 = Dialog.InputNumber("Введите пункт меню", 1, 4);
                switch (menuItemLevel1)
                {
                    case 1:
                        MenuArray();
                        break;
                    case 2:
                        Matr();
                        break;
                    case 3:
                        JagArr();
                        break;
                    case 4:
                        exit = true;
                        break;
                    default:
                        break;
                }

            } while (!exit);
        }
        //Подменю "Рваный массив"
        private static void JagArr()
        {
            int[][] jagged_array = new int[0][];
            int stringSize = 0;
            bool createJag = false;
            int userAnswer;
            do
            {
                Dialog.PrintMenu2ndLevelJaggedArray();
                userAnswer = Convert.ToInt32(Console.ReadLine());
                switch (userAnswer)
                {
                    case 1:
                        {
                            Dialog.PrintMenu3dLevel();
                            userAnswer = Convert.ToInt32(Console.ReadLine());
                            switch (userAnswer)
                            {
                                case 1:
                                    {
                                        createJag = ConsoleFormJaggedArray(out stringSize, out jagged_array);
                                        break;
                                    }
                                case 2:
                                    {
                                        createJag = RandomFormJaggedArray(out stringSize, out jagged_array);
                                        break;
                                    }
                                default: break;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (createJag) PrintJaggedArray(stringSize, jagged_array);
                            else Console.WriteLine("массив не создан");
                            break;
                        }
                    case 3:
                        {
                            if (createJag) InsertStringsToJaggedArray(ref stringSize, ref jagged_array);
                            else Console.WriteLine("массив не создан");
                            break;
                        }
                    case 4: break;
                    default: Console.WriteLine("Нет такого пункта в меню"); break;
                } 
            } while (userAnswer != 4);
        }

        //Добавление строк в рваный массив
        private static bool InsertStringsToJaggedArray(ref int stringSize, ref int[][] jagged_array)
        {
            int k, n;
            k = Dialog.InputNumber("Введите количество строк, которое требуется добавить", 1, 10);
            n = Dialog.InputNumber("Введите номер строки, после которой требуется вставка (отсчёт с 0)", 0, stringSize-1);
            int[][] tempRag = new int[stringSize + k][];
            int columnSize;
            int t = 0;
            Random rnd = new Random();
            //Копирование строк до строки с номером n
            for (int i = 0; i <= n; i++)
            {
                tempRag[t] = jagged_array[i];
                t++;
            }
            //Копирование строк от n + 1 до последней
            for (int i = n + 1; i < stringSize; i++)
            {
                tempRag[t+k] = jagged_array[i];
                t++;
            }
            //Формирование новых строк
            for (int i = n + 1; i <= n + k; i++)
            {
                columnSize = Dialog.InputNumber("Введите количество элементов в строке", 1, 10);
                tempRag[i] = new int[columnSize];
                for (int j = 0; j < columnSize; j++)
                {
                    tempRag[i][j] = rnd.Next(1, 100);
                }
                stringSize++;
            }
            jagged_array = new int[stringSize+k][];
            jagged_array = tempRag;
            
            return true;
        }

        //Создание рваного массива с помощью ручного ввода
        private static bool ConsoleFormJaggedArray(out int stringSize, out int[][] jagged_array)
        {
            int i, j;
            int columnSize;
            stringSize = Dialog.InputNumber("Введите количество строк массива", 1, 10);
            jagged_array = new int[stringSize][];
            for (i = 0; i < stringSize; i++)
            {
                columnSize = Dialog.InputNumber("Введите количество элементов в строке", 1, 10);
                jagged_array[i] = new int[columnSize];
                for (j = 0; j < columnSize; j++)
                {
                    jagged_array[i][j] = Dialog.InputNumber("Введите элемент", 1, 100);
                }
            }
            Console.WriteLine("Массив создан");
            return true;
        }

        //Создание рваного массива с помощью датчика случайных чисел
        private static bool RandomFormJaggedArray(out int stringSize, out int[][] jagged_array)
        {
            int i, j;
            int columnSize;
            Random rnd = new Random();
            stringSize = Dialog.InputNumber("Введите количество строк", 1, 10);
            jagged_array = new int[stringSize][];
            for (i = 0; i < stringSize; i++)
            {
                columnSize = Dialog.InputNumber("Введите количество элементов в строке", 1, 10);
                jagged_array[i] = new int[columnSize];
                for (j = 0; j < columnSize; j++)
                {
                    jagged_array[i][j] = rnd.Next(1, 100);
                }
            }
            Console.WriteLine("Массив создан");
            return true;
        }

        //Распечатка рваного массива
        private static void PrintJaggedArray(int stringSize, int[][] jagged_array)
        {
            int i, j;
            for (i = 0; i < stringSize; i++)
            {
                for (j = 0; j < jagged_array[i].Length; j++)
                    Console.Write(jagged_array[i][j].ToString() + " ");
                Console.WriteLine();
            }
        }

        //Подменю "Двумерный массив"
        private static void Matr()
        {
            bool createMatr = false;
            int userAnswer, formMatr;
            int[,] matr = new int[0, 0];
            int stringSize = 0, columnSize = 0;
            do
            {
                Dialog.PrintMenu2ndLevelArray2D();
                userAnswer = Convert.ToInt32(Console.ReadLine());
                switch (userAnswer)
                {
                    case 1:
                        {
                            Dialog.PrintMenu3dLevel();
                            formMatr = Convert.ToInt32(Console.ReadLine());
                            switch (formMatr)
                            {
                                case 1:
                                    {
                                        createMatr = ConsoleFormMatr(out stringSize, out columnSize, out matr);
                                        break;
                                    }
                                case 2:
                                    {
                                        createMatr = RandomFormMatr(out stringSize, out columnSize, out matr);
                                        break;
                                    }
                                default: break;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (createMatr) PrintMatr(stringSize, columnSize, matr);
                            else Console.WriteLine("Массив не сформирован");
                            break;
                        }
                    case 3:
                        {
                            if (createMatr) DeleteStringInMatrix(ref stringSize, columnSize, ref matr);
                            else Console.WriteLine("Массив не сформирован");
                            break;
                        }
                    case 4: break;
                    default: Console.WriteLine("Нет такого пункта в меню"); break;
                }

            } while (userAnswer != 4);
        }

        //Удаление строки в матрице
        private static void DeleteStringInMatrix(ref int stringSize, int columnSize, ref int[,] matr)
        {
            int k = Dialog.InputNumber("Введите строку, которую требуется удалить (отсчёт с 0)", 0, stringSize - 1);
            int i, j;
            int newStringSize = stringSize - 1;
            int[,] tempMatr = new int[newStringSize, columnSize];
            int t = 0;
            for (i = 0; i < stringSize; i++)
                if (i != k)
                {
                    for (j = 0; j < columnSize; j++)
                        tempMatr[t, j] = matr[i, j];
                    t++;
                }
            matr = tempMatr;
            stringSize = newStringSize;
            Console.WriteLine("Удаление выполнено!");
        }

        //Создание двумерного массива с помощью ручного ввода
        private static bool ConsoleFormMatr(out int stringSize, out int columnSize, out int[,] matr)
        {
            int i, j;
            stringSize = Dialog.InputNumber("Введите количество строк матрицы", 1, 10);
            columnSize = Dialog.InputNumber("Введите количество столбцов матрицы", 1, 10);
            matr = new int[stringSize, columnSize];
            for (i = 0; i < stringSize; i++)
                for (j = 0; j < columnSize; j++)
                {
                    matr[i,j] = Dialog.InputNumber("Введите элемент матрицы", 1, 100);
                }
            Console.WriteLine("Массив создан");
            return true;
        }

        //Создание двумерного массива с помощью датчика случайных чисел
        private static bool RandomFormMatr(out int stringSize, out int columnSize, out int[,] matr)
        {
            int i, j;
            Random rnd = new Random();
            stringSize = Dialog.InputNumber("Введите количество строк матрицы", 1, 10);
            columnSize = Dialog.InputNumber("Введите количество столбцов матрицы", 1, 10);
            matr = new int[stringSize, columnSize];
            for (i = 0; i < stringSize; i++)
                for (j = 0; j < columnSize; j++)
                {
                    matr[i, j] = rnd.Next(1, 100);
                }
            Console.WriteLine("Массив создан");
            return true;
        }

        //Распечатка двумерного массива
        private static void PrintMatr(int stringSize, int columnSize, int[,] matr)
        {
            int i, j;

            for (i = 0; i < stringSize; i++)
            {
                for (j = 0; j < columnSize; j++)
                    Console.Write(matr[i, j].ToString() + " ");
                Console.WriteLine();
            }
        }

        //Подменю "Одномерный массив"
        private static void MenuArray()
        {
            int[] a = null;
            int numberOfAddedElements = 1;
            int lengthOfNewArr = 0;
            int[] newArr = new int[9 + numberOfAddedElements];
            int menuItemLevel2;
            do
            {
                Dialog.PrintMenu2ndLevelArray1D();
                menuItemLevel2 = Dialog.InputNumber("Введите пункт меню", 1, 4);
                switch (menuItemLevel2)
                {
                    case 1:
                        Dialog.PrintMenu3dLevel();
                        int menuItemLevel3 = Dialog.InputNumber("Введите пункт меню", 1, 3);
                        switch (menuItemLevel3)
                        {
                            case 1:
                                a = MakeArray1DConsoleInput();
                                newArr = null;
                                lengthOfNewArr = 0;
                                break;
                            case 2:
                                a = MakeArray1DRandomElements();
                                newArr = null;
                                lengthOfNewArr = 0;
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        if (lengthOfNewArr > 0)
                        {
                            PrintArray(newArr);
                        }
                        else
                        {
                            PrintArray(a);
                        }
                        break;
                    case 3:
                        if (a == null)
                        {
                            Console.WriteLine("Необходимо сначала сформировать массив");
                            break;
                        }
                        newArr = newArr == null || newArr.Length == 10 ? InsertElement(a, numberOfAddedElements) : InsertElement(newArr, numberOfAddedElements);
                        lengthOfNewArr = newArr.Length;
                        numberOfAddedElements++;
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Нет такого пункта меню");
                        break;
                }
            } while (menuItemLevel2 != 4);
        }

        //Создание одномерного массива с помощью датчика случайных чисел
        private static int[] MakeArray1DRandomElements()
        {
            int[] arr = new int[10];
            Random rnd = new Random();
            int number = rnd.Next(1, 100);
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = number;
                number = rnd.Next(1, 100);
            }
            Console.WriteLine("Массив создан");
            return arr;
        }

        //Создание одномерного массива с помощью ручного ввода
        private static int[] MakeArray1DConsoleInput()
        {
            int i;
            int[] arr = new int[10];
            for (i = 0; i < 10; i++)
            {
                arr[i] = Dialog.InputNumber("Введите число", 1, 100);
            }
            Console.WriteLine("Массив создан");
            return arr;
        }

        //Распечатка одномерного массива
        private static void PrintArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }

        //Добавление элемента в начало массива
        private static int[] InsertElement(int[] arr, int numberOfAddedElements)
        {
            int[] newArr = new int[arr.Length + 1];
            for (int i = 0; i < numberOfAddedElements; i++)
            {
                for (int j = arr.Length - 1; j >= 0; j--)
                {
                    newArr[j + 1] = arr[j];
                }
            }
            newArr[0] = Dialog.InputNumber("Введите элемент для добавления", 1, 100);
            return newArr;
        }
    }
}
