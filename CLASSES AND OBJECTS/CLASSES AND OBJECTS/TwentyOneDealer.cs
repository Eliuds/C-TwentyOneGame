﻿using System;
using System.Collections.Generic;
using System.Text;
using TwentyOne;

namespace CLASSES_AND_OBJECTS
{
    public class TwentyOneDealer : Dealer
    {
        private List<Card> _hand = new List<Card>();
        public List<Card>Hand{ get { return _hand; } set { _hand = value; } }
        public bool Stay { get; set; }
        public bool isBusted { get; set; }
    }
}
