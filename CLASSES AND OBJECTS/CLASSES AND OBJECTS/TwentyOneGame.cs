using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwentyOne;

namespace CLASSES_AND_OBJECTS
{
   public  class TwentyOneGame : Game, IWalkAway
    {
        public TwentyOneDealer Dealer { get; set; }
        public override void Play()
        {
            Dealer = new TwentyOneDealer();
            foreach (Player player in Players)
            {
                player.Hand = new List<Card>();
                player.Stay = false;
            }
           Dealer.Hand = new List<Card>();
            Dealer.Stay = false;
            Dealer.Deck = new Deck();
            Dealer.Deck.Shuffle();
            Console.WriteLine("Place your bet!");

            foreach(Player player in Players)
            {
                int bet = Convert.ToInt32(Console.ReadLine());
                bool successfullyBet = player.Bet(bet);
                if(!successfullyBet)
                {
                    return;
                }
                Bets[player] = bet;
            }
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Dealing...");
                foreach (Player player in Players)
                {
                    Console.Write("{0}: ", player.Name);
                    Dealer.Deal(player.Hand);
                    if (i == 1)
                    {
                        bool blackJack = TwentyOneRules.CheckForBlackJack(player.Hand);// givimg the check for black jack function the players hand to see if they have black jack. To get a static method from another class you declare it with the class name.
                        if (blackJack)
                        {
                            Console.WriteLine("Blackjack!! {0} wins {1} ", player.Name, Bets[player]);// if a player gets blackjack
                            player.Balance += Convert.ToInt32((Bets[player] * 1.5) + Bets[player]);// returning the money the player won back to him
                            Bets.Remove(player);// removing him cause he already won
                            return;
                        }
                    }
                }
                Console.Write("Dealer: ");
                Dealer.Deal(Dealer.Hand);
                if (i == 1)
                {
                    bool blackJack = TwentyOneRules.CheckForBlackJack(Dealer.Hand);//Checking the dealers hand with the function
                    if (blackJack)// if dealer has black jack
                    {
                        Console.WriteLine("Dealer has BlackJack!! Everyone loses! ");
                        foreach (KeyValuePair<Player, int> entry in Bets)
                        {
                            Dealer.Balance += entry.Value; // Dealer gets all the money i think
                        }
                        return;
                    }
                }
            }
                 foreach (Player player in Players)
                {
                    while (!player.Stay)
                    {
                        Console.WriteLine("Your cards are : ");
                        foreach (Card card in player.Hand)
                        {
                            Console.Write("{0} ", card.ToString());
                        }
                        Console.WriteLine("\n\nHit or stay?"); //\n means new line, asking if they wanna stay or Hit.
                        string answer = Console.ReadLine().ToLower();
                        if (answer == "stay")
                        {
                            player.Stay = true;// if they stay it stops here
                            break;
                        }
                        else if( answer == "hit")
                        {
                            Dealer.Deal(player.Hand);
                        }
                        bool busted = TwentyOneRules.IsBusted(player.Hand);// if a player loses this runs.
                        if (busted)
                        {
                            Dealer.Balance += Bets[player];
                            Console.WriteLine("{0} Busted! You lose your bet of {1}. Your balance is now {2}.", player.Name, Bets[player], player.Balance);
                            Console.WriteLine("Do you want to play again?");
                            answer = Console.ReadLine().ToLower();
                            if (answer == "yes" || answer == "yea"|| answer =="yeah")//asking if they want to continue playing
                            {
                                player.isActivelyPlaying = true;
                                return;
                            }
                            else
                            {
                                player.isActivelyPlaying = false;
                                return;
                            }
                        }
                    }
                }
                Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);// seeing if the the dealer is busted
                Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
                while(!Dealer.Stay && !Dealer.isBusted)// dealer is hiting if he is not busted
                {
                    Console.WriteLine("Dealer is hitting...");
                    Dealer.Deal(Dealer.Hand);
                    Dealer.isBusted = TwentyOneRules.IsBusted(Dealer.Hand);
                    Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);      
                }
                if (Dealer.Stay)
                {
                    Console.WriteLine("Dealer is staying.");
                }
                if (Dealer.isBusted)
                {
                    Console.WriteLine("Dealer Busted!");
                    foreach (KeyValuePair<Player, int> entry in Bets)// if dealer is busted the players win and get theyre prizes
                    {
                        Console.WriteLine("{0} won {1}!", entry.Key.Name, entry.Value);
                        Players.Where(x => x.Name == entry.Key.Name).First().Balance += (entry.Value * 2);
                        Dealer.Balance -= entry.Value;
                    }
                    return;
                }
                foreach(Player player in Players)// comparing all players hands to dealers
                {
                    bool? playerWon = TwentyOneRules.CompareHands(player.Hand, Dealer.Hand);
                    if(playerWon == null)
                    {
                        Console.WriteLine("Push! No one wins.");
                        player.Balance += Bets[player];
                       
                    }
                    else if (playerWon == true)
                    {
                        Console.WriteLine("{0} won {1}!", player.Name, Bets[player]);
                        player.Balance += (Bets[player] * 2);
                        Dealer.Balance -= Bets[player];
                    }
                    else
                    {
                        Console.WriteLine("Dealer wins {0}!", Bets[player]);
                        Dealer.Balance += Bets[player];                        
                    }
                    Console.WriteLine("Would you like to play again?");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "yes" || answer == "yeah")
                    {
                        player.isActivelyPlaying = true;

                    }
                    else
                    {
                        player.isActivelyPlaying = false;
                    }
                }
             
             
            
        }
        public override void ListPlayers()
        {
            Console.WriteLine("21 playes:");
            base.ListPlayers();
        }
        public void WalkAway(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
