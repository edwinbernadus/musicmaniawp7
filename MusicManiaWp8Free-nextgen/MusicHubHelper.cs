using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Coding4Fun.Toolkit.Controls;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Media.PhoneExtensions;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;

namespace MusicManiaWp7
{
    public static class MusicHubHelper
    {
        //public async static void  ExportToHub(string fileName)
        public async static void  ExportToHub(string fileName)
        {
            
            var savedViewModel =
                MainRepository.SavedViewModelRepository.SavedSongsViewModel.FirstOrDefault(x => x.DownloadFileNameWithPath == fileName);

            if (savedViewModel != null)
            {
                try
                {
                    var library = new MediaLibrary();
                    library.SaveSong(new Uri(fileName, UriKind.RelativeOrAbsolute),
                                     new SongMetadata()
                                         {
                                             ArtistName = savedViewModel.Artist,
                                             AlbumArtistName = savedViewModel.Artist,
                                             Name = savedViewModel.SongName,
                                             AlbumName = "Z-Album",
                                             Duration = new TimeSpan(0, 0, 1),
                                             TrackNumber = 1,
                                             AlbumReleaseDate = DateTime.Now,
                                             GenreName = "Download"
                                         },
                                     SaveSongOperation.CopyToLibrary);


                    var isCalled = await OneTimeCalledRepository.GetOneTimeCall(OneTimeCalledRepository.SaveMusicFileName);
                    if (!isCalled)
                    {
                        await OneTimeCalledRepository.SetOneTimeCall(OneTimeCalledRepository.SaveMusicFileName);

                        var message = DictionaryDisplayMessageHelper.TipsCopyMusic();
                        MessageBox.Show(message);
                    }
                    //MessageBox.Show(DictionaryDisplayMessageHelper.SongCopied(), "", MessageBoxButton.OK);
                    CopyMusicToastStreaming();

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

            }
        }

        private static void CopyMusicToastStreaming()
        {
            var message = DictionaryDisplayMessageHelper.SongCopied();

            var toast = new ToastPrompt
            {
                Title = "",
                Message = message,
                TextOrientation = System.Windows.Controls.Orientation.Horizontal,
                FontSize = 20,
                //  ImageSource = new BitmapImage(new Uri("..\\Image\\icon\\ApplicationIcon.png", UriKind.RelativeOrAbsolute))
            };

            //toast.Tap += ToastStreamingOnTap;
            toast.Show();
        }
    }
}