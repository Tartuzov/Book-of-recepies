using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Книга_кулинарных_рецептов
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public RecipE newrecipe { get; set; }
        public Add()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newrecipe = new RecipE 
            { 
                TypeFood = typefood.Text, 
                KitchenName = kitchenname.Text, 
                FoodName = foodname.Text, 
                PhotoLink = photolink.Text,
                Ingredients = ingredients.Text, 
                Guide = guide.Text 
            };
            string json = JsonConvert.SerializeObject(newrecipe);
            string path = System.IO.Path.GetFullPath(this.ToString());
            path = path.Remove(path.Length - 29);
            string filename = "\\Recipes\\" + foodname.Text + ".txt";
            string filePath = Path.Combine(path, filename);             
            if (File.Exists(filePath))
            {
                MessageBox.Show("Помилка! Ви намагаєтесь додати рецепт з назвою, яка вже є!");
                DialogResult = false;
            }
            else
            {
                StreamWriter sw = new StreamWriter(path + foodname.Text + ".txt");
                sw.WriteLine(json);
                sw.Close();
                DialogResult = true;
            }
        }
    }
}
