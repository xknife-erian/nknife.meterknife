namespace NKnife.Encrypt
{
    /// <summary>
    ///     简单加密类
    /// </summary>
    public class SimpleCipher
    {
        static SimpleCipher()
        {
            XorVector = new byte[] {8, 3, 6, 1, 0, 9};
        }

        public static byte[] XorVector { get; set; }

        public static void EncryptBytes(byte[] byteArray)
        {
            int k = 0;
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] ^= XorVector[k];
                k++;
                k = k%XorVector.Length;
            }
        }
    }
}