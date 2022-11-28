// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
using static System.Console;
class Program
{
    protected const int DEFAULTLIMIT=3;
    protected const char DEFAULTSEPARATOR=' ';
    const string DEFAULTFILEPATH="../README.MD";


    protected int GetOutputLength(int defaultLimit=DEFAULTLIMIT)
    {
        WriteLine($"Введите кол-во символов для фильтра. Одна попытка. По умолчанию - {defaultLimit}");
        bool inputValid;
        int output;
        inputValid=Int32.TryParse(ReadLine(), out output);
        return output=inputValid==true? output: defaultLimit;
    }

    protected string[] GetIncomingStringArray(string defaultFilePath=DEFAULTFILEPATH,char defaultSeparator=DEFAULTSEPARATOR)
    {
        string?[] output= new string? [1];
        WriteLine($"Здесь можно ввести \"массив с клавиатуры\", разделитель- {defaultSeparator}. Если пропустить- программа распарсит {defaultFilePath}");
        string catchInput=ReadLine()!;
        if (catchInput==string.Empty)
        {
            //парсим файлик...тэги оставлять, не?
        }
        else
        {
            while (catchInput.Contains(defaultSeparator.ToString()+defaultSeparator.ToString()))
            {
                catchInput.Replace(defaultSeparator.ToString()+defaultSeparator.ToString(),defaultSeparator.ToString());//решаем вопрос с несколькими разделителями подряд
            }

        }
        

        return output!;
    }
    static void Main(string[] args)
    {
        int q=GetOutputLength();
        WriteLine(q);
    }
}