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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public RecipE newrecipe { get; set; }
        string name="";
        public Edit(RecipE recipE)
        {
            InitializeComponent();
            typefood.Text = recipE.TypeFood;
            kitchenname.Text = recipE.KitchenName;
            foodname.Text = recipE.FoodName;
            name = recipE.FoodName;
            photolink.Text = recipE.PhotoLink;
            ingredients.Text = recipE.Ingredients;
            guide.Text = recipE.Guide;
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
            path = path.Remove(path.Length - 30);
            string filename = "Recipes\\" + foodname.Text + ".txt";
            string filePath = Path.Combine(path, filename);
            if (File.Exists(filePath))
            {
                MessageBox.Show("Помилка! Ви намагаєтесь змінити нову назву рецепту, яка вже є назвою якогось рецепту!");
                DialogResult = false;
            }
            else
            {
                StreamWriter sw = new StreamWriter(filePath);
                sw.WriteLine(json);
                sw.Close();
                DialogResult = true;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
