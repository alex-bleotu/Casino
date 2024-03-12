using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Casino.Theme;
using MaterialSkin.Controls;
using Microsoft.VisualBasic;
using static Cards;

public class Blackjack {
	public const int startingMoney = 1000;
	public const int startingBet = 50;

	public class Player {
		public bool isPlaying { get; set; } = false;
		public int money { get; set; } = startingMoney;
		public int bet { get; set; } = startingBet;
		public int handValue { get; set; } = 0;
		public bool isBusted { get; set; } = false;
		public bool hasAce { get; set; } = false;

		public Button joinButton { get; set; }
		public Button hitButton { get; set; }
		public Button standButton { get; set; }
		public Button doubleButton { get; set; }
		public Button leaveButton { get; set; }
		public List<Button> cards { get; set; } = new List<Button>();
		public Control buttonsTable { get; set; }
		public Label label { get; set; }
		public Label moneyLabel { get; set; }
		public Label betLabel { get; set; }
		public Label handLabel { get; set; }
	}

	List<Player> players = new List<Player>();
	Button startButton = new Button();
	Label houseLabel = new Label();
	Label houseHandLabel = new Label();
	List<Button> houseCards = new List<Button>();
	Cards cards;

	int currentPlayersTurn = 0;
	int houseHandValue = 0;
	bool hasHouseAce = false;
	int playersPlayingCount = 0;

	public Blackjack(Control blackJackControl) {
		cards = new Cards();

		players.Add(new Player());
		players.Add(new Player());
		players.Add(new Player());
		players.Add(new Player());

		startButton = blackJackControl.Controls.Find("StartButton", true).FirstOrDefault() as Button;
		startButton.Click += new EventHandler(StartButton_Click);
		startButton.Enabled = false;

		houseLabel = blackJackControl.Controls.Find("HouseLabelBig", true).FirstOrDefault() as Label;
		houseHandLabel = blackJackControl.Controls.Find("HouseHandLabel", true).FirstOrDefault() as Label;

		houseCards.Add(blackJackControl.Controls.Find("HouseCard1", true).FirstOrDefault() as Button);
		houseCards.Add(blackJackControl.Controls.Find("HouseCard2", true).FirstOrDefault() as Button);
		houseCards.Add(blackJackControl.Controls.Find("HouseCard3", true).FirstOrDefault() as Button);
		houseCards.Add(blackJackControl.Controls.Find("HouseCard4", true).FirstOrDefault() as Button);

		foreach (Control control in blackJackControl.Controls)
			if (control.Name.Contains("player"))
				foreach (Control childControl in control.Controls) {
					int index = int.Parse(control.Name.Substring(6, 1)) - 1;

					if (childControl.Name.Contains("JoinButton")) {
						players[index].joinButton = childControl as Button;
						childControl.Click += new EventHandler(JoinButton_Click);
					} else if (childControl.Name.Contains("ButtonTable")) {
						players[index].buttonsTable = childControl;
						players[index].hitButton = childControl.Controls[1] as Button;
						players[index].standButton = childControl.Controls[0] as Button;
						players[index].doubleButton = childControl.Controls[2] as Button;
						players[index].leaveButton = childControl.Controls[3] as Button;
						players[index].buttonsTable.Visible = false;
						players[index].hitButton.Enabled = false;
						players[index].standButton.Enabled = false;
						players[index].doubleButton.Enabled = false;
						players[index].hitButton.Click += new EventHandler(HitButton_Click);
						players[index].standButton.Click += new EventHandler(StandButton_Click);
						players[index].doubleButton.Click += new EventHandler(DoubleButton_Click);
						players[index].leaveButton.Click += new EventHandler(LeaveButton_Click);

						players[index].cards.Add(blackJackControl.Controls.Find("Player" + (index + 1) + "Card4", true).FirstOrDefault() as Button);
						players[index].cards.Add(blackJackControl.Controls.Find("Player" + (index + 1) + "Card3", true).FirstOrDefault() as Button);
						players[index].cards.Add(blackJackControl.Controls.Find("Player" + (index + 1) + "Card2", true).FirstOrDefault() as Button);
						players[index].cards.Add(blackJackControl.Controls.Find("Player" + (index + 1) + "Card1", true).FirstOrDefault() as Button);

                    } else if (childControl.Name.Contains("Cards"))
						foreach (Control cardControl in childControl.Controls) {
							if (cardControl.Name.Contains("MoneyLabel"))
								players[index].moneyLabel = cardControl as Label;
							else if (cardControl.Name.Contains("BetLabel"))
								players[index].betLabel = cardControl as Label;
							else if (cardControl.Name.Contains("HandLabel"))
								players[index].handLabel = cardControl as Label;
							else if (cardControl.Name.Contains("Label"))
								players[index].label = cardControl as Label;
						}
				}
	}


    private void NextRound() {
		players[currentPlayersTurn].buttonsTable.Visible = false;
		players[currentPlayersTurn].hitButton.Enabled = false;
		players[currentPlayersTurn].standButton.Enabled = false;
		players[currentPlayersTurn].doubleButton.Enabled = false;
		if (players[currentPlayersTurn].handValue != 21 && !players[currentPlayersTurn].isBusted)
            players[currentPlayersTurn].label.Text = "Player " + (currentPlayersTurn + 1);
		int counter = 0;
		while (true && counter < 4) {
            currentPlayersTurn++;
            if (currentPlayersTurn == players.Count) {
				EndGame();
				return;
			}
            if (players[currentPlayersTurn].isPlaying && !players[currentPlayersTurn].isBusted && players[currentPlayersTurn].handValue < 21)
                break;
			counter++;
        }
		if (counter < 4) {
			players[currentPlayersTurn].hitButton.Enabled = true;
			players[currentPlayersTurn].standButton.Enabled = true;
			players[currentPlayersTurn].doubleButton.Enabled = true;
			players[currentPlayersTurn].label.Text = "Player " + (currentPlayersTurn + 1) + " - Turn";
		}
    }

	private void EndGame() {
		AddHouseCard(0);
		if (houseHandValue < 17)
            AddHouseCard(2);
        if (houseHandValue < 17)
			AddHouseCard(3);

        if (hasHouseAce && houseHandValue + 10 <= 21)
            houseHandValue += 10;

		if (houseHandValue == 21) {
			houseLabel.Text = "House - Blackjack";
			houseLabel.ForeColor = Colors.accent;
		}

		int winners = 0;

        if (houseHandValue > 21) {
			houseLabel.Text = "House - Busted";
			for (int i = 0; i < players.Count; i++) {
                if (players[i].isPlaying) {
					if (players[i].isBusted) {
						players[i].label.Text = "Player " + (i + 1) + " - Busted";
						players[i].money -= players[i].bet;
					} else if (players[i].handValue == 21 || players[i].hasAce && players[i].handValue == 11) {
						players[i].label.Text = "Player " + (i + 1)+ " - Blackjack";
						players[i].label.ForeColor = Colors.accent;
						players[i].money += players[i].bet * 2;
                        winners++;
                    } else {
						players[i].label.Text = "Player " + (i + 1)+ " - Winner";
						players[i].money += players[i].bet;
						players[i].label.ForeColor = Colors.accent;
                        winners++;
                    }
                    players[i].moneyLabel.Text = "Money: " + players[i].money + "$";
                }
            }
		} else {
			for (int i = 0; i < players.Count; i++) {
				if (players[i].isPlaying) {
					if (players[i].isBusted) {
						players[i].label.Text = "Player " + (i + 1) + " - Busted";
						players[i].money -= players[i].bet;
					}

					if (players[i].handValue + 10 <= 21 && players[i].hasAce)
						players[i].handValue += 10;

                    if (players[i].handValue == 21 || players[i].hasAce && players[i].handValue == 11) {
                        players[i].label.Text = "Player " + (i + 1)+ " - Blackjack";
                        players[i].label.ForeColor = Colors.accent;
                        players[i].money += players[i].bet * 2;
                        winners++;
                    } else if (players[i].handValue > houseHandValue && !players[i].isBusted) {
						players[i].label.Text = "Player " + (i + 1) + " - Winner";
						players[i].money += players[i].bet;
						players[i].label.ForeColor = Colors.accent;
                        winners++;
                    } else if (players[i].handValue == houseHandValue && !players[i].isBusted)
						players[i].label.Text = "Player " + (i + 1) + " - Draw";
					else if (!players[i].isBusted) {
						players[i].label.Text = "Player " + (i + 1) + " - Loser";
						players[i].money -= players[i].bet;
					}
                    players[i].moneyLabel.Text = "Money: " + players[i].money + "$";
                }
			}
		}

		if (winners == 0) {
			houseLabel.Text = "House - Winner";
			houseLabel.ForeColor = Colors.accent;
		}

		for (int i = 0; i < players.Count; i++) {
			players[i].joinButton.Enabled = true;
			if (players[i].money <= 0) {
				players[i].joinButton.Visible = true;
				players[i].buttonsTable.Visible = false;
				players[i].cards[0].Visible = false;
				players[i].cards[1].Visible = false;
				players[i].cards[2].Visible = false;
				players[i].cards[3].Visible = false;
				players[i].cards[0].Image = null;
				players[i].cards[1].Image = null;
				players[i].cards[2].Image = null;
				players[i].cards[3].Image = null;
				players[i].label.Text = "Player " + (i + 1) + " - Bankrupt";
            }
		}

        players[0].joinButton.Enabled = true;
        players[1].joinButton.Enabled = true;
        players[2].joinButton.Enabled = true;
        players[3].joinButton.Enabled = true;
        startButton.Enabled = true;
	}

	private void AddHouseCard(int cardIndex) {
        Card card = cards.NextCard();
		string cardType = "";
		if (card.value == 1) {
            cardType = "A";
			hasHouseAce = true;
		} else if (card.value < 11)
            cardType = card.value.ToString();
		else
			switch (card.value) {
				case 11:
                    cardType = "J";
					break;
				case 12:
                    cardType = "Q";
					break;
				case 13:
                    cardType = "K";
					break;
			}
        switch (card.suit) {
            case Suit.Clubs:
				cardType += "clubs";
                break;
            case Suit.Hearts:
				cardType += "hearts";
                break;
            case Suit.Spades:
				cardType += "spades";
                break;
            case Suit.Diamonds:
				cardType += "diamonds";
                break;
        }

		houseCards[cardIndex].Image = GetImageFromString(cardType);
		houseCards[cardIndex].Visible = true;

		if (card.value > 10)
			houseHandValue += 10;
		else
			houseHandValue += card.value;

		houseHandLabel.Text = houseHandValue + "";
    }

	private void AddPlayerCard(int playerIndex, int cardIndex) {
		if (cardIndex != 0) {
			Card card;
            do {
                card = cards.TryCard();
            } while (players[playerIndex].hasAce && card.value == 1);
			cards.AddCard(card);

			string cardType = "";

			if (card.value == 1) {
                cardType = "A";
				players[playerIndex].hasAce = true;
			} else if (card.value < 11)
                cardType = card.value.ToString();
			else
				switch (card.value) {
					case 11:
                        cardType = "J";
						break;
					case 12:
                        cardType = "Q";
						break;
					case 13:
                        cardType = "K";
						break;
				}
			switch (card.suit) {
				case Suit.Clubs:
					cardType += "clubs";
                    break;
				case Suit.Hearts:
					cardType += "hearts";
					break;
				case Suit.Spades:
					cardType += "spades";
					break;
				case Suit.Diamonds:
					cardType += "diamonds";
					break;
			}

			players[playerIndex].cards[cardIndex].BackgroundImage = GetImageFromString(cardType);

			players[playerIndex].cards[cardIndex].Visible = true;
			if (card.value > 10)
				players[playerIndex].handValue += 10;
            else
				players[playerIndex].handValue += card.value;

			players[playerIndex].handLabel.Text = players[playerIndex].handValue + "";


            if (players[playerIndex].handValue > 21)
				players[playerIndex].isBusted = true;

            if (players[playerIndex].isBusted) {
                players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.Text = "Player " + (playerIndex + 1) + " - Busted";
				players[playerIndex].buttonsTable.Visible = false;
                if (cardIndex < 2)
					NextRound();
            } else if (players[playerIndex].handValue == 21 || players[playerIndex].hasAce && players[playerIndex].handValue == 11) {
                players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.Text = "Player " + (playerIndex + 1) + " - Blackjack";
				players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.ForeColor = Colors.accent;
                players[playerIndex].handValue = 21;
                players[playerIndex].handLabel.Text = players[playerIndex].handValue + "";
                if (cardIndex < 2)
                    NextRound();
            }
        } else {
			Card card;
			do {
				card = cards.TryCard();
				if (card.value > 6)
					break;
			} while (players[playerIndex].handValue + card.value < 21);
			cards.AddCard(card);

            string cardType = "";
            if (card.value == 1) {
                cardType = "A";
                players[playerIndex].hasAce = true;
            } else if (card.value < 11)
                cardType = card.value.ToString();
            else
                switch (card.value) {
                    case 11:
                        cardType = "J";
                        break;
                    case 12:
                        cardType = "Q";
                        break;
                    case 13:
                        cardType = "K";
                        break;
                }
            switch (card.suit) {
                case Suit.Clubs:
                    cardType += "clubs";
                    break;
                case Suit.Hearts:
                    cardType += "hearts";
                    break;
                case Suit.Spades:
                    cardType += "spades";
                    break;
                case Suit.Diamonds:
                    cardType += "diamonds";
                    break;
            }
            players[playerIndex].cards[cardIndex].BackgroundImage = GetImageFromString(cardType);
            players[playerIndex].cards[cardIndex].Visible = true;
            if (card.value > 10)
                players[playerIndex].handValue += 10;
            else
                players[playerIndex].handValue += card.value;

            players[playerIndex].handLabel.Text = players[playerIndex].handValue + "";

            if (players[playerIndex].handValue > 21)
				players[playerIndex].isBusted = true;

            if (players[playerIndex].isBusted) {
                players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.Text = "Player " + (playerIndex + 1) + " - Busted";
                if (cardIndex < 2)
                    NextRound();
            } else if (players[playerIndex].handValue == 21 || players[playerIndex].hasAce && players[playerIndex].handValue == 11) {
                players[playerIndex].buttonsTable.Visible = false;
                players[playerIndex].label.Text = "Player " + (playerIndex + 1) + " - Blackjack";
				players[playerIndex].label.ForeColor = Colors.accent;
				players[playerIndex].handValue = 21;
                players[playerIndex].handLabel.Text = players[playerIndex].handValue + "";
                if (cardIndex < 2)
                    NextRound();
            }
        }
	}


    private void JoinButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
        players[index].isPlaying = true;
        players[index].joinButton = button;
        players[index].joinButton.Visible = false;
        players[index].buttonsTable.Visible = true;
        players[index].money = startingMoney;
        players[index].bet = startingBet;
        players[index].label.Text = "Player " + (index + 1);
        players[index].moneyLabel.Text = ("Money: " + (players[index].money.ToString() + "$"));
        players[index].betLabel.Text = "Bet: " + players[index].bet + "$";
        button.Visible = false;
		playersPlayingCount++;

        startButton.Enabled = true;
    }

    private void HitButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
        int i;
        for (i = players[index].cards.Count - 1; i >= 0; i--)
            if (players[index].cards[i].Visible == false) {
                AddPlayerCard(index, i);
                break;
            }
        if (i == 0 && players[index].handValue < 21)
            NextRound();
    }

    private void StandButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
        NextRound();
    }

    private void DoubleButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
        players[index].bet *= 2;
        players[index].betLabel.Text = "Bet: " + players[index].bet;
        for (int i = players[index].cards.Count - 1; i >= 0; i--)
            if (players[index].cards[i].Visible == false) {
                AddPlayerCard(index, i);
                break;
            }
    }

    private void LeaveButton_Click(object sender, EventArgs e) {
        Button button = sender as Button;
        int index = int.Parse(button.Name.Substring(6, 1)) - 1;
        players[index].isPlaying = false;
        players[index].buttonsTable.Visible = false;
        players[index].joinButton.Visible = true;
        players[index].label.Text = "Player " + (index + 1);
		players[index].handLabel.Text = "";
        players[index].money = startingMoney;
        players[index].bet = startingBet;
        players[index].isBusted = false;
        players[index].cards.ForEach(card => card.Visible = false);
        players[index].joinButton.Enabled = true;
        players[index].moneyLabel.Text = "";
        players[index].betLabel.Text = "";
        players[index].hasAce = false;
        players[index].label.ForeColor = Color.White;
        players[index].handValue = 0;
		playersPlayingCount--;
		if (playersPlayingCount == 0) {
			startButton.Enabled = false;
			currentPlayersTurn = 0;
			houseCards[0].Visible = false;
			houseCards[1].Visible = false;
			houseCards[2].Visible = false;
			houseCards[3].Visible = false;
            houseCards[0].Image = null;
            houseCards[1].Image = null;
            houseCards[2].Image = null;
            houseCards[3].Image = null;
            houseHandValue = 0;
			houseLabel.Text = "House";
			houseHandLabel.Text = "";
            players[0].joinButton.Enabled = true;
            players[1].joinButton.Enabled = true;
            players[2].joinButton.Enabled = true;
            players[3].joinButton.Enabled = true;
        } else if (currentPlayersTurn == index)
            NextRound();
    }

    private void StartButton_Click(object sender, EventArgs e) {
        startButton.Enabled = false;
        hasHouseAce = false;
        houseHandValue = 0;
        houseLabel.Text = "House";
        houseLabel.ForeColor = Color.White;
        houseCards[0].Visible = false;
        houseCards[1].Visible = false;
        houseCards[2].Visible = false;
        houseCards[3].Visible = false;
        houseCards[0].Image = null;
        houseCards[1].Image = null;
        houseCards[2].Image = null;
        houseCards[3].Image = null;

        currentPlayersTurn = 0;

        for (int index = 0; index < players.Count; index++) {
            players[index].joinButton.Enabled = false;
            for (int i = 0; i < players[index].cards.Count; i++)
                players[index].cards[i].Visible = false;
            players[index].handValue = 0;
            players[index].isBusted = false;
            players[index].label.ForeColor = Color.White;
            players[index].label.Text = "Player " + (index + 1);
            players[index].bet = startingBet;
            players[index].hasAce = false;

            if (players[index].isPlaying) {
                players[index].buttonsTable.Visible = true;
                players[index].moneyLabel.Text = "Money: " + players[index].money + "$";
                players[index].betLabel.Text = "Bet: " + players[index].bet + "$";
                AddPlayerCard(index, 3);
                AddPlayerCard(index, 2);
            }
        }

        houseCards[0].Visible = true;
		//houseCards[0].Image = GetImageFromString
        AddHouseCard(1);

        for (int index = 0; index < players.Count; index++) {
            if (players[index].isPlaying) {
                currentPlayersTurn = index;
                if (players[index].handValue == 21) {
                    NextRound();
                    return;
                }
                players[index].hitButton.Enabled = true;
                players[index].standButton.Enabled = true;
                players[index].doubleButton.Enabled = true;
                players[index].label.Text = "Player " + (index + 1) + " - Turn";
                break;
            }
        }
    }

	private Image GetImageFromString(string card) {
		return (Image)Casino.Properties.Resources.ResourceManager.GetObject("_" + card);
    }
}
