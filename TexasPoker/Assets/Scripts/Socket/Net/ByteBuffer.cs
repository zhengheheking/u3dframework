namespace Socket.Net
{
    using System;
    using System.Text;

    public class ByteBuffer
    {
        private const int MAX_LENGTH = 1024 * 10;

        private byte[] TEMP_BYTE_ARRAY = new byte[MAX_LENGTH];

        private int CURRENT_LENGTH = 0;

        private int CURRENT_POSITION = 0;

        private byte[] RETURN_ARRAY;

        public ByteBuffer()
        {
            this.Initialize();
        }

        public ByteBuffer(byte[] bytes)
        {
            this.Initialize();
            this.WriteByteArray(bytes);
        }

        public int Length
        {
            get
            {
                return CURRENT_LENGTH;
            }
        }

        public int Position
        {
            get
            {
                return CURRENT_POSITION;
            }
            set
            {
                CURRENT_POSITION = value;
            }
        }

        public byte[] ToByteArray()
        {
            RETURN_ARRAY = new byte[CURRENT_LENGTH];
            Array.Copy(TEMP_BYTE_ARRAY, 0, RETURN_ARRAY, 0, CURRENT_LENGTH);
            return RETURN_ARRAY;
        }

        public void Initialize()
        {
            TEMP_BYTE_ARRAY.Initialize();
            CURRENT_LENGTH = 0;
            CURRENT_POSITION = 0;
        }

        public void WriteByte(byte by)
        {
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = by;
        }

        public void WriteByteArray(byte[] ByteArray)
        {
            ByteArray.CopyTo(TEMP_BYTE_ARRAY, CURRENT_LENGTH);
            CURRENT_LENGTH += ByteArray.Length;
        }

        public void WriteShort(short Num)
        {
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0xff00) >> 8) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)((Num & 0x00ff) & 0xff);
        }

        public void WriteString(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            WriteShort((short)bytes.Length);
            WriteByteArray(bytes);
        }

        public void WriteInt(int Num)
        {
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0xff000000) >> 24) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0x00ff0000) >> 16) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0x0000ff00) >> 8) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)((Num & 0x000000ff) & 0xff);
        }

        public void WriteLong(long Num)
        {
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0xff000000) >> 24) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0x00ff0000) >> 16) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)(((Num & 0x0000ff00) >> 8) & 0xff);
            TEMP_BYTE_ARRAY[CURRENT_LENGTH++] = (byte)((Num & 0x000000ff) & 0xff);
        }
        public void WriteFloat(float Num)
        {
            byte[] b = BitConverter.GetBytes(Num);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(b);
            }
            WriteByteArray(b);
        }
        public float ReadFloat()
        {
            byte[] b = ReadByteArray(4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(b);
            }
            float c = BitConverter.ToSingle(b, 0);//应为a在b中其实字节为0，故第二个参数为0
            return c;
        }
        public byte ReadByte()
        {
            byte ret = TEMP_BYTE_ARRAY[CURRENT_POSITION++];
            return ret;
        }

        public short ReadShort()
        {
            if (CURRENT_POSITION + 1 >= CURRENT_LENGTH)
            {
                return 0;
            }
            short ret = (short)(TEMP_BYTE_ARRAY[CURRENT_POSITION] << 8 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 1]);
            CURRENT_POSITION += 2;
            return ret;
        }

        public int ReadInt()
        {
            if (CURRENT_POSITION + 3 >= CURRENT_LENGTH)
                return 0;
            int ret = (int)(TEMP_BYTE_ARRAY[CURRENT_POSITION] << 24 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 1] << 16 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 2] << 8 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 3]);
            CURRENT_POSITION += 4;
            return ret;
        }

        public long ReadLong()
        {
            if (CURRENT_POSITION + 3 >= CURRENT_LENGTH)
                return 0;
            long ret = (long)(TEMP_BYTE_ARRAY[CURRENT_POSITION] << 24 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 1] << 16 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 2] << 8 | TEMP_BYTE_ARRAY[CURRENT_POSITION + 3]);
            CURRENT_POSITION += 4;
            return ret;
        }
        public static int ToInt(byte[] buff)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(buff);
            }
            return BitConverter.ToInt32(buff, 0);
        }
        public byte[] ReadByteArray(int Length)
        {
            if (CURRENT_POSITION + Length - 1 >= CURRENT_LENGTH)
            {
                return new byte[0];
            }
            byte[] ret = new byte[Length];
            Array.Copy(TEMP_BYTE_ARRAY, CURRENT_POSITION, ret, 0, Length);
            CURRENT_POSITION += Length;
            return ret;
        }

        public string ReadString()
        {
            short length = ReadShort();
            byte[] bytes = ReadByteArray(length);
            return Encoding.Default.GetString(bytes);
        }
    }
}
