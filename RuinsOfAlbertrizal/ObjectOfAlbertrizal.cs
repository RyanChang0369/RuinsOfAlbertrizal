using RuinsOfAlbertrizal.Environment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RuinsOfAlbertrizal
{
    /// <summary>
    /// Everything must have a name, description, and GUID in this world.
    /// </summary>
    
    public abstract class ObjectOfAlbertrizal : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Guid globalID;

        public Guid GlobalID
        {
            get
            {
                if (globalID == null || globalID == Guid.Empty)
                    globalID = Guid.NewGuid();

                return globalID;
            }
            set => globalID = value;
        }

        private Guid instanceID;

        public Guid InstanceID
        {
            get
            {
                if (instanceID == null || instanceID == Guid.Empty)
                    instanceID = Guid.NewGuid();

                return instanceID;
            }
            set => instanceID = value;
        }

        public string Name { get; set; }

        [XmlIgnore]
        public virtual string DisplayName { get => Name; }

        public string Description { get; set; }

        public ObjectOfAlbertrizal()
        {
            Name = "";
            Description = "";
        }

        public void GetNewInstanceID()
        {
            instanceID = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the names of all members of a list of ObjectsOfAlbertrizal
        /// </summary>
        /// <param name="objectsOfAlbertrizal"></param>
        /// <returns></returns>
        public static string[] GetNames(List<ObjectOfAlbertrizal> objectsOfAlbertrizal)
        {
            string[] names = new string[objectsOfAlbertrizal.Count];

            for (int i = 0; i < objectsOfAlbertrizal.Count; i++)
            {
                names[i] = objectsOfAlbertrizal[i].Name;
            }

            return names;
        }

        /// <summary>
        /// Gets the index of the ObjectOfAlberizal by name, or returns -1 if list does not contain an object with such name.
        /// </summary>
        /// <param name="objectsOfAlbertrizal"></param>
        /// <param name="objectOfAlbertrizal"></param>
        /// <returns></returns>
        public static int GetIndexOfName(List<ObjectOfAlbertrizal> objectsOfAlbertrizal, ObjectOfAlbertrizal objectOfAlbertrizal)
        {
            string[] names = GetNames(objectsOfAlbertrizal);

            for (int i = 0; i < objectsOfAlbertrizal.Count; i++)
            {
                if (objectsOfAlbertrizal[i].Name == objectOfAlbertrizal.Name)
                {
                    return i;
                }
            }

            return -1;
        }

        public bool HasSameGlobalIDAs(ObjectOfAlbertrizal obj)
        {
            return GlobalID.Equals(obj.GlobalID);
        }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual void Load(Map map)
        {

        }

        /// <summary>
        /// Unloads the object, making sure that all guid lists are updated
        /// </summary>
        /// <param name="force">Set to true if editing the item.</param>
        public virtual void Unload(bool force)
        {

        }
    }
}
