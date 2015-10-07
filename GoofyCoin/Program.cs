using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoofyCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            var alice = new Signature(256);
            var bob = new Signature(256);
            var clark = new Signature(256);
            var david = new Signature(256);

            var coin = new Transfer(new Coin(), alice.PublicKey);

            var coinHash = new TransferHash(coin, alice);
            var trans1 = new Transfer(coinHash, bob.PublicKey);

            var trans1Hash = new TransferHash(trans1, bob);
            var trans2 = new Transfer(trans1Hash, clark.PublicKey);

            var trans2Hash = new TransferHash(trans2, clark);
            var trans3 = new Transfer(trans2Hash, david.PublicKey);

            trans3.Previous.isValidHash();
        }
    }
}
