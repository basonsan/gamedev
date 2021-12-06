using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class TerminalControl : MonoBehaviour
{
    // Start is called before the first frame update
    enum Screen {Menu, Password, Win};
    int level;
    string password;
    Screen currentScreen = Screen.Menu;

    

    

    void Start()
    {
        ShowMainMenu(" Эдик!"); 
        string[] Level1Password = {"Первый", "Второй", "Третий"};
        print(Level1Password.Length); //получим 3
    }
    void ShowMainMenu(string playerName) 
    {
        currentScreen = Screen.Menu;
        level = 0;
        password = "";
        string welcomeMessage = "Привет" + playerName;
        Terminal.ClearScreen();
        Terminal.WriteLine(welcomeMessage);
        Terminal.WriteLine("Какой терминал вы хотите взломать сегодня?\n");
        Terminal.WriteLine("Введите 1 - библиотеку");
        Terminal.WriteLine("Введите 2 - пентагон");
        Terminal.WriteLine("Введите 3 - планету земля");
        Terminal.WriteLine("Введите меню - вернуться в меню");
        Terminal.WriteLine("Введите 007 - Бонд?");
    }
    
    void OnUserInput(string input) {
        if (input == "меню") {
            ShowMainMenu(", рад снова вас видеть!");
        } else if (currentScreen  == Screen.Menu) {
            RunMainMenu(input);
        } else if (currentScreen  == Screen.Password) {
            StartCoroutine(SeePassword(level, password, input));
        }
        
    }

    void GameStart() {
        currentScreen = Screen.Password;
        Terminal.WriteLine("Вы выбрали " + level + " уровень");
        Terminal.WriteLine("Возможный пароль 111*" + Convert.ToInt32(password)/111);
        Terminal.WriteLine("Попробуйте угадать пароль:");
    }

    void RunMainMenu (string input) {
        if (input == "007") {
            Terminal.WriteLine("Бонд, джеймс бонд!");
        }
        else if (input == "1") {
            level = 1;
            password = "111";
            GameStart();
        } else if (input == "2") {
            level = 2;
            password = "222";
            GameStart();
        } else if (input == "3") {
            level = 3;
            password = "333";
            GameStart();
        } else {
            Terminal.WriteLine("Ошибка ввода, попробуйте еще раз.");
        }
        
    }

    IEnumerator SeePassword (int level, string password, string input) {
        Terminal.WriteLine("Производится взлом...");
        if (password == input) {
            Terminal.ClearScreen();
            Terminal.WriteLine("ПОЗДРАВЛЯЮ! \nВзлом прошел успешно!");
            Terminal.WriteLine("Игра перезагрузится через:");
            for (int i = 3; i > 0; i--)
            {
                Terminal.WriteLine(Convert.ToString(i));
                yield return new WaitForSeconds(2);
                if (i == 1) {
                    ShowMainMenu(", давай еще что нибудь взломаем");
                }
            }
            
            Terminal.WriteLine("Что бы вернуться в меню, введите меню");
        } else {
            Terminal.WriteLine("Неудачная попытка попробуйте еще раз...");
            Terminal.WriteLine("Возможный пароль 111*" + Convert.ToInt32(password)/111);
            Terminal.WriteLine("Что бы вернуться в меню, введите меню");
        }
    }
}





