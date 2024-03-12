using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;
using System.Collections;

namespace SSX3_Server
{
    class StreamUtil
    {
        #region Read
        public static string ReadNullEndString(Stream stream)
        {
            bool tillNull = false;
            int a = 0;
            while (!tillNull)
            {
                int temp1 = stream.ReadByte();
                if (temp1 == 0x00)
                {
                    tillNull = true;
                }
                else
                {
                    a++;
                }
            }
            stream.Position -= a + 1;
            byte[] FilePath = new byte[a];
            stream.Read(FilePath, 0, a);
            return Encoding.ASCII.GetString(FilePath);
        }

        public static string ReadString(Stream stream, int Length, bool FixNull = true)
        {
            byte[] tempByte = new byte[Length];
            stream.Read(tempByte, 0, tempByte.Length);
            if (FixNull)
            {
                return Encoding.ASCII.GetString(tempByte).Replace("\0", "");
            }
            else
            {
                return Encoding.ASCII.GetString(tempByte);
            }
        }

        public static byte[] ReadBytes(Stream stream, int Length, bool BigEndian = false)
        {
            byte[] tempByte = new byte[Length];
            stream.Read(tempByte, 0, tempByte.Length);

            if (BigEndian)
                Array.Reverse(tempByte);

            return tempByte;
        }

        public static byte ReadUInt8(Stream stream)
        {
            byte tempByte = (byte)stream.ReadByte();
            return tempByte;
        }

        public static sbyte ReadInt8(Stream stream)
        {
            sbyte tempByte = (sbyte)stream.ReadByte();
            return Convert.ToSByte(tempByte);
        }

        //public static int ReadInt12(Stream stream)
        //{
        //    byte[] tempByte = new byte[2];
        //    stream.Read(tempByte, 0, tempByte.Length);
        //    return ByteUtil.BytesToBitConvert(tempByte, 4, 15);
        //}

        //public static int ReadIntCustom(Stream stream, int bytesCount, int Bits, int StartBit, bool BigEndian = false)
        //{
        //    byte[] tempByte = new byte[bytesCount];
        //    stream.Read(tempByte, 0, tempByte.Length);
        //    //if (BigEndian)
        //    //    Array.Reverse(tempByte);

        //    int Value = ByteUtil.BytesToBitConvert(tempByte, StartBit, StartBit + Bits-1);

        //    if ((Value) >= IntPow(2, (Bits - 1)))
        //    {
        //        Value -= IntPow(2, (Bits - 1)) * 2;
        //    }

        //    //BitArray myBA5 = new BitArray(tempByte);
        //    //if (myBA5[StartBit]==true)
        //    //{
        //    //    myBA5[StartBit] = false;
        //    //    Value -= 2 ^ Bits; 
        //    //}


        //    return Value;
        //}

        static int IntPow(int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        public static int ReadInt16(Stream stream, bool BigEndian = false)
        {
            byte[] tempByte = new byte[2];
            stream.Read(tempByte, 0, tempByte.Length);

            if (BigEndian)
                Array.Reverse(tempByte);

            return BitConverter.ToInt16(tempByte, 0);
        }

        public static int ReadUInt16(Stream stream, bool BigEndian = false)
        {
            byte[] tempByte = new byte[2];
            stream.Read(tempByte, 0, tempByte.Length);

            if (BigEndian)
                Array.Reverse(tempByte);

            return BitConverter.ToUInt16(tempByte, 0);
        }

        public static int ReadInt24(Stream stream, bool BigEndian = false)
        {
            byte[] tempByte = new byte[4];
            stream.Read(tempByte, 0, 3);

            if(BigEndian)
            {
                Array.Reverse(tempByte);
                for (int bi = 1; bi < tempByte.Length; bi++)
                {
                    tempByte[bi - 1] = tempByte[bi];
                }
                tempByte[3] = 0x00;
            }

            return BitConverter.ToInt32(tempByte, 0);
        }

        public static int ReadUInt32(Stream stream, bool BigEndian = false)
        {
            byte[] tempByte = new byte[4];
            stream.Read(tempByte, 0, tempByte.Length);

            if (BigEndian)
                Array.Reverse(tempByte);

            return (int)BitConverter.ToUInt32(tempByte, 0);
        }

        public static ulong ReadUInt64(Stream stream, bool BigEndian = false)
        {
            byte[] tempByte = new byte[8];
            stream.Read(tempByte, 0, tempByte.Length);

            if (BigEndian)
                Array.Reverse(tempByte);

            return BitConverter.ToUInt64(tempByte, 0);
        }

        public static float ReadFloat(Stream stream, bool BigEndian = false)
        {
            byte[] tempByte = new byte[4];
            stream.Read(tempByte, 0, tempByte.Length);

            if (BigEndian)
                Array.Reverse(tempByte);

            return BitConverter.ToSingle(tempByte, 0);
        }

        public static float ReadHalfFloat(Stream stream, bool BigEndian = false)
        {
            byte[] tempByte = new byte[2];
            stream.Read(tempByte, 0, tempByte.Length);

            if (BigEndian)
                Array.Reverse(tempByte);

            return (float)BitConverter.ToHalf(tempByte, 0);
        }

        public static Color ReadColour(Stream stream)
        {
            int R = stream.ReadByte();
            int G = stream.ReadByte();
            int B = stream.ReadByte();
            int A = stream.ReadByte();
            return Color.FromArgb(A, R, G, B);
        }

        public static Vector2 ReadVector2(Stream stream, bool BigEndian = false)
        {
            Vector2 vertex = new Vector2();
            vertex.X = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.Y = StreamUtil.ReadFloat(stream, BigEndian);
            return vertex;
        }

        public static Vector3 ReadVector3(Stream stream, bool BigEndian = false)
        {
            Vector3 vertex = new Vector3();
            vertex.X = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.Y = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.Z = StreamUtil.ReadFloat(stream, BigEndian);
            return vertex;
        }

        public static Vector4 ReadVector4(Stream stream, bool BigEndian = false)
        {
            Vector4 vertex = new Vector4();
            vertex.X = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.Y = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.Z = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.W = StreamUtil.ReadFloat(stream, BigEndian);
            return vertex;
        }

        public static Quaternion ReadQuaternion(Stream stream, bool BigEndian = false)
        {
            Quaternion vertex = new Quaternion();
            vertex.X = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.Y = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.Z = StreamUtil.ReadFloat(stream, BigEndian);
            vertex.W = StreamUtil.ReadFloat(stream, BigEndian);
            return vertex;
        }

        public static Matrix4x4 ReadMatrix4x4(Stream stream, bool BigEndian = false)
        {
            Matrix4x4 matrix4X4 = new Matrix4x4();
            matrix4X4.M11 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M12 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M13 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M14 = StreamUtil.ReadFloat(stream, BigEndian);

            matrix4X4.M21 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M22 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M23 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M24 = StreamUtil.ReadFloat(stream, BigEndian);

            matrix4X4.M31 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M32 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M33 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M34 = StreamUtil.ReadFloat(stream, BigEndian);

            matrix4X4.M41 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M42 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M43 = StreamUtil.ReadFloat(stream, BigEndian);
            matrix4X4.M44 = StreamUtil.ReadFloat(stream, BigEndian);

            return matrix4X4;
        }


        #endregion

        #region Write
        public static void WriteNullString(Stream stream, string String)
        {
            if (String != null)
            {
                byte[] tempByte = new byte[String.Length + 1];
                Encoding.ASCII.GetBytes(String).CopyTo(tempByte, 0);
                stream.Write(tempByte, 0, tempByte.Length);
            }
        }

        public static void WriteString(Stream stream, string String, int Length = 0)
        {
            if(String==null)
            {
                String = "";
            }
            int tempLength = String.Length;
            byte[] tempByte = new byte[tempLength];
            Encoding.ASCII.GetBytes(String).CopyTo(tempByte, 0);

            if (Length != 0)
            {
                byte[] tempByte1 = new byte[Length];
                for (int i = 0; i < Length; i++)
                {
                    if(tempByte.Length==i || String == "")
                    {
                        break;
                    }
                    tempByte1[i] = tempByte[i];
                }
                tempByte = tempByte1;
            }


            stream.Write(tempByte, 0, tempByte.Length);
        }

        public static void WriteBytes(Stream stream, byte[] bytes, int Offset = 0)
        {
            stream.Write(bytes, Offset, bytes.Length);
        }

        public static void WriteUInt8(Stream stream, int Int)
        {
            byte[] tempByte = new byte[4];
            BitConverter.GetBytes(Int).CopyTo(tempByte, 0);
            stream.Write(tempByte, 0, 1);
        }

        public static void WriteInt16(Stream stream, int Int, bool BigEndian = false)
        {
            short TempInt = (short)Int;
            byte[] tempByte = new byte[2];
            BitConverter.GetBytes(TempInt).CopyTo(tempByte, 0);

            if (BigEndian)
                Array.Reverse(tempByte);


            stream.Write(tempByte, 0, 2);
        }

        public static void WriteInt24(Stream stream, int Int, bool BigEndian = false)
        {
            byte[] tempByte = new byte[4];
            BitConverter.GetBytes(Int).CopyTo(tempByte, 0);

            if (BigEndian)
            {
                Array.Reverse(tempByte);
                tempByte[0] = tempByte[1];
                tempByte[1] = tempByte[2];
                tempByte[2] = tempByte[3];
            }

            stream.Write(tempByte, 0, 3);
        }

        public static void WriteInt32(Stream stream, int Int, bool BigEndian = false)
        {
            byte[] tempByte = new byte[4];
            BitConverter.GetBytes(Int).CopyTo(tempByte, 0);

            if (BigEndian)
            {
                Array.Reverse(tempByte);
            }

            stream.Write(tempByte, 0, tempByte.Length);
        }

        public static void WriteInt64(Stream stream, long Int, bool BigEndian = false)
        {
            byte[] tempByte = new byte[8];
            BitConverter.GetBytes(Int).CopyTo(tempByte, 0);

            if (BigEndian)
            {
                Array.Reverse(tempByte);
            }

            stream.Write(tempByte, 0, tempByte.Length);
        }

        public static void WriteFloat32(Stream stream, float Float, bool BigEndian = false)
        {
            byte[] tempByte = new byte[4];
            BitConverter.GetBytes(Float).CopyTo(tempByte, 0);

            if (BigEndian)
            {
                Array.Reverse(tempByte);
            }

            stream.Write(tempByte, 0, tempByte.Length);
        }

        public static void WriteVector2(Stream stream, Vector2 vertex3, bool BigEndian = false)
        {
            StreamUtil.WriteFloat32(stream, vertex3.X, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex3.Y, BigEndian);
        }

        public static void WriteVector3(Stream stream, Vector3 vertex3, bool BigEndian = false)
        {
            StreamUtil.WriteFloat32(stream, vertex3.X, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex3.Y, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex3.Z, BigEndian);
        }

        public static void WriteVector4(Stream stream, Vector4 vertex4, bool BigEndian = false)
        {
            StreamUtil.WriteFloat32(stream, vertex4.X, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex4.Y, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex4.Z, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex4.W, BigEndian);
        }

        public static void WriteQuaternion(Stream stream, Quaternion vertex4, bool BigEndian = false)
        {
            StreamUtil.WriteFloat32(stream, vertex4.X, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex4.Y, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex4.Z, BigEndian);
            StreamUtil.WriteFloat32(stream, vertex4.W, BigEndian);
        }

        public static void WriteMatrix4x4(Stream stream, Matrix4x4 matrix4X4, bool BigEndian = false)
        {
            StreamUtil.WriteFloat32(stream, matrix4X4.M11, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M12, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M13, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M14, BigEndian);

            StreamUtil.WriteFloat32(stream, matrix4X4.M21, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M22, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M23, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M24, BigEndian);

            StreamUtil.WriteFloat32(stream, matrix4X4.M31, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M32, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M33, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M34, BigEndian);

            StreamUtil.WriteFloat32(stream, matrix4X4.M41, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M42, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M43, BigEndian);
            StreamUtil.WriteFloat32(stream, matrix4X4.M44, BigEndian);
        }

        public static void WriteStreamIntoStream(Stream MainStream, Stream Input, long StartPos = -1, long Length = -1)
        {
            if(Length == -1)
            {
                Length = (int)Input.Length;
            }
            if(StartPos==-1)
            {
                StartPos = 0;
            }

            Input.Position = StartPos;
            byte[] Buffer = new byte[Length];
            Buffer = ReadBytes(Input, (int)Length);
            WriteBytes(MainStream, Buffer);
        }

        #endregion

        public static void AlignBy16(Stream stream)
        {
            int Num = 16 - ((int)stream.Position % 16);
            if (Num != 16)
            {
                stream.Position += Num;
            }
        }

        public static void AlignBy(Stream stream, int Alignment, long StartLoc = 0)
        {
            long StreamPos = stream.Position;

            if(StartLoc!=0)
            {
                StreamPos = StreamPos - StartLoc;
            }


            long Num = Alignment - ((int)StreamPos % Alignment);

            if (Num != Alignment)
            {
                stream.Position += Num;
            }
        }
    }
}
