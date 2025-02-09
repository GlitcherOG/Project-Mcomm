using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SSX3_Server
{
    public static class ByteUtil
    {
        public static string ReadString(byte[] bytes, int Offset, int Size)
        {
            byte[] tempByte = new byte[Size];

            Array.Copy(bytes, Offset, tempByte, 0, Size);

            return Encoding.ASCII.GetString(tempByte);
        }

        public static int ReadInt32(byte[] bytes, int Offset)
        {
            byte[] NewBytes = new byte[4];

            Array.Copy(bytes, Offset, NewBytes, 0, 4);

            Array.Reverse(NewBytes);

            return BitConverter.ToInt32(NewBytes, 0);
        }

        public static int ReadInt8(byte[] bytes, int Offset)
        {
            return bytes[Offset];
        }

        public static string Decrypt(string Input, string rawMask, bool hasedMask = false)
        {
            string currentByte = "\0";
            int maskIndex = 0;
            char[] MaskArray = rawMask.ToCharArray();

            string result = "";

            foreach (string Char in SplitInParts(Input, 2))
            {
                int hexChar = Encoding.ASCII.GetBytes(Char)[0];
                //convert to hexdec
                if (Char.Length==2)
                {
                    hexChar = BitConverter.ToInt16(Encoding.ASCII.GetBytes(Char));
                }
                hexChar ^= MaskArray[maskIndex];
                hexChar = (((hexChar << 5) &0xFF) | ((hexChar >> 3) &0xFF));

                int Temp = Encoding.ASCII.GetBytes(Char)[0];
                if (currentByte.Length == 2)
                {
                    Temp = BitConverter.ToInt16(Encoding.ASCII.GetBytes(currentByte));
                }

                hexChar ^= Temp;
                currentByte = Char;
                maskIndex++;
                if (maskIndex>MaskArray.Length)
                {
                    maskIndex = 0;
                }

                result = result + Encoding.ASCII.GetString(BitConverter.GetBytes(hexChar));
            }

            return result;
        }

        public static IEnumerable<string> SplitInParts(this string s, Int32 partLength)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (partLength <= 0)
                throw new ArgumentException("Part length has to be positive.", nameof(partLength));

            for (var i = 0; i < s.Length; i += partLength)
                yield return s.Substring(i, Math.Min(partLength, s.Length - i));
        }

        //    public static function decrypt($string, $rawMask, $hashedMask = false)
        //    {
        //        $currentByte = "\0";
        //        $maskIndex = 0;
        //        $mask = $hashedMask ? $rawMask: self::makeMask($rawMask);

        //        $result = '';

        //        foreach (str_split($string, 2) as $char) {
        //            $char = hexdec($char);
        //            $charOrig = $char;
        //            $char ^= ord($mask[$maskIndex]);
        //            $char = ((($char << 5) &0xFF) | (($char >> 3) &0xFF));
        //            $char ^= (int) $currentByte;
        //            $currentByte = $charOrig;
        //            if (!isset($mask[++$maskIndex]))
        //            {
        //                $maskIndex = 0;
        //            }

        //            $result.= chr($char);
        //        }

        //        return $result;
        //    }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes);
            }
        }
    }
}

//<? php

//namespace mpcmf\apps\nfsu2\libraries\helpers;

///**
// * Ea account password hash generator/checker
// */
//class EAPassword
//{
//    const EA_MASK_LENGTH = 32;

//    public static function encrypt($password, $rawMask, $hashedMask = false)
//    {
//        $currentByte = "\0";
//        $maskIndex = 0;
//        $mask = $hashedMask ? $rawMask: self::makeMask($rawMask);

//        $result = '';

//        foreach (str_split($password) as $char) {
//            $char = ord($char);
//            $char ^= $currentByte;
//            $currentByte = ((($char << 3) &0xFF) | (($char >> 5) &0xFF));
//            $currentByte ^= ord($mask[$maskIndex]);
//            if (!isset($mask[++$maskIndex]))
//            {
//                $maskIndex = 0;
//            }

//            $result.= sprintf('%02x', $currentByte);
//        }
//        return $result;
//    }

//    public static function decrypt($string, $rawMask, $hashedMask = false)
//    {
//        $currentByte = "\0";
//        $maskIndex = 0;
//        $mask = $hashedMask ? $rawMask: self::makeMask($rawMask);

//        $result = '';

//        foreach (str_split($string, 2) as $char) {
//            $char = hexdec($char);
//            $charOrig = $char;
//            $char ^= ord($mask[$maskIndex]);
//            $char = ((($char << 5) &0xFF) | (($char >> 3) &0xFF));
//            $char ^= (int) $currentByte;
//            $currentByte = $charOrig;
//            if (!isset($mask[++$maskIndex]))
//            {
//                $maskIndex = 0;
//            }

//            $result.= chr($char);
//        }

//        return $result;
//    }

//    public static function makeMask($something)
//    {
//        /** @noinspection SubStrUsedAsArrayAccessInspection */
//        $offset = (int)substr((string)$something, -1, 1);
//        $hash = hash('sha256', gethostname().self::getSalt(). $something, false);

//        return strrev(substr($hash, $offset, self::EA_MASK_LENGTH));
//    }

//    protected static function getSalt()
//    {
//        $gitHeadCommitFile = APP_ROOT. '/.git/ORIG_HEAD';

//        /** @noinspection PhpUnusedLocalVariableInspection */
//        $composerLockFile = APP_ROOT. '/composer.lock';

//        if (file_exists($gitHeadCommitFile))
//        {
//            $salt = file_get_contents($gitHeadCommitFile);

//        }
//        elseif(file_exists($composerLockFile)) {
//            $salt = md5_file($composerLockFile);

//        } else
//        {
//            $salt = md5_file(APP_ROOT. '/bin/console');
//        }

//        return $salt;
//    }
//}
