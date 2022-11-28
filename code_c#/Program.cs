// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using static System.Console;
using System.Text.RegularExpressions;
class Program
{
    protected const int DEFAULTLIMIT=3;
    protected const char DEFAULTSEPARATOR=' ';
    protected Regex FIND_TAGS=new Regex()
    const string DEFAULTFILEPATH="../README.MD";


    protected static int GetOutputLength(int defaultLimit=DEFAULTLIMIT)
    {
        WriteLine($"Введите кол-во символов для фильтра. Одна попытка. По умолчанию - {defaultLimit}");
        bool inputValid;
        int output;
        inputValid=Int32.TryParse(ReadLine(), out output);
        return output=inputValid==true? output: defaultLimit;
    }

    protected static string[] GetIncomingStringArray(string defaultFilePath=DEFAULTFILEPATH,char defaultSeparator=DEFAULTSEPARATOR)
    {
        string?[] output= new string? [1];
        WriteLine($"Здесь можно ввести \"массив с клавиатуры\", разделитель- {defaultSeparator}. Если пропустить- программа распарсит {defaultFilePath}");
        string catchInput=ReadLine()!;
        if (catchInput==string.Empty)
        {
            StreamReader file = new StreamReader(defaultFilePath);
            string line;
            line=file.ReadLine()!;
            while (line!=null)
            {
                line = file.ReadLine()!;
                catchInput=catchInput+line;
            }
            //парсим файлик...тэги оставлять, не?... не^^
            WriteLine("open");
        }
            while (catchInput.Contains(defaultSeparator.ToString()+defaultSeparator.ToString()))
            {
                catchInput=catchInput.Replace(defaultSeparator.ToString()+defaultSeparator.ToString(),defaultSeparator.ToString());//решаем вопрос с несколькими разделителями подряд
            }
            WriteLine(catchInput);
        return output!;
    }
    static void Core_DefaultArgs()
    {
        int cutOut=GetOutputLength();
        WriteLine(cutOut);
        string[] arrayIncoming= GetIncomingStringArray();
    }
    static void Core_vocalArgs(int cutOut, string[] arrayInherited)
    {

    }
    static void Main(string[] args)
    {
        if (args.Length>1)
        {
            int parsedArg=new int();
            bool FirstArgParsable=Int32.TryParse(args[0], out parsedArg);
            WriteLine($"_{args[0]}_");
            if (FirstArgParsable)
            {
                string [] arrayIncoming= new string [args.Length-1];
                for (int nextArg = 1; nextArg < args.Length; nextArg++)
                {
                    arrayIncoming[nextArg-1]=args[nextArg];
                }
                Core_vocalArgs(cutOut:parsedArg,arrayInherited:arrayIncoming);
                WriteLine(parsedArg);
                WriteLine(String.Join(", ",arrayIncoming));

            }
            else
            {
                WriteLine("Аргументы из консоли заданы не верно");
                Core_DefaultArgs();
            }
        }

        else
        {
            Core_DefaultArgs();
        }
        WriteLine("Anything to quit");
        Console.ReadKey();
        
    }
}