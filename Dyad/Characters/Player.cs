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

namespace Dyad
{
    class Player : Moveable
    {
        private string name;
        private List<int> bestTimes = new List<int>();
        private List<int> scores = new List<int>(); 

        public Player(String name, float scale, bool rot, int playerNum, int numLvls)
            : base(scale, rot, true)
        {
            this.name = name;
            rotation = 0;
            playerNumber = playerNum;                        
        }

        public override string GetName()
        {
            return name;
        }

        public override int GetBestTime(int lvl)
        {   
            return bestTimes[lvl];
        }

        public override List<int> GetBestTimes()
        {
            return bestTimes;
        }

        public override int GetPlayerNumber()
        {
            return playerNumber;
        }
        
        public override int GetBestMPScore(int lvl)
        {
            return scores[lvl];
        }

        public override void SetBestMPScore(int score, int lvl)
        {
            scores[lvl] = score;
        }

        public override void SetBestMPScores(List<int> scores, int numlvls)
        {
            if (scores == null || scores.Count == 0)
            {
                for (int i = 0; i < numlvls; i++)
                {
                    this.scores.Add(0);
                }
            }
            else
            {
                this.scores = scores;
            }
        }

        public override List<int> GetBestMPScores()
        {
            return scores;
        }

        public override void SetBestTime(int time, int lvl)
        {            
           bestTimes[lvl] = time;            
        }

        public override void SetBestTimes(List<int> times, int numlvls)
        {
            if (times == null || times.Count == 0)
            {
                for (int i = 0; i < numlvls; i++)
                {
                    bestTimes.Add(9999);
                }
            }
            else
            {
                bestTimes = times;
            }
        }        

        public override void Update(GameTime time)
        {            
            base.Update(time);
        }
    }
}