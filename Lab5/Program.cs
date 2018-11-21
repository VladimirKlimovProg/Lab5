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
            Console.WriteLine("1. Напечатать массив");
            Console.WriteLine("2. Удалить строку с номером К");
            Console.WriteLine("3. Назад");
        }

        public static void PrintMenu2ndLevelJaggedArray()
        {
            Console.WriteLine("1. Напечатать массив");
            Console.WriteLine("2. Добавить К строк, начиная с номера N");
            Console.WriteLine("3. Назад");
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
                    case 4:
                        exit = true;
                        break;
                    default:
                        break;
                }

            } while (!exit);
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
                            PrintArray(newArr, "Одномерный массив");
                        }
                        else
                        {
                            PrintArray(a, "Одномерный массив");
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

        //Создание одномерного массива с помощью датчика случайных чисел.
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

        //Создание одномерного массива с помощью ручного ввода.
        private static int[] MakeArray1DConsoleInput()
        {
            int i;
            int[] arr = new int[10];
            for (i = 0; i < 10; i++)
            {
                arr[i] = Dialog.InputNumber("Введите число");
            }
            Console.WriteLine("Массив создан");
            return arr;
        }

        //Распечатка массива
        private static void PrintArray(int[] arr, string message)
        {
            Console.WriteLine(message);
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
            newArr[0] = Dialog.InputNumber("Введите элемент для добавления");
            return newArr;
        }
    }
}
