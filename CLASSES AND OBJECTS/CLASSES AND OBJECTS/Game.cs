using System;
using System.Collections.Generic;
using System.Text;

namespace CLASSES_AND_OBJECTS
{
    public abstract class Game// abstract classes are meant to only be inherited from
    {
        private List<Player> _players = new List<Player>();
        private Dictionary<Player, int> _bets = new Dictionary<Player, int>();
        public List<Player> Players { get { return _players; } set { _players = value; } }
        public string Name { get; set; }
        public Dictionary<Player, int> Bets { get { return _bets; } set { _bets = value; } }

        public abstract void Play();// all classes inheriting this class must have this method
        public virtual void ListPlayers()//virtual method in abstract class means this method gets inherited but has the ability to over ride it
        {
            foreach (Player player in Players)
            {
                Console.WriteLine(player.Name);
            }
        }
    }
}
