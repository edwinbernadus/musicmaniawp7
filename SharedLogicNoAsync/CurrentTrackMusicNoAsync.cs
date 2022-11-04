//using System;


//namespace SharedLogicNoAsync
//{
//    public static class CurrentTrackMusicNoAsync
//    {
//        private static string _fileName = "currenttrack";

//        public static int Load()
//        {

//            var output = 0;
//            try
//            {
//                var item = FileHelper.ReadFromFile(_fileName);
//                output = int.Parse(item);
//            }
//            catch (Exception exception)
//            {
//            }

//            return output;

//        }

//        public static void Save(int currentTrack)
//        {
//            try
//            {

//                FileHelper.TextToFile(_fileName, currentTrack.ToString());
//            }
//            catch (Exception exception)
//            {


//            }

//        }

       
//    }
//}