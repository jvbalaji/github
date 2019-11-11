using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Brit
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FileCalc();
        }

        static void FileCalc()
        {
            var filePath = string.Empty;
            float calc; int row = 0;
            string operation, operand;
            string[] lines = { };
            string[] operators = { "add", "subtract", "multiply", "divide", "apply" };
            Queue<string> q1 = new Queue<string>();
            Queue<int> q2 = new Queue<int>();

            //Open file dialog for input file
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            // read input file
            if (!string.IsNullOrEmpty(filePath))
            {
                lines = File.ReadAllLines(filePath, Encoding.UTF8);
            }

            //string[] lines = File.ReadAllLines(@"C:\Users\bperu\source\repos\Brit\Test\Input.txt", Encoding.UTF8);

            foreach (string line in lines)
            {
                try
                {
                    operation = line.Split(' ').First().ToLower();
                    operand = line.Split(' ')[1];

                    var match = operators.Contains(operation);
                    var isNum = int.TryParse(operand, out int n);

                    if (!(match && isNum))     // ignore line if it does not contain any of the keywords or non-numeric
                        continue;

                    if (operation != "apply")
                    {
                        //add to queue
                        q1.Enqueue(operation);
                        q2.Enqueue(Convert.ToInt32(operand));
                        row++;

                        Console.WriteLine(line);
                    }
                    else
                    {
                        //on apply dequeue and calculate

                        calc = Convert.ToInt32(operand);

                        while (row > 0)
                        {
                            string o1 = q1.Dequeue();
                            int i1 = q2.Dequeue();
                            row--;

                            switch (o1.ToLower())
                            {
                                case "add":
                                    calc = calc + i1;
                                    break;
                                case "subtract":
                                    calc = calc - i1;
                                    break;
                                case "multiply":
                                    calc = calc * i1;
                                    break;
                                case "divide":
                                    calc = calc / i1;
                                    break;
                            }
                        }
                        Console.WriteLine(line);
                        Console.WriteLine("Result : {0}\n", calc);

                        q1.Clear();
                        q2.Clear();
                        row = 0;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is IndexOutOfRangeException || ex is FormatException)
                        continue;
                }
            }
        }
    }
}
