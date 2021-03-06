﻿using RuinsOfAlbertrizal.Mechanics;
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

namespace RuinsOfAlbertrizal.Editor.AdderPrompts
{
    /// <summary>
    /// Interaction logic for GUIDAdderPrompt.xaml
    /// </summary>
    public partial class GUIDAdderPrompt : BaseAdderPrompt
    {
        private List<ObjectOfAlbertrizal> TargetObjects { get; set; }

        private List<ObjectOfAlbertrizal> OriginalObjects { get; set; }

        private List<ObjectOfAlbertrizal> StoredObjects { get; set; }

        public List<Guid> TargetGuids { get => TargetObjects.ToGuidList(); }

        public GUIDAdderPrompt()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new GUIDAdderPrompt
        /// </summary>
        /// <param name="targetObjects">The objects that appears on the "added" (right) side.</param>
        /// <param name="storedObjects">The objects that appears on the "add" (left) side.</param>
        /// <param name="title">The title for the window.</param>
        public GUIDAdderPrompt(List<ObjectOfAlbertrizal> targetObjects, List<ObjectOfAlbertrizal> storedObjects, string title)
        {
            InitializeComponent();

            if (targetObjects == null)
            {
                TargetObjects = new List<ObjectOfAlbertrizal>();
                OriginalObjects = new List<ObjectOfAlbertrizal>();
            }
            else
            {
                TargetObjects = targetObjects;
                OriginalObjects = targetObjects;

                for (int i = 0; i < TargetObjects.Count; i++)
                {
                    AddedObjectsList.Items.Add(TargetObjects[i]);
                }
            }

            if (storedObjects == null)
                StoredObjects = new List<ObjectOfAlbertrizal>();
            else
            {
                StoredObjects = storedObjects;

                for (int i = 0; i < StoredObjects.Count; i++)
                {
                    AvailableObjectsList.Items.Add(StoredObjects[i]);
                }
            }

            Title = title;
        }

        protected virtual void AvailableObjectsList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            ObjectOfAlbertrizal objectOfAlbertrizal = StoredObjects[listBox.SelectedIndex];

            TargetObjects.Add(objectOfAlbertrizal);
            AddedObjectsList.Items.Add(objectOfAlbertrizal);
            ListChanged();
        }

        protected virtual void AddedObjectsList_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            if (listBox.SelectedIndex < 0)
                return;

            TargetObjects.RemoveAt(listBox.SelectedIndex);
            listBox.Items.RemoveAt(listBox.SelectedIndex);
            ListChanged();
        }

        protected override void ResetVariable()
        {
            StoredObjects = OriginalObjects;
        }
    }
}
