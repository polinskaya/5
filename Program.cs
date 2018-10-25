using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    interface IGradExam//позволяют определить требования к реализации 
    {
        void Show();
    }

    public class Exam : IGradExam
    {
        public string name;
        public int GradExam;
        virtual public void Show()//полиморфный интерфейс в базовом классе набор членов класса, которые могут быть переопределены в классе-наследнике
        {
            Console.WriteLine($"Name : {name}    GradExam mark : {GradExam}");
        }
    }
    class First : Exam
    {
        public override string ToString()
        {
            return ($"Type : First Exam , Name : {name}, GradExam mark : {GradExam}");
        }
    }
    public class Test : IGradExam
    {
        public string name;
        public int GradExam;
        virtual public void Show()
        {
            Console.WriteLine($"Type: Test , Name: {name} , GradExam mark  : {GradExam}");
        }
    }
    public abstract class Isp : IGradExam
    {//Служит только для порождения потомков предоставляют базовый функционал для классов-наследников
        public abstract void FirstQuestion();
        public string name;
        public int GradExam;
        public override string ToString()
        {
            return ($"Type: Isp , Name: {name} , GradExam mark  : {GradExam}");
        }
        virtual public void Show()
        {
            Console.WriteLine($"Work with virtual and override in class Isp: Name : {name}    GradExam : {GradExam}");
        }
    }
    sealed class Question : Isp //класс герметизированный  (бесплодный)
    {//класс, от которого наследовать запрещается
        new readonly string name = "Question 1";
        public override void FirstQuestion()
        {
            Console.WriteLine($"Work with abstract class : Type Question: {name}");
        }
    }

    class Printer
    {
        public virtual void IAmPrinting(IGradExam someEx)
        {
            Console.WriteLine(someEx.GetType());
            Console.WriteLine(someEx.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Exam first = new First() { name = "Math", GradExam = 9 };
            Isp quest = new Question() { name = "Graphic", GradExam = 7};
            quest.Show();
            first.Show();

            Console.WriteLine();
            Test task = new Test() { name = "Grammar (16quest)", GradExam = 10 };
            task.Show();

            Console.WriteLine();
            Question exam = new Question() { name = "Litersture" };
            exam.FirstQuestion();
            exam.Show();

            Console.WriteLine();
            IGradExam first1 = new First() { name = "Music", GradExam = 9 };  //Обращение через интерфейсную ссылку
            first1.Show();
            IGradExam test = new Test() { name = "Chemistry", GradExam = 5 };
            test.Show();
           // ((IGradExam)first1).Show();
           
            //операторы is и as
            Console.WriteLine();
            Test test1 = new Test();
            Boolean checkProd = test1 is Test;
            if (checkProd == true)
            {//Возвращает булевское значение, говорящее о том,
                //можете ли вы преобразовать данное выражение в указанный тип
                Console.WriteLine("test1 is Test");
            }
            Console.WriteLine("test1 {0} System.ValueType",
                test is ValueType ? "is" : "is not");
            Console.WriteLine("test1 {0} Test",
                test is Test ? "is" : "is not");
            // позволяет преобразовывать тип в определенный ссылочный тип
            Console.WriteLine();
            Question second = new Question();
            Isp QSecond = second as Isp;
            QSecond.FirstQuestion();

            Console.WriteLine();
            IGradExam[] array = new IGradExam[4];
            array[0] = first;
            array[1] = quest;
            array[2] = test;
            array[3] = exam;
            Printer printer = new Printer();
            for (int i = 0; i < 4; i++)
            {
                printer.IAmPrinting(array[i]);
            }
            Console.ReadLine();
        }
    }
}
