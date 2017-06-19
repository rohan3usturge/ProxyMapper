namespace ProxyMapper.Util
{
    internal static class ByteUtil
    {
       /* public static object ByteArrayToObject(this byte[] arrBytes)
        {
            if (arrBytes == null)
            {
                return null;
            }
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                object obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        public static byte[] ObjectToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }*/
    }
}
