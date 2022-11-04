using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Phone.BackgroundAudio;
using Model;
using MusicLogicExtendedWp7.Repository;
using System.Threading.Tasks;
using SharedLogic;

namespace MusicLogicExtendedWp7.Helper
{
    public static class BackgroundPlayerHelper
    {
       
        public static void BackgroundPlayerFileValidation(Uri downloadLocation)
        {
            if (BackgroundAudioPlayer.Instance.Track != null)
            {
                if (BackgroundAudioPlayer.Instance.Track.Source == downloadLocation)
                {
                    if (BackgroundAudioPlayer.Instance.PlayerState == PlayState.Playing)
                    {
                        BackgroundAudioPlayer.Instance.Pause();
                    }
                    BackgroundAudioPlayer.Instance.Track = null;
                }
            }
        }


        public static async Task StartPlaylistOnBackground(string guidInput, string playListName)
        {

            {
                Debug.WriteLine("play background: " + playListName);

                {
                    var repo = new Repository.PlayListRepository();
                    var t = await repo.Load();
                    var t2 = t.FirstOrDefault(x => x.Name == playListName);

                    if (t2!= null)
                    {
                        var repoDetail = new Repository.PlayListDetailRepository();
                        //var playListName = t2.Name;
                        var t3 = await repoDetail.Load(playListName);
                        var playListDetail = t3.FirstOrDefault(x => x.GuidString.ToString() == guidInput);

                        if (playListDetail != null)
                        {
                            var index = t3.IndexOf(playListDetail);


                            //var index = MainRepository.PersistantSongRepository.PersistantSongs.IndexOf(persistance);

                            var item = new AudioState()
                                           {
                                               AudioTrackNumber = index,
                                               BackgroundType =
                                                   AudioState.PlaylistEnum.Playlist.ToString
                                                   (),
                                               PlaylistName = playListName,
                                               GuidString = playListDetail.GuidString

                                           };

                            await AudioStateManager.SaveAsync(item);
                            BackgroundAudioPlayer.Instance.Play();
                        }
                    }
                }
            }
        }

        public static async Task StartSongOnBackground(string downloadFileNameWithPath, string playListEnum)
        {

            {
                Debug.WriteLine("play background: " + downloadFileNameWithPath);

                if (playListEnum== AudioState.PlaylistEnum.Download.ToString())
                {
                    var persistance =
                        MainRepository.PersistantSongRepository.PersistantSongs.FirstOrDefault(
                            x => x.DownloadFileNameWithPath == downloadFileNameWithPath);
                    if (persistance != null)
                    {
                        var index = MainRepository.PersistantSongRepository.PersistantSongs.IndexOf(persistance);
                        //await CurrentTrackMusic.SaveAsync(index);

                        var item = new AudioState()
                                       {
                                           AudioTrackNumber = index,
                                           BackgroundType =
                                               AudioState.PlaylistEnum.Download.ToString
                                               (),
                                           PlaylistName = ""
                                       };
                        await AudioStateManager.SaveAsync(item);
                        BackgroundAudioPlayer.Instance.Play();

                    }
                }
                else
                {
                    var streaming=
                      MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.FirstOrDefault(
                          x => x.DownloadFileNameWithPath == downloadFileNameWithPath);
                    if (streaming != null)
                    {
                        var index = MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.IndexOf(streaming);
                        //await CurrentTrackMusic.SaveAsync(index);

                        var item = new AudioState()
                        {
                            AudioTrackNumber = index,
                            BackgroundType =
                                AudioState.PlaylistEnum.Streaming.ToString
                                (),
                            PlaylistName = ""
                        };
                        await AudioStateManager.SaveAsync(item);
                        BackgroundAudioPlayer.Instance.Play();

                    }
                }
            }

        }

        //TODO
        
        public static async Task UpdateSongOnBackgroundPosition(string playListEnum)
        {

            {

                if (BackgroundAudioPlayer.Instance.Track != null)
                {
                    var downloadFileNameWithPath = BackgroundAudioPlayer.Instance.Track.Source.ToString();

                    if (playListEnum == AudioState.PlaylistEnum.Download.ToString())
                    {
                        var persistance =
                            MainRepository.PersistantSongRepository.PersistantSongs.FirstOrDefault(
                                x => x.DownloadFileNameWithPath == downloadFileNameWithPath);
                        if (persistance != null)
                        {
                            var index = MainRepository.PersistantSongRepository.PersistantSongs.IndexOf(persistance);
                            //await CurrentTrackMusic.SaveAsync(index);
                            var item = new AudioState()
                                           {
                                               AudioTrackNumber = index,
                                               BackgroundType =
                                                   AudioState.PlaylistEnum.Download.ToString(),
                                               PlaylistName = ""
                                           };
                            AudioStateManager.SaveAsync(item);
                        }
                    }
                    else
                    {
                        var streaming =
                        MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.FirstOrDefault(
                            x => x.DownloadFileNameWithPath == downloadFileNameWithPath);
                        if (streaming != null)
                        {
                            var index = MainRepository.StreamingInfoViewModelRepository.StreamingInfoViewModels.IndexOf(streaming);
                            //await CurrentTrackMusic.SaveAsync(index);
                            var item = new AudioState()
                            {
                                AudioTrackNumber = index,
                                BackgroundType =
                                    AudioState.PlaylistEnum.Streaming.ToString(),
                                PlaylistName = ""
                            };
                            AudioStateManager.SaveAsync(item);
                        }
                    }
                }
            }

        }
    }
}