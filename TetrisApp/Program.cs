using System;
using TetrisApp.Helpers;

namespace TetrisApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PlaySound.Playsound();
            Tetris tetris;
            string answer;

            do
            { 
                tetris = new Tetris();
                tetris.Play();
                tetris.ShowResult();
                Console.WriteLine(" Нажмите Ввод\n");
                Console.Clear();
                Console.WriteLine("\nХотите сыграть заново?\n");
                Console.WriteLine("Введите \"Да\" или \"Нет\" \n");
                answer = Console.ReadLine().ToLowerInvariant();
                Console.Clear();
            }while (answer != "нет") ;

        }
    }
}
