using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarians.MediaPlayer;

namespace Mobile.Services
{
    public interface IFileService
    {
        bool FileExists(string filePath);
        string GetFile(string file);
        void PlayMedia(string file);
        void PlayMediaVideo(string file);
        void Mute();
    }
}
