using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Dyad
{
    static class Music
    {

        static bool musicOn = true;
        static bool effectsOn = true;
        static List<SoundEffectInstance> songs = new List<SoundEffectInstance>();
        static int currentSong = 0;
        static float soundEffectVolume = .5f;        

        /// <summary>
        /// load all sounds
        /// </summary>
        /// <param name="content"></param>
        static public void LoadContent(ContentManager content)
        {    
            musicOn = SoundOptionsMenuScreen.IsMusicOn();
            effectsOn = SoundOptionsMenuScreen.IsEffectsOn();

            SoundEffect musicFile1;
            musicFile1 = content.Load<SoundEffect>(@"Sounds\beat_a");
            songs.Add(musicFile1.CreateInstance());

            SoundEffect musicFile2;
            musicFile2 = content.Load<SoundEffect>(@"Sounds\beat_b");
            songs.Add(musicFile2.CreateInstance());

            SoundEffect musicFile3;
            musicFile3 = content.Load<SoundEffect>(@"Sounds\beat_c");
            songs.Add(musicFile3.CreateInstance());

            SoundEffect musicFile4;
            musicFile4 = content.Load<SoundEffect>(@"Sounds\beat_d");
            songs.Add(musicFile4.CreateInstance());            

            foreach (SoundEffectInstance music in songs)
            {
                music.IsLooped = true;
            }

        }

        /// <summary>
        /// update every frame: logic to play/pause/turn off sounds
        /// </summary>
        static public void Update()
        {
            musicOn = SoundOptionsMenuScreen.IsMusicOn();
            effectsOn = SoundOptionsMenuScreen.IsEffectsOn();


            // song logic
            if (musicOn)
            {
                if (songs[currentSong].State != SoundState.Playing)
                {
                    songs[currentSong].Play();
                }
            }
            else
            {
                songs[currentSong].Pause();
            }
            
        }

        /// <summary>
        /// change to next song in list
        /// </summary>
        static public void ChangeSong()
        {
            songs[currentSong].Stop();
            currentSong++;
            
            if (currentSong == songs.Count - 1)
            {
                currentSong = 0;
            }

            songs[currentSong].Play();
        }

        /// <summary>
        /// pause current song
        /// </summary>
        static public void Pause()
        {
            songs[currentSong].Pause();
        }

        /// <summary>
        /// play current song
        /// </summary>
        static public void Play()
        {
            songs[currentSong].Play();
        }

        /// <summary>
        /// stop current song
        /// </summary>
        static public void Stop()
        {
            songs[currentSong].Stop();
        }

        /// <summary>
        /// state of the current song
        /// </summary>
        /// <returns></returns>
        static public SoundState SongState()
        {
            return songs[currentSong].State;
        }

        /// <summary>
        /// change volume of music
        /// </summary>
        /// <param name="vol"></param>
        static public void ChangeVolumeMusic(float vol)
        {
            foreach (SoundEffectInstance song in songs)
            {
                song.Volume = vol/10;
            }            
        }

        /// <summary>
        /// change volume of sound effects
        /// </summary>
        /// <param name="vol"></param>
        static public void ChangeVolumeEffects(float vol)
        {
            soundEffectVolume = vol/10;            
        }

        /// <summary>
        /// Play any sound effect instance at proper volume
        /// </summary>
        /// <param name="effect"></param>
        static public void PlaySoundEffect(SoundEffectInstance effect)
        {
            effect.Volume = soundEffectVolume;
            effect.Play();
        }

    }
}
