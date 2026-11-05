using System.Text;
using System.Text.RegularExpressions;

namespace SSX3_Server.EAServer
{
    // Port of the game's DirtySock password scramble (SLUS_207.72 func_003F35D0 / func_003F3748).
    // The client keys it with the binary SKEY the server returned in the skey exchange.
    public static class LobbyPassword
    {
        static readonly uint[] CrcTable = BuildCrcTable();

        static uint[] BuildCrcTable()
        {
            var table = new uint[256];
            for (uint i = 0; i < 256; i++)
            {
                uint c = i;
                for (int k = 0; k < 8; k++)
                {
                    c = (c & 1) != 0 ? (c >> 1) ^ 0xEDB88320 : c >> 1;
                }
                table[i] = c;
            }
            return table;
        }

        class Cipher
        {
            readonly byte[] state = new byte[256];
            byte index;
            uint crc;

            public Cipher(byte[] key, int iterations)
            {
                for (int i = 0; i < 256; i++)
                {
                    state[i] = (byte)i;
                }

                uint mix = 0;
                for (int n = 0; n < iterations * 256; n++)
                {
                    int i = n & 0xFF;
                    byte old = state[i];
                    mix = (mix >> 8) ^ CrcTable[(mix & 0xFF) ^ key[n % key.Length]];
                    mix = (mix >> 8) ^ CrcTable[(mix & 0xFF) ^ old];
                    int j = (int)(mix & 0xFF);
                    state[i] = state[j];
                    state[j] = old;
                }
            }

            public byte Next()
            {
                byte oldI = state[index];
                index++;
                crc = (crc >> 8) ^ CrcTable[(crc & 0xFF) ^ oldI];
                byte a = state[index];
                int j = (int)(crc & 0xFF);
                byte b = state[j];
                state[index] = b;
                state[j] = a;
                return state[(byte)(a - b)];
            }
        }

        public static byte[] ParseBinary(string value)
        {
            return Convert.FromHexString(value.Trim('"').TrimStart('$'));
        }

        public static string Decode(string pass, byte[] key)
        {
            string text = Regex.Replace(pass.Trim('"'), "%([0-9a-fA-F]{2})", m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());

            if (!text.StartsWith('~'))
            {
                return text;
            }

            var cipher = new Cipher(key, 16);
            byte k = 0;
            var result = new StringBuilder();

            foreach (char c in text.Substring(1))
            {
                k ^= cipher.Next();
                int value = (c - (k % 96 - 64)) % 96 + 32;
                if (value == 0x7F)
                {
                    break;
                }
                result.Append((char)value);
            }

            return result.ToString();
        }
    }
}
