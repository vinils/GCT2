using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GoofyCoin
{
    [Serializable]
    public class TransferHash
    {
        [NonSerialized]
        private Transfer transfer;

        [NonSerialized]
        private byte[] transHash;

        [NonSerialized]
        private SignedTransfer sgndTrans;

        private byte[] hashSgndTrans;

        public byte[] HashSgndTrans
        {
            get { return hashSgndTrans; }
        }

        public TransferHash(Transfer trans, Signature mySignature)
        {
            this.transfer = trans;
            var serializedTrans = SerializeObject(trans);
            this.sgndTrans = mySignature.SignHash(serializedTrans);
            this.hashSgndTrans = Hash(sgndTrans.SignedData);


            //var hexStr = ByteArrayToString(transHash);
            //Console.WriteLine(hexStr);
            //Console.WriteLine(ToString(transHash));
            //Console.WriteLine(EndianFlip32BitChunks(ToString(transHash)));
            //var hashcomp = StringToByteArray(hexStr);

            //bool bRet;
            //for (int x = 0; x < transHash.Length; x++)
            //{
            //    if (this.transHash[x] != hashcomp[x])
            //    {
            //        bRet = false;
            //    }
            //}

            //bRet = true;

            ////var text = Encoding.ASCII.GetBytes("hello");
            ////var Hash1 = Hash(text);
            ////var Hash2 = Hash(Hash1);
            ////Console.WriteLine(ByteArrayToString(Hash2));

            //Console.WriteLine("test");

            ////var test = "8c14f0db3df150123e6f3dbbf30f8b955a8249b62ac1d1ff16284aefa3d06d87";
            ////var bytet = StringToByteArray(test);

            ////Console.WriteLine(Encoding.Default.GetString(bytet));
            ////var hexStr = BitConverter.ToString(transHash).Replace("-", "").ToLowerInvariant();
            ////Console.WriteLine("0x" + hexStr);
            ////Console.WriteLine(ToHexString(transHash));





            //var a = "8c14f0db3df150123e6f3dbbf30f8b955a8249b62ac1d1ff16284aefa3d06d87";
            //var b = "fff2525b8931402dd09222c50775608f75787bd2b87e56995a7bdd30f79702c4";

            ////error 204ee246463fecd48b2071a6512204cf952a006f8fc08bb61d0cbd8f53b063e0
            //var hash1b = StringToByteArray(a + b);
            //var hash1 = Hash(Hash(hash1b));
            //var hash2 = Hash(hash1);

            //Console.WriteLine(ByteArrayToString(hash2));

            //Console.WriteLine("Reverse");

            //var revA = Reverse(a);
            //var revB = Reverse(b);

            ////expected ccdafb73d8dcd0173d5d5c3c9a0770d0b3953db889dab99ef05b1907518cb815
            //var hasha = Hash(ToBytes(revA + revB));
            //var hashb = Hash(hasha);
            ////var hashc = Hash(StringToByteArray(Reverse(ByteArrayToString(hash12))));
            //var ret = ByteArrayToString(hashb);
            //Console.WriteLine(ret);





            //Console.WriteLine("bitcoin");
            //var a2 = "b1fea52486ce0c62bb442b530a3f0132b826c74e473d1f2c220bfa78111c5082";
            //var b2 = "f4184fc596403b9d638783cf57adfe4c75c605f6356fbc91338530e9831e9e16";
            //var reva2 = Reverse(a2);
            //var revb2 = Reverse(b2);
            //var bhash2 = StringToByteArray(reva2 + revb2);
            ////var bhash2 = StringToByteArray(Reverse(b2+a2));
            //var hash2 = Hash(bhash2);
            //var revhash2 = Reverse(ByteArrayToString(hash2));
            //var revhash2b = StringToByteArray(revhash2);
            //Console.WriteLine(ByteArrayToString(revhash2b));



            //Console.ReadKey();

        }

        public bool isValidHash()
        {
            var serializedObj = SerializeObject(transfer);
            var hash = Hash(serializedObj);
            var bo = sgndTrans.IsValidSignedHash(hash, transfer.DestinyPk);
            return true;
        }

        private byte[] Hash(byte[] info)
        {
            //double hash
            //var hash1 = new SHA256Managed().ComputeHash(info);
            //return new SHA256Managed().ComputeHash(hash1);



            //return new SHA256Managed().ComputeHash(info);
            return new SHA256Managed().ComputeHash(info, 0, info.Length);
        }

        /// <summary>
        /// Serialize an object
        /// </summary>
        /// <param name="obj">Object instance</param>
        /// <returns>Serialized object</returns>
        private static byte[] SerializeObject(object obj)
        {
            byte[] ret;
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ret = ms.ToArray();
            }

            return ret;
        }

        //public static string ByteArrayToString(byte[] ba)
        //{
        //    StringBuilder hex = new StringBuilder(ba.Length * 2);
        //    foreach (byte b in ba)
        //        hex.AppendFormat("{0:x2}", b);
        //    return hex.ToString();

        //    //string hex = BitConverter.ToString(ba);
        //    //return hex.Replace("-", "");
        //}

        //public static byte[] StringToByteArray(String hex)
        //{
        //    int NumberChars = hex.Length;
        //    byte[] bytes = new byte[NumberChars / 2];
        //    for (int i = 0; i < NumberChars; i += 2)
        //        bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
        //    return bytes;
        //}

        //public static string Reverse(string s)
        //{
        //    char[] charArray = s.ToCharArray();
        //    Array.Reverse(charArray);
        //    return new string(charArray);
        //}

        //public static string ToString(byte[] input)
        //{
        //    string result = "";
        //    foreach (byte b in input)
        //        result += b.ToString("x2");

        //    return result;
        //}

        //public static string EndianFlip32BitChunks(string input)
        //{
        //    //32 bits = 4*4 bytes = 4*4*2 chars
        //    string result = "";
        //    for (int i = 0; i < input.Length; i += 8)
        //        for (int j = 0; j < 8; j += 2)
        //        {
        //            //append byte (2 chars)
        //            result += input[i - j + 6];
        //            result += input[i - j + 7];
        //        }
        //    return result;
        //}

        //public static byte[] ToBytes(string input)
        //{
        //    byte[] bytes = new byte[input.Length / 2];
        //    for (int i = 0, j = 0; i < input.Length; j++, i += 2)
        //        bytes[j] = byte.Parse(input.Substring(i, 2), System.Globalization.NumberStyles.HexNumber);

        //    return bytes;
        //}
    }

    [Serializable]
    public class Transfer
    {
        private TransferHash previous;

        byte[] destinyPk;

        Coin coin;

        public TransferHash Previous
        {
            get { return previous; }
        }

        public byte[] DestinyPk
        {
            get { return destinyPk; }
        }

        public Transfer(Coin coin, byte[] destinyPk)
        {
            this.coin = coin;
            this.destinyPk = destinyPk;
            this.previous = null;
        }

        public Transfer(TransferHash previous, byte[] destinyPk)
        {
            this.coin = null;
            this.destinyPk = destinyPk;
            this.previous = previous;
        }
    }

    public class Person
    {
        private Signature mySig;
        private const int sizeKey = 256;

        public byte[] PublicKey
        {
            get { return mySig.PublicKey; }
        }

        public Person()
        {
            this.mySig = new Signature(sizeKey);
        }

        public Transfer PayTo(Transfer trans, byte[] destinyPk)
        {
            var prevHash = new TransferHash(trans, mySig);
            return new Transfer(prevHash, destinyPk);
        }
    }
    public class Authority : Person
    {
        public Transfer CreateCoin(byte[] destinyPk)
        {
            return new Transfer(new Coin(), destinyPk);
        }
    }
}
