using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

namespace Dyad
{
    public static class FileHandler
    {
        // xml serializtion for saving
        // add any feild needed here to save any data
        public struct SaveGameData
        {
            public string Name;
            public List<int> times;
            public List<int> scores;
        }

        public static StorageDevice storageDevice;
        public static SaveGameData data= new SaveGameData();
        public static List<SaveGameData> list = new List<SaveGameData>();
        public static bool saveCompleted;
        public static XmlSerializer serializer = new XmlSerializer(typeof(List<SaveGameData>));
        
        public static void Save(Moveable[] players)
        {
            foreach(Moveable player in players)
            {
                data.Name = player.GetName();
                data.times = player.GetBestTimes();
                data.scores = player.GetBestMPScores();

                bool exist = false;

                for (int i = 0; i < list.Count; i++)
                {
                    if (String.Equals(list[i].Name, data.Name))                        
                    {
                        list.RemoveAt(i);                                          
                        list.Insert(0, data);
                        exist = true;
                        break;
                    }                    
                }

                if (!exist)
                {
                    list.Insert(0, data);
                }

            }

            FileSaver();
            saveCompleted = true;
        }

        public static void Load()
        {            
            FileLoader();
        }

        public static List<int> GetBestScores(string PlayerName)
        {
            List<int> num = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (String.Equals(list[i].Name, PlayerName))
                {
                    num = list[i].scores;
                }
            }

            return num;
        }

        public static List<int> GetBestTimes(string PlayerName)
        {           
            List<int> num = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (String.Equals(list[i].Name, PlayerName))
                {
                    num = list[i].times;
                }
            }
            
            return num;
        }

        public static List<SaveGameData> GetAllData()
        {
            return list;
        }

        public static void startThread()
        {
            SaveHighScoresCallback(null);
        }

        static void FileSaver()
        {
            try
            {
                if ((storageDevice != null) &&
                     storageDevice.IsConnected)
                {
                    Thread t = new Thread(new ThreadStart(startThread));
                    t.Start();
                }
                else
                {
                    StorageDevice.BeginShowSelector(
                        new AsyncCallback(SaveHighScoresCallback), null);
                }
            }
            catch { }
        }

        // IAsyncCallBack for saving the file
        private static void SaveHighScoresCallback(IAsyncResult result)
        {
            if ((result != null) && result.IsCompleted)
            {
                storageDevice = StorageDevice.EndShowSelector(result);
            }

            if ((storageDevice != null) &&
                storageDevice.IsConnected)
            {

                // Open a storage container.
                IAsyncResult result2 =
                    storageDevice.BeginOpenContainer("Dyad Saves", null, null);

                // Wait for the WaitHandle to become signaled.
                result2.AsyncWaitHandle.WaitOne();

                using (StorageContainer storageContainer =
                     storageDevice.EndOpenContainer(result2))
                {

                    string filename = "Saves.xml";

                    // Check to see whether the save exists.
                    if (storageContainer.FileExists(filename))
                        // Delete it so that we can create one fresh.
                        storageContainer.DeleteFile(filename);

                    using (Stream file = storageContainer.CreateFile(filename))
                    {
                        serializer.Serialize(file, list);
                    }
                }

                result2.AsyncWaitHandle.Close();
            }
        }

        static void FileLoader()
        {
            try
            {
                if ((storageDevice != null) &&
                    storageDevice.IsConnected)
                {
                    LoadScoresCallBack(null);
                }
                else
                {
                    StorageDevice.BeginShowSelector(new AsyncCallback(LoadScoresCallBack), null);
                }
            }
            catch { }
        }

        // IAsyncCallBack for loading the file
        public static void LoadScoresCallBack(IAsyncResult result)
        {
            if ((result != null) && result.IsCompleted)
            {
                storageDevice = StorageDevice.EndShowSelector(result);
            }
            if ((storageDevice != null) &&
                storageDevice.IsConnected)
            {

                // Open a storage container.
                IAsyncResult result2 =
                    storageDevice.BeginOpenContainer("Dyad Saves", null, null);

                // Wait for the WaitHandle to become signaled.
                result2.AsyncWaitHandle.WaitOne();                                

                using (StorageContainer storageContainer =
                     storageDevice.EndOpenContainer(result2))
                {
                    if (storageContainer.FileExists("Saves.xml"))
                    {
                        using (Stream file = storageContainer.OpenFile("Saves.xml", FileMode.Open))
                        {                            
                            list = (List<SaveGameData>)serializer.Deserialize(file);
                        }
                    }
                }
                result2.AsyncWaitHandle.Close();
            }
        }
    }
}