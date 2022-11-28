using static System.Console;
using System.Text.RegularExpressions;
class Program
{
    protected const int DEFAULTLIMIT = 3;
    const string DEFAULTFILEPATH = "../README.MD";
    const string EXEFILEPATH="../../../../README.MD";


    protected static int GetOutputLength(int defaultLimit = DEFAULTLIMIT)
    {
        //Здесь даём пользователю ввести отбор по размеру слова, если в консоли чушь или пусто
        WriteLine($"Введите кол-во символов для фильтра. Одна попытка. По умолчанию - {defaultLimit}");
        bool inputValid;
        int output;
        inputValid = Int32.TryParse(ReadLine(), out output);
        return output = inputValid == true ? output : defaultLimit;
    }

    protected static string[] GetIncomingStringArray(
        string defaultFilePath = DEFAULTFILEPATH,
        string pathForExeFile= EXEFILEPATH
    )
    {
        //здесь даём пользователю ввести "массив строк" с клавиатуры, если в консоли чушь или пусто... можно пропустить и тогда распарсится readme.md
        string?[] output = new string?[1];
        char[] defaultSeparators =new char[2]{' ','\n'};
        WriteLine($"Здесь можно ввести \"массив с клавиатуры\", разделители- {String.Join(',',defaultSeparators)}. Если пропустить- программа распарсит {defaultFilePath}");
        string catchInput = ReadLine()!;
        if (catchInput == string.Empty)//собираем ридми в строку, чтобы потом разбить его на массив слов
        {
            var pathToOpen=File.Exists(defaultFilePath)? defaultFilePath: pathForExeFile;
            StreamReader file = new StreamReader(pathToOpen);
            
            string line;
            line = file.ReadLine()!;
            while (line != null)
            {
                line = file.ReadLine()!;
                catchInput = catchInput + line + " ";
            }
            //парсим файлик...тэги оставлять, не?... не^^
            Regex findTags = new Regex("</?.*?>");
            catchInput = findTags.Replace(catchInput, "");
        }
        foreach (var item in defaultSeparators)//решаем вопрос с несколькими разделителями подряд, на всякий случай
        {
            string strfChar=item.ToString();
            while (catchInput.Contains(strfChar + strfChar))
            {
                catchInput = catchInput.Replace(strfChar + strfChar, strfChar);
            }   
        }
        output=catchInput.Split(defaultSeparators);
        return output!;
    }
    protected static string[] RejectExcessiveValues(string [] arrayIncoming, int stringSizeInLetters)
    {//интересно, а можно это сделать через рекурсию?...
        string[] output;
        int count = new int();
        foreach (var item in arrayIncoming)
        {
            if (item.Length<=stringSizeInLetters) count++;
        }
        int output_index= new int();
        output= new string[count];
        for (int i = 0; i <arrayIncoming.Length; i++)
        {
            if (arrayIncoming[i].Length<=stringSizeInLetters)
            {
                output[output_index]=arrayIncoming[i];
                output_index++;
            }
        }
        return output;
    }
    protected static void Core(int? cutOut=null, string[]? arrayInherited=null)
    {
        cutOut =cutOut.HasValue? cutOut: GetOutputLength();
        string[] arrayIncoming= (arrayInherited!=null)? arrayInherited:GetIncomingStringArray();
        var result=RejectExcessiveValues(arrayIncoming,cutOut.Value);
        WriteLine(String.Join(", ", result));
    }
    static void Main(string[] args)
    {
        if (args.Length > 1)
        {
            int parsedArg = new int();
            bool firstArgParsable = Int32.TryParse(args[0], out parsedArg);
            if (firstArgParsable)
            {
                string[] arrayIncoming = new string[args.Length - 1];
                for (int nextArg = 1; nextArg < args.Length; nextArg++)
                {
                    arrayIncoming[nextArg - 1] = args[nextArg];
                }
                Core(cutOut: parsedArg, arrayInherited: arrayIncoming);

            }
            else
            {
                WriteLine("Аргументы из консоли заданы не верно");
                Core();
            }
        }
        else
        {
            Core();
        }
        WriteLine("Press any key... :)");
        Console.ReadKey();

    }
}