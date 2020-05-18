﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal.Environment
{
    public class Block : ObjectOfAlbertrizal, INotifyPropertyChanged
    {
        private string tileImageLocation;

        public string TileImageLocation
        {
            get => tileImageLocation;
            set
            {
                tileImageLocation = value;
                OnPropertyChanged();
            }
        }

        protected Bitmap tileImage = Properties.Resources.error;

        public event PropertyChangedEventHandler PropertyChanged;

        [XmlIgnore]
        public Bitmap TileImage
        {
            get
            {
                try
                {
                    tileImage = new Bitmap(Path.Combine(GameBase.CurrentMapLocation, tileImageLocation));
                }
                catch (Exception)
                {

                }
                return tileImage;
            }
        }

        public Point Location { get; set; }

        public BlockType TypeOfBlock { get; set; }

        public Block()
        {
            TypeOfBlock = new BlockType();
        }

        public void OnPropertyChanged([CallerMemberName] string tileImgLocation = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(tileImgLocation));
        }
    }
}
