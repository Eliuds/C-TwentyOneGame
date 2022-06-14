using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwentyOne;
using System.IO;

namespace CLASSES_AND_OBJECTS
{
    public class Dealer
    {
        public string Name { get; set; }
        public Deck Deck { get; set; }

        public int Balance { get; set; }

        public void Deal(List<Card> Hand)
        {
            Hand.Add(Deck.Cards.First());
            string card = string.Format(Deck.Cards.First().ToString() + "\n");
            Console.WriteLine(card);
            using (StreamWriter file = new StreamWriter(@"C:\Users\13218\log.txt", true))
            {
                file.WriteLine(card);
                file.WriteLine(DateTime.Now);
            }
                Deck.Cards.RemoveAt(0);
        }
    }
}
