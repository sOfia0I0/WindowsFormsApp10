using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp10
{
    public partial class Form1 : Form
    {
        // Номер сортировки по возрастанию (индекс в списке)
        const int ASCENDING_SORT = 0;
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = ASCENDING_SORT;
        }
        // Делегат для сравнивания двух чисел
        private delegate bool CompareNumbers(double num1, double num2);
        private static bool IsGreater(double num1, double num2)
        {
            return num1 > num2;
        }private static bool IsLess(double num1, double num2)
        {
            return num1 < num2;
        }
        // Сортировка массива методом вставки
        // Если compare ссылается на isGreater, то сортировка по возрастанию
        // Если compare ссылается на isLess, то сортировка по убыванию
        private void SortNumberArray(double[] array, CompareNumbers compare)
        {
            for (int i = 1; i < array.Length; i++)
            { 
                double key = array[i]; 
                int j = i - 1;
                while (j >= 0 && compare(array[j], key)) 
                {
                    array[j + 1] = array[j]; 
                    j -= 1; 
                } 
                array[j + 1] = key; 
            }
        }
        // По нажатию кнопки для сортировки введенного массива
        private void button1_Click(object sender, EventArgs e)
        {
            // Инициализируем введенный массив
            string[] tmp = textBox1.Text.Split('\n');
            double[] array = new double[tmp.Length];
            for (int i = 0; i < tmp.Length; i++)
                double.TryParse(tmp[i], out array[i]);
            // Создаем делегат, который будет определять порядок сортировки
            CompareNumbers compare;
            if (comboBox1.SelectedIndex == ASCENDING_SORT)
                compare = new CompareNumbers(IsGreater);
            else
                compare = new CompareNumbers(IsLess);
            // Вызываем метод сортировки, передавая в его аргументах делегат
            SortNumberArray(array, compare);
            // Выводим сортированный массив
            textBox2.Text = "";
            foreach (double number in array)
                textBox2.Text += $"{number}\r\n";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
