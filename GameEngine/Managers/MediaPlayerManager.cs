using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Managers
{
    public class MediaPlayerManager
    {
        private List<Song> _listSong;
        private int maxIndex = 0;
        private static MediaPlayerManager instance;
        private Queue<int> songIndexQue;
        private Song _CurrentSong;
        private Song _NextSong;
        public static MediaPlayerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MediaPlayerManager();
                }
                return instance;
            }
        }
        public void Start()
        {
            int key = songIndexQue.Dequeue();
            MediaPlayer.Play(_listSong[key]);
            songIndexQue.Enqueue(key);
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }
        private void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            if (MediaPlayer.State.ToString().Equals("Stopped"))
            {
                int key = songIndexQue.Dequeue();
                _CurrentSong = _listSong[key];
                _NextSong = _listSong[songIndexQue.Peek()];
                MediaPlayer.Play(_CurrentSong);
                songIndexQue.Enqueue(key);
            }
        }

        public void addSong(Song song)
        {
            if (this._listSong == null)
            {
                this._listSong = new List<Song>();
            }
            this._listSong.Add(song);
            if (songIndexQue == null)
            {
                songIndexQue = new Queue<int>();
            }
            songIndexQue.Enqueue(maxIndex++);
        }
        public void removeSong(Song song)
        {
            if (this._listSong == null)
            {
                return;
            }
            this._listSong.Remove(song);
            if (songIndexQue == null)
            {
                return;
            }
            if (maxIndex == 0)
            {
                return;
            }
            songIndexQue.ToList().Remove(maxIndex--);
            if (maxIndex < 0)
            {
                maxIndex = 0;
            }
        }
    }
}
