using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Net;
using Microsoft.Win32;

namespace Книга_кулинарных_рецептов
{
    /// <summary>
    /// Логика взаимодействия для RecipeWin.xaml
    /// </summary>
    public partial class RecipeWin : Window
    {
        string linc;

        public RecipeWin(RecipE recipE)
        {
            InitializeComponent();
            foodname.Content = recipE.FoodName;
            foodtype.Content = recipE.TypeFood;
            kitchenname.Content = "Назва кухні: " + recipE.KitchenName;
            ingredients.Text = recipE.Ingredients;
            linc = recipE.PhotoLink;
            picture.ImageSource = new BitmapImage(new Uri(recipE.PhotoLink));
            guide.Text = recipE.Guide;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "(*.txt)|*.txt|(*.doc)|*.doc|All files(*.*)|*.*";
            saveFileDialog.Title = "Recipe";
            if (saveFileDialog.ShowDialog() == DialogResult.HasValue)
                return;
            string filename = saveFileDialog.FileName;
            System.IO.File.WriteAllText(filename, $"{foodname.Content}\n\n{foodtype.Content}\n\n{kitchenname.Content}\n\n{linc}\n\n{ingredients.Text}\n{guide.Text}");
            MessageBox.Show("Файл сохранен");
        }
    }
}
