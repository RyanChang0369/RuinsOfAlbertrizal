﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RuinsOfAlbertrizal.Editor
{
    public abstract class EditorProperties
    {
        public TextBox[] RequiredTextBoxes;
        public TextBox[] NumericalTextBoxes;
        public ComboBox[] RequiredComboBoxes;

        public string Title;

        public EditorProperties()
        {

        }

        //public EditorProperties(string title, TextBox[] requiredTextBoxes, TextBox[] numericalTextBoxes,
        //    ComboBox[] requiredComboBoxes)
        //{
        //    Title = title;

        //    RequiredComboBoxes = requiredComboBoxes;
        //    NumericalTextBoxes = numericalTextBoxes;
        //    RequiredTextBoxes = requiredTextBoxes;
        //}

        public void Save()
        {

        }

        public void Create()
        {

        }

        public void Load()
        {

        }
    }
}
