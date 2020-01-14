using System;
using System.Runtime.InteropServices;

namespace NKnife.API
{
    public sealed partial class API
    {
        /// <summary>
        /// API播放Wav声音文件
        /// </summary>
        public class Sounder
        {
            //System.Media.SoundPlayer sp = new System.Media.SoundPlayer();

            [DllImport("winmm")]
            static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);

            [Flags]
            enum PlaySoundFlags : int
            {
                /// <summary>
                /// 同步播放声音
                /// </summary>
                SND_SYNC = 0x0000,    /* play synchronously (default) */ //同步
                /// <summary>
                /// 异步播放声音
                /// </summary>
                SND_ASYNC = 0x0001,    /* play asynchronously */ //异步
                SND_NODEFAULT = 0x0002,    /* silence (!default) if sound not found */
                SND_MEMORY = 0x0004,    /* pszSound points to a memory file */
                SND_LOOP = 0x0008,    /* loop the sound until next sndPlaySound */
                SND_NOSTOP = 0x0010,    /* don't stop any currently playing sound */
                SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
                SND_ALIAS = 0x00010000, /* name is a registry alias */
                SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
                SND_FILENAME = 0x00020000, /* name is file name */
                SND_RESOURCE = 0x00040004    /* name is resource name or atom */
            }

            /// <summary>
            /// 同步播放指定的Wav文件
            /// </summary>
            /// <param name="wavFilename"></param>
            public static void SyncWavPlay(string wavFilename)
            {
                PlaySound(wavFilename, IntPtr.Zero, PlaySoundFlags.SND_SYNC);
            }

            /// <summary>
            /// 停止同步播放Wav文件
            /// </summary>
            public static void SyncWavStop()
            {
                PlaySound(null, IntPtr.Zero, PlaySoundFlags.SND_SYNC);
            }

            /// <summary>
            /// 异步播放指定的Wav文件
            /// </summary>
            /// <param name="wavFilename"></param>
            public static void AsyncWavPlay(string wavFilename)
            {
                PlaySound(wavFilename, IntPtr.Zero, PlaySoundFlags.SND_ASYNC);
            }

            /// <summary>
            /// 停止异步播放Wav文件
            /// </summary>
            public void AsyncWavStop()
            {
                PlaySound(null, IntPtr.Zero, PlaySoundFlags.SND_ASYNC);
            }
        }
    }
}