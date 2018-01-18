using Mobile.Models;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.EventArguments;
using Plugin.MediaManager.Abstractions.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class MediaPlayerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaiseAllPropertiesChanged()
        {
            OnPropertyChanged(null);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TagItem> Items { get; }

        private  IMediaManager mediaPlayer;
        public IMediaManager MediaPlayer => mediaPlayer;

        public IMediaQueue Queue => mediaPlayer.MediaQueue;

        public IMediaFile CurrentTrack => Queue.Current;
        public IVideoPlayer MediaVideoPlayer => mediaPlayer.VideoPlayer;

        public TimeSpan Duration  {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        private bool _isSeeking = false;

        public bool IsSeeking
        {
            get
            {
                return _isSeeking;
            }
            set
            {
                // Put into an action so we can await the seek-command before we update the value. Prevents jumping of the progress-bar.
                var a = new Action(async () =>
                {
                    // When disable user-seeking, update the position with the position-value
                    if (value == false)
                    {
                        await mediaPlayer.Seek(TimeSpan.FromSeconds(Position.TotalSeconds));
                    }

                    _isSeeking = value;
                });
                a.Invoke();
            }
        }

        private TimeSpan _position;
        private TimeSpan _duration;

        public TimeSpan Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;

                OnPropertyChanged(nameof(Position));
            }
        }

        public int Downloaded => Convert.ToInt32(mediaPlayer.Buffered.TotalSeconds);

        public bool IsPlaying => mediaPlayer.Status == MediaPlayerStatus.Playing || mediaPlayer.Status == MediaPlayerStatus.Buffering;

        public MediaPlayerStatus Status => mediaPlayer.Status;

        public object Cover => mediaPlayer.MediaQueue.Current.Metadata.AlbumArt;

        public string PlayingText => $"Playing: {(Queue.Index + 1)} of {Queue.Count}";

        public IPlaybackController PlaybackController => MediaPlayer.PlaybackController;

        public Command RemoveTagCommand { get; }

        public MediaPlayerViewModel(submateri sub)
        {
            RemoveTagCommand = new Command((arg) => RemoveTag(arg));
            var tags = new ObservableCollection<TagItem>();
            foreach(var item in sub.Topiks)
            {
                tags.Add(new TagItem { Name = item.Judul, Position = item.PosisiMulai });
            }
            Items = tags;
            mediaPlayer = CrossMediaManager.Current;
            //mediaPlayer.RequestProperties = new Dictionary<string, string> { { "Test", "1234" } };
            mediaPlayer.StatusChanged -= OnStatusChanged;
            mediaPlayer.StatusChanged += OnStatusChanged;
            mediaPlayer.PlayingChanged -= OnPlaying;
            mediaPlayer.PlayingChanged += OnPlaying;
            mediaPlayer.BufferingChanged -= OnBuffering;
            mediaPlayer.BufferingChanged += OnBuffering;
            mediaPlayer.MediaFileChanged -= OnMediaFileChanged;
            mediaPlayer.MediaFileChanged += OnMediaFileChanged;
            mediaPlayer.MediaQueue.PropertyChanged -= OnQueuePropertyChanged;
            mediaPlayer.MediaQueue.PropertyChanged += OnQueuePropertyChanged;
            
        }

        private void RemoveTag(object arg)
        {
            var tagItem = (TagItem)arg;
            if (tagItem == null)
                return;

            PlaybackController.SeekTo(tagItem.Position.TotalSeconds);
        }

        private void OnQueuePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var currentChanged = e.PropertyName == nameof(MediaQueue.Current);
            var countChanged = e.PropertyName == nameof(MediaQueue.Count);
            var indexChanged = e.PropertyName == nameof(MediaQueue.Index);

            if (currentChanged)
            {
                OnPropertyChanged(nameof(CurrentTrack));
            }

            if (countChanged || indexChanged)
            {
                OnPropertyChanged(nameof(PlayingText));
            }
        }

        private void OnPlaying(object sender, EventArgs e)
        {
            if (!IsSeeking)
            {
                // TODO: Please kick that one out here when we have true forwarding of the triggers the player fires.
                Duration = TimeSpan.FromMilliseconds(mediaPlayer.Duration.TotalSeconds > 0 ? Convert.ToInt32(mediaPlayer.Duration.TotalSeconds) : 0); 
                Position=TimeSpan.FromMilliseconds( mediaPlayer.Position.TotalSeconds > 0 ? Convert.ToInt32(mediaPlayer.Position.TotalSeconds) : 0);
            }
        }

        private void OnBuffering(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Downloaded));
        }

        private void OnStatusChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(IsPlaying));
            OnPropertyChanged(nameof(Duration));
            OnPropertyChanged(nameof(Position));
            OnPropertyChanged(nameof(Status));
        }

        private void OnMediaFileChanged(object sender, MediaFileChangedEventArgs args)
        {
            OnPropertyChanged(nameof(CurrentTrack));
            OnPropertyChanged(nameof(Cover));
        }

        public string GetFormattedTime(int value)
        {
            var span = TimeSpan.FromMilliseconds(value);
            if (span.Hours > 0)
            {
                return string.Format("{0}:{1:00}:{2:00}", (int)span.TotalHours, span.Minutes, span.Seconds);
            }
            else
            {
                return string.Format("{0}:{1:00}", (int)span.Minutes, span.Seconds);
            }
        }
    }
}
