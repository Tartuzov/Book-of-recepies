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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Книга_кулинарных_рецептов
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<RecipE> recipEs = new List<RecipE>();
        public MainWindow()
        {
            InitializeComponent();
            string path = System.IO.Path.GetFullPath(this.ToString());
            path = path.Remove(path.Length - 37);
            video1.Source = new Uri(path + "\\video.mp4");
            Restart();
        }

        public void AddRecept(RecipE recipE)
        {
            bool flag1 = false;
            foreach (var item in Recept.Items)
            {
                if (item is MenuItem menuItem)
                {
                    if (menuItem.Header.ToString() == recipE.TypeFood)
                    {
                        foreach (var i in menuItem.Items)
                        {
                            if (i is MenuItem menuItem2)
                            {
                                if (menuItem2.Header.ToString() == recipE.KitchenName)
                                {
                                    Button button = new Button { Content = recipE.FoodName};
                                    button.MinWidth = 150;
                                    button.Click += Button_Click;
                                    menuItem2.Items.Add(button);
                                    flag1 = true;
                                    break;
                                }

                            }
                        }
                        if (flag1 == true)
                        {
                            break;
                        }
                        else
                        {
                            MenuItem m = new MenuItem();
                            m.Header = recipE.KitchenName;
                            menuItem.Items.Add(m);
                            Button button = new Button { Content = recipE.FoodName };
                            button.Click += Button_Click;
                            m.Items.Add(button);
                            flag1 = true;
                            break;
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        public void Restart()
        {
            recipEs.Clear();
            one.Items.Clear();
            two.Items.Clear();
            three.Items.Clear();
            four.Items.Clear();
            five.Items.Clear();
            six.Items.Clear();
            seven.Items.Clear();
            eight.Items.Clear();
            string path = System.IO.Path.GetFullPath(this.ToString());
            path = path.Remove(path.Length - 37);
            string[] txtFiles = Directory.GetFiles(path + "\\Recipes", "*.txt");
            foreach (string file in txtFiles)
            {
                string fileContent = File.ReadAllText(file);
                RecipE recipE = JsonConvert.DeserializeObject<RecipE>(fileContent);
                recipEs.Add(recipE);
                AddRecept(recipE);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            main.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if(edittext.Text == null)
            {
                MessageBox.Show("Ви не ввели назву страви, яку хочете змінити!");
            }
            else
            {
                string path = System.IO.Path.GetFullPath(this.ToString());
                path = path.Remove(path.Length - 36);
                if (File.Exists(path + "Recipes\\" + edittext.Text + ".txt"))
                {
                    string fileContent = File.ReadAllText(path + "Recipes\\"+ edittext.Text + ".txt");
                    RecipE recipE = JsonConvert.DeserializeObject<RecipE>(fileContent);                           
                    File.Delete(path + "Recipes\\" + edittext.Text + ".txt");
                    Edit editWindow = new Edit(recipE);
                    if(editWindow.ShowDialog() == true)
                    {
                        Restart();
                    }
                }
                else
                {
                    MessageBox.Show("Рецепту з такою назвою не існує!");
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (deletetext.Text == null)
            {
                MessageBox.Show("Ви не ввели назву страви, яку хочете видалити!");
            }
            else
            {
                string path = System.IO.Path.GetFullPath(this.ToString());
                path = path.Remove(path.Length - 36);
                if (File.Exists(path + "Recipes\\" + deletetext.Text + ".txt"))
                {
                    string fileContent = File.ReadAllText(path+ "Recipes\\"  + deletetext.Text + ".txt");                                 
                    File.Delete(path + "Recipes\\" + deletetext.Text + ".txt");
                    MessageBox.Show("Ви успішно видалили рецепт!");
                    Restart();
                }
                else
                {
                    MessageBox.Show("Рецепту з такою назвою не існує!");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            foreach (var item in recipEs)
            {
                if(item.FoodName == clickedButton.Content.ToString())
                {
                    RecipeWin recipeWindow = new RecipeWin(item);
                    if(recipeWindow.ShowDialog() == true)
                    {
                        break;
                    }
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Add addWindow = new Add();
            bool flag1 = false;
            if (addWindow.ShowDialog() == true)
            {
                recipEs.Add(addWindow.newrecipe);
                foreach(var item in Recept.Items)
                {
                    if(item is MenuItem menuItem)
                    {
                        if(menuItem.Header.ToString() == addWindow.newrecipe.TypeFood)
                        {
                            foreach (var i in menuItem.Items)
                            {
                                if(i is MenuItem menuItem2)
                                {
                                    if (menuItem2.Header.ToString() == addWindow.newrecipe.KitchenName)
                                    {
                                        Button button = new Button { Content = addWindow.newrecipe.FoodName };
                                        button.Click += Button_Click;
                                        menuItem2.Items.Add(button);
                                        flag1 = true;
                                        break;
                                    }
                                    
                                }
                            }
                            if (flag1 == true)
                            {
                                break;
                            }
                            else
                            {
                                MenuItem m = new MenuItem();
                                m.Header = addWindow.newrecipe.KitchenName;
                                menuItem.Items.Add(m);
                                Button button = new Button { Content = addWindow.newrecipe.FoodName };
                                button.Click += Button_Click;
                                m.Items.Add(button);
                                flag1 = true;
                                break;
                            }
                            
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            findrez.Items.Clear();
            if (typefind.Text== "Назва")
            {
                for (int i = 0; i < recipEs.Count;i++)
                {
                    if (recipEs[i].FoodName.Contains(findtext.Text))
                    {
                        Button button = new Button { Content = recipEs[i].FoodName};
                        button.Click += Button_Click;
                        findrez.Items.Add(button);
                    }
                }
            }
            else if(typefind.Text == "Страва")
                for (int i = 0; i < recipEs.Count; i++)
                {
                    if (recipEs[i].TypeFood.Contains(findtext.Text))
                    {
                        Button button = new Button { Content = recipEs[i].FoodName };
                        button.Click += Button_Click;
                        findrez.Items.Add(button);
                    }
                }         
            else if(typefind.Text == "Кухня")
                for (int i = 0; i < recipEs.Count; i++)
                {
                    if (recipEs[i].KitchenName.Contains(findtext.Text))
                    {
                        Button button = new Button { Content = recipEs[i].FoodName };
                        button.Click += Button_Click;
                        findrez.Items.Add(button);
                    }
                }         
            else if(typefind.Text == "Інгредієнти")
                for (int i = 0; i < recipEs.Count; i++)
                {
                    if (recipEs[i].Ingredients.Contains(findtext.Text))
                    {
                        Button button = new Button { Content = recipEs[i].FoodName };
                        button.Click += Button_Click;
                        findrez.Items.Add(button);
                    }
                }

        }
    }
    public partial class RecipE
    {
        public string TypeFood = "";
        public string KitchenName = "";
        public string FoodName = "";
        public string PhotoLink = "";
        public string Ingredients = "";
        public string Guide = "";
    }

}
