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
using System.Xml.Serialization;

namespace ScrollingTesting
{
    class FileHandler
    {
        // xml serializtion for saving
        // add any feild needed here to save any data
        //[Serializable]
        public struct SaveGameData
        {
            public string Name;
            public int HighScore;
        }

        public static SaveGameData data;
        public static List<SaveGameData> list;
        public static StorageDevice storageDevice = null;
        public bool saveCompleted;
        private SaveGameData[] array;
        public FileHandler()
        {
            list = new List<SaveGameData>();
            data = new SaveGameData();
        }

        public void SaveScores(Player[] players, int numSaves)
        {
            int numPlayers = numSaves;

            while (numSaves > 0)
            {
                data.Name = players[numPlayers - numSaves].getName();
                data.HighScore = players[numPlayers - numSaves].HighScore;

                for (int i = 0; i < list.Count; i++)
                {
                    if (String.Equals(list[i].Name, data.Name)
                        && list[i].HighScore < data.HighScore)
                    {

                        list.RemoveAt(i);
                        list.Add(data);
                        numSaves--;
                        break;
                    }
                    else if (String.Equals(list[i].Name, data.Name)
                        && list[i].HighScore >= data.HighScore)
                    {
                        numSaves--;
                        break;
                    }
                    else if (numSaves > 0 && i == list.Count - 1)
                    {
                        list.Add(data);
                        numSaves--;
                        break;
                    }
                }
            }

            FileSaver();
            saveCompleted = true;
        }

        public void LoadScores()
        {
            SaveGameData defaultSave = new SaveGameData();
            defaultSave.Name = "Name";
            defaultSave.HighScore = 0;
            for (int i = 0; i < 20; i++)
            {
                list.Add(defaultSave);
            }
            FileLoader();
        }

       

        public int getHighscore(string PlayerName)
        {
            int num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (String.Equals(list[i].Name, PlayerName))
                {
                    num = list[i].HighScore;
                }
            }

            return num;
        }

        public void sort()
        {
            array = new SaveGameData[list.Count];
            SaveGameData temp = new SaveGameData();
            array = list.ToArray();
            int index = 0;

            for (int i = 0; i < array.Length; i++)
            {
                index = i;

                for (int k = i; k < array.Length; k++)
                {
                    if (array[index].HighScore < array[k].HighScore)
                    {
                        index = k;
                    }
                }

                temp = array[i];
                array[i] = array[index];
                array[index] = temp;

            }
        }

        public string getName(int i)
        {
            return array[i].Name;
        }

        public int getScore(int i)
        {
            return array[i].HighScore;
        }

        
    }
}