using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Documents;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;
using System.IO;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;

namespace TextEditor.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string text;
        private string fontFamily;
        private int fontSize;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public string FontFamily
        {
            get { return fontFamily; }
            set
            {
                fontFamily = value;
                OnPropertyChanged(nameof(FontFamily));
            }
        }

        public int FontSize
        {
            get { return fontSize; }
            set
            {
                fontSize = value;
                OnPropertyChanged(nameof(FontSize));
            }
        }

        public ICommand NewCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand SetFontFamilyCommand { get; set; }
        public ICommand SetFontSizeCommand { get; set; }


        public MainViewModel()
        {
            NewCommand = new RelayCommand(New);
            OpenCommand = new RelayCommand(Open);
            SaveCommand = new RelayCommand(Save);
            SetFontFamilyCommand = new RelayCommand<string>(SetFontFamily);
            SetFontSizeCommand = new RelayCommand<string>(SetFontSize);
            FontFamily = "Arial";
            FontSize = 12;
        }

        private void New()
        {
            Text = "";
        }

        private void Open()
        {

            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Save()
        {

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|Word documents (*.doc)|*.doc"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, Text);
            }
        }

        private void SetFontFamily(string fontFamily)
        {
            FontFamily = fontFamily;
        }

        private void SetFontSize(string fontSize)
        {
            if (int.TryParse(fontSize, out int size))
            {
                FontSize = size;
            }
        }



        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
