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
using Microsoft.Win32;

namespace WpfApp1
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      OpenFileDialog folderBrowser = new OpenFileDialog();
      folderBrowser.ValidateNames = false;
      folderBrowser.CheckFileExists = false;
      folderBrowser.CheckPathExists = true;
      folderBrowser.FileName = "Folder Selection.";
      Nullable<bool> dialogOk = folderBrowser.ShowDialog();
      if (dialogOk == true)
      {
        string folderPath = folderBrowser.FileName.Remove(folderBrowser.FileName.Length-17, 17) + "\\myData.csv";
        SaveToCsv(folderPath);
        MessageBox.Show("The file is successfully saved!", "Info");
      }
    }

    private void SaveToCsv(string folderPath)
    {   
      var csv = new StringBuilder();  
      csv.AppendLine("salary,"+salary.Text);
      csv.AppendLine("car," + car.Text);
      csv.AppendLine("clothing," + clothing.Text);
      csv.AppendLine("leisure," + leisure.Text);
      csv.AppendLine("living," + living.Text);

      File.WriteAllText(folderPath, csv.ToString());
    }


    private void btnLoad_Click(object sender, RoutedEventArgs e)
    {
      Dictionary<string, TextBox> boxList = GetTextboxList();
      OpenFileDialog fileDialog = new OpenFileDialog();
      fileDialog.Multiselect = false;
      fileDialog.DefaultExt = ".csv";
      Nullable<bool> dialogOk = fileDialog.ShowDialog();
      if (dialogOk == true)
      {
        string sFileName = fileDialog.FileName;

        using (var reader = new StreamReader(@sFileName))
        {
          while (!reader.EndOfStream)
          {
            var line = reader.ReadLine();
            var values = line.Split(',');
            boxList[values[0]].Text = values[1];
          }
          MessageBox.Show("The file is successfully loaded!", "Info");

        }

      }
    }

    private Dictionary<string, TextBox> GetTextboxList()
    {
      Dictionary<string, TextBox> boxList = new Dictionary<string, TextBox>();
      boxList.Add("salary", salary);
      boxList.Add("car", car);
      boxList.Add("clothing", clothing);
      boxList.Add("leisure", leisure);
      boxList.Add("living", living);
      return boxList;
    }

  }
}
